using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.ViewModels.Test
{
    public class TestOrderConfirmation
    {
        public int OrderID { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime? ScheduleDate { get; set; }

        public string ScheduleSlot { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Collector { get; set; }

        public string RazorOrderID { get; set; }

        public List<TestPatient> TestPatients { get; set; }
    }
}
