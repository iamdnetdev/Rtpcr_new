namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Vaccination
{
    using BusinessModels.Common;
    using System;

    public class VaccinatorLoginResult
    {
        public Guid UserID { get; set; }
        public Role Role { get; set; }
        public int TokenExpirationDuration { get; set; }
        public bool IsAuthenticated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
