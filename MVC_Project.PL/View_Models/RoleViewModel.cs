using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.PL.View_Models
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Id=Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        [Display(Name ="Role Model")]
        public string RoleName { get; set; }
    }
}
