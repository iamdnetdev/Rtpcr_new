namespace RtpcrCustomerApp.Api.Models
{
    using RtpcrCustomerApp.BusinessModels.Common;
    using System.Collections.Generic;

    public class RoleDefinition
    {
        public Role Role { get; set; }
        public int TokenExpirationDuration { get; set; }
        public HashSet<string> AuthorizedEndpoints { get; set; }

    }
}