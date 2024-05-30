namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Vaccination
{
    using System;

    public class VaccinatorLocationResult
    {
        public Guid VaccinatorID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
