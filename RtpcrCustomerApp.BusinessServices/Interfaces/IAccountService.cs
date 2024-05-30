namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    using BusinessModels.DTO.Request.Common;
    using BusinessModels.DTO.Response.Common;
    using BusinessModels.DTO.Response;
    using System;

    public interface IAccountService
    {
        UserSignInResponse Login(UserSignInRequest request);
        GetAccountResponse GetAccount(Guid userID);
        GetAccountResponse CreateAccount(CreateAccountRequest account);
        GetAccountResponse UpdateProfile(AccountUpdateRequest account);
        StatusResponse UpdateProfileAccount(AccountProfileUpdateRequest account);
        StatusResponse ChangeMPIN(ChangeMPINRequest changeMPIN);
        StatusResponse ChangePassword(ChangePasswordRequest changePassword);
        bool VerifyEmailToken(string emailVerifyToken);
    }
}
