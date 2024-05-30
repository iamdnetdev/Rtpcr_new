using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace RtpcrCustomerApp.Common.Utils
{
    public static class AppSettings
    {
        private static IDictionary<string, string> settings;
        static AppSettings()
        {
            settings = ConfigurationManager.AppSettings.ToDictionary(StringComparer.InvariantCultureIgnoreCase);

            Environment = (Environments)Enum.Parse(typeof(Environments), Get<string>("Environment", "QA"));

            RazorKey = GetEncrypted("Razor.Key");
            RazorSecret = GetEncrypted("Razor.Secret");

            SmtpServer = Get<string>("Mail.SmtpServer");
            SmtpPortNumber = Get<int>("Mail.SmtpPort");
            SmtpUsername = Get<string>("Mail.Username");
            SmtpPassword = Get<string>("Mail.Password");
            FromAddressDisplay = Get<string>("Mail.FromAddressDisplay");
            FromAddress = Get<string>("Mail.FromAddress");

            TwilioAccountId = Get<string>("Twilio.AccountID");
            TwilioAuthToken = Get<string>("Twilio.AuthToken");
            TwilioPhoneNumber = Get<string>("Twilio.PhoneNumber");
            TwilioVerifySID = Get<string>("Twilio.VerifySID");

            PushNotificationsURL = new Uri(Get<string>("Push.NotificationsURL"));
            PushApiKey = Get<string>("Push.ApiKey");
            PushPassword = Get<string>("Push.Password");
            var pushCertPath = Get<string>("Push.CertificatePath");
            if (File.Exists(pushCertPath)) PushCertificate = File.ReadAllBytes(pushCertPath); ;

            PushNotifications = Get<bool>("PushNotifications");

            AllowedFileSize = Get<Int64>("AllowedFileSize", 1024) * 1024 * 1024;
        }

        public static Environments Environment { get; }

        public static string SmtpServer { get; }
        public static int SmtpPortNumber { get; }
        public static string SmtpUsername { get; }
        public static string SmtpPassword { get; }
        public static string FromAddressDisplay { get; }
        public static string FromAddress { get; }

        public static string TwilioAccountId { get; }
        public static string TwilioAuthToken { get; }
        public static string TwilioPhoneNumber { get; }
        public static string TwilioVerifySID { get; }

        public static Uri PushNotificationsURL { get; }
        public static string PushApiKey { get; }
        public static string PushPassword { get; }
        public static byte[] PushCertificate { get; }

        public static bool PushNotifications { get; }
        public static string RazorKey { get; }
        public static string RazorSecret { get; }

        public static long AllowedFileSize { get; }

        public static SmtpClient GetSmtpClient()
        {
            var client = new SmtpClient(SmtpServer, SmtpPortNumber);
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential(AppSettings.SmtpUsername, AppSettings.SmtpPassword);
            client.EnableSsl = true;
            return client;
        }

        private static string GetEncrypted(string key)
        {
            var val = Get<string>(key);
            val = EncryptionUtil.Decrypt(val);
            return val;
        }

        private static T Get<T>(string key)
        {
            return Get(key, default(T));
        }

        private static T Get<T>(string key, T defaultValue)
        {
            if (!settings.ContainsKey(key)) return defaultValue;
            if (TryChangeType<T>(settings[key], out T value)) return value;
            return defaultValue;
        }

        private static bool TryChangeType<T>(string v, out T value)
        {
            try
            {
                value = (T)Convert.ChangeType(v, typeof(T));
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }
    }
}
