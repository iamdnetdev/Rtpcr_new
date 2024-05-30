namespace RtpcrCustomerApp.BusinessServices.Common
{
    using BusinessModels.Common;
    using log4net;
    using Razorpay.Api;
    using RtpcrCustomerApp.BusinessModels.DTO.Response;
    using RtpcrCustomerApp.BusinessServices.Interfaces;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Utils;
    using System;
    using System.Collections.Generic;

    public class PaymentService : IPaymentService
    {
        private readonly ILog logger;
        public PaymentService(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.GetLogger<PaymentService>();
        }

        public string Refund(string paymentID, decimal amount = 0)
        {
            try
            {
                var client = new RazorpayClient(AppSettings.RazorKey, AppSettings.RazorSecret);
                var refund = new Payment(paymentID).Refund();
                return refund.Attributes["id"];
            }
            catch (Exception ex)
            {
                logger.Error("Error in Razor Refund: ", ex);
                throw ex;
            }
        }
    }
}
