namespace RtpcrCustomerApp.Repositories.Common
{
    public static class Queries
    {
        public static class Role
        {
            public const string GetRoleAccessDetails = "up_RoleAccessDetails_GetAll";
        }

        public static class Account
        {
            public const string GetAccountById = "up_Account_GetById";
            public const string InsertAccount = "up_Account_Insert";
            public const string UpdateAccount = "up_Account_Update";
            public const string UpdateProfileAccount = "up_Account_Profile_Update";
            public const string LockUnlockAccount = "up_Account_LockUnlock";
            public const string GetHash = "up_Account_GetHash";
            public const string Login = "up_Account_Login";
            public const string ChangePassword = "up_Account_Password_Update";
        }

        public static class DeviceDetails
        {
            public const string GetAllDevices = "up_DeviceDetails_GetAll";
        }

        public static class ConsumerProfile
        {
            public const string GetAccountById = "up_Consumer_GetById";
            public const string InsertAccount = "up_Consumer_Insert";
            public const string UpdateAccount = "up_Consumer_Update";
            public const string LockUnlockAccount = "up_Consumer_LockUnlock";
        }

        public static class Products
        {
            public const string GetProductsByLab = "up_Product_GetAll";
            public const string GetProduct = "up_Product_GetById";
            public const string InsertProduct = "up_Product_Insert";
            public const string UpdateProduct = "up_Product_Update";
        }

        public static class Category
        {
            public const string GetCategory = "up_Category_GetById";
            public const string InsertCategory = "up_Category_Insert";
            public const string UpdateCategory = "up_Category_Update";
        }

        public static class Location
        {
            public const string GetLocation = "up_Location_GetById";
            public const string InsertLocation = "up_Location_Insert";
            public const string UpdateLocation = "up_Location_Update";
        }

        public static class Family
        {
            public const string GetFamilyMember = "up_Family_GetById";
            public const string InsertFamily = "up_Family_Insert";
            public const string UpdateFamily = "up_Family_Update";
            public const string GetFamilyMembers = "up_Family_GetByPrimaryUserID";
        }


        public static class OrderUser
        {
            public const string GetOrderUser = "up_OrderUser_GetById";
            public const string InsertOrderUser = "up_Order_Insert";
        }

        public static class User
        {
            public const string GetUserSignIn = "up_User_SignIn";
        }

        public static class Lab
        {
            public const string GetLabByuserID = "up_Lab_GetAll";
        }

        public static class Company
        {
            public const string GetCompanyAll = "up_Company_GetAll";
        }

        public static class Vaccinator
        {
            public const string Login = "up_Vaccinator_Login";
            public const string GetCurrentLocation = "up_VaccinatorLocation_Current_Get";
            public const string GetCurrentLocationTrack = "up_Vaccinator_Location_Track";
            public const string GetVaccinators = "up_Vaccinator_GetAll";
            public const string PurgeLocationTrail = "up_VaccinatorLocationTrail_Purge";
            public const string UpdateLocation = "up_VaccinatorLocation_Update";
            public const string UpdateLoggedInStatus = "up_VaccinatorStatus_Update";
            //public const string GetOpenOrders = "up_VaccinatorOpenOrders_Get";
            public const string GetAssignedOrders = "up_VaccinatorAssignedOrders_Get";
            public const string GeOrderHistory = "up_Vaccinator_Order_History";
            public const string UpdatePaymentDetails = "up_VaccineOrder_PaymentDetails_Update";
            public const string UpdatePatientAadharPhoto = "up_VaccineOrder_Adhaar_update";
            public const string UpdatePatientVaccinePhoto = "up_VaccineOrder_Vaccine_update";
            public const string UpdatePatientDetails = "up_VaccineOrder_PatientDetails_Update";
            public const string UpdateVaccineGivenStatus = "up_VaccineOrder_VaccineGiven_Update";
            public const string AssignOrder = "up_VaccineOrder_Vaccinator_Update";
            public const string AcceptOrder = "up_VaccinatorOrderAccept_Update";
            public const string DeclineOrder = "up_VaccinatorOrderDecline_Update";
            public const string AssignVaccinatorForScheduledOrders = "up_VaccineOrderScheduled_Vaccinator_Update";
        }

