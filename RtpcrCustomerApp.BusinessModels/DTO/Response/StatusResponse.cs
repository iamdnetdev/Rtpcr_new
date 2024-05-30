using System.Collections.Generic;

namespace RtpcrCustomerApp.BusinessModels.DTO.Response
{
    public class StatusResponse : IResponse
    {
        public StatusResponse(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public StatusResponse(string error)
        {
            IsSuccessful = false;
            Error = error;
        }

        public StatusResponse(List<string> errors)
        {
            IsSuccessful = false;
            Errors = errors;
        }

        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public List<string> Errors { get; set; }
    }
}
