using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.ViewModels.Vaccination
{
	public class VaccinePatient
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }

		public decimal ProductPrice { get; set; }

		public decimal SGST { get; set; }

		public decimal CGST { get; set; }

		public string ProductName { get; set; }
		
		public string ProductDescription { get; set; }
		
		public string OrderItemStatus { get; set; }
		
		public DateTime VaccinationDate { get; set; }
	}

}
