namespace RtpcrCustomerApp.BusinessServices.Admin
{
    using BusinessModels.DBO.InParams.Admin;
    using BusinessModels.DBO.OutParams.Admin;
    using BusinessModels.DTO.Request.Admin;
    using BusinessModels.DTO.Response;
    using BusinessModels.DTO.Response.Admin;
    using Interfaces;
    using log4net;
    using Repositories.Interfaces;
    using RtpcrCustomerApp.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminService : IAdminService
    {
        private readonly IAdminRepository repository;
        private readonly ILog logger;
        private readonly IMapper mapper;
        private readonly IPushNotificationService pushNotificationService;
        public AdminService(IAdminRepository adminRepository, ILoggerFactory loggerFactory, IMapper mapper, IPushNotificationService pushNotificationService)
        {
            repository = adminRepository;
            logger = loggerFactory.GetLogger<AdminService>();
            this.mapper = mapper;
            this.pushNotificationService = pushNotificationService;
        }

        public StatusResponse AssignVaccinator(AssignVaccinatorRequest request)
        {
            try
            {
                var param = mapper.Map<AssignVaccinatorRequest, VaccineOrderVaccinatorUpdate>(request);
                repository.AssignVaccinator(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in AssignVaccinator: ", ex);
                throw ex;
            }
        }

        public ListResponse<VaccineOrderAdminResponse> GetOrdersByRegion(Guid regionID, bool showOnlyUnassigned, Guid? vaccinatorID)
        {
            try
            {
                var result = repository.GetOrdersByRegion(regionID, showOnlyUnassigned, vaccinatorID);
                return mapper.Map<List<VaccineOrderAdminResult>, ListResponse<VaccineOrderAdminResponse>>(result);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetOrdersByRegionVaccinator: ", ex);
                throw ex;
            }
        }

        public StatusResponse AssignVaccinatorForScheduledOrders()
        {
            logger.Info("AssignVaccinatorForScheduledOrders");
            try
            {
                var scheduledOrderDetails = repository.AssignVaccinatorForScheduledOrders();

                scheduledOrderDetails.ForEach(o =>
                {
                    pushNotificationService.SendNotification($"Vaccinator - {o.VaccinatorName} has been assigned to your order #{o.OrderID}", "", o.UserID);
                    pushNotificationService.SendNotification($"Order #{o.OrderID} has been assigned to you", "", o.VaccinatorID);
                });
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in AssignVaccinatorForScheduledOrders: ", ex);
                throw ex;
            }
        }

        public StatusResponse AssignCollector(AssignCollectorRequest request)
        {
            try
            {
                var param = mapper.Map<AssignCollectorRequest, TestOrderCollectorUpdate>(request);
                repository.AssignCollector(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in AssignCollector: ", ex);
                throw ex;
            }
        }

        public ListResponse<TestOrderAdminResponse> GetOrdersByRegionCollector(Guid regionID, bool showOnlyUnassigned, Guid? CollectorID)
        {
            try
            {
                var result = repository.GetOrdersByRegionCollector(regionID, showOnlyUnassigned, CollectorID);
                return mapper.Map<List<TestOrderAdminResult>, ListResponse<TestOrderAdminResponse>>(result);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetOrdersByRegionCollector: ", ex);
                throw ex;
            }
        }

        public StatusResponse AssignCollectorForScheduledOrders()
        {
            try
            {
                var scheduledOrderDetails = repository.AssignCollectorForScheduledOrders();
                scheduledOrderDetails.ForEach(o =>
                {
                    pushNotificationService.SendNotification($"Collector - {o.CollectorName} has been assigned to your order #{o.OrderID}", "", o.UserID);
                    pushNotificationService.SendNotification($"Order #{o.OrderID} has been assigned to you", "", o.CollectorID);
                });
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in AssignCollectorForScheduledOrders: ", ex);
                throw ex;
            }
        }
    }
}
