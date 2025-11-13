using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.AccountViewModels;
using GymSystemG2AL.Entities;
using Microsoft.AspNetCore.Identity;


namespace GymSystemBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public ApplicationUser? ValidateUser(LoginViewModel loginVm)
        {
            var User = _userManager.FindByEmailAsync(loginVm.Email).Result;

            if (User is null) return null;

            var IsPassValid = _userManager.CheckPasswordAsync(User, loginVm.Password).Result;
            return IsPassValid ? User : null;
        }
    }
}