namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    using BusinessModels.DTO.Request.Test;
    using BusinessModels.DTO.Response;
    using BusinessModels.DTO.Response.Test;
    using RtpcrCustomerApp.BusinessModels.ViewModels.Test;
    using System;

    public interface ITestConsumerService
    {
        ListResponse<TestProductResponse> GetAvailableTestsByRegion(Guid regionId, bool onlyFavourite);
        TestOrderResponse PlaceOrder(TestOrderRequest vactestOrdercineOrder);
        StatusResponse UpdatePaymentStatus(TestPaymentUpdateRequest request);
        StatusResponse CancelOrder(int orderID);
        ListResponse<TestOrderHistoryResponse> GetOrderHistory(Guid userID);
        ListResponse<TestOrderItemResponse> GetOrderDetails(int orderId);
        byte[] GetReport(Guid orderId, Guid patientId);
        TestOrderConfirmation GetOrderConfirmationDetails(int orderID);
    }
}
