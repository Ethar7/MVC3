using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.PlanViewModel;
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


        #region Edit Plan

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cannot Be 0 or Negative Number !";
                return RedirectToAction(nameof(Index));
            }
            var Plan = _planService.GetPlanToUpdate(id);
            if (Plan == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Plan);
        }


        [HttpPost]

        public ActionResult Edit([FromRoute] int id, UpdatePlanViewModel updatePlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Data Again !");
                return View(updatePlan);
            }
            var Result = _planService.UpdatePlan(id, updatePlan);

            if (!Result)
            {
                TempData["ErrorMessage"] = "Failed to Update Plan !";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Plan Upadeted Successfully !";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    
        #region Soft Delete - Active & Deactive

        [HttpPost]
        public ActionResult Activate(int id)
        {
            var Result = _planService.ToggleStatus(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Plan Status Changed Successfully !";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErroeMessage"] = "failed to Change Plan Status !";
                return RedirectToAction(nameof(Index));
            }
        }
            
        #endregion
    }
}