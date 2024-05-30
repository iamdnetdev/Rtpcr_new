namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Vaccination
{
    using System;
    using static Dapper.SqlMapper;

    public class VaccineOrderInsert
    {
        public Guid UserID { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public Guid RegionID { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public ICustomQueryParameter Location { get; set; }
        public ICustomQueryParameter PatientVaccine { get; set; }
    }
}
