namespace RtpcrCustomerApp.Common.Models
{
    using RtpcrCustomerApp.BusinessModels.Common;
    using System.Net;

    public class ValidateTokenResult
    {
        public Role Role { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
