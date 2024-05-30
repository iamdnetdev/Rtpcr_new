using RtpcrCustomerApp.Api.Filters;
using RtpcrCustomerApp.Api.Security;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace RtpcrCustomerApp.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterWebApiFilters(HttpFilterCollection filters)
        {
            //filters.Add(new AppAuthorizeAttribute(new TokenManager()));
        }
    }
}
