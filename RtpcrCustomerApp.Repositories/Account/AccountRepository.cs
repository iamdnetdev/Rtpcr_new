namespace RtpcrCustomerApp.Repositories.Account
{
    using BusinessModels.Common;
    using Common;
    using Interfaces;
    using RtpcrCustomerApp.BusinessModels.DBO;
    using RtpcrCustomerApp.BusinessModels.DBO.InParams.Common;
    using RtpcrCustomerApp.BusinessModels.DBO.OutParams.Common;
    using RtpcrCustomerApp.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        public AccountRepository(IDbSetting settings, ILoggerFactory loggerFactory) : base(settings, loggerFactory.GetLogger<AccountRepository>())
        {

        }

        public GetAccountResult GetById(Guid id)
        {
            var account = Query<GetAccountResult>(Queries.Account.GetAccountById,
                                         CommandType.StoredProcedure,
                                         new KeyValuePair<string, object>("UserID", id))
                            .FirstOrDefault();
            return account;
        }

        public GetAccountResult Insert(AccountInsert account)
        {
            ExecuteCommand(Queries.Account.InsertAccount, account);
            return GetById(account.UserID);
        }

        public GetAccountResult Update(AccountUpdate account)
        {
            ExecuteCommand(Queries.Account.UpdateAccount, account);
            return GetById(account.UserID);
        }

        public void UpdateProfileAccount(AccountProfileUpdate account)
        {
            ExecuteCommand(Queries.Account.UpdateProfileAccount, account);
        }

        public void UpdatePassword(AccountPasswordUpdate account)
        {
            ExecuteCommand(Queries.Account.ChangePassword, account);
        }

        public void LockUnlock(Guid id, bool toBeLocked)
        {
            ExecuteCommand(Queries.Account.LockUnlockAccount, new { Lock = toBeLocked });
        }

        public string GetHash(string phone)
        {
            return Query<dynamic, string>(Queries.Account.GetHash, parameter: new { Phone = phone }, CommandType.StoredProcedure).FirstOrDefault();
        }

        public LoginResult GetLoginInfo(string username, string deviceID, DevicePlatform devicePlatform)
        {
            var loginResult = Query<dynamic, LoginResult>(Queries.Account.Login, parameter: new { Phone = username, DeviceID = deviceID, DevicePlatform = (short)devicePlatform }, CommandType.StoredProcedure).FirstOrDefault();
            return loginResult;
        }
    }
}