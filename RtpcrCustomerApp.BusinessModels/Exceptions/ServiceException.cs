namespace RtpcrCustomerApp.BusinessModels.Exceptions
{
    using RtpcrCustomerApp.BusinessModels.Common;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class ServiceException : Exception
    {
        private static readonly Environments Environment;
        static ServiceException()
        {
            Enum.TryParse(ConfigurationManager.AppSettings["Enviroment"] ?? "Dev", out Environment);
        }

        private const string GenericError = "Something went wrong! Please try later or get in touch with our support team if it still persists.";

        public string Code { get; set; }
        public string Module { get; }
        public string Operation { get; }

        public ServiceException(Exception ex, string module)
        {
            this.Module = module;
        }

        public ServiceException(Exception ex, string module, string operation)
        {
            this.Module = module;
            this.Operation = operation;
        }

        public List<Error> GetErrors()
        {
            var baseEx = GetBaseException();
            return new List<Error>
            {
                new Error
                {
                    Code = "100",
                    Description = Environment == Environments.Prod ? GenericError : baseEx.ToString(),
                    Message = Environment == Environments.Prod ? GenericError : baseEx.Message,
                    Module = Module,
                    Operation = Operation,
                    Type = ErrorType.Fatal
                }
            };
        }
    }
}
