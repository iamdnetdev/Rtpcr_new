using RtpcrCustomerApp.BusinessModels.DTO.Request.Common;
using RtpcrCustomerApp.BusinessModels.DTO.Request.Test;
using RtpcrCustomerApp.BusinessModels.DTO.Response;
using RtpcrCustomerApp.BusinessModels.DTO.Response.Test;
using System;

namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    public interface ICollectorService
    {
        CollectorSignInResponse Login(GenericSignInRequest request);

        ListResponse<CollectorDetailsResponse> GetCollectors(decimal latitude, decimal longitude, Guid? regionID = null, int? orderID = null, bool ignoreIfAlreadyDeclined = true);

        StatusResponse UpdateLocation(CollectorLocationRequest collectorLocation);

        CollectorLocationResponse GetCurrentLocation(Guid collectorID);

        CollectorLocationResponse GetCurrentLocationTrack(Guid userID, int orderID);

        StatusResponse UpdateLoggedInStatus(CollectorStatusUpdateRequest collectorStatus);

        ListResponse<CollectorAssignedOrderResponse> GetAssignedOrders(Guid collectorID, bool showOnlyOpen);

        ListResponse<CollectorOrderHistoryResponse> GetOrderHistory(Guid collectorID);

        StatusResponse UpdatePaymentStatus(TestPaymentUpdateRequest request);

        StatusResponse UpdatePatientDetails(TestPatientUpdateRequest request);

        StatusResponse UpdateTestGivenStatus(int orderID);
        StatusResponse AcceptOrder(CollectorAcceptOrderRequest request);
        StatusResponse DeclineOrder(CollectorDeclineOrderRequest request);
        StatusResponse UpdatePatientAadharPhoto(int orderID, Guid patientID, string adhaarPhoto);
        ListResponse<TestOrderByRegionResponse> GetOrdersByRegion(Guid regionID, Guid? collectorID, bool? showOnlyUnassigned);
    }
}
