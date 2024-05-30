using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Test
{
    public class CollectorStatusUpdateRequest : ModelBase
    {
        [Required]
        public Guid? CollectorID { get; set; }

        [Required]
        public bool? IsLoggedIn { get; set; }
    }
}
