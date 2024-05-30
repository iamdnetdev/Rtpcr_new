namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Common
{
    using System;

    public class CompanyResponse : ResponseBase
    {
        public Guid CompanyID { get; set; }
        public string Name { get; set; }
    }
}
