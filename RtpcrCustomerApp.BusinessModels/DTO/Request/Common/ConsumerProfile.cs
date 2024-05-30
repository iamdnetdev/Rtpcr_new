namespace RtpcrCustomerApp.BusinessModels.DTO
{
    using Common;
    using System;
    using System.Collections.Generic;

    public class ConsumerProfile : ModelBase
    {
		public Guid UserID { get; set; }
		public bool IsAssociateWithCompany { get; set; }
		public Guid CompanyId { get; set; }
		public string EmployeeId { get; set; }
		public string AadharNumber { get; set; }
		public string Proof { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }

        //public override IEnumerable<ValidationError> GetValidationErrors()
        //{
        //    var errors = new List<ValidationError>();
        //    if (IsNullOrDefault(UserID)) errors.Add(new ValidationError(nameof(UserID), ValidationType.Required));
        //    return errors;
        //}
    }
}
