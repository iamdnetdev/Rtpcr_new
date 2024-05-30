namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    using BusinessModels.DTO.Request.Admin;
    using BusinessModels.DTO.Response;
    using BusinessModels.DTO.Response.Admin;
    using System;

    public interface IAdminService
    {
        StatusResponse AssignVaccinator(AssignVaccinatorRequest request);
        ListResponse<VaccineOrderAdminResponse> GetOrdersByRegion(Guid regionID, bool showOnlyUnassigned, Guid? vaccinatorID);
        StatusResponse AssignVaccinatorForScheduledOrders();
        StatusResponse AssignCollector(AssignCollectorRequest request);
        ListResponse<TestOrderAdminResponse> GetOrdersByRegionCollector(Guid regionID, bool showOnlyUnassigned, Guid? CollectorID);
        StatusResponse AssignCollectorForScheduledOrders();
    }
}
