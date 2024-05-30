using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;

namespace RtpcrCustomerApp.BusinessModels.ViewModels.Vaccination
{
    public class VaccineOrderConfirmation
    {
        public int OrderID { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime? ScheduleDate { get; set; }

        public string ScheduleSlot { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Vaccinator { get; set; }
        
        public string RazorOrderID { get; set; }

        public List<VaccinePatient> VaccinePatients { get; set; }
    }
}
