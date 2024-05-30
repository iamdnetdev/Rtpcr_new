namespace RtpcrCustomerApp.Repositories.Vaccination
{
    using BusinessModels.Common;
    using BusinessModels.DBO.InParams.Vaccination;
    using BusinessModels.DBO.OutParams.Common;
    using BusinessModels.DBO.OutParams.Vaccination;
    using Repositories.Common;
    using Repositories.Interfaces;
    using RtpcrCustomerApp.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class VaccinatorRepository : RepositoryBase, IVaccinatorRepository
    {
        public VaccinatorRepository(IDbSetting settings, ILoggerFactory loggerFactory) : base(settings, loggerFactory.GetLogger<VaccinatorRepository>())
        {

        }

        public VaccinatorLoginResult GetLoginInfo(string username, string deviceID, DevicePlatform devicePlatform)
        {
            var loginResult = Query<dynamic, VaccinatorLoginResult>(Queries.Vaccinator.Login, parameter: new { Phone = username, DeviceID = deviceID, DevicePlatform = (short)devicePlatform }, CommandType.StoredProcedure).FirstOrDefault();
            return loginResult;
        }

        public VaccinatorLocationResult GetCurrentLocation(Guid vaccinatorID)
        {
            return Query<VaccinatorLocationResult>(
                    Queries.Vaccinator.GetCurrentLocation,
                    CommandType.StoredProcedure,
                    new KeyValuePair<string, object>("VaccinatorID", vaccinatorID)
                ).FirstOrDefault();
        }

        public VaccinatorLocationResult GetCurrentLocationTrack(Guid userId, int orderId)
        {
            return Query<VaccinatorLocationResult>(
                    Queries.Vaccinator.GetCurrentLocationTrack,
                    CommandType.StoredProcedure,
                    new KeyValuePair<string, object>("UserId", userId),
                    new KeyValuePair<string, object>("OrderID", orderId)
                ).FirstOrDefault();
        }

        public List<VaccinatorAssignedOrderResult> GetAssignedOrders(Guid vaccinatorID, bool showOnlyOpen)
        {
            return Query<dynamic, VaccinatorAssignedOrderResult>(
                    Queries.Vaccinator.GetAssignedOrders,
                    new { VaccinatorID = vaccinatorID, ShowOnlyOpen = showOnlyOpen }
                ).ToList();
        }

        public List<VaccinatorOrderHistoryResult> GetOrderHistory(Guid vaccinatorID)
        {
            return Query<dynamic, VaccinatorOrderHistoryResult>(
                    Queries.Vaccinator.GeOrderHistory,
                    new { VaccinatorID = vaccinatorID}
                ).ToList();
        }

        public List<VaccinatorDetailsResult> GetVaccinators(decimal latitude, decimal longitude, Guid? regionID = null, int? orderID = null, bool ignoreIfAlreadyDeclined = true)
        {
            return Query<VaccinatorDetailsResult>(
                    Queries.Vaccinator.GetVaccinators,
                    CommandType.StoredProcedure,
                    new KeyValuePair<string, object>("Latitude", latitude),
                    new KeyValuePair<string, object>("Longitude", longitude),
                    new KeyValuePair<string, object>("RegionID", regionID),
                    new KeyValuePair<string, object>("OrderID", orderID),
                    new KeyValuePair<string, object>("IgnoreIfAlreadyDeclined", ignoreIfAlreadyDeclined)
                ).ToList();
        }

        public void PurgeLocationTrail(int ageOfTrailInDays)
        {
            ExecuteCommand(Queries.Vaccinator.PurgeLocationTrail, new { AgeOfTrailInDays = ageOfTrailInDays });
        }

        public void UpdateLocation(VaccinatorLocationUpdate vaccinatorLocation)
        {
            ExecuteCommand(Queries.Vaccinator.UpdateLocation, vaccinatorLocation);
        }

        public void UpdateLoggedInStatus(Guid vaccinatorID, bool isLoggedIn)
        {
            ExecuteCommand(Queries.Vaccinator.UpdateLoggedInStatus, new { VaccinatorID = vaccinatorID, IsLoggedIn = isLoggedIn });
        }

        public void AssignOrder(VaccinatorOrderAssign vaccinatorUpdate)
        {
            ExecuteCommand(Queries.Vaccinator.AssignOrder, vaccinatorUpdate);
        }

        public void AcceptOrder(VaccinatorOrderAccept vaccinatorUpdate)
        {
            ExecuteCommand(Queries.Vaccinator.AcceptOrder, vaccinatorUpdate);
        }

        public void DeclineOrder(VaccinatorOrderDecline vaccinatorUpdate)
        {
            ExecuteCommand(Queries.Vaccinator.DeclineOrder, vaccinatorUpdate);
        }

        public void UpdatePatientDetails(VaccineOrderPatientUpdate patientDetails)
        {
            ExecuteCommand(Queries.Vaccinator.UpdatePatientDetails, patientDetails);
        }

        public void UpdatePaymentDetails(VaccineOrderPaymentUpdate paymentDetails)
        {
            ExecuteCommand(Queries.Vaccinator.UpdatePaymentDetails, paymentDetails);
        }

        public void UpdatePatientAadharPhoto(int orderID, Guid patientID, string adhaarPhoto)
        {
            ExecuteCommand(Queries.Vaccinator.UpdatePatientAadharPhoto, new { OrderID= orderID, PatientID = patientID, AdhaarPhoto = adhaarPhoto } );
        }

        public void UpdatePatientVaccinePhoto(int orderID, Guid patientID, string vaccinePhoto)
        {
            ExecuteCommand(Queries.Vaccinator.UpdatePatientVaccinePhoto, new { OrderID = orderID, PatientID = patientID, VaccinePhoto = vaccinePhoto });
        }

        public void UpdateVaccineGivenStatus(int orderID)
        {
            ExecuteCommand(Queries.Vaccinator.UpdateVaccineGivenStatus, new { OrderID = orderID });
        }
        
    }
}