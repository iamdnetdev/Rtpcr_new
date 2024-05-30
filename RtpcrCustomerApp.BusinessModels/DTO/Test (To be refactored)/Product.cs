namespace RtpcrCustomerApp.BusinessModels.DTO
{
	using Common;
	using System;

	public class Product : ModelBase
	{
		public Guid ProductId { get; set; }
		public string ProductName { get; set; }
		public string Description { get; set; }
		public int CategoryId { get; set; }
		public bool IsActive { get; set; }
		public decimal Price { get; set; }
		public decimal CGST { get; set; }
		public decimal SGST { get; set; }
		public Guid LabId { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}
