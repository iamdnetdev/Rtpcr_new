using Autofac;
using Autofac.Integration.WebApi;
using log4net;
using RtpcrCustomerApp.Api.Mappings;
using RtpcrCustomerApp.Api.Security;
using RtpcrCustomerApp.BusinessServices.Account;
using RtpcrCustomerApp.BusinessServices.Admin;
using RtpcrCustomerApp.BusinessServices.Common;
//using RtpcrCustomerApp.BusinessServices.Company;
//using RtpcrCustomerApp.BusinessServices.Family;
using RtpcrCustomerApp.BusinessServices.Interfaces;
using RtpcrCustomerApp.BusinessServices.Test;
//using RtpcrCustomerApp.BusinessServices.Product;
//using RtpcrCustomerApp.BusinessServices.User;
using RtpcrCustomerApp.BusinessServices.Vaccination;
using RtpcrCustomerApp.Common.Interfaces;
using RtpcrCustomerApp.Common.Logging;
using RtpcrCustomerApp.Common.Models;
using RtpcrCustomerApp.Common.Utils;
using RtpcrCustomerApp.Repositories.Account;
using RtpcrCustomerApp.Repositories.Admin;
using RtpcrCustomerApp.Repositories.Common;
//using RtpcrCustomerApp.Repositories.Company;
//using RtpcrCustomerApp.Repositories.Family;
using RtpcrCustomerApp.Repositories.Interfaces;
using RtpcrCustomerApp.Repositories.Test;
//using RtpcrCustomerApp.Repositories.Product;
//using RtpcrCustomerApp.Repositories.User;
using RtpcrCustomerApp.Repositories.Vaccination;
using System;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace RtpcrCustomerApp.Api.Common
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            //   builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => 
            //t.Name.EndsWith("Controller"));
            builder.RegisterInstance<Func<Type, ILog>>((type) => LogManager.GetLogger(type));
            builder.RegisterType<LoggerFactory>().As<ILoggerFactory>().SingleInstance();
            builder.RegisterInstance<IDbSetting>(new DbSettings
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString,
                Name = "AppDb",
                SchemaName = "dbo"
            }).SingleInstance();

            builder.RegisterType<MappingConfigurationProvider>();
            builder.Register(ctx => ctx.Resolve<MappingConfigurationProvider>().GetMappingConfiguration());
            builder.RegisterType<ObjectMapper>()
                  .As<IMapper>()
                  .SingleInstance();

            builder.RegisterType<TokenManager>()
                  .As<ITokenManager>()
                  .SingleInstance();

            builder.RegisterType<RoleRepository>()
                   .As<IRoleRepository>()
                   .SingleInstance();

            builder.RegisterType<AccountRepository>()
                   .As<IAccountRepository>()
                   .InstancePerRequest();

            builder.RegisterType<AccountService>()
                   .As<IAccountService>()
                   .InstancePerRequest();

            builder.RegisterType<AdminRepository>()
                   .As<IAdminRepository>()
                   .InstancePerRequest();

            builder.RegisterType<AdminService>()
                   .As<IAdminService>()
                   .InstancePerRequest();

            builder.RegisterType<TestConsumerRepository>()
                  .As<ITestConsumerRepository>()
                  .InstancePerRequest();

            builder.RegisterType<TestConsumerService>()
                   .As<ITestConsumerService>()
                   .InstancePerRequest();

            builder.RegisterType<CollectorRepository>()
                  .As<ICollectorRepository>()
                  .InstancePerRequest();

            builder.RegisterType<CollectorService>()
                   .As<ICollectorService>()
                   .InstancePerRequest();

            //builder.RegisterType<FamilyRepository>()
            //       .As<IFamilyRepository>()
            //       .InstancePerRequest();

            //builder.RegisterType<FamilyService>()
            //       .As<IFamilyService>()
            //       .InstancePerRequest();

            //builder.RegisterType<ProductRepository>()
            //      .As<IProductRepository>()
            //      .InstancePerRequest();

            //builder.RegisterType<ProductService>()
            //       .As<IProductService>()
            //       .InstancePerRequest();

            //builder.RegisterType<CompanyRepository>()
            //      .As<ICompanyRepository>()
            //      .InstancePerRequest();

            //builder.RegisterType<CompanyService>()
            //       .As<ICompanyService>()
            //       .InstancePerRequest();

            //builder.RegisterType<UserRepository>()
            //       .As<IUserRepository>()
            //       .InstancePerRequest();

            //builder.RegisterType<UserService>()
            //       .As<IUserService>()
            //       .InstancePerRequest();

            //builder.RegisterType<OrderUserRepository>()
            //       .As<IOrderUserRepository>()
            //       .InstancePerRequest();

            //builder.RegisterType<OrderUserService>()
            //       .As<IOrderUserService>()
            //       .InstancePerRequest();

            builder.RegisterType<VaccinatorRepository>()
                  .As<IVaccinatorRepository>()
                  .InstancePerRequest();

            builder.RegisterType<VaccinatorService>()
                   .As<IVaccinatorService>()
                   .InstancePerRequest();

            builder.RegisterType<VaccineConsumerRepository>()
                  .As<IVaccineConsumerRepository>()
                  .InstancePerRequest();

            builder.RegisterType<VaccineConsumerService>()
                   .As<IVaccineConsumerService>()
                   .InstancePerRequest();

            builder.RegisterType<DeviceDetailsRepository>()
                  .As<IDeviceDetailsRepository>()
                  .InstancePerRequest();

            builder.RegisterType<DeviceDetailsService>()
                   .As<IDeviceDetailsService>()
                   .InstancePerRequest();

            builder.RegisterType<EmailService>()
                   .As<IEmailService>()
                   .SingleInstance();

            builder.RegisterType<SMSService>()
                   .As<ISMSService>()
                   .SingleInstance();

            builder.RegisterType<PushNotificationService>()
                   .As<IPushNotificationService>()
                   .SingleInstance();

            builder.RegisterType<PaymentService>()
                   .As<IPaymentService>()
                   .SingleInstance();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            InstanceFactory.Container = Container;
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)Container); //Set the WebApi DependencyResolver

            return Container;
        }
    }
}