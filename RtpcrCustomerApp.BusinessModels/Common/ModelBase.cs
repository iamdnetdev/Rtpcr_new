namespace RtpcrCustomerApp.BusinessModels.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract class ModelBase
    {
        public virtual bool IsValid()
        {
            var context = new ValidationContext(this, null, null);
            var errors = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, errors);
        }

        public virtual List<ValidationResult> GetValidationErrors()
        {
            var context = new ValidationContext(this, null, null);
            var errors = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, errors, true);
            return errors;
        }

        protected bool IsNullOrDefault<T>(T value)
        {
            var isDefault = value.Equals(default(T));
            if (isDefault) return true;
            if (typeof(T) == typeof(string))
            {
                return string.IsNullOrEmpty(value?.ToString());
            }
            return false;
        }
    }
}
