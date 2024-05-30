using RtpcrCustomerApp.BusinessModels.Common;
using System;

namespace RtpcrCustomerApp.BusinessModels.DTO
{
    public class Category : ModelBase
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int Parent { get; set; }
		public bool IsActive { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}
