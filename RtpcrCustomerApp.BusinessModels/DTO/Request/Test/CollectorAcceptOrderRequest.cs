﻿using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Test
{
    public class CollectorAcceptOrderRequest : ModelBase
    {
        public int OrderID { get; set; }

        public Guid CollectorID { get; set; }
    }
}
