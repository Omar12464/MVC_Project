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
        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        //[HttpGet]
        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {
                var departments = _unitOfWork.department.GetAll();
                return View(departments);
            }
            else
            {
                var departments = _unitOfWork.department.GetDepartmentName(searchInp.ToLower());
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
               var count = _unitOfWork.complete();
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
            var department = _unitOfWork.department.GetById(id.Value);
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
                _unitOfWork.department.Update(department);
                _unitOfWork.complete();
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
                _unitOfWork.department.Delete(department);
                _unitOfWork.complete();
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
