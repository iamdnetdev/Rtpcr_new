namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Common
{
    using Dapper.Contrib.Extensions;
    using BusinessModels.Common;
    using System;

    public class AccountInsert
    {
        public Guid UserID { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public string AadharNumber { get; set; }

        public string Address { get; set; }

        public string LocationName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public Role Role { get; set; }

        [Write(false)]
        public string EmailToken { get; set; }

        [Write(false)]
        public DateTime EmailTokenExpiration { get; set; }

        public string DeviceID { get; set; }

        public DevicePlatform DevicePlatform { get; set; }
    }
}
