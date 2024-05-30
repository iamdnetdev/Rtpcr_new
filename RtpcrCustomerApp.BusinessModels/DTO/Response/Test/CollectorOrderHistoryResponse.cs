namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Test
{
    using System;
    using System.Collections.Generic;
    public class CollectorOrderHistoryResponse : IResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Distance { get; set; }
        public List<TestPatientDetails> PatientDetails { get; set; }
    }

    public class TestPatientDetails
    {
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
