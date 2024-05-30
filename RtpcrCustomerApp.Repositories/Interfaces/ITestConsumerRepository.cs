namespace RtpcrCustomerApp.Repositories.Interfaces
{
    using BusinessModels.DBO.InParams.Test;
    using BusinessModels.DBO.OutParams.Test;
    using System;
    using System.Collections.Generic;
    using TestOrderConfirmation = BusinessModels.DBO.OutParams.Test.TestOrderConfirmation;

    public interface ITestConsumerRepository
    {
        List<TestProductResult> GetAvailableTestsByRegion(Guid regionId, bool onlyFavourite);
        int PlaceOrder(TestOrderInsert testOrder);
        List<TestOrderHistoryResult> GetOrderHistory(Guid userID);
        List<TestOrderItemResult> GetOrderDetails(int orderId);
        void UpdatePaymentDetails(TestOrderPaymentUpdate paymentDetails);
        string CancelOrder(int orderID);
        void UpdateRefundDetails(int orderID, string refundID, string refundStatus);
        List<TestOrderConfirmation> GetOrderConfirmationDetails(int orderId);
    }
}
