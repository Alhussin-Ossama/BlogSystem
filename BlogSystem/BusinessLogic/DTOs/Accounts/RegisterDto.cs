using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.DTOs.Accounts
{
	public class RegisterDto
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+]).*$",
			ErrorMessage = "Password must contain 1 Uppercase, 1 Lowercase, 1 Digit, 1 Special Character")]
		public string Password { get; set; }

		[Phone]
        public string? PhoneNumber { get; set; }
    }
}
