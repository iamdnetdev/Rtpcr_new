namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Vaccination
{
    using RtpcrCustomerApp.BusinessModels.Common;
    using System;

    public class VaccinatorLocationRequest : ModelBase
    {
        public Guid VaccinatorID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
