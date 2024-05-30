namespace RtpcrCustomerApp.BusinessServices.Vaccination
{
    using BusinessModels.DBO.InParams.Vaccination;
    using BusinessModels.DBO.OutParams.Vaccination;
    using BusinessModels.DTO.Request.Common;
    using BusinessModels.DTO.Request.Vaccination;
    using BusinessModels.DTO.Response;
    using BusinessModels.DTO.Response.Vaccination;
    using Interfaces;
    using log4net;
    using Repositories.Interfaces;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VaccinatorService : IVaccinatorService
    {
        private readonly IVaccinatorRepository repository;
        private readonly IAccountRepository accountRepository;
        private readonly ITokenManager tokenManager;
        private readonly ILog logger;
        private readonly IMapper mapper;
        private readonly IPaymentService paymentService;

        public VaccinatorService(IVaccinatorRepository vaccinatorRepository, IAccountRepository accountRepository, ITokenManager tokenManager, IPaymentService paymentService, ILoggerFactory loggerFactory, IMapper mapper)
        {
            repository = vaccinatorRepository;
            this.accountRepository = accountRepository;
            this.tokenManager = tokenManager;
            this.paymentService = paymentService;
            logger = loggerFactory.GetLogger<VaccinatorService>();
            this.mapper = mapper;
        }

        public VaccinatorSignInResponse Login(GenericSignInRequest request)
        {
            try
            {
                var response = new VaccinatorSignInResponse();
                var hash = accountRepository.GetHash(request.Phone);
                var hashPwd = EncryptionUtil.GetHash(request.Password);
                if (EncryptionUtil.VerifyHash(request.Password, hash))
                {
                    var loginResult = repository.GetLoginInfo(request.Phone, request.DeviceID, request.DevicePlatform);
                    response = mapper.Map<VaccinatorLoginResult, VaccinatorSignInResponse>(loginResult);
                    response.Token = tokenManager.GenerateToken(loginResult.UserID.ToString(), loginResult.Role.ToString(), request.Phone, loginResult.TokenExpirationDuration);
                }
                else
                {
                    response.Message = "Login failed";
                    response.IsAuthenticated = false;
                }
                return response;
            }
            catch (Exception ex)
            {
                logger.Error("Error in login: ", ex);
                throw ex;
            }
        }

        public VaccinatorLocationResponse GetCurrentLocation(Guid vaccinatorID)
        {
            try
            {
                var result = repository.GetCurrentLocation(vaccinatorID) ??
                    new VaccinatorLocationResult { VaccinatorID = vaccinatorID, IsLoggedIn = false };
                return mapper.Map<VaccinatorLocationResult, VaccinatorLocationResponse>(result);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetCurrentLocation: ", ex);
                throw ex;
            }
        }

        public VaccinatorLocationResponse GetCurrentLocationTrack(Guid userId, int orderId)
        {
            try
            {
                var result = repository.GetCurrentLocationTrack(userId, orderId);
                return mapper.Map<VaccinatorLocationResult, VaccinatorLocationResponse>(result);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetCurrentLocation: ", ex);
                throw ex;
            }
        }

        public ListResponse<VaccinatorAssignedOrderResponse> GetAssignedOrders(Guid vaccinatorID, bool showOnlyOpen)
        {
            try
            {
                var result = repository.GetAssignedOrders(vaccinatorID, showOnlyOpen);
                var grp = result.GroupBy(r => r.OrderId).ToList();
                var response = new ListResponse<VaccinatorAssignedOrderResponse>();
                grp.ForEach(r =>
                {
                    var responseObj = mapper.Map<VaccinatorAssignedOrderResult, VaccinatorAssignedOrderResponse>(r.First());
                    responseObj.AssignedOrderDetails = r.Select(dtl =>
                        mapper.Map<VaccinatorAssignedOrderResult, VaccinatorAssignedOrderDetails>(dtl)).ToList();
                    response.Add(responseObj);
                });
                return response;
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetAssignedOrders: ", ex);
                throw ex;
            }
        }

        public ListResponse<VaccinatorOrderHistoryResponse> GetOrderHistory(Guid vaccinatorID)
        {
            try
            {
                var result = repository.GetOrderHistory(vaccinatorID);
                var grp = result.GroupBy(r => r.OrderId).ToList();
                var response = new ListResponse<VaccinatorOrderHistoryResponse>();
                grp.ForEach(r =>
                {
                    var responseObj = mapper.Map<VaccinatorOrderHistoryResult, VaccinatorOrderHistoryResponse>(r.First());
                    responseObj.PatientDetails = r.Select(dtl =>
                        mapper.Map<VaccinatorOrderHistoryResult, VaccinePatientDetails>(dtl)).ToList();
                    response.Add(responseObj);
                });
                return response;
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetOrderHistory: ", ex);
                throw ex;
            }
        }

        public ListResponse<VaccinatorDetailsResponse> GetVaccinators(decimal latitude, decimal longitude, Guid? regionID = null, int? orderID = null, bool ignoreIfAlreadyDeclined = true)
        {
            try
            {
                var result = repository.GetVaccinators(latitude, longitude, regionID, orderID, ignoreIfAlreadyDeclined);
                return mapper.Map<List<VaccinatorDetailsResult>, ListResponse<VaccinatorDetailsResponse>>(result);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetVaccinators: ", ex);
                throw ex;
            }
        }

        public StatusResponse AssignOrder(VaccinatorAssignOrderRequest request)
        {
            try
            {
                var param = mapper.Map<VaccinatorAssignOrderRequest, VaccinatorOrderAssign>(request);
                repository.AssignOrder(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in AssignOrder: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdatePatientAadharPhoto(int orderID, Guid patientID, string adhaarPhoto)
        {
            try
            {               
                repository.UpdatePatientAadharPhoto(orderID,patientID, adhaarPhoto);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdatePatientAadharPhoto: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdatePatientVaccinePhoto(int orderID, Guid patientID, string vaccinePhoto)
        {
            try
            {
                repository.UpdatePatientVaccinePhoto(orderID, patientID, vaccinePhoto);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdatePatientVaccinePhoto: ", ex);
                throw ex;
            }
        }

        public StatusResponse AcceptOrder(VaccinatorAcceptOrderRequest request)
        {
            try
            {
                var param = mapper.Map<VaccinatorAcceptOrderRequest, VaccinatorOrderAccept>(request);
                repository.AcceptOrder(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in AcceptOrder: ", ex);
                throw ex;
            }
        }

        public StatusResponse DeclineOrder(VaccinatorDeclineOrderRequest request)
        {
            try
            {
                var param = mapper.Map<VaccinatorDeclineOrderRequest, VaccinatorOrderDecline>(request);
                repository.DeclineOrder(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in DeclineOrder: ", ex);
                throw ex;
            }
        }

        public StatusResponse PurgeLocationTrail(int ageOfTrailInDays)
        {
            try
            {
                repository.PurgeLocationTrail(ageOfTrailInDays);
                return new StatusResponse(true);
            }

            catch (Exception ex)
            {
                logger.Error("Error in PurgeLocationTrail: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdateLocation(VaccinatorLocationRequest vaccinatorLocation)
        {
            try
            {
                var location = mapper.Map<VaccinatorLocationRequest, VaccinatorLocationUpdate>(vaccinatorLocation);
                repository.UpdateLocation(location);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdateLocation: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdateLoggedInStatus(VaccinatorStatusUpdateRequest vaccinatorStatus)
        {
            try
            {
                repository.UpdateLoggedInStatus(vaccinatorStatus.VaccinnatorID.Value, vaccinatorStatus.IsLoggedIn.Value);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdateLoggedInStatus: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdatePatientDetails(VaccinePatientUpdateRequest request)
        {
            try
            {
                var param = mapper.Map<VaccinePatientUpdateRequest, VaccineOrderPatientUpdate>(request);
                repository.UpdatePatientDetails(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdatePatientDetails: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdatePaymentStatus(VaccinePaymentUpdateRequest request)
        {
            try
            {
                var param = mapper.Map<VaccinePaymentUpdateRequest, VaccineOrderPaymentUpdate>(request);
                //var razorPayment = mapper.Map<VaccinePaymentUpdateRequest, RazorPayment>(request);
                //var result = paymentService.MakePayment(razorPayment);
                //param.PaymentSucceeded = result.IsSuccessful;
                //param.PaymentError = result.Error;
                repository.UpdatePaymentDetails(param);
                //return result;
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdatePaymentStatus: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdateVaccineGivenStatus(int orderID)
        {
            try
            {
                repository.UpdateVaccineGivenStatus(orderID);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdateVaccineGivenStatus: ", ex);
                throw ex;
            }
        }

    }
}
