using Newtonsoft.Json;
using System.Collections.Generic;

namespace RtpcrCustomerApp.BusinessModels.Common
{
    public class PushNotificationMessageAndroid
    {
        [JsonProperty("notification")]
        public Notification Notification { get; set; }

        [JsonProperty("data")]
        public NotificationData Data { get; set; }

        [JsonProperty("registration_ids")]
        public List<string> DeviceID { get; set; }
    }

    public class NotificationData
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Notification
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}