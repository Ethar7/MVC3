using System.Collections.Generic;
using GymSystemBLL.ViewModels;
using GymSystemG2AL.Entities;

namespace GymSystemBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();

        bool CreateTrainer(CreateTrainerViewModel createTrainer);

        TrainerViewModel? GetTrainerDetails(int id);

        TrainerToUpdateViewModel? GetTrainerToUpdate(int id);

        bool UpdateTrainerDetails(int id, TrainerToUpdateViewModel updateTrainer);

        bool RemoveTrainer(int id);
    }
}
