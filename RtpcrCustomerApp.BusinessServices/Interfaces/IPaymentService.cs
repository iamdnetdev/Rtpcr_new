namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    using BusinessModels.Common;
    using RtpcrCustomerApp.BusinessModels.DTO.Response;

    public interface IPaymentService
    {
        /// <summary>
        /// Create refund
        /// </summary>
        /// <param name="paymentID">Payment ID from RazorPay</param>
        /// <param name="amount">0 for full refund or calculated value for partial refund</param>
        /// <returns></returns>
        string Refund(string paymentID, decimal amount = 0);
    }
}
