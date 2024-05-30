using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Admin
{
    public class TestOrderCollectorUpdate
    {
        public int OrderID { get; set; }

        public Guid CollectorID { get; set; }
    }
}
