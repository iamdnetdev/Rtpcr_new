namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Vaccination
{
    using System;

    public class VaccinatorLocationResponse : IResponse
    {
        public Guid VaccinatorID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
