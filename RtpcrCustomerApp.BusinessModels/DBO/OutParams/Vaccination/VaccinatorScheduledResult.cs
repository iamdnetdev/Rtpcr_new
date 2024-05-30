namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Vaccination
{
    using System;
    public class VaccinatorScheduledResult
    {
        public int OrderID { get; set; }
        public Guid UserID { get; set; }
        public Guid VaccinatorID { get; set; }
        public string VaccinatorName { get; set; }
        public string Hospital { get; set; }
    }
}
