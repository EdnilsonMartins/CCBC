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
                name: "Index22222",
                url: "Evento/{eventoid}/{titulo}/{lang}",
                defaults: new { controller = "Eventos", action = "Index", eventoid = string.Empty }
            );

            routes.MapRoute(
                name: "Indexsss",
                url: "Eventos/{eventoid}/{titulo}",
                defaults: new { controller = "Eventos", action = "Index", eventoid = string.Empty }
            );

            routes.MapRoute(
                name: "Indexsss2",
                url: "Eventos/{eventoid}/{titulo}",
                defaults: new { controller = "Eventos", action = "Index", eventoid = string.Empty }
            );

            routes.MapRoute(
                name: "IndexsssN2",
                url: "Noticias/{noticiaid}/{titulo}",
                defaults: new { controller = "Noticias", action = "Index", noticiaid = string.Empty } 
            );

            routes.MapRoute(
                name: "IndexN22",
                url: "Noticias/{noticiaid}/{titulo}/{lang}",
                defaults: new { controller = "Noticias", action = "Index", noticiaid = string.Empty }
            );

            routes.MapRoute(
                name: "Index3",
                url: "Materia/{materiaid}/{titulo}",
                defaults: new { controller = "Materia", action = "Index", materiaid = string.Empty }
            );

            routes.MapRoute(
                name: "Index32",
                url: "Materia/{materiaid}/{titulo}/{lang}",
                defaults: new { controller = "Materia", action = "Index", materiaid = string.Empty }
            );

            routes.MapRoute(
                name: "Index4",
                url: "Interna/{internaid}/{titulo}",
                defaults: new { controller = "Interna", action = "Index", internaid = string.Empty }
            );

            routes.MapRoute(
                name: "Index42",
                url: "Interna/{internaid}/{titulo}/{lang}",
                defaults: new { controller = "Interna", action = "Index", internaid = string.Empty }
            );

            routes.MapRoute(
                name: "ValidarSenha1",
                url: "MinhaConta/ValidarSenha/{ID}/{senha}",
                defaults: new { controller = "MinhaConta", action = "ValidarSenha", ID = string.Empty, senha = string.Empty }
            );

            routes.MapRoute(
                name: "ID",
                url: "MinhaConta/{ID}",
                defaults: new { controller = "MinhaConta", action = "Index", ID = string.Empty }
            );

            routes.MapRoute(
                name: "ID2",
                url: "MinhaConta/{ID}/{Fluxo}",
                defaults: new { controller = "MinhaConta", action = "Index", ID = string.Empty, Fluxo = string.Empty }
            );


            routes.MapRoute(
                name: "Email",
                url: "LembrarSenha/{Email}",
                defaults: new { controller = "LembrarSenha", action = "Index", Email = string.Empty }
            );

            //RevistasNewsletter
            routes.MapRoute(
                name: "RevistasNewsletter_1",
                url: "Revistas",
                defaults: new { controller = "Conteudo", action = "RevistasNewsletter", Id = 1 }
            );

            routes.MapRoute(
                name: "RevistasNewsletter_2",
                url: "Newsletter",
                defaults: new { controller = "Conteudo", action = "RevistasNewsletter", Id = 2 }
            );

            routes.MapRoute(
                name: "RevistasNewsletter_3",
                url: "Publicacoes",
                defaults: new { controller = "Conteudo", action = "RevistasNewsletter", Id = 3 }
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