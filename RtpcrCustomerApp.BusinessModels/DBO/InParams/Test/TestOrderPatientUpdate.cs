using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Test
{
    public class TestOrderPatientUpdate
    {
		public int OrderID { get; set; }

		public Guid PatientID { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Adhaar { get; set; }

		public int Age { get; set; }

		public DateTime DateOfBirth { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }

		public Guid TestProductID { get; set; }

		public bool SampleCollectionDenied { get; set; }

		//public string AdhaarPhoto { get; set; }
	}
}
