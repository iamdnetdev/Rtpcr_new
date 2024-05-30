namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Vaccination
{
    using System;

    public class VaccinatorLocationUpdate 
    {
        public Guid VaccinatorID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
