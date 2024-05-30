namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    using BusinessModels.DTO.Request.Common;
    using BusinessModels.DTO.Request.Vaccination;
    using BusinessModels.DTO.Response;
    using BusinessModels.DTO.Response.Vaccination;
    using System;

    public interface IVaccinatorService
    {
        VaccinatorSignInResponse Login(GenericSignInRequest request);

        ListResponse<VaccinatorDetailsResponse> GetVaccinators(decimal latitude, decimal longitude, Guid? regionID = null, int? orderID = null, bool ignoreIfAlreadyDeclined = true);
        
        StatusResponse UpdateLocation(VaccinatorLocationRequest vaccinatorLocation);

        StatusResponse PurgeLocationTrail(int ageOfTrailInDays);

        VaccinatorLocationResponse GetCurrentLocation(Guid vaccinatorID);

        VaccinatorLocationResponse GetCurrentLocationTrack(Guid userID, int orderID);
        
        StatusResponse UpdateLoggedInStatus(VaccinatorStatusUpdateRequest vaccinatorStatus);

        ListResponse<VaccinatorAssignedOrderResponse> GetAssignedOrders(Guid vaccinatorID, bool showOnlyOpen);

        ListResponse<VaccinatorOrderHistoryResponse> GetOrderHistory(Guid vaccinatorID);

        StatusResponse UpdatePaymentStatus(VaccinePaymentUpdateRequest request);

        StatusResponse UpdatePatientDetails(VaccinePatientUpdateRequest request);

        StatusResponse AssignOrder(VaccinatorAssignOrderRequest request);
        
        StatusResponse AcceptOrder(VaccinatorAcceptOrderRequest request);
        
        StatusResponse DeclineOrder(VaccinatorDeclineOrderRequest request);

        StatusResponse UpdateVaccineGivenStatus(int orderID);

        StatusResponse UpdatePatientAadharPhoto(int orderID, Guid patientID, string adhaarPhoto);

        StatusResponse UpdatePatientVaccinePhoto(int orderID, Guid patientID, string vaccinePhoto);
    }
}
