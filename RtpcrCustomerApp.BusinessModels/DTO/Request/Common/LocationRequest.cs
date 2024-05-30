namespace RtpcrCustomerApp.BusinessModels.DTO
{
    using Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LocationRequest : ModelBase
    {
        public Guid? LocationID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        [Required]
        public string Address { get; set; }
        public string LocationName { get; set; }
        public bool IsDefault { get; set; }
    }
}
