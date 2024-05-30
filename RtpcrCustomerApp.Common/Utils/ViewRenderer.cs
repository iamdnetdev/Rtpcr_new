using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RtpcrCustomerApp.Common.Utils
{
    public static class ViewRenderer
    {
        public static string RenderViewToString<T>(string viewPath, object model = null, bool partial = false) where T : Controller, new()
        {
            ViewEngineResult viewEngineResult;
            var controller = CreateController<T>();
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            var view = viewEngineResult.View;
            controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(controller.ControllerContext, view, controller.ViewData, controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }

        public static T CreateController<T>(RouteData routeData = null) where T : Controller, new()
        {
            T controller = new T();

            HttpContextBase wrapper;
            if (System.Web.HttpContext.Current != null)
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            else
                throw new InvalidOperationException("Cannot create Controller Context if no active HttpContext instance is available.");

            if (routeData == null)
                routeData = new RouteData();

            // add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }
    }
}
