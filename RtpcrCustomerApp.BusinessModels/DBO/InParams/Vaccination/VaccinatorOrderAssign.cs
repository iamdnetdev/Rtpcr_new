namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Vaccination
{
    using System;

    public class VaccinatorOrderAssign
    {
        public int OrderID { get; set; }

        public Guid VaccinatorID { get; set; }
    }
}
