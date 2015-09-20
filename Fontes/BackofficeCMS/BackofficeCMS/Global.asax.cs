using BackofficeCMS.App_Start;
using DAL;
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

namespace BackofficeCMS
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

            //Escolhe um Site Padrão
            var siteCookie = HttpContext.Current.Request.Cookies["CMS_SiteId"];
            if (siteCookie == null)
            {
                var siteIdCookie = new HttpCookie("CMS_SiteId", "1") { HttpOnly = true };
                Response.AppendCookie(siteIdCookie);
                var siteNomeCookie = new HttpCookie("CMS_SiteNome", "CCBC") { HttpOnly = true };
                Response.AppendCookie(siteNomeCookie);
            }

            CultureInfo cultureInfo = CultureInfo.GetCultureInfo(languageCookie.Value);

            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        }
    }
}