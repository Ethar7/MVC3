using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.SessionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GymSysteG2PL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        #region Get All Sessions
        public IActionResult Index()
        {
            var Sessions = _sessionService.GetAllSessions();
            return View(Sessions);
        }
        #endregion

        #region Get Session Details

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction("Index");
            }
            var session = _sessionService.GetSessionById(id);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction("Index");
            }
            return View(session);
        }
        #endregion

        #region Create Session
        public ActionResult Create()
        {
            LoadDropDownsForCategories();
            LoadDropDownsForTrainers();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDownsForCategories();
                LoadDropDownsForTrainers();
                return View(createSession);
            }
            var Result = _sessionService.CreateSession(createSession);
            if (!Result)
            {
                TempData["ErrorMessage"] = "Failed to Create Session";
                LoadDropDownsForCategories();
                LoadDropDownsForTrainers();
                return View(createSession);
            }
            else
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                LoadDropDownsForCategories();
                LoadDropDownsForTrainers();
                return RedirectToAction("Index");
            }
        }

        #endregion



        #region Edit Session

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {

                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction("Index");
            }


            var Session = _sessionService.GetSessionToUpdate(id);
            if (Session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction("Index");
            }
            LoadDropDownsForTrainers();
            return View(Session);

        }

        [HttpPost]
        
        public ActionResult Edit([FromRoute] int id, UpdateSessionViewModel updateSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDownsForTrainers();
                return View(updateSession);
            }
            var Result = _sessionService.UpdateSession(updateSession, id);
            if (!Result)
            {
                TempData["ErrorMessage"] = "Failed to update Session";
                LoadDropDownsForTrainers();
                return View(updateSession);
            }
            else
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                return RedirectToAction("Index");
            }
        }


        #endregion
        private void LoadDropDownsForTrainers()
        {
            var Trainers = _sessionService.GetTrainerForSessions();
            
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
           
        }

        private void LoadDropDownsForCategories()
        {

            var Categories = _sessionService.GetCategoryForSession();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
        }
        
    }
}

