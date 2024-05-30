namespace RtpcrCustomerApp.Repositories.Common
{
    using Common;
    using Interfaces;
    using log4net;
    using RtpcrCustomerApp.Common.Interfaces;
    using System;
    using RtpcrCustomerApp.BusinessModels.DTO;
    using System.Collections.Generic;
    using System.Data;
    using BusinessModels.Common;
    using System.Linq;
    using RtpcrCustomerApp.BusinessModels.DBO;

    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        public RoleRepository(IDbSetting settings, ILoggerFactory loggerFactory) : base(settings, loggerFactory.GetLogger<RoleRepository>())
        {

        }

        public List<RoleAccessDetails> GetRoleAccessDetails()
        {
            try
            {
                var result = Query<RoleAccessDetails>(Queries.Role.GetRoleAccessDetails,
                                CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                logger.Error("Error in getting role access details", ex);
                return new List<RoleAccessDetails>();
            }
        }
    }
}
