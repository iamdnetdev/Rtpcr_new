using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Test
{
    public class TestPatientRequest : ModelBase
	{
		[Required, MinLength(2), MaxLength(100), RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid first name")]
		public string FirstName { get; set; }

		[Required, MinLength(2), MaxLength(100), RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid last name")]
		public string LastName { get; set; }

		[Required, MinLength(12), MaxLength(14), RegularExpression("^[1-9]{1}[0-9]{3}[ -]?[0-9]{4}[ -]?[0-9]{4}$", ErrorMessage = "Invalid Adhaar number")]
		public string Adhaar { get; set; }

		[Range(1, 100)]
		public int Age { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }

		[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email")]
		public string Email { get; set; }

		[Required, RegularExpression(@"^\+?\d[\d -]{8,12}\d$", ErrorMessage = "Invalid phone number")]
		public string Phone { get; set; }

		[Required]
		public Guid TestProductID { get; set; }
    
    }
}

