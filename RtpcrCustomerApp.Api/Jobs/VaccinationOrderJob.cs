namespace RtpcrCustomerApp.Api.Jobs
{
    using BusinessServices.Admin;
    using BusinessServices.Interfaces;
    using log4net;
    using Repositories.Admin;
    using RtpcrCustomerApp.BusinessServices.Common;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Utils;
    using RtpcrCustomerApp.Repositories.Common;
    using System;

    public class VaccinationOrderJob : IHangfireJob
    {
        private static readonly IAdminService service;
        private static readonly ILog logger;
        static VaccinationOrderJob()
        {
            var loggerFactory = InstanceFactory.GetInstance<ILoggerFactory>();
            var dbSettings = InstanceFactory.GetInstance<IDbSetting>();
            var mapper = InstanceFactory.GetInstance<IMapper>();
            var deviceDetailsRepo = new DeviceDetailsRepository(dbSettings, loggerFactory);
            var pushNotificationService = new PushNotificationService(loggerFactory, deviceDetailsRepo);
            var repo = new AdminRepository(dbSettings, loggerFactory);
            service = new AdminService(repo, loggerFactory, mapper, pushNotificationService);
            logger = loggerFactory.GetLogger<VaccinationOrderJob>();
        }

        public VaccinationOrderJob() { }

        public void Execute()
        {
            try
            {
                logger.Info("VaccinationOrderJob Execute");
                service.AssignVaccinatorForScheduledOrders();
            }
            catch(Exception ex)
            {
                logger.Error("Error in AssignVaccinatorForScheduledOrders", ex);
            }
        }
    }
}