using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Test
{
    public class TestPaymentUpdateRequest : ModelBase
    {
        [Required]
        public int OrderID { get; set; }
        public string PaymentType { get; set; }
        public string RazorPaymentID { get; set; }
        //public string PaymentError { get; set; }
        //public bool PaymentSucceeded { get; set; }
        public Guid? CollectorID { get; set; }

    }
}
