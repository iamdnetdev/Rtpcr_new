using RtpcrCustomerApp.BusinessModels.Common;

namespace RtpcrCustomerApp.BusinessModels.DBO
{
    public class RoleAccessDetails
    {
        public Role Role { get; set; }
        public int TokenExpirationDuration { get; set; }
        public string Endpoint { get; set; }
    }
}
