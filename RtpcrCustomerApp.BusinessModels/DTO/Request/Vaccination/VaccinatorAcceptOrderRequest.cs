﻿namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Vaccination
{
    using BusinessModels.Common;
    using System;

    public class VaccinatorAcceptOrderRequest : ModelBase
    {
        //public Guid UserID { get; set; }

        public int OrderID { get; set; }

        public Guid VaccinatorID { get; set; }
    }
}
