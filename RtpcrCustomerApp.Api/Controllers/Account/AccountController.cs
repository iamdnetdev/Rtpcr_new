namespace RtpcrCustomerApp.Api.Controllers.Account
{
    using BusinessModels.Common;
    using BusinessModels.DTO;
    using BusinessServices.Interfaces;
    using log4net;
    using RtpcrCustomerApp.Api.Filters;
    using RtpcrCustomerApp.BusinessModels.DTO.Request.Common;
    using RtpcrCustomerApp.BusinessModels.DTO.Response;
    using RtpcrCustomerApp.BusinessModels.DTO.Response.Common;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Logging;
    using RtpcrCustomerApp.Common.Models;
    using RtpcrCustomerApp.Common.Utils;
    using System;
    using System.Linq;
    using System.Web.Http;

    [Module(Modules.Account)]
    public class AccountController : ApiController
    {
        private readonly IAccountService m_accountService;
        private readonly ISMSService m_smsService;
        private readonly ILog logger;
        private const string Module = Modules.Account;

        public AccountController(IAccountService accountService, ISMSService smsService, ILoggerFactory loggerFactory)
        {
            m_accountService = accountService;
            m_smsService = smsService;
            logger = loggerFactory.GetLogger<AccountController>();
        }


        [HttpPost]
        [Route("LoginConsumer")]
        [NonAuthorize]
        public ApiResponse<UserSignInResponse> Login(UserSignInRequest request)
        {
            try
            {
                var validationErrors = request.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in login request: {0}", request.Phone);
                    return new ApiResponse<UserSignInResponse>(validationErrors.ToErrors());
                }
                var response = m_accountService.Login(request);
                return new ApiResponse<UserSignInResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserSignInResponse>(ex.GetErrors(Module, "Login"));
            }
        }

        [HttpPost]
        [Route("Vaccination/Consumer/Register")]
        [NonAuthorize]
        public ApiResponse<GetAccountResponse> Register(CreateAccountRequest account)
        {
            try
            {
                var validationErrors = account.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in register request: {0}", account.Email);
                    return new ApiResponse<GetAccountResponse>(validationErrors.ToErrors());
                }
                var response = m_accountService.CreateAccount(account);
                logger.InfoFormat("Registration succeeded: {0} - {1}", response.UserID, response.Email);
                return new ApiResponse<GetAccountResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetAccountResponse>(ex.GetErrors(Module, "CreateAccount"));
            }
        }

        [HttpPost]
        [Route("Update")]
        public ApiResponse<GetAccountResponse> Update(AccountUpdateRequest account)
        {
            try
            {
                var validationErrors = account.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in update request: {0}", account.UserID);
                    return new ApiResponse<GetAccountResponse>(validationErrors.ToErrors());
                }
               
                var response = m_accountService.UpdateProfile(account);
                logger.InfoFormat("Updated succeeded: {0} - {1}", response.UserID, response.Email);
                return new ApiResponse<GetAccountResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetAccountResponse>(ex.GetErrors(Module, "UpdateProfile"));
            }
        }

        [HttpPost]
        [Route("UpdateProfile")]
        public ApiResponse<StatusResponse> UpdateProfileAccount(AccountProfileUpdateRequest account)
        {
            try
            {
                var validationErrors = account.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in update request: {0}", account.UserID);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }

                var response = m_accountService.UpdateProfileAccount(account);
                logger.InfoFormat("Updated succeeded: {0} - {1}", account.UserID, account.Email);
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "UpdateProfile"));
            }
        }

        [HttpPost]
        [Route("ChangeMPIN")]
        public ApiResponse<StatusResponse> ChangeMPIN(ChangeMPINRequest changeMPIN)
        {
            try
            {
                var validationErrors = changeMPIN.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in Change MPIN request");
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }

                var response = m_accountService.ChangeMPIN(changeMPIN);
                logger.InfoFormat("Updated MPIN succeeded");
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "ChangeMPIN"));
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public ApiResponse<StatusResponse> ChangePassword(ChangePasswordRequest changePassword)
        {
            try
            {
                var validationErrors = changePassword.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in Change Password request");
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }

                var response = m_accountService.ChangePassword(changePassword);
                logger.InfoFormat("Updated Password succeeded");
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "ChangePassword"));
            }
        }

        [HttpPost]
        [Route("Vaccination/Consumer/SendOTP")]
        [NonAuthorize]
        public ApiResponse<StatusResponse> SendOTP(SendOTPRequest sendOTPRequest)
        {
            try
            {
                var validationErrors = sendOTPRequest.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in send OTP request: {0}", sendOTPRequest.PhoneNumber);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = m_smsService.SendOTP(sendOTPRequest.PhoneNumber, null);
                logger.InfoFormat("OTP sent");
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "SendOTP"));
            }
        }

        [HttpPost]
        [Route("Vaccination/Consumer/VerifyOTP")]
        [NonAuthorize]
        public ApiResponse<StatusResponse> VerifyOTP(VerifyOTPRequest verifyOTPRequest)
        {
            try
            {
                var validationErrors = verifyOTPRequest.GetValidationErrors();
                if (validationErrors.Any())
                {
                    logger.WarnFormat("Validation fails in verify OTP request: {0}", verifyOTPRequest.PhoneNumber);
                    return new ApiResponse<StatusResponse>(validationErrors.ToErrors());
                }
                var response = m_smsService.VerifyOTP(verifyOTPRequest.PhoneNumber, verifyOTPRequest.OTP);
                logger.InfoFormat("OTP verified");
                return new ApiResponse<StatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<StatusResponse>(ex.GetErrors(Module, "VerifyOTP"));
            }
        }

        // DELETE: api/Account/5
        //public void Delete(int id)
        //{
        //}
    }
}
