using Hangfire;
using log4net;
using log4net.Config;
using RtpcrCustomerApp.Api.App_Start;
using RtpcrCustomerApp.Api.Common;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;

namespace RtpcrCustomerApp.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            //set the property to our new object
            LogicalThreadContext.Properties["ModuleName"] = new ModuleNameHelper();

            //log.Debug("WebApiApplication_BeginRequest");
        }

        private void OnError(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }

    public class ModuleNameHelper
    {
        public ModuleNameHelper()
        {

        }
        public override string ToString()
        {
            return DateTime.Now.Second % 2 == 0 ? "Consumer" : "Accounts";
        }
    }
}
