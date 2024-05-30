namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TestOrderItemResult
    {
        public int OrderID { get; set; }
        public Guid PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adhaar { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string OrderItemStatus { get; set; }        
        public string TestResult { get; set; }
    }
}
