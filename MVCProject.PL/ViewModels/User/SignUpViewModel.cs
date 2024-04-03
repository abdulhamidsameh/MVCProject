using System.ComponentModel.DataAnnotations;

namespace MVCProject.PL.ViewModels.User
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "UserName is Required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Display(Name = "First Name")]
		[Required(ErrorMessage = "First Name is Required")]
		public string FName { get; set; }

		[Display(Name = "Last Name")]
		[Required(ErrorMessage = "Last Name is Required")]
		public string LName { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Required")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Minimum Confirm Password Length is 5")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password doesn't match with Password")]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }

	}
}
