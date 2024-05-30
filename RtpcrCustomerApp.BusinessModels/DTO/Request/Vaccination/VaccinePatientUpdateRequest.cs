using System;

namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Vaccination
{
    public class VaccinePatientUpdateRequest : VaccinePatientRequest
    {
        public bool VaccineDenied { get; set; }

        public int OrderID { get; set; }

        public Guid PatientID { get; set; }

        //public string AdhaarPhoto { get; set; }

        //public string VaccinePhoto { get; set; }
    }
}
