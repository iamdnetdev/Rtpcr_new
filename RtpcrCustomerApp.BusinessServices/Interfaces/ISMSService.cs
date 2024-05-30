using RtpcrCustomerApp.BusinessModels.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessServices.Interfaces
{
    public interface ISMSService
    {
        StatusResponse SendMessage(string phoneNumber, string message);
        StatusResponse SendOTP(string phoneNumber, string message);
        StatusResponse VerifyOTP(string phoneNumber, string otp);
    }
}
