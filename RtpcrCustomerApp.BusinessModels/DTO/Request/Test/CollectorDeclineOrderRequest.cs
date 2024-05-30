using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Test
{
    public class CollectorDeclineOrderRequest : ModelBase
    {
        //public Guid UserID { get; set; }

        public int OrderID { get; set; }

        public Guid CollectorID { get; set; }

        public string DeclineReason { get; set; }
    }
}
