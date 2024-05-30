namespace RtpcrCustomerApp.BusinessServices.Account
{
    using BusinessModels.Common;
    using BusinessModels.DBO;
    using BusinessModels.DBO.InParams.Common;
    using BusinessModels.DBO.OutParams.Common;
    using BusinessModels.DTO.Request.Common;
    using BusinessModels.DTO.Response;
    using BusinessModels.DTO.Response.Common;
    using BusinessServices.Interfaces;
    using log4net;
    using Repositories.Interfaces;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Logging;
    using RtpcrCustomerApp.Common.Utils;
    using System;

    [Module(Modules.Consumer)]
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository repository;
        private readonly ILog logger;
        private readonly ITokenManager tokenManager;
        private readonly IMapper mapper;
        public AccountService(IAccountRepository accountRepository, ILoggerFactory loggerFactory, ITokenManager tokenManager, IMapper mapper)
        {
            repository = accountRepository;
            this.tokenManager = tokenManager;
            logger = loggerFactory.GetLogger<AccountService>();
            this.mapper = mapper;
        }

        #region Private Methods

        private void SendRegistrationConfirmEmail(AccountInsert account)
        {

        }

        #endregion

        public UserSignInResponse Login(UserSignInRequest request)
        {
            try
            {
                var response = new UserSignInResponse();
                var genericRequest = mapper.Map<UserSignInRequest, GenericSignInRequest>(request);
                //return Login(genericRequest);
                var hash = repository.GetHash(request.Phone);
                if (EncryptionUtil.VerifyHash(genericRequest.Password, hash))
                {
                    var loginResult = repository.GetLoginInfo(request.Phone, request.DeviceID, request.DevicePlatform);
                    response = mapper.Map<LoginResult, UserSignInResponse>(loginResult);
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
                throw ex;
            }
        }

        public GetAccountResponse GetAccount(Guid userID)
        {
            try
            {
                var account = repository.GetById(userID);
                return mapper.Map<GetAccountResult, GetAccountResponse>(account);
            }
            catch (Exception ex)
            {
                logger.Error("Error in GetAccount: ", ex);
                throw ex;
            }
        }

        public GetAccountResponse CreateAccount(CreateAccountRequest account)
        {
            try
            {
                var accountInsert = mapper.Map<CreateAccountRequest, AccountInsert>(account);
                accountInsert.Role = Role.Consumer;
                //accountInsert.EmailToken = EmailTokenUtil.CreateEmailToken(accountInsert.UserID, accountInsert.Email);
                //accountInsert.EmailTokenExpiration = DateTime.Now.AddDays(2);
                var accountResult = repository.Insert(accountInsert);
                
                logger.InfoFormat("Registration succeeded: {0}", accountResult.UserID);
                return mapper.Map<GetAccountResult, GetAccountResponse>(accountResult);
            }
            catch (Exception ex)
            {
                logger.Error("Error in CreateAccount: ", ex);
                throw ex;
            }
        }

        public GetAccountResponse UpdateProfile(AccountUpdateRequest account)
        {
            try
            {
                var accountUpdate = mapper.Map<AccountUpdateRequest, AccountUpdate>(account);
                var accountResult = repository.Update(accountUpdate);
                logger.InfoFormat("Account update succeeded: {0}", accountResult.UserID);
                return mapper.Map<GetAccountResult, GetAccountResponse>(accountResult);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdateAccount: ", ex);
                throw ex;
            }
        }

        public StatusResponse UpdateProfileAccount(AccountProfileUpdateRequest account)
        {
            try
            {
                var accountUpdate = mapper.Map<AccountProfileUpdateRequest, AccountProfileUpdate>(account);
                repository.UpdateProfileAccount(accountUpdate);
                logger.InfoFormat("Account update succeeded: {0}", accountUpdate.UserID);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in UpdateAccount: ", ex);
                throw ex;
            }
        }

        public StatusResponse ChangeMPIN(ChangeMPINRequest changeMPIN)
        {
            try
            {
                var accountUpdate = mapper.Map<ChangeMPINRequest, AccountPasswordUpdate>(changeMPIN);
                repository.UpdatePassword(accountUpdate);
                logger.InfoFormat("Change MPIN succeeded: {0}", changeMPIN.Phone);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in ChangeMPIN: ", ex);
                throw ex;
            }
        }

        public StatusResponse ChangePassword(ChangePasswordRequest changePassword)
        {
            try
            {
                var accountUpdate = mapper.Map<ChangePasswordRequest, AccountPasswordUpdate>(changePassword);
                repository.UpdatePassword(accountUpdate);
                logger.InfoFormat("Change Password succeeded: {0}", changePassword.Phone);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in ChangePassword: ", ex);
                throw ex;
            }
        }

        public bool VerifyEmailToken(string emailVerifyToken)
        {
            throw new NotImplementedException();
        }
    }
}