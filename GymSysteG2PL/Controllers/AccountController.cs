using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.AccountViewModels;
using GymSystemG2AL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymSysteG2PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        #region LoginAction

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginViewModel model)
        {
            if(!ModelState.IsValid) return View(model);

            var User = _accountService.ValidateUser(model);
            if (User == null)
            {
                ModelState.AddModelError("InvalidModel", "Invalid Email or Password !");
                return View(model);
            }

            var Result = _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false).Result;

            if (Result.IsNotAllowed)
                ModelState.AddModelError("InvalidModel", "Account Not Allowed");
            if (Result.IsLockedOut)
                ModelState.AddModelError("InvalidModel", "Account Locked Out!");
            if (Result.Succeeded)
                return RedirectToAction("Index", "Home");
            
            return View(model);
        }

        #endregion
    
        #region Logout Action
        [HttpPost]
        public ActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login");
        }

        #endregion
    
    
        #region Access Denied Action
        public ActionResult AccessDenied()
        {
            return View();
        }

        #endregion
    }
}