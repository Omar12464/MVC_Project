using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.DAL.Models
{
   public enum Gender
    {
        [EnumMember(Value ="Male")]
        Male=1,
        [EnumMember(Value ="Female")]
            Feail=2
    }
    public enum EmpType
    {
        [EnumMember(Value = "PartTime")]

        PartTime = 1,
        [EnumMember(Value = "FullTime")]

        FullTime = 2
    }
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
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


    }
}
