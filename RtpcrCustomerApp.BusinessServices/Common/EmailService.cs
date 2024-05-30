namespace RtpcrCustomerApp.BusinessServices.Common
{
    using BusinessServices.Interfaces;
    using log4net;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Utils;
    using System;
    using System.Net.Mail;
    using System.Web.Mvc;

    public class EmailService : IEmailService
    {
        private readonly ILog logger;
        public EmailService(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.GetLogger<EmailService>();
        }

        public void SendMail(string toAddress, string subject, string body, bool isBodyHtml)
        {
            try
            {
                using (var client = AppSettings.GetSmtpClient())
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(AppSettings.FromAddressDisplay, AppSettings.FromAddress),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = isBodyHtml
                    };
                    mailMessage.To.Add(toAddress);

                    client.Send(mailMessage);
                }
            }
            catch(Exception ex)
            {
                logger.Error("Error in SendMail: ", ex);
                throw ex;
            }
        }

        public void SendMail<C, T>(string toAddress, string subject, string viewPath, T data) where C : Controller, new()
        {
            try
            {
                var body = ViewRenderer.RenderViewToString<C>(viewPath, data);
                SendMail(toAddress, subject, body, true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in SendMail: ", ex);
                throw ex;
            }
        }
    }
}
