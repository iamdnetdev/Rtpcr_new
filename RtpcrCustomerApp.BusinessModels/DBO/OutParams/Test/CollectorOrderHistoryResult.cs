namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test
{
    using System;
    public class CollectorOrderHistoryResult
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Distance { get; set; }
        public bool TestDenied { get; set; }
        public Guid PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid TestProductID { get; set; }
        public string TestProductName { get; set; }
    }
}
