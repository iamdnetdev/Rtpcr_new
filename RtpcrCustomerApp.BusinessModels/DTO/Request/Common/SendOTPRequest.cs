namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Common
{
    using BusinessModels.Common;
    using System.ComponentModel.DataAnnotations;

    public class SendOTPRequest : ModelBase
    {
        [Required, RegularExpression("^[6789]\\d{9}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }
    }
}
