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
    }
}