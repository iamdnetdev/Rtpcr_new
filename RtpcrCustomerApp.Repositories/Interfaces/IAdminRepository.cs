namespace RtpcrCustomerApp.Repositories.Interfaces
{
    using BusinessModels.DBO.InParams.Admin;
    using BusinessModels.DBO.OutParams.Admin;
    using BusinessModels.DBO.OutParams.Test;
    using BusinessModels.DBO.OutParams.Vaccination;
    using System;
    using System.Collections.Generic;

    public interface IAdminRepository
    {
        void AssignVaccinator(VaccineOrderVaccinatorUpdate vaccinatorUpdate);
        List<VaccineOrderAdminResult> GetOrdersByRegion(Guid regionID, bool showOnlyUnassigned, Guid? vaccinatorID);
        List<VaccinatorScheduledResult> AssignVaccinatorForScheduledOrders();
        void AssignCollector(TestOrderCollectorUpdate collectorUpdate);
        List<TestOrderAdminResult> GetOrdersByRegionCollector(Guid regionID, bool showOnlyUnassigned, Guid? collectorID);
        List<CollectorScheduledResult> AssignCollectorForScheduledOrders();
    }
}
