using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RtpcrCustomerApp.BusinessModels.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VaccineOrderStatus
    {
        Initiated = 1,
        PaymentReceived,
        VaccinatorAssigned,
        VaccinatorAccepted,
        VaccinatorDeclined,
        VaccineGiven,
        VaccinatorApproval,
        OrderCompleted,
        CancelledByConsumer,
        PaymentFailed
    }
}
