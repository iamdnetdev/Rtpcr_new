namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test
{
    using System;

    public class TestProductResult
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Guid LabId { get; set; }
        public string LabName { get; set; }
        public decimal Price { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
    }
}
