using log4net;
using RtpcrCustomerApp.Api.Filters;
using RtpcrCustomerApp.BusinessModels.Common;
using RtpcrCustomerApp.BusinessModels.DTO.Request.Common;
using RtpcrCustomerApp.BusinessModels.DTO.Request.Test;
using RtpcrCustomerApp.BusinessModels.DTO.Response;
using RtpcrCustomerApp.BusinessModels.DTO.Response.Test;
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
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace RtpcrCustomerApp.Api.Controllers.Test
{
    [Module(Modules.Test)]
    [RoutePrefix("Test/Collector")]
    public class CollectorController : ApiController
    {
        private readonly ICollectorService collectorService;
        private const string Module = Modules.Test;
        private static readonly HashSet<string> AllowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".gif", ".png", ".bmp", ".pdf" };
        private ILog logger;

        public CollectorController (ICollectorService collectorService, ILoggerFactory loggerFactory)
        {
            this.collectorService = collectorService;
            this.logger = loggerFactory.GetLogger<CollectorController>();
        }

        [HttpPost]
        [Route("Login")]
        [NonAuthorize]
        public ApiResponse<CollectorSignInResponse> Login(GenericSignInRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in login request: {0}", request.Phone);
                    return new ApiResponse<CollectorSignInResponse>(validationErrors.ToErrors());
                }
                var response = collectorService.Login(request);
                return new ApiResponse<CollectorSignInResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CollectorSignInResponse>(ex.GetErrors(Module, "Login"));
            }
        }
        [HttpGet]
        [Route("GetCurrentLocation")]
        public ApiResponse<CollectorLocationResponse> GetCurrentLocation(Guid userId)
        {
            try
            {
                var response = collectorService.GetCurrentLocation(userId);
                return new ApiResponse<CollectorLocationResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CollectorLocationResponse>(ex.GetErrors(Module, "GetCurrentLocation"));
            }
        }

        [HttpGet]
        [Route("GetCurrentLocationTrack")]
        public ApiResponse<CollectorLocationResponse> GetCurrentLocationTrack(Guid userId, int orderID)
        {
            try
            {
                var response = collectorService.GetCurrentLocationTrack(userId, orderID);
                return new ApiResponse<CollectorLocationResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CollectorLocationResponse>(ex.GetErrors(Module, "GetCurrentLocationTrack "));
            }
        }

        [HttpGet]
        [Route("GetCollectors")]
        public ApiResponse<ListResponse<CollectorDetailsResponse>> GetCollectors(decimal latitude, decimal longitude, Guid? regionID = null, int? orderID = null, bool ignoreIfAlreadyDeclined = true)
        {
            try
            {
                var response = collectorService.GetCollectors(latitude, longitude, regionID, orderID, ignoreIfAlreadyDeclined);
                return new ApiResponse<ListResponse<CollectorDetailsResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<CollectorDetailsResponse>>(ex.GetErrors(Module, "GetCollectors"));
            }
        }

        [HttpPost]
        [Route("AcceptOrder")]
        public ApiResponse<StatusResponse> AcceptOrder(CollectorAcceptOrderRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in accepting order: {0}", request.CollectorID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = collectorService.AcceptOrder(request);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "AcceptOrder"));
            }
        }

        [HttpPost]
        [Route("DeclineOrder")]
        public ApiResponse<StatusResponse> DeclineOrder(CollectorDeclineOrderRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in declining order: {0}", request.CollectorID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = collectorService.DeclineOrder(request);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "DeclineOrder"));
            }
        }

        //[HttpGet]
        //[Route("GetOrdersByRegion")]
        //public ApiResponse<ListResponse<TestOrderByRegionResponse>> GetOrdersByRegion(Guid regionID, Guid? collectorID, bool? showOnlyUnassigned)
        //{
        //    try
        //    {
        //        var response = collectorService.GetOrdersByRegion(regionID, collectorID, showOnlyUnassigned);
        //        return new ApiResponse<ListResponse<TestOrderByRegionResponse>>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<ListResponse<TestOrderByRegionResponse>>(ex.GetErrors(Module, "GetOrdersByRegion"));
        //    }
        //}

        //[HttpPost]
        //[Route("AssignCollector")]
        //public ApiResponse<StatusResponse> AssignCollector(AssignCollectorRequest request)
        //{
        //    try
        //    {
        //        var validationErrors = request.GetValidationErrors();
        //        if (validationErrors.Any())
        //        {
        //            logger.WarnFormat("Validation fails in assigning sample collector: {0}", request.CollectorID);
        //            return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
        //        }
        //        var response = collectorService.AssignCollector(request);
        //        return new ApiResponse<StatusResponse>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "AssignCollector"));
        //    }
        //}

        [HttpGet]
        [Route("GetAssignedOrders")]
        public ApiResponse<ListResponse<CollectorAssignedOrderResponse>> GetAssignedOrders(Guid collectorID, bool showOnlyOpen)
        {
            try
            {
                var response = collectorService.GetAssignedOrders(collectorID, showOnlyOpen);
                return new ApiResponse<ListResponse<CollectorAssignedOrderResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<CollectorAssignedOrderResponse>>(ex.GetErrors(Module, "GetAssignedOrders"));
            }
        }

        [HttpGet]
        [Route("OrderDetails")]
        public ApiResponse<ListResponse<CollectorOrderHistoryResponse>> GetOrderHistory(Guid collectorID)
        {
            try
            {
                var response = collectorService.GetOrderHistory(collectorID);
                return new ApiResponse<ListResponse<CollectorOrderHistoryResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<CollectorOrderHistoryResponse>>(ex.GetErrors(Module, "GetOrderHistory"));
            }
        }

        [HttpPost]
        [Route("UpdateLocation")]
        public ApiResponse<StatusResponse> UpdateLocation(CollectorLocationRequest collectorLocation)
        {
            try
            {
                var validationErrors = collectorLocation.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in updating location: {0}", collectorLocation.CollectorID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }

                var response = collectorService.UpdateLocation(collectorLocation);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdateLocation"));
            }
        }

        [HttpPost]
        [Route("UpdateLoggedInStatus")]
        public ApiResponse<StatusResponse> UpdateLoggedInStatus(CollectorStatusUpdateRequest collectorStatus)
        {
            try
            {
                var validationErrors = collectorStatus.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in updating status: {0}", collectorStatus.CollectorID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = collectorService.UpdateLoggedInStatus(collectorStatus);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdateLoggedInStatus"));
            }
        }

        [HttpPost]
        [Route("UpdatePatientDetails")]
        public ApiResponse<StatusResponse> UpdatePatientDetails(TestPatientUpdateRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in updating patient details: {0}", request.FirstName);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = collectorService.UpdatePatientDetails(request);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdateLoggedInStatus"));
            }
        }

        [HttpPost]
        [Route("UploadAadharPhoto")]
        public ApiResponse<StatusResponse> UploadAadharPhoto()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Current.Request.Params.Get("OrderID"))) throw new HttpRequestValidationException("Order ID is required");
                if (string.IsNullOrEmpty(HttpContext.Current.Request.Params.Get("PatientID"))) throw new HttpRequestValidationException("Patient ID is required");

                if (!TypeConverter.TryChangeType(HttpContext.Current.Request.Params.Get("OrderID"), out int orderId) || orderId == 0) throw new HttpRequestValidationException("Invalid order ID");
                if (!TypeConverter.TryChangeType(HttpContext.Current.Request.Params.Get("PatientID"), out Guid patientId) || patientId == default(Guid)) throw new HttpRequestValidationException("Invalid patient ID");

                var files = HttpContext.Current.Request.Files;
                if (files == null || files.Count == 0) throw new HttpRequestValidationException("Aadhaar photo is required");
                var file = files[0];
                if (!AllowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower())) throw new HttpRequestValidationException("Invalid file format");
                if (file.ContentLength == 0) throw new HttpRequestValidationException($"Uploaded file in invalid");
                if (file.ContentLength > AppSettings.AllowedFileSize) throw new HttpRequestValidationException($"Uploaded file exceeds maximum allowed size of {AppSettings.AllowedFileSize / (1024 * 1024)}MB");

                var uploadDir = Path.Combine(HostingEnvironment.MapPath("~/Upload/"), DateTime.Today.ToString("yyyyMMdd"));
                var filePath = Path.Combine(uploadDir, "Test_" + orderId + "_" + patientId + Path.GetExtension(file.FileName));
                if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);
                StatusResponse response;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    response = new StatusResponse(true);
                }
                else
                {
                    response = collectorService.UpdatePatientAadharPhoto(orderId, patientId, filePath);
                }
                if (response.IsSuccessful)
                {
                    file.SaveAs(filePath);
                }
                return new ApiResponse<StatusResponse>(response);
            }

            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UploadAadharPhoto"));
            }
        }

        //[HttpPost]
        //[Route("UpdatePaymentStatus")]
        //public ApiResponse<StatusResponse> UpdatePaymentStatus(TestPaymentUpdateRequest request)
        //{
        //    try
        //    {
        //        var validationErrors = request.GetValidationErrors();
        //        if (validationErrors.Any())
        //        {
        //            logger.WarnFormat("Validation fails in updating payment details: {0}", request.OrderID);
        //            return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
        //        }
        //        var response = collectorService.UpdatePaymentStatus(request);
        //        return new ApiResponse<StatusResponse>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdatePaymentStatus"));
        //    }
        //}

        //[HttpPost]
        //[Route("UpdateTestGivenStatus")]
        //public ApiResponse<StatusResponse> UpdateTestGivenStatus(int orderID)
        //{
        //    try
        //    {
        //        if (orderID == 0)
        //        {
        //            logger.WarnFormat("Validation fails in updating Test given status: {0}", orderID);
        //            return new ApiResponse<StatusResponse>(new List<Error>
        //            {
        //                new Error
        //                {
        //                    Code = "VALIDATION",
        //                    Description="Order ID is required",
        //                    Message = "Order ID is required",
        //                    Module = Module,
        //                    Operation = "UpdateTestGivenStatus",
        //                    Type = ErrorType.Validation
        //                }
        //            });
        //        }
        //        var response = collectorService.UpdateTestGivenStatus(orderID);
        //        return new ApiResponse<StatusResponse>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdateTestGivenStatus"));
        //    }
        //}
    }
}
