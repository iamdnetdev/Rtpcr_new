namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Vaccination
{
    using RtpcrCustomerApp.BusinessModels.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VaccinatorStatusUpdateRequest : ModelBase
    {
        [Required]
        public Guid? VaccinnatorID { get; set; }

        [Required]
        public bool? IsLoggedIn { get; set; }
    }
}
