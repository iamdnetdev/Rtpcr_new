namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Vaccination
{
    using BusinessModels.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VaccinePaymentUpdateRequest : ModelBase
    {
        [Required]
        public int OrderID { get; set; }
        public string PaymentType { get; set; }
        public string RazorPaymentID { get; set; }        
        //public string PaymentError { get; set; }
        //public bool PaymentSucceeded { get; set; }
        public Guid? VaccinatorID { get; set; }
    }
}
