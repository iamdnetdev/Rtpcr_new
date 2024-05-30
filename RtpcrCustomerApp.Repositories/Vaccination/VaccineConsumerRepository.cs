namespace RtpcrCustomerApp.Repositories.Vaccination
{
    using RtpcrCustomerApp.BusinessModels.DBO.InParams.Vaccination;
    using RtpcrCustomerApp.BusinessModels.DBO.OutParams.Vaccination;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Repositories.Common;
    using RtpcrCustomerApp.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VaccineConsumerRepository : RepositoryBase, IVaccineConsumerRepository
    {
        private readonly IMapper mapper;
        public VaccineConsumerRepository(IDbSetting settings, ILoggerFactory loggerFactory, IMapper mapper) : base(settings, loggerFactory.GetLogger<VaccinatorRepository>())
        {
            this.mapper = mapper;
        }

        public List<PatientResult> GetPatient(Guid userId)
        {
            return Query<dynamic, PatientResult>(Queries.VaccineConsumer.GetPatients, new { UserID = userId }).ToList();
        }

        public List<VaccineProductResult> GetAvailableVaccinesByRegion(Guid regionId)
        {
            return Query<dynamic, VaccineProductResult>(Queries.VaccineConsumer.GetAvailableVaccinesByRegion, new { RegionID = regionId }).ToList();
        }

        public List<VaccineOrderItemResult> GetOrderDetails(int orderId)
        {
            return Query<dynamic, VaccineOrderItemResult>(Queries.VaccineConsumer.GetOrderTestDetails, new { OrderId = orderId }).ToList();
        }

        public List<VaccineOrderHistoryResult> GetOrderHistory(Guid userId)
        {
            return Query<dynamic, VaccineOrderHistoryResult>(Queries.VaccineConsumer.GetOrderHistory, new { UserID = userId }).ToList();
        }

        public int PlaceOrder(VaccineOrderInsert vaccineOrder)
        {
            return ExecuteScalar<VaccineOrderInsert, int>(Queries.VaccineConsumer.PlaceOrder, vaccineOrder);
        }

        public List<VaccineOrderConfirmation> GetOrderConfirmationDetails(int orderId)
        {
            return Query<dynamic, VaccineOrderConfirmation>(Queries.VaccineConsumer.GetOrderDetailsConfirmationMail, new { OrderID = orderId }).ToList();
        }

        public void UpdatePaymentDetails(VaccineOrderPaymentUpdate paymentDetails)
        {
            ExecuteCommand(Queries.VaccineConsumer.UpdatePaymentDetails, paymentDetails);
        }

        public string CancelOrder(int orderID)
        {
            var paymentID = ExecuteScalar<dynamic, string>(Queries.VaccineConsumer.CancelOrderDetails, new { OrderID = orderID });
            return paymentID;
        }

        public void UpdateRefundDetails(int orderID, string refundID, string refundError)
        {
            ExecuteCommand(Queries.VaccineConsumer.UpdateRefundDetails, new
            {
                OrderID = orderID,
                RefundReference = refundID,
                RefundSucceeded = string.IsNullOrEmpty(refundError),
                RefundError = refundError
            });
        }
    }
}