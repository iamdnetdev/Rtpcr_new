namespace RtpcrCustomerApp.BusinessModels.DTO
{
    using Common;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Lab : ModelBase
    {
		public Guid LabId { get; set; }
		public string LabName { get; set; }
		public string LabSerialId { get; set; }
		public string ICMRID { get; set; }
	}
}
