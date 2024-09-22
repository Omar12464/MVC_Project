using System.ComponentModel.DataAnnotations;

namespace MVC_Project.PL.View_Models
{
    public class ForgetPasswordViewModel
    {
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "InvalidEmail")]
		public string Email { get; set; }
	}
}
