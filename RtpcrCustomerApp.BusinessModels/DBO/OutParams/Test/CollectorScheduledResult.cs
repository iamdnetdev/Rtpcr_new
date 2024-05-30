namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test
{
    using System;

    public class CollectorScheduledResult
    {
        public int OrderID { get; set; }
        public Guid UserID { get; set; }
        public Guid CollectorID { get; set; }
        public string CollectorName { get; set; }
        public string Lab { get; set; }
    }
}
