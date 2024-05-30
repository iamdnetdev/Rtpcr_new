using RtpcrCustomerApp.BusinessModels.Common;
using RtpcrCustomerApp.BusinessModels.DBO.InParams.Test;
using RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test;
using RtpcrCustomerApp.Common.Interfaces;
using RtpcrCustomerApp.Repositories.Common;
using RtpcrCustomerApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.Repositories.Test
{
    public class CollectorRepository : RepositoryBase, ICollectorRepository
    {
        public CollectorRepository(IDbSetting settings, ILoggerFactory loggerFactory) : base(settings, loggerFactory.GetLogger<CollectorRepository>())
        {

        }

        public CollectorLoginResult GetLoginInfo(string username, string deviceID, DevicePlatform devicePlatform)
        {
            var loginResult = Query<dynamic, CollectorLoginResult>(Queries.Collector.Login, parameter: new { Phone = username, DeviceID = deviceID, DevicePlatform = (short)devicePlatform }, CommandType.StoredProcedure).FirstOrDefault();
            return loginResult;
        }

        public CollectorLocationResult GetCurrentLocation(Guid collectorID)
        {
            return Query<CollectorLocationResult>(
                    Queries.Collector.GetCurrentLocation,
                    CommandType.StoredProcedure,
                    new KeyValuePair<string, object>("CollectorID", collectorID)
                ).FirstOrDefault();
        }

        public CollectorLocationResult GetCurrentLocationTrack(Guid userId, int orderId)
        {
            return Query<CollectorLocationResult>(
                    Queries.Collector.GetCurrentLocationTrack,
                    CommandType.StoredProcedure,
                    new KeyValuePair<string, object>("UserId", userId),
                    new KeyValuePair<string, object>("OrderID", orderId)
                ).FirstOrDefault();
        }

        public List<CollectorAssignedOrderResult> GetAssignedOrders(Guid collectorID, bool showOnlyOpen)
        {
            return Query<dynamic, CollectorAssignedOrderResult>(
                    Queries.Collector.GetAssignedOrders,
                    new { CollectorID = collectorID, ShowOnlyOpen = showOnlyOpen }
                ).ToList();
        }

        public List<CollectorOrderHistoryResult> GetOrderHistory(Guid collectorID)
        {
            return Query<dynamic, CollectorOrderHistoryResult>(
                    Queries.Collector.GetOrderHistory,
                    new { CollectorID = collectorID }
                ).ToList();
        }

        public List<CollectorDetailsResult> GetCollectors(decimal latitude, decimal longitude, Guid? regionID = null, int? orderID = null, bool ignoreIfAlreadyDeclined = true)
        {
            return Query<CollectorDetailsResult>(
                    Queries.Collector.GetCollectors,
                    CommandType.StoredProcedure,
                    new KeyValuePair<string, object>("Latitude", latitude),
                    new KeyValuePair<string, object>("Longitude", longitude),
                    new KeyValuePair<string, object>("RegionID", regionID),
                    new KeyValuePair<string, object>("OrderID", orderID),
                    new KeyValuePair<string, object>("IgnoreIfAlreadyDeclined", ignoreIfAlreadyDeclined)
                ).ToList();
        }

        public void UpdateLocation(CollectorLocationUpdate collectorLocation)
        {
            ExecuteCommand(Queries.Collector.UpdateLocation, collectorLocation);
        }

        public void UpdateLoggedInStatus(Guid collectorID, bool isLoggedIn)
        {
            ExecuteCommand(Queries.Collector.UpdateLoggedInStatus, 
                new { collectorID = collectorID, IsLoggedIn = isLoggedIn });
        }
        public void AcceptOrder(CollectorOrderAccept collectorUpdate)
        {
            ExecuteCommand(Queries.Collector.AcceptOrder, collectorUpdate);
        }

        public void DeclineOrder(CollectorOrderDecline collectorUpdate)
        {
            ExecuteCommand(Queries.Collector.DeclineOrder, collectorUpdate);
        }

        public void UpdatePatientDetails(TestOrderPatientUpdate patientDetails)
        {
            ExecuteCommand(Queries.Collector.UpdatePatientDetails, patientDetails);
        }

        public void UpdatePaymentDetails(TestOrderPaymentUpdate paymentDetails)
        {
            ExecuteCommand(Queries.Collector.UpdatePaymentDetails, paymentDetails);
        }

        public void UpdateTestGivenStatus(int orderID)
        {
            ExecuteCommand(Queries.Collector.UpdateTestGivenStatus, new { OrderID = orderID });
        }

        public void UpdateCollectorDetails(TestSampleCollectorUpdate testSampleCollector)
        {
            ExecuteCommand(Queries.Collector.AssignCollector, testSampleCollector);
        }

        public void UpdatePatientAadharPhoto(int orderID, Guid patientID, string adhaarPhoto)
        {
            ExecuteCommand(Queries.Collector.UpdatePatientAadharPhoto, new { OrderID = orderID, PatientID = patientID, AdhaarPhoto = adhaarPhoto });
        }

        public List<TestOrderByRegionResult> GetOrdersByRegion(Guid regionID, Guid? collectorID, bool? showOnlyUnassigned)
        {
            return Query<TestOrderByRegionResult>(
                    Queries.Collector.GetOrdersByRegion,
                    CommandType.StoredProcedure,
                    new KeyValuePair<string, object>("RegionID", regionID),
                    new KeyValuePair<string, object>("CollectorID", collectorID),
                    new KeyValuePair<string, object>("OnlyUnassigned", showOnlyUnassigned)
                ).ToList();
        }
    }
}
