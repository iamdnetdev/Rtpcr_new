using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RtpcrCustomerApp.BusinessModels.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ValidationType
    {
        Required,
        Regex,
        MinLength,
        MaxLength,
        MinValue,
        MaxValue,
        Custom
    }
}
