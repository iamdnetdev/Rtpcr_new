namespace RtpcrCustomerApp.Api.Mappings
{
    using AutoMapper;
    using BusinessModels.Common;
    using BusinessModels.DBO;
    using BusinessModels.DBO.InParams.Admin;
    using BusinessModels.DBO.InParams.Common;
    using BusinessModels.DBO.InParams.Test;
    using BusinessModels.DBO.InParams.Vaccination;
    using BusinessModels.DBO.OutParams.Admin;
    using BusinessModels.DBO.OutParams.Common;
    using BusinessModels.DBO.OutParams.Test;
    using BusinessModels.DBO.OutParams.Vaccination;
    using BusinessModels.DTO;
    using BusinessModels.DTO.Request.Admin;
    using BusinessModels.DTO.Request.Common;
    using BusinessModels.DTO.Request.Test;
    using BusinessModels.DTO.Request.Vaccination;
    using BusinessModels.DTO.Response.Admin;
    using BusinessModels.DTO.Response.Common;
    using BusinessModels.DTO.Response.Test;
    using BusinessModels.DTO.Response.Vaccination;
    using Dapper;
    using Newtonsoft.Json;
    using RtpcrCustomerApp.Common.Utils;
    using System;
    using System.Collections.Generic;
    using static Dapper.SqlMapper;

    public class MappingConfigurationProvider
    {
        public MappingConfigurationProvider()
        {

        }

        public IConfigurationProvider GetMappingConfiguration()
        {
            return new MapperConfiguration(x =>
            {
                MapLocationToDatatable(x);
                MapVaccinePatientsToDatatable(x);
                MapTestPatientsToDatatable(x);
                MapToJsonString(x);
                x.CreateMap<GetAccountResult, GetAccountResponse>();
                x.CreateMap<CreateAccountRequest, AccountInsert>()
                    .ForMember(dest => dest.Password, act => act.MapFrom(src => EncryptionUtil.GetHash(src.MPIN)));
                x.CreateMap<UserSignInRequest, GenericSignInRequest>()
                    .ForMember(dest => dest.Password, act => act.MapFrom(src => src.MPIN));
                x.CreateMap<LoginResult, UserSignInResponse>();
                x.CreateMap<VaccinatorLoginResult, VaccinatorSignInResponse>();
                x.CreateMap<ChangeMPINRequest, AccountPasswordUpdate>()
                    .ForMember(dest => dest.Password, act => act.MapFrom(src => EncryptionUtil.GetHash(src.MPIN)));
                x.CreateMap<ChangePasswordRequest, AccountPasswordUpdate>()
                    .ForMember(dest => dest.Password, act => act.MapFrom(src => EncryptionUtil.GetHash(src.Password)));
                x.CreateMap<AccountUpdateRequest, AccountUpdate>();
                x.CreateMap<AccountProfileUpdateRequest, AccountProfileUpdate>();

                x.CreateMap<DeviceDetailsResult, DeviceDetailsResponse>();

                x.CreateMap<CompanyResult, CompanyResponse>();

                x.CreateMap<VaccineProductResult, VaccineProductResponse>();
                x.CreateMap<VaccineOrderHistoryResult, VaccineOrderHistoryResponse>();
                x.CreateMap<VaccineOrderRequest, VaccineOrderInsert>()
                    .ForMember(dest => dest.ScheduleTime, act => act.MapFrom(src => MapVaccineScheduleDate(src.ScheduleDate, src.ScheduleSlot)));

                x.CreateMap<VaccinatorLocationResult, VaccinatorLocationResponse>();
                x.CreateMap<VaccinatorDetailsResult, VaccinatorDetailsResponse>();
                x.CreateMap<VaccinatorLocationRequest, VaccinatorLocationUpdate>();
                
                x.CreateMap<VaccinatorAssignedOrderResult, VaccinatorAssignedOrderDetails>();
                x.CreateMap<VaccinatorAssignedOrderResult, VaccinatorAssignedOrderResponse>();

                x.CreateMap<VaccinatorOrderHistoryResult, VaccineOrderHistoryResponse>();
                x.CreateMap<VaccinatorOrderHistoryResult, VaccinatorOrderHistoryResponse>();
                x.CreateMap<VaccinatorOrderHistoryResult, VaccinePatientDetails>();

                x.CreateMap<CollectorOrderHistoryResult, CollectorOrderHistoryResponse>();
                x.CreateMap<CollectorOrderHistoryResult, TestPatientDetails>();

                //x.CreateMap<VaccineOrderRequest, VaccineOrderInsert>();
                x.CreateMap<VaccineOrderItemResult, VaccineOrderItemResponse>();

                x.CreateMap<AssignVaccinatorRequest, VaccineOrderVaccinatorUpdate>();
                x.CreateMap<VaccinePatientUpdateRequest, VaccineOrderPatientUpdate>();
                x.CreateMap<VaccinePaymentUpdateRequest, VaccineOrderPaymentUpdate>()
                    .ForMember(dest => dest.PaymentReference, act => act.MapFrom(src => src.RazorPaymentID))
                    .ForMember(dest => dest.PaymentSucceeded, act => act.MapFrom(src => !string.IsNullOrEmpty(src.RazorPaymentID)))
                    .ForMember(dest => dest.PaymentError, act => act.MapFrom(src => string.IsNullOrEmpty(src.RazorPaymentID) ? "Payment failed" : null));

                x.CreateMap<VaccinatorAssignOrderRequest, VaccinatorOrderAssign>();
                x.CreateMap<VaccinatorAcceptOrderRequest, VaccinatorOrderAccept>();
                x.CreateMap<VaccinatorDeclineOrderRequest, VaccinatorOrderDecline>();
                //x.CreateMap<VaccinePaymentUpdateRequest, RazorPayment>()
                //    .ForMember(dest => dest.OrderID, act => act.MapFrom(src => src.RazorOrderID))
                //    .ForMember(dest => dest.PaymentID, act => act.MapFrom(src => src.RazorPaymentID))
                //    .ForMember(dest => dest.Signature, act => act.MapFrom(src => src.RazorSignature));


                x.CreateMap<TestProductResult, TestProductResponse>();
                x.CreateMap<TestOrderItemResult, TestOrderItemResponse>();
                x.CreateMap<TestOrderHistoryResult, TestOrderHistoryResponse>();
                x.CreateMap<TestOrderRequest, TestOrderInsert>()
                    .ForMember(dest => dest.ScheduleTime, act => act.MapFrom(src => MapVaccineScheduleDate(src.ScheduleDate, src.ScheduleSlot)));
                x.CreateMap<CollectorDetailsResult, CollectorDetailsResponse>();
                
                x.CreateMap<CollectorAssignedOrderResult, CollectorAssignedOrderDetails>();
                x.CreateMap<CollectorAssignedOrderResult, CollectorAssignedOrderResponse>();
                
                x.CreateMap<CollectorLocationResult, CollectorLocationResponse>();
                x.CreateMap<CollectorOrderHistoryResult, TestOrderHistoryResponse>();
                x.CreateMap<CollectorLocationRequest, CollectorLocationUpdate>();
                x.CreateMap<TestPatientUpdateRequest, TestOrderPatientUpdate>();
                x.CreateMap<TestPaymentUpdateRequest, TestOrderPaymentUpdate>()
                    .ForMember(dest => dest.PaymentReference, act => act.MapFrom(src => src.RazorPaymentID))
                    .ForMember(dest => dest.PaymentSucceeded, act => act.MapFrom(src => !string.IsNullOrEmpty(src.RazorPaymentID)))
                    .ForMember(dest => dest.PaymentError, act => act.MapFrom(src => string.IsNullOrEmpty(src.RazorPaymentID) ? "Payment failed" : null));

                //x.CreateMap<TestPaymentUpdateRequest, RazorPayment>()
                //    .ForMember(dest => dest.OrderID, act => act.MapFrom(src => src.RazorOrderID))
                //    .ForMember(dest => dest.PaymentID, act => act.MapFrom(src => src.RazorPaymentID))
                //    .ForMember(dest => dest.Signature, act => act.MapFrom(src => src.RazorSignature));

                x.CreateMap<AssignCollectorRequest, TestSampleCollectorUpdate>();
                x.CreateMap<CollectorAcceptOrderRequest, CollectorOrderAccept>();
                x.CreateMap<CollectorDeclineOrderRequest, CollectorOrderDecline>();
                x.CreateMap<TestOrderByRegionResult, TestOrderByRegionResponse>();
                x.CreateMap<AssignCollectorRequest, TestOrderCollectorUpdate>();
                x.CreateMap<TestOrderAdminResult, TestOrderAdminResponse>();
                x.CreateMap<VaccineOrderAdminResult, VaccineOrderAdminResponse>();
            });
        }

        private DateTime? MapVaccineScheduleDate(DateTime? scheduleDate, string scheduleSlot)
        {
            if (scheduleDate == null) return null;
            return scheduleDate.Value.Date.AddHours(VaccinationSlots.Slots[scheduleSlot]);
        }

        private void MapToJsonString(IMapperConfigurationExpression expr)
        {
            expr.CreateMap<object, string>().ConvertUsing(x => ToJsonString(x));
        }

        private string ToJsonString(object obj)
        {
            if (obj == null) return null;
            if (obj.GetType() == typeof(string)) return (string)obj;
            return JsonConvert.SerializeObject(obj);
        }

        private void MapVaccinePatientsToDatatable(IMapperConfigurationExpression expr)
        {
            expr.CreateMap<List<VaccinePatientRequest>, ICustomQueryParameter>().ConvertUsing(data => BuildVaccinePatientsDatatable(data));
        }

        private void MapLocationToDatatable(IMapperConfigurationExpression expr)
        {
            expr.CreateMap<LocationRequest, ICustomQueryParameter>().ConvertUsing(location => BuildLocationDatatable(location));
        }

        private void MapTestPatientsToDatatable(IMapperConfigurationExpression expr)
        {
            expr.CreateMap<List<TestPatientRequest>, ICustomQueryParameter>().ConvertUsing(data => BuildTestPatientsDatatable(data));
        }

        private ICustomQueryParameter BuildLocationDatatable(LocationRequest location)
        {
            return new List<LocationRequest>() { location }
                    .ToDataTable(new List<string>() { "LocationID", "Latitude", "Longitude", "Address", "LocationName", "IsDefault" })
                    .AsTableValuedParameter("Location");
        }

        private ICustomQueryParameter BuildVaccinePatientsDatatable(List<VaccinePatientRequest> patients)
        {
            return patients
                    .ToDataTable(new List<string>() { "FirstName", "LastName", "Adhaar", "Age", "DateOfBirth", "Email", "Phone", "VaccineProductID" })
                    .AsTableValuedParameter("PatientVaccine");
        }
        private ICustomQueryParameter BuildTestPatientsDatatable(List<TestPatientRequest> patients)
        {
            return patients
                    .ToDataTable(new List<string>() { "FirstName", "LastName", "Adhaar", "Age", "DateOfBirth", "Email", "Phone", "TestProductID" })
                    .AsTableValuedParameter("PatientTest");
        }

    }
}