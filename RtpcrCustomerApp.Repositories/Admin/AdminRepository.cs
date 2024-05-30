namespace RtpcrCustomerApp.Repositories.Admin
{
    using BusinessModels.DBO.InParams.Admin;
    using BusinessModels.DBO.OutParams.Admin;
    using BusinessModels.DBO.OutParams.Test;
    using BusinessModels.DBO.OutParams.Vaccination;
    using Interfaces;
    using Repositories.Common;
    using RtpcrCustomerApp.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class AdminRepository : RepositoryBase, IAdminRepository
    {
        public AdminRepository(IDbSetting settings, ILoggerFactory loggerFactory) : base(settings, loggerFactory.GetLogger<AdminRepository>())
        {

        }

        public void AssignVaccinator(VaccineOrderVaccinatorUpdate vaccinatorUpdate)
        {
            ExecuteCommand(Queries.Admin.AssignVaccinator, vaccinatorUpdate);
        }

        public List<VaccineOrderAdminResult> GetOrdersByRegion(Guid regionID, bool showOnlyUnassigned, Guid? vaccinatorID)
        {
            return Query<dynamic, VaccineOrderAdminResult>(
                Queries.Admin.GetOrdersByRegion, new
                {
                    RegionID = regionID,
                    OnlyUnassigned = showOnlyUnassigned,
                    VaccinatorID = vaccinatorID
                }
            ).ToList();
        }

        public List<VaccinatorScheduledResult> AssignVaccinatorForScheduledOrders()
        {
            return Query<VaccinatorScheduledResult>(Queries.Vaccinator.AssignVaccinatorForScheduledOrders, CommandType.StoredProcedure).ToList();
        }

        public void AssignCollector(TestOrderCollectorUpdate collectorUpdate)
        {
            ExecuteCommand(Queries.Admin.AssignCollector, collectorUpdate);
        }

        public List<TestOrderAdminResult> GetOrdersByRegionCollector(Guid regionID, bool showOnlyUnassigned, Guid? collectorID)
        {
            return Query<dynamic, TestOrderAdminResult>(
                Queries.Admin.GetCollectorOrdersByRegion, new
                {
                    RegionID = regionID,
                    OnlyUnassigned = showOnlyUnassigned,
                    CollectorID = collectorID
                }
            ).ToList();
        }

        public List<CollectorScheduledResult> AssignCollectorForScheduledOrders()
        {
            return Query<CollectorScheduledResult>(Queries.Collector.AssignCollectorForScheduledOrders, CommandType.StoredProcedure).ToList();
        }
    }
}
