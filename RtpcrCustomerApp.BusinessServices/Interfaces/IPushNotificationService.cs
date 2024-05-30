using System;
using System.Collections.Generic;

namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    public interface IPushNotificationService
    {
        void SendNotification(string message, string type, Guid userID);
        void SendAndroidNotification(string message, string type, List<string> devices);
        void SendAppleNotification(string message, string type, List<string> devices);
    }
}
