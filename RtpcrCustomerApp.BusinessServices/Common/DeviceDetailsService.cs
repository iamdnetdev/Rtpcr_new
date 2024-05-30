namespace RtpcrCustomerApp.BusinessServices.Common
{
    using log4net;
    using BusinessModels.DTO.Response.Common;
    using RtpcrCustomerApp.Common.Interfaces;
    using Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using RtpcrCustomerApp.BusinessModels.DBO.OutParams.Common;
    using RtpcrCustomerApp.BusinessServices.Interfaces;

    public class DeviceDetailsService : IDeviceDetailsService
    {
        private readonly ILog logger;
        private readonly IDeviceDetailsRepository repository;
        private readonly IMapper mapper;
        public DeviceDetailsService(ILoggerFactory loggerFactory, IDeviceDetailsRepository deviceDetailsRepository, IMapper mapper)
        {
            logger = loggerFactory.GetLogger<DeviceDetailsService>();
            this.repository = deviceDetailsRepository;
            this.mapper = mapper;
        }

        public List<DeviceDetailsResponse> GetDevices(Guid userID)
        {
            try
            {
                var account = repository.GetDeviceDetails(userID);
                return mapper.Map<List<DeviceDetailsResult>, List<DeviceDetailsResponse>>(account);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetAccount: ", ex);
                throw ex;
            }
        }
    }
}
