using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.DAL.Models;
using MVC_Project.PL.Helper;
using MVC_Project.PL.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserController(UserManager <ApplicationUser> UserManager,RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
			_userManager = UserManager;
			_roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				//var users = await _userManager.Users.Select(async u => new UserViewModel
				//{
				//	Id = u.Id,
				//	Email = u.Email,
				//	FName = u.FirstName,
				//	LName = u.LastName,
				//	PhonNumber = u.PhoneNumber,
				//	Roles = _userManager.GetRolesAsync(u).Result

				//}).ToListAsync();
				var users=await _userManager.Users.Select( u=>new UserViewModel
				{
					Id = u.Id,
					Email = u.Email,
					FName=u.FirstName,
					LName=u.LastName,
					PhonNumber=u.PhoneNumber,
					Roles=_userManager.GetRolesAsync(u).Result,
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

        [HttpGet]
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }
			var User =await _userManager.FindByIdAsync(id);
            if (User is null)
            {
                return NotFound();

            }
			var mappuser = _mapper.Map<ApplicationUser, UserViewModel>(User);
            return View(viewName,mappuser);
        }
        [HttpGet]
        public Task<IActionResult> Delete(string id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel uservm)
        {
            try
            {
               var user=await _userManager.FindByIdAsync(uservm.Id);
               await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
            }

            [HttpGet]
        public Task<IActionResult> Edit(string id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel UserVm)
        { 
            if (id != UserVm.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(UserVm);
            }
            try
            {
                var user =await _userManager.FindByIdAsync(UserVm.Id);
                user.PhoneNumber = UserVm.PhonNumber;
                user.FirstName=UserVm.FName;
                user.LastName=UserVm.LName;
                var result=await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                
                return BadRequest();
            }
        }
    }
}
