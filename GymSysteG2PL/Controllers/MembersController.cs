using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSysteG2PL.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
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
                return RedirectToAction(nameof(Index));

            var MemberDetails = _memberService.GetMemberDetails(id);

            if (MemberDetails == null)
                return RedirectToAction(nameof(Index));
            return View(MemberDetails);
        }

        #endregion
    }
}