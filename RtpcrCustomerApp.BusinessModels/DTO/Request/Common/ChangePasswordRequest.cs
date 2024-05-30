using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Common
{
    public class ChangePasswordRequest : ModelBase
    {
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
