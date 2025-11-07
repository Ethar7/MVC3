using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;

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
        #endregion

        #region  EditMember

        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cant Be 0 or Negative Number!";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberService.GetMemberToUpdate(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found !";
                return RedirectToAction(nameof(Index));
            }
            return View(Member);
        }
        [HttpPost]
        public ActionResult MemberEdit([FromRoute] int id, MemberToUpdateViewModel memberToUpdate)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(MemberEdit), memberToUpdate);
            }
            var Result = _memberService.UpdateMemberDetails(id, memberToUpdate);

            if (Result)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Update Member";
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region DeleteMember
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant be 0 or negative number!";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberService.GetMemberDetails(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberName = Member.Name;
            return View();
        }
        
        public ActionResult DeleteConfirmed([FromForm]int id)
        {
            var Result = _memberService.RemoveMember(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Delete Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Delete Member !";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}