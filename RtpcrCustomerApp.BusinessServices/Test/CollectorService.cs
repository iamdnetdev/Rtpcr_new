using log4net;
using RtpcrCustomerApp.BusinessModels.Common;
using RtpcrCustomerApp.BusinessModels.DBO.InParams.Test;
using RtpcrCustomerApp.BusinessModels.DBO.OutParams.Test;
using RtpcrCustomerApp.BusinessModels.DTO.Request.Admin;
using RtpcrCustomerApp.BusinessModels.DTO.Request.Common;
using RtpcrCustomerApp.BusinessModels.DTO.Request.Test;
using RtpcrCustomerApp.BusinessModels.DTO.Response;
using RtpcrCustomerApp.BusinessModels.DTO.Response.Test;
using RtpcrCustomerApp.BusinessServices.Interfaces;
using RtpcrCustomerApp.Common.Interfaces;
using RtpcrCustomerApp.Common.Utils;
using RtpcrCustomerApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessServices.Test
{
    public class CollectorService : ICollectorService
    {
        private readonly ICollectorRepository repository;
        private readonly IAccountRepository accountRepository;
        private readonly ITokenManager tokenManager;
        private readonly ILog logger;
        private readonly IMapper mapper;
        private readonly IPaymentService paymentService;

        public CollectorService(ICollectorRepository collectorRepository, IAccountRepository accountRepository, ITokenManager tokenManager, IPaymentService paymentService, ILoggerFactory loggerFactory, IMapper mapper)
        {
            repository = collectorRepository;
            this.paymentService = paymentService;
            this.accountRepository = accountRepository;
            this.tokenManager = tokenManager;
            logger = loggerFactory.GetLogger<CollectorService>();
            this.mapper = mapper;
        }

        public CollectorSignInResponse Login(GenericSignInRequest request)
        {
            try
            {
                var response = new CollectorSignInResponse();
                var hash = accountRepository.GetHash(request.Phone);
                var hashPwd = EncryptionUtil.GetHash(request.Password);
                if (EncryptionUtil.VerifyHash(request.Password, hash))
                {
                    var loginResult = repository.GetLoginInfo(request.Phone, request.DeviceID, request.DevicePlatform);
                    response = mapper.Map<CollectorLoginResult, CollectorSignInResponse>(loginResult);
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

        public CollectorLocationResponse GetCurrentLocation(Guid collectorID)
        {
            try
            {
                var result = repository.GetCurrentLocation(collectorID) ??
                    new CollectorLocationResult { CollectorID = collectorID, IsLoggedIn = false };
                return mapper.Map<CollectorLocationResult, CollectorLocationResponse>(result);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetCurrentLocation: ", ex);
                throw ex;
            }
        }

        public CollectorLocationResponse GetCurrentLocationTrack(Guid userId, int orderId)
        {
            try
            {
                var result = repository.GetCurrentLocationTrack(userId, orderId);
                return mapper.Map<CollectorLocationResult, CollectorLocationResponse>(result);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetCurrentLocation: ", ex);
                throw ex;
            }
        }

        public ListResponse<CollectorAssignedOrderResponse> GetAssignedOrders(Guid collectorID, bool showOnlyOpen)
        {
            try
            {
                var result = repository.GetAssignedOrders(collectorID, showOnlyOpen);
                var grp = result.GroupBy(r => r.OrderId).ToList();
                var response = new ListResponse<CollectorAssignedOrderResponse>();
                grp.ForEach(r =>
                {
                    var responseObj = mapper.Map<CollectorAssignedOrderResult, CollectorAssignedOrderResponse>(r.First());
                    responseObj.AssignedOrderDetails = r.Select(dtl =>
                        mapper.Map<CollectorAssignedOrderResult, CollectorAssignedOrderDetails>(dtl)).ToList();
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

        public ListResponse<CollectorOrderHistoryResponse> GetOrderHistory(Guid collectorID)
        {
            try
            {
                var result = repository.GetOrderHistory(collectorID);
                var grp = result.GroupBy(r => r.OrderId).ToList();
                var response = new ListResponse<CollectorOrderHistoryResponse>();
                grp.ForEach(r =>
                {
                    var responseObj = mapper.Map<CollectorOrderHistoryResult, CollectorOrderHistoryResponse>(r.First());
                    responseObj.PatientDetails = r.Select(dtl =>
                        mapper.Map<CollectorOrderHistoryResult, TestPatientDetails>(dtl)).ToList();
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

        public StatusResponse AcceptOrder(CollectorAcceptOrderRequest request)
        {
            try
            {
                var param = mapper.Map<CollectorAcceptOrderRequest, CollectorOrderAccept>(request);
                repository.AcceptOrder(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in AcceptOrder: ", ex);
                throw ex;
            }
        }

        public StatusResponse DeclineOrder(CollectorDeclineOrderRequest request)
        {
            try
            {
                var param = mapper.Map<CollectorDeclineOrderRequest, CollectorOrderDecline>(request);
                repository.DeclineOrder(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in DeclineOrder: ", ex);
                throw ex;
            }
        }

        public ListResponse<CollectorDetailsResponse> GetCollectors(decimal latitude, decimal longitude, Guid? regionID = null, int? orderID = null, bool ignoreIfAlreadyDeclined = true)
        {
            try
            {
                var result = repository.GetCollectors(latitude, longitude, regionID, orderID, ignoreIfAlreadyDeclined);
                return mapper.Map<List<CollectorDetailsResult>, ListResponse<CollectorDetailsResponse>>(result);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetCollectors: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdateLocation(CollectorLocationRequest collectorLocation)
        {
            try
            {
                var location = mapper.Map<CollectorLocationRequest, CollectorLocationUpdate>(collectorLocation);
                repository.UpdateLocation(location);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdateLocation: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdateLoggedInStatus(CollectorStatusUpdateRequest collectorStatus)
        {
            try
            {
                repository.UpdateLoggedInStatus(collectorStatus.CollectorID.Value, collectorStatus.IsLoggedIn.Value);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdateLoggedInStatus: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdatePatientDetails(TestPatientUpdateRequest request)
        {
            try
            {
                var param = mapper.Map<TestPatientUpdateRequest, TestOrderPatientUpdate>(request);
                repository.UpdatePatientDetails(param);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdatePatientDetails: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdatePatientAadharPhoto(int orderID, Guid patientID, string adhaarPhoto)
        {
            try
            {
                repository.UpdatePatientAadharPhoto(orderID, patientID, adhaarPhoto);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdatePatientAadharPhoto: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdatePaymentStatus(TestPaymentUpdateRequest request)
        {
            try
            {
                var param = mapper.Map<TestPaymentUpdateRequest, TestOrderPaymentUpdate>(request);
                //var razorPayment = mapper.Map<TestPaymentUpdateRequest, RazorPayment>(request);
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

        public StatusResponse UpdateTestGivenStatus(int orderID)
        {
            try
            {
                //repository.UpdateTestGivenStatus(orderID);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdateTestGivenStatus: ", ex);
                throw ex;
            }
        }

        public ListResponse<TestOrderByRegionResponse> GetOrdersByRegion(Guid regionID, Guid? collectorID, bool? showOnlyUnassigned)
        {
            try
            {
                var result = repository.GetOrdersByRegion(regionID, collectorID, showOnlyUnassigned);
                return mapper.Map<List<TestOrderByRegionResult>, ListResponse<TestOrderByRegionResponse>>(result);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetCollectors: ", ex);
                throw ex;
            }
        }
    }
}
