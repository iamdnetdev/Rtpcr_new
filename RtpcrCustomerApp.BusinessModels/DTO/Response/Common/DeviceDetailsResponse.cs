namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Common
{
    using BusinessModels.Common;

    public class DeviceDetailsResponse : IResponse
    {
        public string DeviceID { get; set; }
        public DevicePlatform DevicePlatform { get; set; }
    }
}
