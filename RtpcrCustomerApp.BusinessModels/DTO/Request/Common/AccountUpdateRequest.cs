namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Common
{
    using BusinessModels.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AccountUpdateRequest : ModelBase
    {

        [Required]
        [MinLength(2), MaxLength(100), RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid first name")]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(100), RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid last name")]
        public string LastName { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [RegularExpression("^[6789]\\d{9}$", ErrorMessage = "Invalid mobile number")]
        public string Phone { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        [Required]
        public string Address { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }
    }
}
