namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Vaccination
{
    using System;

    public class VaccinatorOpenOrderResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatusActive { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public float Distance { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
