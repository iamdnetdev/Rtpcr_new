namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Vaccination
{
    using System;

    public class PatientResult
	{
		public Guid PatientID { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int Age { get; set; }

		public DateTime DateOfBirth { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }
	}
}
