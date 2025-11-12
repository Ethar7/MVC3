using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace GymSysteG2PL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        #region get All Plans

        public IActionResult Index()
        {
            var Plans = _planService.GeAllPlans();
            return View(Plans);
        }
        #endregion
    

        #region Get Plan Details

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cannot Be 0 or negative Number !";
                return RedirectToAction(nameof(Index));
            }
            var Plan = _planService.GetPlanById(id);
            if (Plan == null)
            {
                TempData["ErrorMessage"] = "Plan not Found !";
                return RedirectToAction(nameof(Index));
            }
            return View(Plan);
        }
            
        #endregion
       
    }
}