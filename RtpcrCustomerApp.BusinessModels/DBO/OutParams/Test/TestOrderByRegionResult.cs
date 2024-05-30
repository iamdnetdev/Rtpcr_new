namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test
{
    using System;
    using BusinessModels.Common;

    public class TestOrderByRegionResult
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatusActive { get; set; }
        public TestOrderStatus OrderStatus { get; set; }
        public int Quantity { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
    }
}
