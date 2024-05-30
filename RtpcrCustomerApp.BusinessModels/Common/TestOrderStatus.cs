using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RtpcrCustomerApp.BusinessModels.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
	public enum TestOrderStatus
    {
		Initiated = 1,
		PaymentReceived,
		SampleCollectorAssigned,
		SampleCollectorAccepted,
		SampleCollectorDeclined,
		SampleCollected,
		LabApprovedSample,
		LabProcessedSample,
		OrderCompleted,
		CancelledByConsumer,
		PaymentFailed
	}
}
