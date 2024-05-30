namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Vaccination
{
    using BusinessModels.Common;
    using System;

    public class VaccinatorOrderHistoryResult
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Distance { get; set; }
        public bool VaccineDenied { get; set; }
        public Guid PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid VaccineProductID { get; set; }
        public string VaccineProductName { get; set; }
    }
}
