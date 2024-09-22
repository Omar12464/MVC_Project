using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.DAL.Models;
using MVC_Project.PL.Helper;
using MVC_Project.PL.View_Models;
using MVC_Project.PL.View_Models.Signup;
using System.Threading.Tasks;
using WebApplication1.Controllers;

namespace MVC_Project.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        #region SignUo
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                var vm = new ApplicationUser
                {
                    UserName = signUpViewModel.Email.Split("@")[0],
                    Email = signUpViewModel.Email,
                    IsAgree = signUpViewModel.IsAgree,
                };
                var add=await _userManager.CreateAsync(vm,signUpViewModel.Password);
                if (add.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach(var item in add.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(signUpViewModel);
        }
		#endregion
		#region SignIn
		[HttpGet]

		public IActionResult SignIn()
		{
			return View();
		}
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid) 
            {
                var user=await _userManager.FindByEmailAsync(signInViewModel.Email);
                if(user is not null)
                {
                    bool flag=await _userManager.CheckPasswordAsync(user, signInViewModel.Password);
                    if (flag) 
                    { 
                        var result=await _signInManager.PasswordSignInAsync(user,signInViewModel.Password, signInViewModel.RemeberMe,lockoutOnFailure:false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index),"Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(signInViewModel);
        }
        #endregion
        #region SignOut
        public async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion
        #region ForegetPassword
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
		{
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(viewModel.Email);
                if (user is not null)
                {
                    var token=await _userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetLink = Url.Action("ResetPassword", "Account", new {email=viewModel.Email, token = token });
                    var email = new Email()
                    {
                        Subject = "Reset Your Password",
                        Recipiant = viewModel.Email,
                        Body = ResetLink
                    };
                    EmailSettingd.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }
            return View(viewModel);
		}
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
			TempData["token"] = token;

			return View();
        }
        [HttpPost]
		public async Task <IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
		{
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
				string token = TempData["token"] as string;
                var user= await _userManager.FindByEmailAsync(email);
                var result=await _userManager.ResetPasswordAsync(user, token,viewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
			}
            return View(viewModel);
		}
        #endregion

    }
}
