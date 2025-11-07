// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using GymSystemBLL.Services.Interfaces;
// using GymSystemBLL.ViewModels;
// using GymSystemG2AL.Entities;
// using GymSystemG2AL.Repositories.Interfaces;
// using GymSystemG2AL.Repositories.Classes;

// namespace GymSystemBLL.Services.Classes
// {
//     public class TrainerService : ITrainerService
//     {
//         private readonly IGenaricRepository<Trainer> trainerRepository;
//         private readonly IGenaricRepository<Session> sessionRepository;

//         public TrainerService(
//             IGenaricRepository<Trainer> trainerRepository,
//             IGenaricRepository<Session> sessionRepository)
//         {
//             this.trainerRepository = trainerRepository;
//             this.sessionRepository = sessionRepository;
//         }

//         // Create new Trainer
//         public bool CreateTrainer(CreateTrainerViewModel createTrainer)
//         {
//             try
//             {
//                 if (IsEmailExist(createTrainer.Email) || IsPhoneExist(createTrainer.Phone))
//                     return false;

//                 var trainer = new Trainer()
//                 {
//                     Name = createTrainer.Name,
//                     Email = createTrainer.Email,
//                     Phone = createTrainer.Phone,
//                     Gender = createTrainer.Gender,
//                     Special = createTrainer.Special,
//                     CreatedAt = DateTime.Now
//                 };

//                 return _trainerRepository.Add(trainer) > 0;
//             }
//             catch
//             {
//                 return false;
//             }
//         }

//         // Get all trainers
//         public IEnumerable<TrainerViewModel> GetAllTrainers()
//         {
//             var trainers = trainerRepository.GetAll();
//             if (trainers is null || !trainers.Any()) return [];

//             var trainerViewModels = trainers.Select(t => new TrainerViewModel
//             {
//                 Id = t.Id,
//                 Name = t.Name,
//                 Email = t.Email,
//                 Phone = t.Phone,
//                 Gender = t.Gender.ToString(),
//                 Special = t.Special.ToString()
//             });

//             return trainerViewModels;
//         }

//         // Get one trainer details by id
//         public TrainerViewModel? GetTrainerDetails(int id)
//         {
//             var trainer = trainerRepository.GetBYId(id);
//             if (trainer == null) return null;

//             return new TrainerViewModel()
//             {
//                 Id = trainer.Id,
//                 Name = trainer.Name,
//                 Email = trainer.Email,
//                 Phone = trainer.Phone,
//                 Gender = trainer.Gender.ToString(),
//                 Special = trainer.Special.ToString(),
//             };
//         }

//         // Get trainer data for update
//         public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId)
//         {
//             var trainer = trainerRepository.GetBYId(TrainerId);
//             if (trainer is null) return null;

//             return new TrainerToUpdateViewModel()
//             {
//                 Name = trainer.Name,
//                 Email = trainer.Email,
//                 Phone = trainer.Phone,
//                 Special = trainer.Special
//             };
//         }

//         // Update trainer info
//         public bool UpdateTrainerDetails(int id, TrainerToUpdateViewModel updateTrainer)
//         {
//             try
//             {
//                 var trainer = trainerRepository.GetBYId(id);
//                 if (trainer is null) return false;

//                 if (IsEmailExist(updateTrainer.Email, id) || IsPhoneExist(updateTrainer.Phone, id))
//                     return false;

//                 trainer.Name = updateTrainer.Name;
//                 trainer.Email = updateTrainer.Email;
//                 trainer.Phone = updateTrainer.Phone;
//                 trainer.Special = updateTrainer.Special;
//                 trainer.UpdatedAt = DateTime.Now;

//                 trainerRepository.Update(trainer);
//                 return _unitOfWork.SaveChanges() > 0;
//             }
//             catch
//             {
//                 return false;
//             }
//         }

//         // Remove trainer if no future sessions
//         public bool RemoveTrainer(int TrainerId)
//         {
//             var trainer = trainerRepository.GetBYId(TrainerId);
//             if (trainer is null) return false;

//             var hasFutureSessions = sessionRepository
//                 .GetAll(x => x.TrainerId == TrainerId && x.StartDate > DateTime.Now)
//                 .Any();

//             if (hasFutureSessions) return false;

//             try
//             {
//                 return trainerRepository.Delete(trainer) > 0;
//             }
//             catch
//             {
//                 return false;
//             }
//         }

