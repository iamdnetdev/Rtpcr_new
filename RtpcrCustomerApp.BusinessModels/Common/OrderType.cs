using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RtpcrCustomerApp.BusinessModels.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderType
    {
        Regular,
        Scheduled
    }
}
