using log4net.Config;
using Owin;
using RtpcrCustomerApp.Api.Common;
using System.Web.Http;

namespace RtpcrCustomerApp.Api.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            XmlConfigurator.Configure();
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            HangfireConfig.ConfigureJobs(app);
        }
    }
}