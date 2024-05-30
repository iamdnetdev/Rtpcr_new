namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Vaccination
{
    using System;

    public class VaccinatorDetailsResponse : IResponse
    {
        public Guid VaccinatorID { get; set; }
        public Guid RegionID { get; set; }
        public string RegionName { get; set; }
        public Guid HospitalID { get; set; }
        public string HospitalName { get; set; }
        public decimal Distance { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
