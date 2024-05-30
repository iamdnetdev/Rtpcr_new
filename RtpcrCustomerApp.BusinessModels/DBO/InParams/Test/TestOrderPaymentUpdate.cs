using System;

namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Test
{
    public class TestOrderPaymentUpdate
    {
        public int OrderID { get; set; }
        public string PaymentType { get; set; }
        public string PaymentReference { get; set; }
        public string PaymentError { get; set; }
        public bool PaymentSucceeded { get; set; }
        public Guid? CollectorID { get; set; }
    }
}
