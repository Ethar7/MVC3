using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemBLL.ViewModels.SessionViewModel;
using GymSystemBLL.Services.Interfaces;
using GymSystemG2AL.Repositories.Interfaces;
using GymSystemG2AL.Entities;
using AutoMapper;

namespace GymSystemBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel createSession)
        {
            try
            {
                // Checktrainer, Cat Exsit StartDate < EndDate

                if (!IsTrainerExsist(createSession.TrainerId)) return false;
                if (!IsCategoryExsist(createSession.CategoryId)) return false;
                if (!IsDateTimeValid(createSession.StartDate, createSession.EndDate)) return false;
                if (createSession.Capacity > 25 || createSession.Capacity < 0) return false;

                var SessionEntity = _mapper.Map<Session>(createSession);
                _unitOfWork.GetRepository<Session>().Add(SessionEntity);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            // var Sessions = _unitOfWork.GetRepository<Session>().GetAll();

            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();

            if (!Sessions.Any()) return [];

            return Sessions.Select(S => new SessionViewModel
            {
                Id = S.Id,
                Description = S.Description,
                StartDate = S.StartDate,
                EndDate = S.EndDate,
                Capacity = S.Capacity,
                TrainerName = S.SessionTrainer.Name,
                CategoryName = S.SessionCategory.CategoryName,
                AvailableSlots = S.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(S.Id)
            });
        }

        public SessionViewModel? GetSessionById(int sessionId)
        {
            var Session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(sessionId);

            if (Session == null) return null;

            // return new SessionViewModel
            // {
            //     Id = Session.Id,
            //     Description = Session.Description,
            //     StartDate = Session.StartDate,
            //     EndDate = Session.EndDate,
            //     Capacity = Session.Capacity,
            //     TrainerName = Session.SessionTrainer.Name,
            //     CategoryName = Session.SessionCategory.CategoryName,
            //     AvailableSlots = Session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(Session.Id)
            // };

            #region Allow Auto Mapper

            var MappedSession = _mapper.Map<Session, SessionViewModel>(Session);
            MappedSession.AvailableSlots = MappedSession.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(MappedSession.Id);
            return MappedSession;
            #endregion

        }
        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var Session = _unitOfWork.SessionRepository.GetBYId(sessionId);

            if (!IsSessionAvaliableForUpdate(Session!)) return null;

            return _mapper.Map<UpdateSessionViewModel>(Session);
        }

        public bool UpdateSession(UpdateSessionViewModel updateSession, int sessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetBYId(sessionId);
                if (!IsSessionAvaliableForUpdate(Session!)) return false;
                if (!IsTrainerExsist(updateSession.TrainerId)) return false;
                if (!IsDateTimeValid(updateSession.StartDate, updateSession.EndDate)) return false;

                _mapper.Map(updateSession, Session);
                Session!.UpdatedAt = DateTime.Now;
                _unitOfWork.SessionRepository.Update(Session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        #region Helper Methods

        private bool IsTrainerExsist(int TrainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetBYId(TrainerId) is not null;
        }

        private bool IsCategoryExsist(int CategoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetBYId(CategoryId) is not null;
        }

        private bool IsDateTimeValid(DateTime StratDate, DateTime EndDate)
        {
            return StratDate < EndDate;
        }

        private bool IsSessionAvaliableForUpdate(Session session)
        {
            if (session is null) return false;

            // if Session completed => cannot update
            if (session.EndDate < DateTime.Now) return false;
            // if Session Started => Cannot Update
            if (session.StartDate <= DateTime.Now) return false;
            // if Session HasActiveBooking => Cannot Update
            var ActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;

            if (ActiveBooking) return false;

            return true;
        }

        #endregion
    }
}