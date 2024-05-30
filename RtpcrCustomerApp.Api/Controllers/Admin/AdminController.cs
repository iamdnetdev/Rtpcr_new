using log4net;
using RtpcrCustomerApp.BusinessModels.Common;
using RtpcrCustomerApp.BusinessModels.DTO.Request.Admin;
using RtpcrCustomerApp.BusinessModels.DTO.Request.Vaccination;
using RtpcrCustomerApp.BusinessModels.DTO.Response;
using RtpcrCustomerApp.BusinessModels.DTO.Response.Admin;
using RtpcrCustomerApp.BusinessModels.DTO.Response.Vaccination;
using RtpcrCustomerApp.BusinessServices.Interfaces;
using RtpcrCustomerApp.Common.Interfaces;
using RtpcrCustomerApp.Common.Logging;
using RtpcrCustomerApp.Common.Utils;
using System;
using System.Linq;
using System.Web.Http;

namespace RtpcrCustomerApp.Api.Controllers.Admin
{
    [Module(Modules.Admin)]
    public class AdminController : ApiController
    {
        private readonly IAdminService adminService;
        private readonly ILog logger;
        private const string Module = Modules.Admin;

        public AdminController(IAdminService adminService, IVaccinatorService vaccinatorService, ILoggerFactory loggerFactory)
        {
            this.adminService = adminService;
            logger = loggerFactory.GetLogger<AdminController>();
        }

        [HttpGet]
        [Route("GetOrdersByRegionVaccinator")]
        public ApiResponse<ListResponse<VaccineOrderAdminResponse>> GetOrdersByRegionVaccinator(Guid regionID, bool showOnlyUnassigned = false, Guid? vaccinatorID = null)
        {
            try
            {
                var response = adminService.GetOrdersByRegion(regionID, showOnlyUnassigned, vaccinatorID);
                return new ApiResponse<ListResponse<VaccineOrderAdminResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<VaccineOrderAdminResponse>>(ex.GetErrors(Module, "GetOrdersByRegionVaccinator"));
            }
        }

        [HttpPost]
        [Route("AssignVaccinator")]
        public ApiResponse<StatusResponse> AssignVaccinator(AssignVaccinatorRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in assigning vaccinator: {0}", request.VaccinatorID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = adminService.AssignVaccinator(request);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "AssignVaccinator"));
            }
        }

        [HttpGet]
        [Route("GetOrdersByRegionCollector")]
        public ApiResponse<ListResponse<TestOrderAdminResponse>> GetOrdersByRegionCollector(Guid regionID, bool showOnlyUnassigned = false, Guid? collectorID = null)
        {
            try
            {
                var response = adminService.GetOrdersByRegionCollector(regionID, showOnlyUnassigned, collectorID);
                return new ApiResponse<ListResponse<TestOrderAdminResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListResponse<TestOrderAdminResponse>>(ex.GetErrors(Module, "GetOrdersByRegionCollector"));
            }
        }

        [HttpPost]
        [Route("AssignCollector")]
        public ApiResponse<StatusResponse> AssignCollector(AssignCollectorRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in assigning Collector: {0}", request.CollectorID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = adminService.AssignCollector(request);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdateLoggedInStatus"));
            }
        }

        //public ApiResponse<StatusResponse> AssignVaccinatorForScheduledOrders()
        //{
        //    try
        //    {
        //        var response = adminService.AssignVaccinatorForScheduledOrders();
        //        return new ApiResponse<StatusResponse>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdateLoggedInStatus"));
        //    }

        //}
    }
}