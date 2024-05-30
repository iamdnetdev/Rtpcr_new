namespace RtpcrCustomerApp.BusinessServices.Common
{
    using BusinessModels.DTO.Response;
    using BusinessServices.Interfaces;
    using log4net;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Utils;
    using System;
    using Twilio;
    using Twilio.Exceptions;
    using Twilio.Rest.Api.V2010.Account;
    using Twilio.Rest.Verify.V2.Service;

    public class SMSService : ISMSService
    {
        private readonly ILog logger;

        static SMSService()
        {
            TwilioClient.Init(AppSettings.TwilioAccountId, AppSettings.TwilioAuthToken);
        }

        public SMSService(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.GetLogger<SMSService>();
        }

        public StatusResponse SendMessage(string phoneNumber, string message)
        {
            try
            {
                phoneNumber = AppendCountryCode(phoneNumber);
                var response = MessageResource.Create(
                     body: message,
                     from: new Twilio.Types.PhoneNumber(AppSettings.TwilioPhoneNumber),
                     to: new Twilio.Types.PhoneNumber(phoneNumber)
                );
                logger.InfoFormat("SendMessage- Sid: {0}, Status: {1}, ErrorCode: {2}, ErrorMessage: {3}", response.Sid, response.Status, response.ErrorCode, response.ErrorMessage);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in SendMessage: ", ex);
                throw ex;
            }
        }

        public StatusResponse SendOTP(string phoneNumber, string message)
        {
            try
            {
                phoneNumber = AppendCountryCode(phoneNumber);
                CreateVerificationOptions options = new CreateVerificationOptions(AppSettings.TwilioVerifySID, phoneNumber, "sms");
                var verification = VerificationResource.Create(options);
                logger.InfoFormat("SendOTP- Sid: {0}, Status: {1}", verification.Sid, verification.Status);
                return new StatusResponse(true);
            }
            catch (Exception ex)
            {
                logger.Error("Error in SendOTP: ", ex);
                throw ex;
            }
        }

        public StatusResponse VerifyOTP(string phoneNumber, string otp)
        {
            try
            {
                phoneNumber = AppendCountryCode(phoneNumber);
                var verificationCheck = VerificationCheckResource.Create(
                    to: phoneNumber,
                    code: otp,
                    pathServiceSid: AppSettings.TwilioVerifySID
                );
                logger.InfoFormat("VerifyOTP- Sid: {0}, Status: {1}", verificationCheck.Sid, verificationCheck.Status);
                if (verificationCheck.Valid ?? false)
                {
                    return new StatusResponse(true);
                }
                else
                {
                    return new StatusResponse("Invalid OTP");
                }
            }
            catch(ApiException ex)
            {
                if (ex.Message.Contains("VerificationCheck was not found"))
                {
                    return new StatusResponse("OTP not sent or has been verified already");
                }
                else
                {
                    logger.Error("Error in VerifyOTP: ", ex);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in VerifyOTP: ", ex);
                throw ex;
            }
        }

        private string AppendCountryCode(string phoneNumber)
        {
            return phoneNumber.StartsWith("+91") ? phoneNumber : "+91" + phoneNumber;
        }

    }
}
