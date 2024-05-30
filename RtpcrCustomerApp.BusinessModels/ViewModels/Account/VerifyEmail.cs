namespace RtpcrCustomerApp.BusinessModels.ViewModels.Account
{
    using System;

    public class VerifyEmail
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
