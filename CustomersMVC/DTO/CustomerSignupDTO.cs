using System.ComponentModel.DataAnnotations;

namespace CustomersMVC.DTO
{
	public class CustomerSignupDTO
	{
		[StringLength(100, MinimumLength = 3, ErrorMessage = "Username should be between 3 - 100 characters")]
		public string Username { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Password should not exceed 32 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$", ErrorMessage = "Password must " +
                "contain at least one lowercase letter, one uppercase letter, one digit, one special character")]
        public string? Password { get; set; }

        [StringLength(100, ErrorMessage = "Firstname should not exceed 100 characters")]
		public string? Firstname { get; set; }

		[StringLength(100, ErrorMessage = "Lastname should not exceed 100 characters")]
		public string? Lastname { get; set; }

		[StringLength(100, ErrorMessage = "E-mail should not exceed 100 characters")]
		[EmailAddress(ErrorMessage = "Invalid e-mail address")]
		public string Email { get; set; } = null!;
	}
}
