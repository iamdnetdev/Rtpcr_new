using RtpcrCustomerApp.BusinessModels.Common;
using RtpcrCustomerApp.BusinessModels.DBO.InParams.Test;
using RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test;
using System;
using System.Collections.Generic;

namespace RtpcrCustomerApp.Repositories.Interfaces
{
    public interface ICollectorRepository
    {
        CollectorLoginResult GetLoginInfo(string phone, string deviceID, DevicePlatform devicePlatform);
        CollectorLocationResult GetCurrentLocation(Guid collectorID);
        CollectorLocationResult GetCurrentLocationTrack(Guid userId, int orderID);
        List<CollectorDetailsResult> GetCollectors(decimal latitude, decimal longitude, Guid? regionID, int? orderID = null, bool ignoreIfAlreadyDeclined = true);
        void UpdateLocation(CollectorLocationUpdate collectorLocation);
        void UpdateLoggedInStatus(Guid CollectorID, bool isLoggedIn);
        List<CollectorAssignedOrderResult> GetAssignedOrders(Guid CollectorID, bool showOnlyOpen);
        List<CollectorOrderHistoryResult> GetOrderHistory(Guid collectorID);
        void UpdatePatientDetails(TestOrderPatientUpdate patientDetails);
        void UpdatePaymentDetails(TestOrderPaymentUpdate paymentDetails);
        void AcceptOrder(CollectorOrderAccept collectorUpdate);
        void DeclineOrder(CollectorOrderDecline collectorUpdate);
        void UpdateTestGivenStatus(int orderID);
        void UpdateCollectorDetails(TestSampleCollectorUpdate testSampleCollector);
        void UpdatePatientAadharPhoto(int orderID, Guid patientID, string adhaarPhoto);
        List<TestOrderByRegionResult> GetOrdersByRegion(Guid regionID, Guid? collectorID, bool? showOnlyUnassigned);
    }
}
