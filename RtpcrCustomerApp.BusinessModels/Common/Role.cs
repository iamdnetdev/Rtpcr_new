using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RtpcrCustomerApp.BusinessModels.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Role
    {
        NoAccess = 0,
        SuperAdmin,
        LocationAdmin,
        LabAdmin,
        SeniorDoctor,
        Doctor,
        LabTechnician,
        SampleCollector,
        Consumer,
        Vaccinator,
        VaccineApprover
    }
}
