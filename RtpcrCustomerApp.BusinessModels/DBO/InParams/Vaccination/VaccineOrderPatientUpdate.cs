namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Vaccination
{
    using System;
    using static Dapper.SqlMapper;

    public class VaccineOrderPatientUpdate
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

		public Guid VaccineProductID { get; set; }

		public bool VaccineDenied { get; set; }

        //public string AdhaarPhoto { get; set; }

        //public string VaccinePhoto { get; set; }

    }
}
