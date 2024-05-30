namespace RtpcrCustomerApp.Api.Controllers.Vaccination
{
    using log4net;
    using RtpcrCustomerApp.Api.Filters;
    using RtpcrCustomerApp.BusinessModels.Common;
    using RtpcrCustomerApp.BusinessModels.DTO;
    using RtpcrCustomerApp.BusinessModels.DTO.Request.Admin;
    using RtpcrCustomerApp.BusinessModels.DTO.Request.Common;
    using RtpcrCustomerApp.BusinessModels.DTO.Request.Vaccination;
    using RtpcrCustomerApp.BusinessModels.DTO.Response;
    using RtpcrCustomerApp.BusinessModels.DTO.Response.Common;
    using RtpcrCustomerApp.BusinessModels.DTO.Response.Vaccination;
    using RtpcrCustomerApp.BusinessServices.Interfaces;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Logging;
    using RtpcrCustomerApp.Common.Models;
    using RtpcrCustomerApp.Common.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Http;

    [Module(Modules.Vaccination)]
    [RoutePrefix("Vaccination/Vaccinator")]
    public class VaccinatorController : ApiController
    {
        private readonly IVaccinatorService vaccinatorService;
        private const string Module = Modules.Vaccination;
        private static readonly HashSet<string> AllowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".gif", ".png", ".bmp", ".pdf", ".mp4", ".mp3" };
        private ILog logger;

        public VaccinatorController(IVaccinatorService vaccinatorService, ILoggerFactory loggerFactory)
        {
            this.vaccinatorService = vaccinatorService;
            this.logger = loggerFactory.GetLogger<VaccinatorController>();
        }

        [HttpPost]
        [Route("Login")]
        [NonAuthorize]
        public ApiResponse<VaccinatorSignInResponse> Login(GenericSignInRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in login request: {0}", request.Phone);
                    return new ApiResponse<VaccinatorSignInResponse>(validationErrors.ToErrors());
                }
                var response = vaccinatorService.Login(request);
                return new ApiResponse<VaccinatorSignInResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<VaccinatorSignInResponse>(ex.GetErrors(Module, "Login"));
            }
        }

        [HttpGet]
        [Route("GetCurrentLocation")]
        [NonAuthorize]
        public ApiResponse<VaccinatorLocationResponse> GetCurrentLocation(Guid userId)
        {
            try
            {
                var response = vaccinatorService.GetCurrentLocation(userId);
                return new ApiResponse<VaccinatorLocationResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<VaccinatorLocationResponse>(ex.GetErrors(Module, "GetCurrentLocation"));
            }
        }

        [HttpGet]
        [Route("GetCurrentLocationTrack")]
        public ApiResponse<VaccinatorLocationResponse> GetCurrentLocationTrack(Guid userId, int orderID)
        {
            try
            {
                var response = vaccinatorService.GetCurrentLocationTrack(userId, orderID);
                return new ApiResponse<VaccinatorLocationResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<VaccinatorLocationResponse>(ex.GetErrors(Module, "GetCurrentLocationTrack "));
            }
        }

        [HttpGet]
        [Route("GetVaccinators")]
        [NonAuthorize]
        public ApiResponse<ListResponse<VaccinatorDetailsResponse>> GetVaccinators(decimal latitude, decimal longitude, Guid? regionID = null, int? orderID = null, bool ignoreIfAlreadyDeclined = true)
        {
            try
            {
                var response = vaccinatorService.GetVaccinators(latitude, longitude, regionID, orderID, ignoreIfAlreadyDeclined);
                return new ApiResponse<ListResponse<VaccinatorDetailsResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<VaccinatorDetailsResponse>>(ex.GetErrors(Module, "GetVaccinators"));
            }
        }

        //[HttpPost]
        //[Route("AssignOrder")]
        //public ApiResponse<StatusResponse> AssignOrder(VaccinatorAssignOrderRequest request)
        //{
        //    try
        //    {
        //        var validationErrors = request.GetValidationErrors();
        //        if (validationErrors.Any())
        //        {
        //            logger.WarnFormat("Validation fails in assigning order: {0}", request.VaccinatorID);
        //            return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
        //        }
        //        var response = vaccinatorService.AssignOrder(request);
        //        return new ApiResponse<StatusResponse>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "AssignOrder"));
        //    }
        //}

        [HttpPost]
        [Route("AcceptOrder")]
        public ApiResponse<StatusResponse> AcceptOrder(VaccinatorAcceptOrderRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in accepting order: {0}", request.VaccinatorID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = vaccinatorService.AcceptOrder(request);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "AcceptOrder"));
            }
        }

        [HttpPost]
        [Route("DeclineOrder")]
        public ApiResponse<StatusResponse> DeclineOrder(VaccinatorDeclineOrderRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in declining order: {0}", request.VaccinatorID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = vaccinatorService.DeclineOrder(request);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "DeclineOrder"));
            }
        }

        [HttpGet]
        [Route("GetAssignedOrders")]
        public ApiResponse<ListResponse<VaccinatorAssignedOrderResponse>> GetAssignedOrders(Guid vaccinatorID, bool showOnlyOpen)
        {
            try
            {
                var response = vaccinatorService.GetAssignedOrders(vaccinatorID, showOnlyOpen);
                return new ApiResponse<ListResponse<VaccinatorAssignedOrderResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<VaccinatorAssignedOrderResponse>>(ex.GetErrors(Module, "GetAssignedOrders"));
            }
        }

        [HttpGet]
        [Route("OrderDetails")]
        public ApiResponse<ListResponse<VaccinatorOrderHistoryResponse>> GetOrderHistory(Guid vaccinatorID)
        {
            try
            {
                var response = vaccinatorService.GetOrderHistory(vaccinatorID);
                return new ApiResponse<ListResponse<VaccinatorOrderHistoryResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<VaccinatorOrderHistoryResponse>>(ex.GetErrors(Module, "GetOrderHistory"));
            }
        }

        [HttpPost]
        [Route("UpdateLocation")]
        public ApiResponse<StatusResponse> UpdateLocation(VaccinatorLocationRequest vaccinatorLocation)
        {
            try
            {
                var validationErrors = vaccinatorLocation.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in updating location: {0}", vaccinatorLocation.VaccinatorID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }

                var response = vaccinatorService.UpdateLocation(vaccinatorLocation);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdateLocation"));
            }
        }

        [HttpPost]
        [Route("UpdateLoggedInStatus")]
        public ApiResponse<StatusResponse> UpdateLoggedInStatus(VaccinatorStatusUpdateRequest vaccinatorStatus)
        {
            try
            {
                var validationErrors = vaccinatorStatus.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in updating status: {0}", vaccinatorStatus.VaccinnatorID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = vaccinatorService.UpdateLoggedInStatus(vaccinatorStatus);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdateLoggedInStatus"));
            }
        }

        [HttpPost]
        [Route("UpdatePatientDetails")]
        public ApiResponse<StatusResponse> UpdatePatientDetails(VaccinePatientUpdateRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in updating patient details: {0}", request.FirstName);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = vaccinatorService.UpdatePatientDetails(request);
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

                //var uploadDir = Path.Combine(HostingEnvironment.MapPath("~/Upload/Aadhaar/"), DateTime.Today.ToString("yyyyMMdd"));
                //var filePath = Path.Combine(uploadDir, "Aadhaar_" + orderId + "_" + patientId + Path.GetExtension(file.FileName));
                var uploadDir = HostingEnvironment.MapPath("~/Upload/Aadhaar/");
                var DBPath = Path.Combine("/Upload/Aadhaar/", orderId + "_" + patientId + Path.GetExtension(file.FileName));
                var filePath = Path.Combine(HostingEnvironment.MapPath("~/Upload/Aadhaar/"), orderId + "_" + patientId + Path.GetExtension(file.FileName));
                if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);
                StatusResponse response;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    response = new StatusResponse(true);
                }
                else
                {
                    //response = vaccinatorService.UpdatePatientAadharPhoto(orderId, patientId, filePath);
                    response = vaccinatorService.UpdatePatientAadharPhoto(orderId, patientId, DBPath);
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

        [HttpPost]
        [Route("UploadVaccineDetails")]
        public ApiResponse<StatusResponse> UploadVaccineDetails()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Current.Request.Params.Get("OrderID"))) throw new HttpRequestValidationException("Order ID is required");
                if (string.IsNullOrEmpty(HttpContext.Current.Request.Params.Get("PatientID"))) throw new HttpRequestValidationException("Patient ID is required");

                if (!TypeConverter.TryChangeType(HttpContext.Current.Request.Params.Get("OrderID"), out int orderId) || orderId == 0) throw new HttpRequestValidationException("Invalid order ID");
                if (!TypeConverter.TryChangeType(HttpContext.Current.Request.Params.Get("PatientID"), out Guid patientId) || patientId == default(Guid)) throw new HttpRequestValidationException("Invalid patient ID");

                var files = HttpContext.Current.Request.Files;
                if (files == null || files.Count == 0) throw new HttpRequestValidationException("Vaccine detail is required");
                var file = files[0];
                //if (!AllowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower())) throw new HttpRequestValidationException("Invalid file format");
                if (file.ContentLength == 0) throw new HttpRequestValidationException($"Uploaded file in invalid");
                //if (file.ContentLength > AppSettings.AllowedFileSize) throw new HttpRequestValidationException($"Uploaded file exceeds maximum allowed size of {AppSettings.AllowedFileSize / (1024 * 1024)}MB");

                //var uploadDir = Path.Combine(HostingEnvironment.MapPath("~/Upload/"), DateTime.Today.ToString("yyyyMMdd"));
                //var filePath = Path.Combine(uploadDir, "Vaccine_" + orderId + "_" + patientId + Path.GetExtension(file.FileName));
                var uploadDir = HostingEnvironment.MapPath("~/Upload/VaccineeProof/");
                var DBPath = Path.Combine("/Upload/VaccineeProof/", orderId + "_" + patientId + Path.GetExtension(file.FileName));
                var filePath = Path.Combine(HostingEnvironment.MapPath("~/Upload/VaccineeProof/"), orderId + "_" + patientId + Path.GetExtension(file.FileName));
                if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);
                StatusResponse response;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    response = new StatusResponse(true);
                }
                else
                {
                    //response = vaccinatorService.UpdatePatientVaccinePhoto(orderId, patientId, filePath);
                    response = vaccinatorService.UpdatePatientVaccinePhoto(orderId, patientId, DBPath);
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

        private bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }


        //[HttpPost]
        //[Route("UpdatePaymentStatus")]
        //public ApiResponse<StatusResponse> UpdatePaymentStatus(VaccinePaymentUpdateRequest request)
        //{
        //    try
        //    {
        //        var validationErrors = request.GetValidationErrors();
        //        if (validationErrors.Any())
        //        {
        //            logger.WarnFormat("Validation fails in updating payment details: {0}", request.OrderID);
        //            return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
        //        }
        //        var response = vaccinatorService.UpdatePaymentStatus(request);
        //        return new ApiResponse<StatusResponse>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdatePaymentStatus"));
        //    }
        //}

        //[HttpPost]
        //[Route("UpdateVaccineGivenStatus")]
        //public ApiResponse<StatusResponse> UpdateVaccineGivenStatus(int orderID)
        //{
        //    try
        //    {
        //        if (orderID == 0)
        //        {
        //            logger.WarnFormat("Validation fails in updating vaccine given status: {0}", orderID);
        //            return new ApiResponse<StatusResponse>(new List<Error> 
        //            { 
        //                new Error 
        //                {
        //                    Code = "VALIDATION", 
        //                    Description="Order ID is required",
        //                    Message = "Order ID is required",
        //                    Module = Module,
        //                    Operation = "UpdateVaccineGivenStatus",
        //                    Type = ErrorType.Validation
        //                } 
        //            });
        //        }
        //        var response = vaccinatorService.UpdateVaccineGivenStatus(orderID);
        //        return new ApiResponse<StatusResponse>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdateVaccineGivenStatus"));
        //    }
        //}

    }
}
