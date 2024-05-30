namespace RtpcrCustomerApp.Repositories.Interfaces
{
    using BusinessModels.Common;
    using BusinessModels.DBO.InParams.Vaccination;
    using BusinessModels.DBO.OutParams.Common;
    using BusinessModels.DBO.OutParams.Vaccination;
    using System;
    using System.Collections.Generic;

    public interface IVaccinatorRepository
    {
        VaccinatorLoginResult GetLoginInfo(string phone, string deviceID, DevicePlatform devicePlatform);
        VaccinatorLocationResult GetCurrentLocation(Guid vaccinatorID);
        VaccinatorLocationResult GetCurrentLocationTrack(Guid userId, int orderID);
        List<VaccinatorDetailsResult> GetVaccinators(decimal latitude, decimal longitude, Guid? regionID = null, int? orderID = null, bool ignoreIfAlreadyDeclined = true);
        void PurgeLocationTrail(int ageOfTrailInDays);
        void UpdateLocation(VaccinatorLocationUpdate vaccinatorLocation);
        void UpdateLoggedInStatus(Guid vaccinatorID, bool isLoggedIn);
        List<VaccinatorAssignedOrderResult> GetAssignedOrders(Guid vaccinatorID, bool showOnlyOpen);
        List<VaccinatorOrderHistoryResult> GetOrderHistory(Guid vaccinatorID);
        void UpdatePatientDetails(VaccineOrderPatientUpdate patientDetails);
        void UpdatePaymentDetails(VaccineOrderPaymentUpdate paymentDetails);
        void UpdatePatientAadharPhoto(int orderID, Guid patientID, string adhaarPhoto);
        void UpdatePatientVaccinePhoto(int orderID, Guid patientID, string vaccinePhoto);
        void AssignOrder(VaccinatorOrderAssign vaccinatorUpdate);
        void AcceptOrder(VaccinatorOrderAccept vaccinatorUpdate);
        void DeclineOrder(VaccinatorOrderDecline vaccinatorUpdate);
        void UpdateVaccineGivenStatus(int orderID);
    }
}
