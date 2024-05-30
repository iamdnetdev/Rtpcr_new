namespace RtpcrCustomerApp.Repositories.Test
{
    using BusinessModels.DBO.InParams.Test;
    using BusinessModels.DBO.OutParams.Test;
    using BusinessModels.DBO.OutParams.Vaccination;
    using BusinessModels.ViewModels.Test;
    using Repositories.Common;
    using Repositories.Interfaces;
    using RtpcrCustomerApp.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestOrderConfirmation = BusinessModels.DBO.OutParams.Test.TestOrderConfirmation;

    public class TestConsumerRepository : RepositoryBase, ITestConsumerRepository
    {
        public TestConsumerRepository(IDbSetting settings, ILoggerFactory loggerFactory) : base(settings, loggerFactory.GetLogger<TestConsumerRepository>())
        {
        }

        public List<PatientResult> GetPatient(Guid userId)
        {
            return Query<dynamic, PatientResult>(Queries.TestConsumer.GetPatients, new { UserID = userId }).ToList();
        }

        public List<TestProductResult> GetAvailableTestsByRegion(Guid regionId, bool onlyFavourite)
        {
            return Query<dynamic, TestProductResult>(Queries.TestConsumer.GetAvailableTestsByRegion, new { RegionID = regionId, OnlyFavourite = onlyFavourite }).ToList();
        }

        public List<TestOrderItemResult> GetOrderDetails(int orderId)
        {
            return Query<dynamic, TestOrderItemResult>(Queries.TestConsumer.GetOrderTestDetails, new { OrderId = orderId }).ToList();
        }

        public List<TestOrderHistoryResult> GetOrderHistory(Guid userId)
        {
            return Query<dynamic, TestOrderHistoryResult>(Queries.TestConsumer.GetOrderHistory, new { UserID = userId }).ToList();
        }

        public int PlaceOrder(TestOrderInsert testOrder)
        {
            return ExecuteScalar<TestOrderInsert, int>(Queries.TestConsumer.PlaceOrder, testOrder);
        }

        public void UpdatePaymentDetails(TestOrderPaymentUpdate paymentDetails)
        {
            ExecuteCommand(Queries.TestConsumer.UpdatePaymentDetails, paymentDetails);
        }

        public string CancelOrder(int orderID)
        {
            var paymentID = ExecuteScalar<dynamic, string>(Queries.TestConsumer.CancelOrderDetails, new { OrderID = orderID });
            return paymentID;
        }

        public void UpdateRefundDetails(int orderID, string refundID, string refundError)
        {
            ExecuteCommand(Queries.TestConsumer.UpdateRefundDetails, new
            {
                OrderID = orderID,
                RefundReference = refundID,
                RefundSucceeded = string.IsNullOrEmpty(refundError),
                RefundError = refundError
            });
        }

        public List<TestOrderConfirmation> GetOrderConfirmationDetails(int orderId)
        {
            return Query<dynamic, TestOrderConfirmation>(Queries.TestConsumer.GetOrderDetailsConfirmationMail, new { OrderID = orderId }).ToList();
        }
    }
}
