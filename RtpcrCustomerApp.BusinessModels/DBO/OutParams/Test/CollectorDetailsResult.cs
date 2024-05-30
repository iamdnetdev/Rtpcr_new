using System;

namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test
{
    public class CollectorDetailsResult
    {
        public Guid CollectorID { get; set; }
        public Guid RegionID { get; set; }
        public string RegionName { get; set; }
        public Guid LabID { get; set; }
        public string LabName { get; set; }
        public decimal Distance { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsLoggedIn { get; set; }
        public float OrderAcceptRadius { get; set; }
    }
}
