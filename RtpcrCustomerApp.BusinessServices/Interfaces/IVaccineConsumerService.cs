namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    using BusinessModels.DTO.Request.Vaccination;
    using BusinessModels.DTO.Response;
    using BusinessModels.DTO.Response.Vaccination;
    using BusinessModels.ViewModels.Vaccination;
    using RtpcrCustomerApp.BusinessModels.DTO.Reponse.Vaccination;
    using System;

    public interface IVaccineConsumerService
    {
        ListResponse<VaccineProductResponse> GetAvailableVaccinesByRegion(Guid regionId);
        VaccineOrderResponse PlaceOrder(VaccineOrderRequest vaccineOrder);
        StatusResponse UpdatePaymentStatus(VaccinePaymentUpdateRequest request);
        ListResponse<VaccineOrderHistoryResponse> GetOrderHistory(Guid userID);
        ListResponse<VaccineOrderItemResponse> GetOrderDetails(int orderId);

        StatusResponse CancelOrder(int orderID);
        byte[] GetReport(Guid orderId, Guid patientId);
        VaccineOrderConfirmation GetOrderConfirmationDetails(int orderID);
    }
}
