namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Vaccination
{
    using Newtonsoft.Json;
    using RtpcrCustomerApp.BusinessModels.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class VaccineOrderVerifyUpdateRequest : ModelBase
    {
        [Required]
        public int OrderID { get; set; }

        [Required, Range(1, 100, ErrorMessage = "At least one patient/vaccine is required")]
        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public LocationRequest Location { get; set; }

        [JsonProperty("Patients")]
        public List<VaccinePatientRequest> PatientVaccine { get; set; }
    }
}
