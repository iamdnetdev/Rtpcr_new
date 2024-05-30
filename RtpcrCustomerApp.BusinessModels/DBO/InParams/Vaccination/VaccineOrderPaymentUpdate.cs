namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Vaccination
{
    using System;

    public class VaccineOrderPaymentUpdate
    {
        public int OrderID { get; set; }
        public string PaymentType { get; set; }
        public string PaymentReference { get; set; }
        public string PaymentError { get; set; }
        public bool PaymentSucceeded { get; set; }
        public Guid? VaccinatorID { get; set; }
    }
}
