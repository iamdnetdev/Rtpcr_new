namespace RtpcrCustomerApp.Repositories.Interfaces
{
    using BusinessModels.DBO.OutParams.Common;
    using System;
    using System.Collections.Generic;

    public interface IDeviceDetailsRepository
    {
        List<DeviceDetailsResult> GetDeviceDetails(Guid userID);
    }
}
