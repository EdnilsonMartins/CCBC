using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SitePortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "SessionSite",
            //    url: "Home/SessionSite/{_site}",
            //    defaults: new { controller = "Home", action = "SessionSite", site = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "SessionCulture",
                url: "Home/SessionCulture/{lang}",
                defaults: new { controller = "Home", action = "SessionCulture", lang = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Index",
                url: "Evento/{eventoid}/{titulo}",
                defaults: new { controller = "Eventos", action = "Index", eventoid = string.Empty }
            );

            routes.MapRoute(
                name: "Indexsss",
                url: "Eventos/{eventoid}/{titulo}",
                defaults: new { controller = "Eventos", action = "Index", eventoid = string.Empty }
            );

            routes.MapRoute(
                name: "Index2",
                url: "Noticias/{noticiaid}/{titulo}",
                defaults: new { controller = "Noticias", action = "Index", noticiaid = string.Empty } 
            );

            routes.MapRoute(
                name: "Index3",
                url: "Materia/{materiaid}/{titulo}",
                defaults: new { controller = "Materia", action = "Index", materiaid = string.Empty }
            );

            routes.MapRoute(
                name: "Index4",
                url: "Interna/{internaid}",
                defaults: new { controller = "Interna", action = "Index", internaid = string.Empty }
            );

            //ToDo: NAO FUNCIONOU: Até funciona pra Index, mas p/ Listar dá pau!
            //routes.MapRoute(
            //    name: "Index5",
            //    url: "BuscaSocio/{AssociadoCategoriaId}",
            //    defaults: new { controller = "BuscaSocio", action = "Index", AssociadoCategoriaId = string.Empty }
            //);
            
            // This one should be the last:
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}