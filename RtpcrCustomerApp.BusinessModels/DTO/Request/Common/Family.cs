namespace RtpcrCustomerApp.BusinessModels.DTO
{
	using Common;
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Family : ModelBase
	{
		public Guid UserID { get; set; } = Guid.NewGuid();

		[Required]
		[MinLength(2), MaxLength(100), RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid FirstName")]
		public string FirstName { get; set; }

		[Required]
		[MinLength(2), MaxLength(100), RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid FirstName")]
		public string LastName { get; set; }

		public int Age { get; set; }

		[Required]
		public Guid PrimaryUserID { get; set; }
        
		[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
		
		public int Phone { get; set; }
		
		public string Address { get; set; }
	}
}
