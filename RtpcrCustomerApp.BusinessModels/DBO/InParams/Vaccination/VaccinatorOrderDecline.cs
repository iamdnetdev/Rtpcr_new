namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Vaccination
{
    using System;

    public class VaccinatorOrderDecline
    {
        public int OrderID { get; set; }

        public Guid VaccinatorID { get; set; }

        public string DeclineReason { get; set; }
    }
}
