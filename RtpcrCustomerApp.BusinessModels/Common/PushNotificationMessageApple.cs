using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RtpcrCustomerApp.BusinessModels.Common
{
    public class PushNotificationMessageApple
    {
        [JsonProperty("aps")]
        public AlertMessage AlertMessage { get; set; }

        public JObject ToJObject()
        {
            return JObject.Parse(JsonConvert.SerializeObject(this));
        }
    }

    public class AlertMessage
    {
        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("sound")]
        public string Sound { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }
    }
}
