﻿namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Vaccination
{
    using System;

    public class VaccineProductResponse : IResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Guid RegionID { get; set; }
        public decimal Price { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
    }
}
