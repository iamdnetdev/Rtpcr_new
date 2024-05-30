using RtpcrCustomerApp.BusinessModels.Common;
using System;

namespace RtpcrCustomerApp.BusinessModels.DTO
{
    public class Company : ModelBase
    {
        public Guid CompanyID { get; set; }
        public string Name { get; set; }
    }
}
