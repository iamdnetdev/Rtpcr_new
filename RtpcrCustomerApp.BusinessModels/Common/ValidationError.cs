using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.Common
{
    public class ValidationError
    {
        public ValidationError()
        {

        }

        public ValidationError(string propertyName)
        {
            Message = $"{propertyName} fails validation";
        }

        public ValidationError(string propertyName, ValidationType type)
        {
            PropertyName = propertyName;
            Type = type;
            Message = $"{propertyName} fails {type} validation";
        }

        public ValidationError(string propertyName, ValidationType type, string message)
        {
            PropertyName = propertyName;
            Type = type;
            Message = message;
        }

        public string PropertyName { get; set; }
        public ValidationType Type { get; set; }
        public string Message { get; set; }
        public string Module { get; set; }
    }
}
