namespace RtpcrCustomerApp.Repositories.Common
{
    using RtpcrCustomerApp.BusinessModels.DBO.OutParams.Common;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class DeviceDetailsRepository : RepositoryBase, IDeviceDetailsRepository
    {
        public DeviceDetailsRepository(IDbSetting settings, ILoggerFactory loggerFactory) : base(settings, loggerFactory.GetLogger<DeviceDetailsRepository>())
        {

        }

        public List<DeviceDetailsResult> GetDeviceDetails(Guid userID)
        {
            var deviceDetails = Query<DeviceDetailsResult>(Queries.Account.GetAccountById,
                                         CommandType.StoredProcedure,
                                         new KeyValuePair<string, object>("UserID", userID))
                            .ToList();
            return deviceDetails;
        }
    }
}
