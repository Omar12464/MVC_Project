using System.ComponentModel.DataAnnotations;

namespace MVC_Project.PL.View_Models
{
	public class SignInViewModel
	{
        [Required(ErrorMessage ="Email Is Required")]
        [EmailAddress(ErrorMessage ="InvalidEmail")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}
