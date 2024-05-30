namespace RtpcrCustomerApp.BusinessModels.DTO
{
    using Common;
    using System;

	public class Orders : ModelBase
	{
		public int OrderId { get; set; }
		public Guid UserId { get; set; }
		public DateTime OrderDate { get; set; }
		public bool OrderStatus { get; set; }
		public Guid LocationId { get; set; }
		public int Quantity { get; set; }
		public decimal Amount { get; set; }
		public Guid LabId { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }

	}
}
