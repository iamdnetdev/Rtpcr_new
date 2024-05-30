namespace RtpcrCustomerApp.Api.Controllers.Test
{
    using log4net;
    using Newtonsoft.Json;
    using Api.Filters;
    using BusinessModels.Common;
    using BusinessModels.DTO.Request.Test;
    using BusinessModels.DTO.Response;
    using BusinessModels.DTO.Response.Test;
    using BusinessServices.Interfaces;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Logging;
    using RtpcrCustomerApp.Common.Utils;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Web.Http;
    using System.Collections.Generic;
    using RtpcrCustomerApp.BusinessModels.ViewModels.Test;

    [Module(Modules.Test)]
    [RoutePrefix("Test/Consumer")]
    public class TestConsumerController : ApiController
    {
        private readonly ITestConsumerService testConsumerService;
        private readonly IEmailService emailService;
        private const string Module = Modules.Test;
        private readonly ILog logger;
        public TestConsumerController(ITestConsumerService testConsumerService, IEmailService emailService, ILoggerFactory loggerFactory)
        {
            this.testConsumerService = testConsumerService;
            this.emailService = emailService;
            logger = loggerFactory.GetLogger<TestConsumerController>();
        }

        [HttpGet]
        [Route("GetAvailableTests")]
        [NonAuthorize]
        public ApiResponse<ListResponse<TestProductResponse>> GetAvailableTests(Guid regionId, bool onlyFavourite)
        {
            try
            {
                var response = testConsumerService.GetAvailableTestsByRegion(regionId, onlyFavourite);
                return new ApiResponse<ListResponse<TestProductResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<TestProductResponse>>(ex.GetErrors(Module, "GetAvailableTestsByRegion"));
            }
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public ApiResponse<TestOrderResponse> PlaceOrder(TestOrderRequest testOrder)
        {
            try
            {
                var validationErrors = testOrder.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in placing order: {0}", testOrder.UserId);
                    return new ApiResponse<TestOrderResponse>(validationErrors.ToErrors());
                }
                var response = testConsumerService.PlaceOrder(testOrder);              
                return new ApiResponse<TestOrderResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TestOrderResponse>(ex.GetErrors(Module, "PlaceOrder"));
            }
        }

        [HttpPost]
        [Route("UpdatePaymentStatus")]
        public ApiResponse<StatusResponse> UpdatePaymentStatus(TestPaymentUpdateRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in updating payment details: {0}", request.OrderID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = testConsumerService.UpdatePaymentStatus(request);
                if (!string.IsNullOrEmpty(request.RazorPaymentID))
                {
                    var confirmationDetails = testConsumerService.GetOrderConfirmationDetails(request.OrderID);
                    emailService.SendMail<MailController, TestOrderConfirmation>(confirmationDetails.Email, "Order Confirmation", "~/Views/Mail/TestOrderConfirmation.cshtml", confirmationDetails);
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
        public ApiResponse<ListResponse<TestOrderHistoryResponse>> OrderHistory(Guid userID)
        {
            try
            {
                var response = testConsumerService.GetOrderHistory(userID);
                return new ApiResponse<ListResponse<TestOrderHistoryResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<TestOrderHistoryResponse>>(ex.GetErrors(Module, "OrderHistory"));
            }
        }

        [HttpGet]
        [Route("OrderDetails")]
        public ApiResponse<ListResponse<TestOrderItemResponse>> OrderDetails(int orderId)
        {
            try
            {
                var response = testConsumerService.GetOrderDetails(orderId);
                return new ApiResponse<ListResponse<TestOrderItemResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<TestOrderItemResponse>>(ex.GetErrors(Module, "OrderDetails"));
            }
        }

        [HttpGet]
        [Route("Report")]
        public HttpResponseMessage Report(Guid orderId, Guid patientId)
        {
            try
            {
                var reportContent = testConsumerService.GetReport(orderId, patientId);
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
                var response = testConsumerService.CancelOrder(orderID);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "CancelOrder"));
            }
        }
    }
}
