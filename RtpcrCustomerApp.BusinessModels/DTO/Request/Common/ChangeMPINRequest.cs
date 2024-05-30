namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Common
{
    using BusinessModels.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ChangeMPINRequest : ModelBase
    {
        [Required]
        public string Phone { get; set; }

        [Required, RegularExpression("^\\d{6}$", ErrorMessage = "Invalid MPIN")]
        public string MPIN { get; set; }
    }
}
