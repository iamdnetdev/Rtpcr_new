using System;

namespace RtpcrCustomerApp.Common.Utils
{
    public class EmailTokenUtil
    {
        public static string CreateEmailToken(Guid userID, string email)
        {
            var tokenPlain = $"{userID}|{email}";
            var token = EncryptionUtil.Encrypt(tokenPlain);
            return token;
        }

        public static bool VerifyEmailToken(Guid userID, string email, string token)
        {
            var tokenPlain = $"{userID}|{email}";
            var tokenDecrypted = EncryptionUtil.Decrypt(token);
            return string.Equals(tokenPlain, tokenDecrypted);
        }
    }
}
