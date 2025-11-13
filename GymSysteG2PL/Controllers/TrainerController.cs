using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSysteG2PL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        #region Get All Trainers
        public IActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }
        #endregion

        #region Get Trainer Details
        public ActionResult TrainerDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cannot Be 0 or Negative Number!";
                return RedirectToAction(nameof(Index));
            }

            var trainerDetails = _trainerService.GetTrainerDetails(id);

            if (trainerDetails == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found!";
                return RedirectToAction(nameof(Index));
            }
            return View(trainerDetails);
        }
        #endregion

        #region Create Trainer
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data and Missing Fields");
                return View("Create", createTrainer);
            }

            bool result = _trainerService.CreateTrainer(createTrainer);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Error In Creating Trainer!";
                return View("Create", createTrainer);
            }
        }
        #endregion

        #region Edit Trainer
        public ActionResult TrainerEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id can't Be 0 or Negative Number!";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerToUpdate(id);

            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found!";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }

        [HttpPost]
        public ActionResult TrainerEdit([FromRoute] int id, TrainerToUpdateViewModel trainerToUpdate)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(TrainerEdit), trainerToUpdate);
            }

            var result = _trainerService.UpdateTrainerDetails(id, trainerToUpdate);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Updated Successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Update Trainer!";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete Trainer
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Can't be 0 or negative number!";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerDetails(id);

            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainerId = id;
            ViewBag.TrainerName = trainer.Name;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed([FromForm] int id)
        {
            var result = _trainerService.RemoveTrainer(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Deleted Successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Delete Trainer!";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
