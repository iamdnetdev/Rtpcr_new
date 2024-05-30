using System.Web.Mvc;

namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    public interface IEmailService
    {
        void SendMail<C, T>(string toAddress, string subject, string viewPath, T data) where C : Controller, new();
        void SendMail(string toAddress, string subject, string body, bool isBodyHtml = true);
    }
}
