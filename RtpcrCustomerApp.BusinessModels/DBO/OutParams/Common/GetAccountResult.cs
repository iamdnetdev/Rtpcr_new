namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Common
{
    using RtpcrCustomerApp.BusinessModels.Common;
    using System;

    public class GetAccountResult
    {
        public Guid UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string LocationName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public Role Role { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