        public static class Collector
        {
            public const string Login = "up_Collector_Login";
            public const string GetCurrentLocation = "up_CollectorLocation_Current_Get";
            public const string GetCurrentLocationTrack = "up_Collector_Location_Track";
            public const string GetCollectors = "up_Collector_GetAll";
            public const string PurgeLocationTrail = "up_CollectorLocationTrail_Purge";
            public const string UpdateLocation = "up_CollectorLocation_Update";
            public const string UpdateLoggedInStatus = "up_CollectorStatus_Update";
            public const string GetOrdersByRegion = "up_TestOrderByRegion_Get";
            public const string GetAssignedOrders = "up_CollectorAssignedOrders_Get";
            public const string GetOrderHistory = "up_Collector_Order_History";
            public const string UpdatePaymentDetails = "up_TestOrder_PaymentDetails_Update";
            public const string UpdatePatientDetails = "up_TestOrder_PatientDetails_Update";
            public const string UpdateTestGivenStatus = "up_TestOrder_TestGiven_Update";
            public const string AssignCollector = "up_TestOrder_Collector_Update";
            public const string UpdatePatientAadharPhoto = "up_TestOrder_Adhaar_update";
            public const string AcceptOrder = "up_TestCollectorOrderAccept_Update";
            public const string DeclineOrder = "up_TestCollectorOrderDecline_Update";
            public const string AssignCollectorForScheduledOrders = "up_TestOrderScheduled_Collector_Update";
        }

        public static class VaccineConsumer
        {
            public const string InsertPatient = "up_VaccinePatient_Insert";
            public const string UpdatePatient = "up_VaccinePatient_Update";
            public const string GetPatients = "up_VaccinePatient_Get";
            public const string UpdateLocation = "up_VaccinatorLocation_Update";
            public const string GetAvailableVaccinesByRegion = "up_Vaccine_GetAll";
            public const string PlaceOrder = "up_VaccineOrder_Insert";
            public const string GetOrderDetailsConfirmationMail = "up_VaccineOrderDetails_ForMail_Get";
            public const string GetOrderHistory = "up_VaccineOrder_GetAll";
            public const string GetOrderTestDetails = "up_VaccineOrder_Details_GetAll";
            public const string UpdatePaymentDetails = "up_VaccineOrder_PaymentDetails_Update";
            public const string CancelOrderDetails = "up_VaccineOrder_Cancelled_Update";
            public const string UpdateRefundDetails = "up_VaccineOrder_Refund_Update";
        }

        public static class Admin
        {
            public const string AssignVaccinator = "up_VaccineOrder_Vaccinator_Update";
            public const string GetOrdersByRegion = "up_VaccineOrderByRegion_Get";
            public const string AssignCollector = "up_TestOrder_Collector_Update";
            public const string GetCollectorOrdersByRegion = "up_TestOrderByRegion_Get";
        }

        public static class TestConsumer
        {
            public const string InsertPatient = "up_TestPatient_Insert";
            public const string UpdatePatient = "up_TestPatient_Update";
            public const string GetPatients = "up_TestPatient_Get";
            public const string UpdateLocation = "up_CollectorLocation_Update";
            public const string GetAvailableTestsByRegion = "up_Test_GetAll";
            public const string GetAvailableTestsByLab = "up_ProductsInLab_GetAll";
            public const string PlaceOrder = "up_TestOrder_Insert";
            public const string GetOrderHistory = "up_TestOrder_GetAll";
            public const string GetOrderTestDetails = "up_TestOrder_Details_GetAll";
            public const string UpdatePaymentDetails = "up_TestOrder_PaymentDetails_Update";
            public const string CancelOrderDetails = "up_TestOrder_Cancelled_Update";
            public const string UpdateRefundDetails = "up_TestOrder_Refund_Update";
            public const string GetOrderDetailsConfirmationMail = "up_TestOrderDetails_ForMail_Get";
        }
    }
}
