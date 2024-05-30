using RtpcrCustomerApp.BusinessModels.Common;
using System;

namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Vaccination
{
    public class VaccineOrderConfirmation
    {
        public int OrderID { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime? ScheduleDate { get; set; }

        public string ScheduleSlot { get; set; }

        public string Address { get; set; }

        public string PrimaryUserEmail { get; set; }

        public string Vaccinator { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public decimal Price { get; set; }

        public decimal SGST { get; set; }

        public decimal CGST { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string OrderItemStatus { get; set; }

        public DateTime VaccinationDate { get; set; }
    }
}
