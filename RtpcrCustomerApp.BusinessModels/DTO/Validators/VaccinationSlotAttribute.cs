namespace RtpcrCustomerApp.BusinessModels.DTO.Validators
{
    using BusinessModels.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VaccinationSlotAttribute : ValidationAttribute
    {
        public VaccinationSlotAttribute()
        {
        }

        public VaccinationSlotAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public VaccinationSlotAttribute(Func<string> errorMessageAccessor) : base(errorMessageAccessor)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return "Invalid vaccination slot";
        }

        public override bool IsValid(object value)
        {
            return string.IsNullOrEmpty((string)value) || VaccinationSlots.Slots.ContainsKey((string)value);
        }
    }
}
