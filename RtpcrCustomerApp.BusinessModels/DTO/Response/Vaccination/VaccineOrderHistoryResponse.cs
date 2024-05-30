namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Vaccination
{
    using System;

    public class VaccineOrderHistoryResponse : IResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatusActive { get; set; }
        public string OrderStatus { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; }
        public string PaymentReference { get; set; }
        public bool PaymentSucceeded { get; set; }
        public string PaymentError { get; set; }
        public string RefundReference { get; set; }
        public bool? RefundSucceeded { get; set; }
        public string RefundError { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
