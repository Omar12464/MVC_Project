using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_Project.BAL.Interfaces;
using MVC_Project.BAL.Repossotiries;
using MVC_Project.DAL.Data;
using MVC_Project.DAL.Models;
using System;

namespace MVC_Project.PL.Controllers
{
    public class DepartmentController : Controller
    {
        public DepartmentController(IDepartmentRepo repo,IWebHostEnvironment env)
        {
            DepartmentRepo = repo;
            _env = env;
        }
        private readonly IDepartmentRepo DepartmentRepo;
        private readonly IWebHostEnvironment _env;

        //[HttpGet]
        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {
                var departments = DepartmentRepo.GetAll();
                return View(departments);
            }
            else
            {
                var departments= DepartmentRepo.GetDepartmentName(searchInp.ToLower());
                return View(departments);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
               var count= DepartmentRepo.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Details(int? id,string viewName="Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department=DepartmentRepo.GetById(id.Value);
            if (department == null)
            {
                return NotFound();

            }
         
       
           return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            try
            {
                DepartmentRepo.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error occured during update");
                }
                return View(department);
            }
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id,"Delete");
        }
        [HttpPost]
        public IActionResult Delete(Department department)
        {
            try
            {
                DepartmentRepo.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error occured during update");
                }
                return View(department);
            }
        }
    }
}
