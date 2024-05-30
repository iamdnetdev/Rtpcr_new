using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Test
{
    public class CollectorLocationRequest : ModelBase
    {
        public Guid CollectorID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
