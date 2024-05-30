using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DTO.Request.Test
{
    public class TestPatientUpdateRequest : TestPatientRequest
    {
        public bool SampleCollectionDenied { get; set; }

        public int OrderID { get; set; }

        public Guid PatientID { get; set; }

        //public string AdhaarPhoto { get; set; }
    }
}
