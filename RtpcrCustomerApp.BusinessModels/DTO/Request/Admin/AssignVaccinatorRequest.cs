namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Admin
{
    using BusinessModels.Common;
    using System;

    public class AssignVaccinatorRequest : ModelBase
    {
        //public Guid UserID { get; set; }

        public int OrderID { get; set; }

        public Guid VaccinatorID { get; set; }
    }
}
