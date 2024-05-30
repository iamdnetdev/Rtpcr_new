namespace RtpcrCustomerApp.BusinessModels.Common
{
    public class Error
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }
        public string Operation { get; set; }
        public ErrorType Type { get; set; }
    }
}
