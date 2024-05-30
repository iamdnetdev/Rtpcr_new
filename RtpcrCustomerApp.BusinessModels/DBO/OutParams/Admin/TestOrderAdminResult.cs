using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Admin
{
    public class TestOrderAdminResult
    {
        public int OrderId { get; set; }
        public Guid? CollectorID { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatusActive { get; set; }
        public string OrderStatus { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public string ScheduleStatus { get; set; }
        public string PaymentType { get; set; }
        public string PaymentReference { get; set; }
    }
}
