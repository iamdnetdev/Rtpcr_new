namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    using BusinessModels.DTO.Response.Common;
    using System;
    using System.Collections.Generic;

    public interface IDeviceDetailsService
    {
        List<DeviceDetailsResponse> GetDevices(Guid userID);
    }
}
