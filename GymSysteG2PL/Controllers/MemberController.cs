using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymSysteG2PL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        #region Get All Members
        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        #endregion

        #region Get Member Details
        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cannot Be 0 or Negative Number!";
                return RedirectToAction(nameof(Index));
            }

            var MemberDetails = _memberService.GetMemberDetails(id);

            if (MemberDetails == null)
            {
                TempData["ErrorMessage"] = "Id Cannot Be 0 or Negative Number!";
                return RedirectToAction(nameof(Index));
            }
            return View(MemberDetails);
        }

        #endregion

        #region  Get Health Record
        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cannot Be 0 or Negative Number!";
                return RedirectToAction(nameof(Index));
            }


            var HealthRecord = _memberService.GetMemberHealthRecordDetails(id);

            if (HealthRecord == null)
            {
                TempData["ErrorMessage"] = "Id Cannot Be 0 or Negative Number!";
                return RedirectToAction(nameof(Index));
            }
            return View(HealthRecord);
        }
        #endregion

        #region create

        public ActionResult Create()
        {
            return View();
        }
        // Add to DB
        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel createMember)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data and Missing Fields");
                return View("Create", createMember); 
            }


            bool result = _memberService.CreateMembers(createMember);

            if (result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Error In Creating Member!";
                return View("Create", createMember);
            }
        }
    }
}
#endregion