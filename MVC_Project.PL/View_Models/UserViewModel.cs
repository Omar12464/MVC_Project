using System;
using System.Collections;
using System.Collections.Generic;

namespace MVC_Project.PL.View_Models
{
	public class UserViewModel
	{
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhonNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public UserViewModel()
        {
            Id=Guid.NewGuid().ToString();
        }
    }
}
