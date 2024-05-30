namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Vaccination
{
    using System;

    public class VaccinatorOrderAccept
    {
        public int OrderID { get; set; }

        public Guid VaccinatorID { get; set; }
    }
}