//         #region Helper Methods
//         private bool IsEmailExist(string email, int? excludeId = null)
//         {
//             return trainerRepository.GetAll(x => x.Email == email && (excludeId == null || x.Id != excludeId)).Any();
//         }

//         private bool IsPhoneExist(string phone, int? excludeId = null)
//         {
//             return trainerRepository.GetAll(x => x.Phone == phone && (excludeId == null || x.Id != excludeId)).Any();
//         }
//         #endregion
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels;
using GymSystemG2AL.Entities;
using GymSystemG2AL.Repositories.Interfaces;

namespace GymSystemBLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Create new Trainer
        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                if (IsEmailExist(createTrainer.Email) || IsPhoneExist(createTrainer.Phone))
                    return false;

                var trainer = new Trainer()
                {
                    Name = createTrainer.Name,
                    Email = createTrainer.Email,
                    Phone = createTrainer.Phone,
                    Gender = createTrainer.Gender,
                    Special = createTrainer.Special,
                    DateOfBirth = createTrainer.DateOfBirth,
                    Address = new Address
                    {
                        BuildingNumber = createTrainer.BuildingNumber,
                        Street = createTrainer.Street,
                        City = createTrainer.City
                    },

                    CreatedAt = DateTime.Now
                };

                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // temporarily rethrow or log so you can see the actual error while debugging
                throw new Exception("Error in CreateTrainer: " + ex.Message, ex);
            }
        }


        // Get all trainers
        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers is null || !trainers.Any()) return new List<TrainerViewModel>();

            return trainers.Select(t => new TrainerViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Phone = t.Phone,
                Gender = t.Gender.ToString(),
                Special = t.Special.ToString()
            });
        }

        // Get one trainer details by id
        public TrainerViewModel? GetTrainerDetails(int id)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetBYId(id);
            if (trainer == null) return null;

            return new TrainerViewModel()
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Gender = trainer.Gender.ToString(),
                Special = trainer.Special.ToString(),
                BuildingNumber = trainer.Address.BuildingNumber.ToString(),
                Street = trainer.Address.Street,
                City = trainer.Address.City
                
            };
        }

        // Get trainer data for update
        public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetBYId(TrainerId);
            if (trainer is null) return null;

            return new TrainerToUpdateViewModel()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Special = trainer.Special,
                BuildingNumber = trainer.Address.BuildingNumber,
                City = trainer.Address.City,
                Street = trainer.Address.Street
            };
        }

        // Update trainer info
                public bool UpdateTrainerDetails(int id, TrainerToUpdateViewModel updateTrainer)
                {
                var trainer = _unitOfWork.GetRepository<Trainer>().GetBYId(id);
                if (trainer is null) return false;

                if (IsEmailExist(updateTrainer.Email, id) || IsPhoneExist(updateTrainer.Phone, id))
                    return false;

                try
                {
                    trainer.Name = updateTrainer.Name;
                    trainer.Email = updateTrainer.Email;
                    trainer.Phone = updateTrainer.Phone;
                    trainer.Special = updateTrainer.Special;
                    trainer.UpdatedAt = DateTime.Now;

                
                    trainer.Address = new Address
                    {
                        BuildingNumber = updateTrainer.BuildingNumber,
                        Street = updateTrainer.Street,
                        City = updateTrainer.City
                    };

                    _unitOfWork.GetRepository<Trainer>().Update(trainer);
                    return _unitOfWork.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
        }

        // Remove trainer if no future sessions
        public bool RemoveTrainer(int TrainerId)
        {
            var trainerRepo = _unitOfWork.GetRepository<Trainer>();
            var sessionRepo = _unitOfWork.GetRepository<Session>();

            var trainer = trainerRepo.GetBYId(TrainerId);
            if (trainer is null) return false;

            var hasFutureSessions = sessionRepo
                .GetAll(x => x.TrainerId == TrainerId && x.StartDate > DateTime.Now)
                .Any();

            if (hasFutureSessions) return false;

            try
            {
                trainerRepo.Delete(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        #region Helper Methods
        private bool IsEmailExist(string email, int? excludeId = null)
        {
            var trainerRepo = _unitOfWork.GetRepository<Trainer>();
            return trainerRepo.GetAll(x => x.Email == email && (excludeId == null || x.Id != excludeId)).Any();
        }

        private bool IsPhoneExist(string phone, int? excludeId = null)
        {
            var trainerRepo = _unitOfWork.GetRepository<Trainer>();
            return trainerRepo.GetAll(x => x.Phone == phone && (excludeId == null || x.Id != excludeId)).Any();
        }
        #endregion
    }
}
