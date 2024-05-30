namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Test
{
    using BusinessModels.Common;
    using BusinessModels.DTO.Validators;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TestOrderRequest : ModelBase
    {
        [Required]
        public Guid UserId { get; set; }

        [Required, Range(1, 100, ErrorMessage = "At least one patient/test is required")]
        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public Guid RegionID { get; set; }

        [Required]
        public Guid LabID { get; set; }

        public DateTime? ScheduleDate { get; set; }

        [VaccinationSlot]
        public string ScheduleSlot { get; set; }

        public LocationRequest Location { get; set; }

        [JsonProperty("Patients")]
        public List<TestPatientRequest> Patients { get; set; }
    }
}
