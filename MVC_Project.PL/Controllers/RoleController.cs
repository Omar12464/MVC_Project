using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.DAL.Models;
using MVC_Project.PL.View_Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MVC_Project.PL.Helper;

namespace MVC_Project.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            { 
                var roles = await _roleManager.Roles.Select(u => new RoleViewModel
                {
                    Id = u.Id,
                    RoleName = u.Name,
                }).ToListAsync();
                return View(roles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);
                if (role == null)
                {
                    var mappRole = new RoleViewModel
                    {
                        Id = role.Id,
                        RoleName=role.Name,
                      
                    };
                    return View(new List<RoleViewModel> { mappRole });

                }
            }
            return View(Enumerable.Empty<RoleViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }
            var User = await _roleManager.FindByIdAsync(id);
            if (User is null)
            {
                return NotFound();

            }
            var mappuser = _mapper.Map<IdentityRole, RoleViewModel>(User);
            return View(viewName, mappuser);
        }
        [HttpGet]
        public Task<IActionResult> Delete(string id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleViewModel rolevm)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(rolevm.Id);
                await _roleManager.DeleteAsync(role);
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
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel roleView)
        {
            if (id != roleView.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(roleView);
            }
            try
            {
                var user = await _roleManager.FindByIdAsync(roleView.Id);
                user.Name=roleView.RoleName;
                var result = await _roleManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"]=_departmentRepo.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel rolevm)
        {
            if (ModelState.IsValid)
            {
                #region Manual Mapping
                //var map = new Employee()
                //{
                //    Name = employee.Name,
                //    Id = employee.Id,
                //    Age = employee.Age,
                //    Address = employee.Address,
                //    Salary = employee.Salary,
                //    Email = employee.Email,
                //    IsActive = employee.IsActive,
                //    HireDate = employee.HireDate,
                //}; 
                #endregion
                var map = _mapper.Map<RoleViewModel, IdentityRole>(rolevm);
               await _roleManager.CreateAsync(map);

                    return RedirectToAction(nameof(Index));
            }
            return View(rolevm);
        }
    }
}
