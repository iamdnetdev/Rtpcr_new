namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Vaccination
{
    using BusinessModels.Common;
    using BusinessModels.DTO.Validators;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class VaccineOrderRequest : ModelBase
    {
        [Required]
        public Guid UserID { get; set; }

        [Required, Range(1, 100, ErrorMessage = "At least one patient/vaccine is required")]
        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public Guid RegionID { get; set; }

        public DateTime? ScheduleDate { get; set; }
        
        [VaccinationSlot]
        public string ScheduleSlot { get; set; }

        public LocationRequest Location { get; set; }

        [JsonProperty("Patients")]
        public List<VaccinePatientRequest> PatientVaccine { get; set; }
    }
}
