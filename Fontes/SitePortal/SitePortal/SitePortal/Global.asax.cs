using DAL;
using SitePortal.App_Start;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SitePortal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Config.tipo = typeof(HttpApplication);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var languageCookie = HttpContext.Current.Request.Cookies["lang"];

            if (languageCookie == null)
            {
                var langCookie = new HttpCookie("lang", "pt-BR") { HttpOnly = true };
                Response.AppendCookie(langCookie);
                languageCookie = HttpContext.Current.Request.Cookies["lang"];
            }

            CultureInfo cultureInfo = CultureInfo.GetCultureInfo(languageCookie.Value);

            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);


            //var siteCookie = HttpContext.Current.Request.Cookies["site"];
            //if (siteCookie == null)
            //{
            //    var sitCookie = new HttpCookie("site", "2") { HttpOnly = true };
            //    Response.AppendCookie(sitCookie);
            //    siteCookie = HttpContext.Current.Request.Cookies["site"];
            //}
        }
    }
}