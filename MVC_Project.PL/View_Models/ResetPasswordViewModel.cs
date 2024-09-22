using System.ComponentModel.DataAnnotations;

namespace MVC_Project.PL.View_Models
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
