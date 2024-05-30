using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test
{
    public class CollectorLocationResult
    {
        public Guid CollectorID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
