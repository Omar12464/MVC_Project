using MVC_Project.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;
using Microsoft.AspNetCore.Http;

namespace MVC_Project.PL.View_Models
{

    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Feail = 2
    }
    public enum EmpType
    {
        [EnumMember(Value = "PartTime")]

        PartTime = 1,
        [EnumMember(Value = "FullTime")]

        FullTime = 2
    }
    public class EmployeeViewModel:ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]

        public decimal Salary { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        public bool IsDeleted { get; set; }
        public Gender gender { get; set; }
        public EmpType Emp_type { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public int? DeptId { get; set; }

        public Department department { get; set; }

    }
}

