namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Common
{
    using BusinessModels.Common;
    using System;

    public class UserSignInResponse : ResponseBase
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
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
