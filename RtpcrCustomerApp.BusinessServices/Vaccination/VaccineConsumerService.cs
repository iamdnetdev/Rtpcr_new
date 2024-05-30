namespace RtpcrCustomerApp.BusinessServices.Vaccination
{
    using BusinessModels.Common;
    using BusinessModels.DBO.InParams.Vaccination;
    using BusinessModels.DBO.OutParams.Vaccination;
    using BusinessModels.DTO.Request.Vaccination;
    using BusinessModels.DTO.Response;
    using BusinessModels.DTO.Response.Vaccination;
    using BusinessModels.Exceptions;
    using BusinessModels.ViewModels.Vaccination;
    using Interfaces;
    using log4net;
    using Repositories.Interfaces;
    using RtpcrCustomerApp.BusinessModels.DTO.Reponse.Vaccination;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VaccineOrderConfirmation = BusinessModels.ViewModels.Vaccination.VaccineOrderConfirmation;

    public class VaccineConsumerService : IVaccineConsumerService
    {
        private readonly IVaccineConsumerRepository repository;
        private readonly ILog logger;
        private readonly IMapper mapper;
        private readonly IPaymentService paymentService;
        public VaccineConsumerService(IVaccineConsumerRepository repository, ILoggerFactory loggerFactory, IMapper mapper, IPaymentService paymentService)
        {
            this.repository = repository;
            logger = loggerFactory.GetLogger<VaccineConsumerService>();
            this.mapper = mapper;
            this.paymentService = paymentService;
        }

        public ListResponse<VaccineProductResponse> GetAvailableVaccinesByRegion(Guid regionId)
        {
            try
            {
                var response = repository.GetAvailableVaccinesByRegion(regionId);
                return mapper.Map<List<VaccineProductResult>, ListResponse<VaccineProductResponse>>(response);
            }
            catch (Exception ex)
            {
                logger.Error("Error in getting available vaccines by region: ", ex);
                throw ex;
            }
        }

        public ListResponse<VaccineOrderItemResponse> GetOrderDetails(int orderId)
        {
            try
            {
                var response = repository.GetOrderDetails(orderId);
                return mapper.Map<List<VaccineOrderItemResult>, ListResponse<VaccineOrderItemResponse>>(response);
            }
            catch (Exception ex)
            {
                logger.Error("Error in getting order details: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdatePaymentStatus(VaccinePaymentUpdateRequest request)
        {
            try
            {
                var param = mapper.Map<VaccinePaymentUpdateRequest, VaccineOrderPaymentUpdate>(request);
                repository.UpdatePaymentDetails(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdatePaymentStatus: ", ex);
                throw ex;
            }
        }

        public ListResponse<VaccineOrderHistoryResponse> GetOrderHistory(Guid userID)
        {
            try
            {
                var response = repository.GetOrderHistory(userID);
                return mapper.Map<List<VaccineOrderHistoryResult>, ListResponse<VaccineOrderHistoryResponse>>(response);
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

        public VaccineOrderResponse PlaceOrder(VaccineOrderRequest vaccineOrder)
        {
            try
            {
                var inParam = mapper.Map<VaccineOrderRequest, VaccineOrderInsert>(vaccineOrder);
                var orderID = repository.PlaceOrder(inParam);
                var response = new VaccineOrderResponse { OrderID = orderID };
                logger.InfoFormat("Placing order succeeded: {0} - {1}", vaccineOrder.UserID);
                return response;
            }
            catch (ServiceException ex)
            {
                logger.Warn(ex.Message, ex);
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Error in placing order: ", ex);
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
                    logger.Error("Error in vaccine order payment refund: ", ex);
                    repository.UpdateRefundDetails(orderID, refundID, ex.Message);
                    return new StatusResponse(AppSettings.Environment.IsProd() ? defaultRefundError : ex.Message);
                }

                repository.UpdateRefundDetails(orderID, refundID, null);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {

                logger.Error("Error in Cancel order vaccine: ", ex);
                throw ex;
            }
        }

        #region Private Methods

        public VaccineOrderConfirmation GetOrderConfirmationDetails(int orderID)
        {
            try
            {
                var details = repository.GetOrderConfirmationDetails(orderID);
                var dtlsGrouped = details.GroupBy(o => o.OrderID)
                    .Select(o => new BusinessModels.ViewModels.Vaccination.VaccineOrderConfirmation
                    {
                        OrderID = o.Key,
                        OrderType = o.First().OrderType,
                        Address = o.First().Address,
                        Email = o.First().PrimaryUserEmail,
                        Amount = o.First().Amount,
                        Quantity = o.First().Quantity,
                        ScheduleDate = o.First().ScheduleDate.HasValue ? o.First().ScheduleDate.Value.Date : (DateTime?)null,
                        ScheduleSlot = VaccinationSlots.FindSlot(o.First().ScheduleDate),
                        Vaccinator = string.IsNullOrEmpty((o.First().Vaccinator ?? string.Empty).Trim()) ?
                            (o.First().OrderType == OrderType.Regular ? "* VACCINATOR NOT YET ASSIGNED" : "* VACCINATOR WILL BE ASSIGNED BEFORE YOUR SCHEDULED TIME") :
                            o.First().Vaccinator,
                        VaccinePatients = o.Select(dtl => new VaccinePatient
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
                            OrderItemStatus = dtl.OrderItemStatus,
                            VaccinationDate = dtl.VaccinationDate
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

        #endregion
    }
}
