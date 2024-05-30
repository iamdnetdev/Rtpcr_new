namespace RtpcrCustomerApp.Api.Security
{
    using RtpcrCustomerApp.Api.Models;
    using RtpcrCustomerApp.BusinessModels.Common;
    using RtpcrCustomerApp.BusinessModels.DBO;
    using RtpcrCustomerApp.Common.Utils;
    using RtpcrCustomerApp.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class RoleDefinitionCache : Cache<RoleDefinitionCache, Dictionary<Role, RoleDefinition>>
    {
        private static readonly IRoleRepository roleRepository = InstanceFactory.GetInstance<IRoleRepository>();
        private static int retryCount = 0;
        public RoleDefinitionCache()
        {
        }

        protected override Dictionary<Role, RoleDefinition> GetData()
        {
            var roleAccessDetails = new List<RoleAccessDetails>();
            try
            {
                roleAccessDetails = roleRepository.GetRoleAccessDetails();
                var result = roleAccessDetails.GroupBy(r => r.Role)
                    .ToDictionary(r => r.Key, r => new RoleDefinition
                    {
                        Role = r.Key,
                        TokenExpirationDuration = r.First().TokenExpirationDuration,
                        AuthorizedEndpoints = new HashSet<string>(r.Select(e => e.Endpoint))
                    });
                retryCount = 0;
                return result;
            }
            catch
            {
                Interlocked.Increment(ref retryCount);
                if (retryCount < 5)
                    return GetData();
                return new Dictionary<Role, RoleDefinition>();
            }
        }

        protected override TimeSpan GetLifetime()
        {
            // Refreshed asynchronously when has spent more than 300 seconds in memory
            return TimeSpan.FromSeconds(300);
        }
    }

}