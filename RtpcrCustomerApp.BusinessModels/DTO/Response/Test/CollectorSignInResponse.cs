namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Test
{
    using RtpcrCustomerApp.BusinessModels.Common;
    using System;

    public class CollectorSignInResponse : ResponseBase
    {
        public Guid UserID { get; set; }
        public Role Role { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
