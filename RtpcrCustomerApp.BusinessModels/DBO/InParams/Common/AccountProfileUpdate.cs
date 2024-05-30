namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Common
{
    using System;
    public class AccountProfileUpdate
    {
        public Guid UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
