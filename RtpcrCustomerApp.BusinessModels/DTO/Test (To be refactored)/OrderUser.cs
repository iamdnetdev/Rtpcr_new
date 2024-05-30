using RtpcrCustomerApp.BusinessModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.BusinessModels.DTO
{
	public class OrderUser : ModelBase
	{
		[Required]
		public Guid UserID { get; set; }
		//public string FirstName { get; set; }
		//public string LastName { get; set; }

		[Required]
		public bool IsAssociateWithCompany { get; set; }

		public Guid CompanyId { get; set; }
		public string EmployeeId { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string LocationName { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }

		[Required]
		public bool OrderStatus { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		public decimal Amount { get; set; }

		[Required]
		public Guid LabId { get; set; }

		[Required]
		public DateTime TestSampleCollectionDate { get; set; }

		[Required]
		public List<TestOrder> TestUserOrders { get; set; }

	}

	public class TestOrder
	{
		[Required]
		public Guid UserID { get; set; }

		[Required]
		public Guid ProductId { get; set; }
	}
}
