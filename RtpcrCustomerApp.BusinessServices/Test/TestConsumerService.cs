namespace RtpcrCustomerApp.BusinessServices.Test
{
    using BusinessModels.DTO.Request.Test;
    using BusinessModels.DTO.Response;
    using BusinessModels.DTO.Response.Test;
    using BusinessServices.Interfaces;
    using log4net;
    using RtpcrCustomerApp.BusinessModels.Common;
    using RtpcrCustomerApp.BusinessModels.DBO.InParams.Test;
    using RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test;
    using RtpcrCustomerApp.BusinessModels.ViewModels.Test;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Utils;
    using RtpcrCustomerApp.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestOrderConfirmation = BusinessModels.ViewModels.Test.TestOrderConfirmation;

    public class TestConsumerService : ITestConsumerService
    {
        private readonly ITestConsumerRepository repository;
        private readonly ILog logger;
        private readonly IMapper mapper;
        private readonly IPaymentService paymentService;

        public TestConsumerService(ITestConsumerRepository repository, IPaymentService paymentService, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.repository = repository;
            this.paymentService = paymentService;
            logger = loggerFactory.GetLogger<TestConsumerService>();
            this.mapper = mapper;
        }

        public ListResponse<TestProductResponse> GetAvailableTestsByRegion(Guid regionId, bool onlyFavourite)
        {
            try
            {
                var response = repository.GetAvailableTestsByRegion(regionId, onlyFavourite);
                return mapper.Map<List<TestProductResult>, ListResponse<TestProductResponse>>(response);
            }
            catch (Exception ex)
            {
                logger.Error("Error in getting available tests by region: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdatePaymentStatus(TestPaymentUpdateRequest request)
        {
            try
            {
                var param = mapper.Map<TestPaymentUpdateRequest, TestOrderPaymentUpdate>(request);
                repository.UpdatePaymentDetails(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdatePaymentStatus: ", ex);
                throw ex;
            }
        }

        public StatusResponse CancelOrder(int orderID)
        {
            const string defaultRefundError = "Refund failed due to some technical error. Please contact our support team";

            try
            {
                var paymentId = repository.CancelOrder(orderID);
                var refundID = string.Empty;
                try
                {
                    refundID = paymentService.Refund(paymentId);
                }
                catch (Exception ex)
                {
                    logger.Error("Error in test order payment refund: ", ex);
                    repository.UpdateRefundDetails(orderID, refundID, ex.Message);
                    return new StatusResponse(AppSettings.Environment.IsProd() ? defaultRefundError : ex.Message);
                }

                repository.UpdateRefundDetails(orderID, refundID, null);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Cancel order test: ", ex);
                throw ex;
            }
        }

        public ListResponse<TestOrderItemResponse> GetOrderDetails(int orderId)
        {
            try
            {
                var response = repository.GetOrderDetails(orderId);
                return mapper.Map<List<TestOrderItemResult>, ListResponse<TestOrderItemResponse>>(response);
            }
            catch (Exception ex)
            {
                logger.Error("Error in getting order details: ", ex);
                throw ex;
            }
        }

        public ListResponse<TestOrderHistoryResponse> GetOrderHistory(Guid userID)
        {
            try
            {
                var response = repository.GetOrderHistory(userID);
                return mapper.Map<List<TestOrderHistoryResult>, ListResponse<TestOrderHistoryResponse>>(response);
            }
            catch (Exception ex)
            {
                logger.Error("Error in getting order history: ", ex);
                throw ex;
            }
        }

        public byte[] GetReport(Guid orderId, Guid patientId)
        {
            throw new NotImplementedException();
        }

        public TestOrderResponse PlaceOrder(TestOrderRequest testOrder)
        {
            try
            {
                var inParam = mapper.Map<TestOrderRequest, TestOrderInsert>(testOrder);
                var orderID = repository.PlaceOrder(inParam);
                var response = new TestOrderResponse { OrderID = orderID };
                logger.InfoFormat("Placing order succeeded: {0} - {1}", testOrder.UserId);
                return response;
            }
            catch (Exception ex)
            {
                logger.Error("Error in placing order: ", ex);
                throw ex;
            }
        }

        public TestOrderConfirmation GetOrderConfirmationDetails(int orderID)
        {
            try
            {
                var details = repository.GetOrderConfirmationDetails(orderID);
                var dtlsGrouped = details.GroupBy(o => o.OrderID)
                    .Select(o => new BusinessModels.ViewModels.Test.TestOrderConfirmation
                    {
                        OrderID = o.Key,
                        OrderType = o.First().OrderType,
                        Address = o.First().Address,
                        Email = o.First().Email,
                        Amount = o.First().Amount,
                        Quantity = o.First().Quantity,
                        ScheduleDate = o.First().ScheduleDate.HasValue ? o.First().ScheduleDate.Value.Date : (DateTime?)null,
                        ScheduleSlot = VaccinationSlots.FindSlot(o.First().ScheduleDate),
                        Collector = string.IsNullOrEmpty((o.First().Collector ?? string.Empty).Trim()) ?
                            (o.First().OrderType == OrderType.Regular ? "* SAMPLE COLLECTOR NOT YET ASSIGNED" : "* SAMPLE COLLECTOR WILL BE ASSIGNED BEFORE YOUR SCHEDULED TIME") :
                            o.First().Collector,
                        TestPatients = o.Select(dtl => new TestPatient
                        {
                            Email = dtl.Email,
                            FirstName = dtl.FirstName,
                            LastName = dtl.LastName,
                            Phone = dtl.Phone,
                            ProductName = dtl.ProductName,
                            ProductDescription = dtl.ProductDescription,
                            ProductPrice = dtl.Price,
                            SGST = dtl.SGST,
                            CGST = dtl.CGST,
                            OrderItemStatus = dtl.OrderItemStatus
                        }).ToList()
                    }).FirstOrDefault();
                if (dtlsGrouped == null) throw new Exception("Error in retrieving order details for sending confirmation mail");
                return dtlsGrouped;
            }
            catch
            {
                throw;
            }
        }
    }
}
