namespace RtpcrCustomerApp.Repositories.Interfaces
{
    using BusinessModels.Common;
    using BusinessModels.DBO;
    using BusinessModels.DBO.InParams.Common;
    using BusinessModels.DBO.OutParams.Common;
    using System;

    public interface IAccountRepository
    {
        GetAccountResult GetById(Guid id);
        GetAccountResult Insert(AccountInsert account);
        GetAccountResult Update(AccountUpdate account);
        void UpdateProfileAccount(AccountProfileUpdate account);
        void UpdatePassword(AccountPasswordUpdate account);
        void LockUnlock(Guid id, bool toBeLocked);        
        LoginResult GetLoginInfo(string username, string deviceID, DevicePlatform devicePlatform);
        string GetHash(string phone);

    }
}
