namespace RtpcrCustomerApp.Repositories.Interfaces
{
    using RtpcrCustomerApp.BusinessModels.DBO.InParams.Vaccination;
    using RtpcrCustomerApp.BusinessModels.DBO.OutParams.Vaccination;
    using RtpcrCustomerApp.BusinessModels.DTO;
    using RtpcrCustomerApp.BusinessModels.DTO.Response;
    using System;
    using System.Collections.Generic;

    public interface IVaccineConsumerRepository
    {
        List<VaccineProductResult> GetAvailableVaccinesByRegion(Guid regionId);
        int PlaceOrder(VaccineOrderInsert vaccineOrder);
        List<VaccineOrderConfirmation> GetOrderConfirmationDetails(int orderId);
        List<VaccineOrderHistoryResult> GetOrderHistory(Guid userID);

        void UpdatePaymentDetails(VaccineOrderPaymentUpdate paymentDetails);
        List<VaccineOrderItemResult> GetOrderDetails(int orderId);

        string CancelOrder(int orderID);
        void UpdateRefundDetails(int orderID, string refundID, string refundStatus);
    }
}
