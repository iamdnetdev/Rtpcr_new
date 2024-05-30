using log4net;
using Newtonsoft.Json;
using RtpcrCustomerApp.Api.Filters;
using RtpcrCustomerApp.BusinessModels.Common;
using RtpcrCustomerApp.BusinessModels.DTO.Reponse.Vaccination;
using RtpcrCustomerApp.BusinessModels.DTO.Request.Vaccination;
using RtpcrCustomerApp.BusinessModels.DTO.Response;
using RtpcrCustomerApp.BusinessModels.DTO.Response.Vaccination;
using RtpcrCustomerApp.BusinessModels.ViewModels.Vaccination;
using RtpcrCustomerApp.BusinessServices.Interfaces;
using RtpcrCustomerApp.Common.Interfaces;
using RtpcrCustomerApp.Common.Logging;
using RtpcrCustomerApp.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace RtpcrCustomerApp.Api.Controllers.Vaccination
{
    [Module(Modules.Vaccination)]
    [RoutePrefix("Vaccination/Consumer")]
    public class VaccineConsumerController : ApiController
    {
        private readonly IVaccineConsumerService vaccineConsumerService;
        private readonly IEmailService emailService;
        private const string Module = Modules.Vaccination;
        private readonly ILog logger;
        public VaccineConsumerController(IVaccineConsumerService vaccineConsumerService, IEmailService emailService, ILoggerFactory loggerFactory)
        {
            this.vaccineConsumerService = vaccineConsumerService;
            this.emailService = emailService;
            logger = loggerFactory.GetLogger<VaccineConsumerController>();
        }

        [HttpGet]
        [Route("GetAvailableVaccines")]
        [NonAuthorize]
        public ApiResponse<ListResponse<VaccineProductResponse>> GetAvailableVaccines(Guid regionId)
        {
            try
            {
                var response = vaccineConsumerService.GetAvailableVaccinesByRegion(regionId);
                return new ApiResponse<ListResponse<VaccineProductResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<VaccineProductResponse>>(ex.GetErrors(Module, "GetAvailableVaccinesByRegion"));
            }
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public ApiResponse<VaccineOrderResponse> PlaceOrder(VaccineOrderRequest vaccineOrder)
        {
            try
            {
                //vaccineOrder = MockData.VaccineOrder;
                var validationErrors = vaccineOrder.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in placing order: {0}",  vaccineOrder.UserID);
                    return new ApiResponse<VaccineOrderResponse>(validationErrors.ToErrors());
                }
                var response = vaccineConsumerService.PlaceOrder(vaccineOrder);
                return new ApiResponse<VaccineOrderResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<VaccineOrderResponse>(ex.GetErrors(Module, "PlaceOrder"));
            }
        }

        [HttpPost]
        [Route("UpdatePaymentStatus")]
        public ApiResponse<StatusResponse> UpdatePaymentStatus(VaccinePaymentUpdateRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in updating payment details: {0}", request.OrderID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = vaccineConsumerService.UpdatePaymentStatus(request);
                if(!string.IsNullOrEmpty(request.RazorPaymentID))
                {
                    var confirmationDetails = vaccineConsumerService.GetOrderConfirmationDetails(request.OrderID);
                    emailService.SendMail<MailController, VaccineOrderConfirmation>(confirmationDetails.Email, "Order Confirmation", "~/Views/Mail/VaccineOrderConfirmation.cshtml", confirmationDetails);
                }
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdatePaymentStatus"));
            }
        }

        [HttpGet]
        [Route("OrderHistory")]
        public ApiResponse<ListResponse<VaccineOrderHistoryResponse>> OrderHistory(Guid userID)
        {
            try
            {
                var response = vaccineConsumerService.GetOrderHistory(userID);
                return new ApiResponse<ListResponse<VaccineOrderHistoryResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<VaccineOrderHistoryResponse>>(ex.GetErrors(Module, "OrderHistory"));
            }
        }

        [HttpGet]
        [Route("OrderDetails")]
        public ApiResponse<ListResponse<VaccineOrderItemResponse>> OrderDetails(int orderId)
        {
            try
            {
                var response = vaccineConsumerService.GetOrderDetails(orderId);
                return new ApiResponse<ListResponse<VaccineOrderItemResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<VaccineOrderItemResponse>>(ex.GetErrors(Module, "OrderDetails"));
            }
        }

        [HttpGet]
        [Route("Report")]
        public HttpResponseMessage Report(Guid orderId, Guid patientId)
        {
            try
            {
                var reportContent = vaccineConsumerService.GetReport(orderId, patientId);
                var dataStream = new MemoryStream(reportContent);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StreamContent(dataStream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "Report-" + patientId + ".pdf";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

                return response;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(ex.GetErrors(Module, "Report")), Encoding.UTF8, "application/json");
                return response;
            }
        }

        [HttpPost]
        [Route("CancelOrder")]
        public ApiResponse<StatusResponse> CancelOrder(int orderID)
        {
            try
            {
                if (orderID == 0)
                {
                    logger.WarnFormat("Validation fails in cancelling order given status: {0}", orderID);
                    return new ApiResponse<StatusResponse>(new List<Error>
                    {
                        new Error
                        {
                            Code = "VALIDATION",
                            Description="Order ID is required",
                            Message = "Order ID is required",
                            Module = Module,
                            Operation = "CancelOrder",
                            Type = ErrorType.Validation
                        }
                    });
                }
                var response = vaccineConsumerService.CancelOrder(orderID);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "CancelOrder"));
            }
        }
    }
}