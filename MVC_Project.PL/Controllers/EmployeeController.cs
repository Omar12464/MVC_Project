using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_Project.BAL.Interfaces;
using MVC_Project.DAL.Models;
using System;

namespace MVC_Project.PL.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController(IEmployeeRepo repo, IWebHostEnvironment env/*IDepartmentRepo departmentRepo*/)
        {
            EmployeeRepo = repo;
            _env = env;
            //_departmentRepo = departmentRepo;
        }
        private readonly IEmployeeRepo EmployeeRepo;
        private  readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepo _departmentRepo;

        [HttpGet]
        public IActionResult Index()
        {
            var Employees = EmployeeRepo.GetAll();
            if (Employees == null)
            {
                return NotFound();
            }
            else
            {
                return View(Employees);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"]=_departmentRepo.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = EmployeeRepo.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Employee = EmployeeRepo.GetById(id.Value);
            //ViewData["Departments"] = _departmentRepo.GetAll();
            if (Employee == null)
            {
                return NotFound();

            }


            return View(Employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee Employee)
        {
            if (id != Employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(Employee);
            }
            try
            {
                EmployeeRepo.Update(Employee);
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
                return View(Employee);
            }
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(Employee Employee)
        {
            try
            {
                EmployeeRepo.Delete(Employee);
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
                return View(Employee);
            }
        }
    }
}

