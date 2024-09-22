using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.DAL.Models;
using MVC_Project.PL.View_Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager <ApplicationUser> UserManager,RoleManager<IdentityRole> roleManager)
        {
			_userManager = UserManager;
			_roleManager = roleManager;
		}
        public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				var users = await _userManager.Users.Select(async u => new UserViewModel
				{
					Id = u.Id,
					Email = u.Email,
					FName = u.FirstName,
					LName = u.LastName,
					PhonNumber = u.PhoneNumber,
					Roles = _userManager.GetRolesAsync(u).Result

				}).ToListAsync();
				return View(users);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(email);
				if (user == null)
				{
					var mappuser = new UserViewModel
					{
						Id = user.Id,
						FName = user.FirstName,
						LName = user.LastName,
						Email = user.Email,
						PhonNumber = user.PhoneNumber,
						Roles = _userManager.GetRolesAsync(user).Result
					};
					return View(new List<UserViewModel> { mappuser });

				}
			}
			return View(Enumerable.Empty<UserViewModel>());
		}
	}
}
