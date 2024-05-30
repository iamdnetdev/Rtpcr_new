namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Common
{
    using BusinessModels.Common;
    using System;

    public class AccountUpdate
    {
        public Guid UserID { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
    }
}
