namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Common
{
    using BusinessModels.Common;
    using System.ComponentModel.DataAnnotations;

    public class CreateAccountRequest : ModelBase
    {       

        [Required]
        [MinLength(2), MaxLength(100), RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid first name")]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(100), RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid last name")]
        public string LastName { get; set; }

        [Required, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required, RegularExpression("^[6789]\\d{9}$", ErrorMessage = "Invalid mobile number")]
        public string Phone { get; set; }

        [Required, RegularExpression("^\\d{6}$", ErrorMessage = "Invalid MPIN")]
        public string MPIN { get; set; }

        [Required, RegularExpression("^\\d{12}$", ErrorMessage = "Invalid Aadhar Number")]
        public string AadharNumber { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string LocationName { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        public string DeviceID { get; set; }
   
        public DevicePlatform DevicePlatform { get; set; }
    }
}
