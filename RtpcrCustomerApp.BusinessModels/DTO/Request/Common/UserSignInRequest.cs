namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Common
{
    using BusinessModels.Common;
    using System.ComponentModel.DataAnnotations;

    public class UserSignInRequest : ModelBase
    {
        [Required, RegularExpression("^[6789]\\d{9}$", ErrorMessage = "Invalid mobile number")]
        public string Phone { get; set; }

        [Required, RegularExpression("^\\d{6}$", ErrorMessage = "Invalid MPIN")]
        public string MPIN { get; set; }

        // 16 char Hexa decimal string for android and 40 char Hexa decimal string for most of the iphones
        public string DeviceID { get; set; }

        public DevicePlatform DevicePlatform { get; set; }

    }
}
