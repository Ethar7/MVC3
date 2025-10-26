using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemBLL.ViewModels.PlanViewModel;

namespace GymSystemBLL.Services.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> GeAllPlans();
        PlanViewModel? GetPlanById(int id);
        UpdatePlanViewModel? GetPlanToUpdate(int PlanId);

        bool UpdatePlan(int PlanId, UpdatePlanViewModel updatePlan);

        bool ToggleStatus(int PlanId);
        
    }
}