using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.DAL.Models;
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
    }
}
