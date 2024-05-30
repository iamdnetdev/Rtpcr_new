﻿namespace RtpcrCustomerApp.BusinessModels.DBO.OutParams.Vaccination
{
    using BusinessModels.Common;
    using System;

    public class VaccinatorAssignedOrderResult
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatusActive { get; set; }
        public VaccineOrderStatus OrderStatus { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Distance { get; set; }
        public bool VaccineDenied { get; set; }
        public Guid PatientID { get; set; }
        public string AdhaarPhoto { get; set; }
        public string VaccinePhoto { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adhaar { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid VaccineProductID { get; set; }
        public string VaccineProductName { get; set; }
        public string Address { get; set; }
    }
}
