using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.PlanViewModel;
using GymSystemG2AL.Entities;
using GymSystemG2AL.Repositories.Interfaces;
namespace GymSystemBLL.Services.Classes
 {
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlanViewModel> GeAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll();

            if (Plans is null || !Plans.Any()) return [];
            return Plans.Select(P => new PlanViewModel
            {
                Id = P.Id,
                Name = P.Name,
                Description = P.Description,
                DurationDays = P.DurationDays,
                Price = P.Price,
                IsActive = P.IsActive,
            });
        }

        public PlanViewModel? GetPlanById(int id)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetBYId(id);
            if (Plan is null) return null;
            return new PlanViewModel
            {
                Id = Plan.Id,
                Name = Plan.Name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Price,
                IsActive = Plan.IsActive
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetBYId(PlanId);

            if (Plan is null || Plan.IsActive == false || HasActiveMembership(PlanId)) return null;

            return new UpdatePlanViewModel()
            {
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Price,
                PlanName = Plan.Name,
            };
        }

        public bool ToggleStatus(int PlanId)
        {
            var Repo = _unitOfWork.GetRepository<Plan>();

            var Plan = Repo.GetBYId(PlanId);

            if (Plan is null || HasActiveMembership(PlanId)) return false;

            Plan.IsActive = Plan.IsActive == true ? false : true;

            // if (Plan.IsActive)
            //     Plan.IsActive = false;
            // else
            //     Plan.IsActive = true;

            Plan.UpdatedAt = DateTime.Now;

            try
            {
                Repo.Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (System.Exception)
            {

                return false;
            }

        }

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel updatePlan)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetBYId(PlanId);

            if (Plan is null || HasActiveMembership(PlanId)) return false;

            try
            {
                (Plan.Description, Plan.DurationDays, Plan.Price, Plan.UpdatedAt) =
                (updatePlan.Description, updatePlan.DurationDays, updatePlan.Price, DateTime.Now);

                _unitOfWork.GetRepository<Plan>().Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        #region HelperMethods
        private bool HasActiveMembership(int PlanId)
        {
            var ActiveMembership = _unitOfWork.GetRepository<MemberShip>()
            .GetAll(X => X.PlanId == PlanId && X.Status == "Active");

            return ActiveMembership.Any();
        }


        #endregion
    }
}