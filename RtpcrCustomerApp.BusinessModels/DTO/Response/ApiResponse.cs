namespace RtpcrCustomerApp.BusinessModels.DTO.Response
{
    using BusinessModels.Common;
    using BusinessModels.DTO;
    using BusinessModels.Exceptions;
    using System.Collections.Generic;

    public class ApiResponse<T> where T : IResponse
    {
        public ApiResponse() { }
        public ApiResponse(T data)
        {
            Data = data;
            Errors = new List<Error>();
        }

        public ApiResponse(List<Error> errors)
        {
            Data = default(T);
            Errors = errors;
        }

        public ApiResponse(ServiceException exception)
        {
            Data = default(T);
            Errors = exception.GetErrors();
        }

        public T Data { get; set; }
        public List<Error> Errors { get; set; }
    }
}
