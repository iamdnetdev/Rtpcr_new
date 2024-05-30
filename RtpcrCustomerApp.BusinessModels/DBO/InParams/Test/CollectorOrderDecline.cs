using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Test
{
    public class CollectorOrderDecline
    {
        public int OrderID { get; set; }

        public Guid CollectorID { get; set; }

        public string DeclineReason { get; set; }
    }
}
