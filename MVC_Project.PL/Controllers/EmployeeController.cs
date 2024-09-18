using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_Project.BAL.Interfaces;
using MVC_Project.DAL.Models;
using MVC_Project.PL.View_Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MVC_Project.PL.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment env/*IDepartmentRepo departmentRepo*/,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
            //_departmentRepo = departmentRepo;
        }
        //private readonly IEmployeeRepo EmployeeRepo;
        private readonly IUnitOfWork _unitOfWork;
        private  readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepo _departmentRepo;

        //[HttpGet]
        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp)) {
                var Employees = _unitOfWork.employee.GetAll();
                var mapp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel> >(Employees);
                return View(mapp);
            }
            else
            {
                var Employees = _unitOfWork.employee.GetEmployeeName(searchInp.ToLower());
                var mapp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mapp);
            }
         }
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"]=_departmentRepo.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
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
                var map=_mapper.Map<EmployeeViewModel,Employee>(employee);
                var count = _unitOfWork.complete();
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
            var Employee = _unitOfWork.employee.GetById(id.Value);
            _unitOfWork.complete();
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
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel Employee)
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
                var employee =_mapper.Map<EmployeeViewModel, Employee>(Employee);
                _unitOfWork.employee.Update(employee);
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
                return View(Employee);
            }
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel Employee)
        {
            try
            {
                var emp=_mapper.Map<EmployeeViewModel, Employee>(Employee);
                _unitOfWork.employee.Delete(emp);
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
                return View(Employee);
            }
        }
    }
}

