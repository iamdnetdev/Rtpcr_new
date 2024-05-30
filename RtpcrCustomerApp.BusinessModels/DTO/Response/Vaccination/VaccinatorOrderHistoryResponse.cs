using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DTO.Response.Vaccination
{
    public class VaccinatorOrderHistoryResponse : IResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Distance { get; set; }
        public List<VaccinePatientDetails> PatientDetails { get; set; }
    }

    public class VaccinePatientDetails
    {
        public bool VaccineDenied { get; set; }
        public Guid PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid VaccineProductID { get; set; }
        public string VaccineProductName { get; set; }
    }
}
