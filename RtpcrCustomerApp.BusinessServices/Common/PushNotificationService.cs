namespace RtpcrCustomerApp.BusinessServices.Common
{
    using log4net;
    using Newtonsoft.Json;
    using PushSharp.Apple;
    using RtpcrCustomerApp.BusinessModels.Common;
    using RtpcrCustomerApp.BusinessServices.Interfaces;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Utils;
    using RtpcrCustomerApp.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;

    public class PushNotificationService : IPushNotificationService
    {
        private readonly ILog logger;
        private readonly IDeviceDetailsRepository deviceDetailsRepository;
        public PushNotificationService(ILoggerFactory loggerFactory, IDeviceDetailsRepository deviceDetailsRepository)
        {
            logger = loggerFactory.GetLogger<PushNotificationService>();
            this.deviceDetailsRepository = deviceDetailsRepository;
        }

        public void SendNotification(string message, string type, Guid userID)
        {
            logger.Info("SendNotification Start " + type);
            var devices = deviceDetailsRepository.GetDeviceDetails(userID);
            SendAndroidNotification(message, type, devices.Where(d => d.DevicePlatform == DevicePlatform.Android).Select(d => d.DeviceID).ToList());
            SendAppleNotification(message, type, devices.Where(d => d.DevicePlatform == DevicePlatform.Apple).Select(d => d.DeviceID).ToList());
        }

        public void SendAndroidNotification(string message, string type, List<string> devices)
        {
            logger.Info("SendAndroidNotification Start " + type);
            if (!AppSettings.PushNotifications) return;
            try
            {
                if (devices.Any())
                {
                    var messageInformation = new PushNotificationMessageAndroid()
                    {
                        Notification = new Notification()
                        {
                            Title = "TICOVID19",
                            Text = message
                        },
                        Data = new NotificationData
                        {
                            Type = type
                        },
                        DeviceID = devices
                    };

                    string requestObj = JsonConvert.SerializeObject(messageInformation, Formatting.None);

                    var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.PushNotificationsURL);

                    request.Headers.TryAddWithoutValidation("Authorization", "key=" + AppSettings.PushApiKey);
                    request.Content = new StringContent(requestObj, Encoding.UTF8, "application/json");

                    HttpResponseMessage result;
                    using (var client = new HttpClient())
                    {
                        result = client.SendAsync(request).Result;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in SendAndroidNotification", ex);
            }
        }

        public void SendAppleNotification(string message, string type, List<string> devices)
        {
            logger.Info("SendAppleNotification Start " + type);
            if (!AppSettings.PushNotifications) return;
            try
            {
                if (devices.Any())
                {
                    var env = AppSettings.Environment.IsProd() ?
                        ApnsConfiguration.ApnsServerEnvironment.Production :
                        ApnsConfiguration.ApnsServerEnvironment.Sandbox;
                    var config = new ApnsConfiguration(env, AppSettings.PushCertificate, AppSettings.PushPassword);
                    var apnsBroker = new ApnsServiceBroker(config);
                    apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
                    {
                        aggregateEx.Handle(ex =>
                        {
                            if (ex is ApnsNotificationException)
                            {
                                var notificationException = (ApnsNotificationException)ex;

                                var apnsNotification = notificationException.Notification;
                                var statusCode = notificationException.ErrorStatusCode;
                                logger.Error($"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");
                            }
                            else
                            {
                                logger.Error($"Apple Notification Failed for some unknown reason : {ex.GetBaseException()}");
                            }
                            return true;
                        });
                    };
                    apnsBroker.OnNotificationSucceeded += (notification) =>
                    {
                        logger.Info("Apple Notification Sent!");
                    };
                    apnsBroker.Start();
                    foreach (var token in devices)
                    {
                        //{\"aps\":{\"alert\":\"" + message + "\",\"sound\":\"default\",\"flag\":\"" + type + "\"}}
                        apnsBroker.QueueNotification(new ApnsNotification
                        {
                            DeviceToken = token,
                            Payload = new PushNotificationMessageApple
                            {
                                AlertMessage = new AlertMessage
                                {
                                    Alert = message,
                                    Sound = "default",
                                    Flag = type
                                }
                            }.ToJObject()
                        });
                    }
                    apnsBroker.Stop();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in SendAppleNotification", ex);
            }
        }
    }
}
