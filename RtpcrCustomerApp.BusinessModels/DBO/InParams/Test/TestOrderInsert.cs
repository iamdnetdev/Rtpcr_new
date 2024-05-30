namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Test
{
    using System;
    using static Dapper.SqlMapper;

    public class TestOrderInsert
    {
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public Guid RegionID { get; set; }
        public Guid LabID { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public ICustomQueryParameter Location { get; set; }
        public ICustomQueryParameter Patients { get; set; }
    }
}
