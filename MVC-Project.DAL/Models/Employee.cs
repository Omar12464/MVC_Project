using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    public class Employee : ModelBase
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    
        public DateTime HireDate { get; set; }
        public bool IsDeleted { get; set; }
        public Gender gender { get; set; }
        public EmpType Emp_type { get; set; }
        public string ImageName { get; set; }
        public int? DeptId { get; set; }
        [ForeignKey(nameof(DeptId))]

        public Department department { get; set; }
        

    }
}
