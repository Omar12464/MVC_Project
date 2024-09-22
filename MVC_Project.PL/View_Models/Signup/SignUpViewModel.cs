using System.ComponentModel.DataAnnotations;
namespace MVC_Project.PL.View_Models.Signup
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email" )]
        public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Password is required")]
        [Compare(nameof(Password),ErrorMessage ="Password doesn't match")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Required To Agree")]
        public bool IsAgree { get; set; }
    }
}
