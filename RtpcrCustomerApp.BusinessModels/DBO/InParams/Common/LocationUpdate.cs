using System;

namespace RtpcrCustomerApp.BusinessModels.DBO.InParams.Common
{
	public class LocationUpdate
	{
		public Guid LocationId { get; set; }
		public Guid UserID { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public string Address { get; set; }
		public string LocationName { get; set; }
		public bool IsDefault { get; set; }
	}
}