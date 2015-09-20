using DTO.Menu;
using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class EventosController : Controller
    {
        
        public ActionResult Index(string eventoid = "", string titulo = "")
        {
            int _eventoId;
            Int32.TryParse(eventoid, out _eventoId);

            Portal model = new Portal().CarregarModel();

            model.Conteudo = new DTO.Publicacao.Publicacao();
            model.Conteudo = model.Eventos.Find(x => x.PublicacaoId == _eventoId);

            if (model.Conteudo == null && _eventoId != 0)
            {
                string site = "1";
                var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                Response.AppendCookie(siteCookie);
                HttpContext.Request.Cookies.Set(siteCookie);

                model = new Portal().CarregarModel();
                model.Conteudo = new DTO.Publicacao.Publicacao();
                model.Conteudo = model.Eventos.Find(x => x.PublicacaoId == _eventoId);
            }
            if (model.Conteudo == null && _eventoId != 0)
            {
                string site = "2";
                var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                Response.AppendCookie(siteCookie);
                HttpContext.Request.Cookies.Set(siteCookie);

                model = new Portal().CarregarModel();
                model.Conteudo = new DTO.Publicacao.Publicacao();
                model.Conteudo = model.Eventos.Find(x => x.PublicacaoId == _eventoId);
            }

            if (model.Conteudo == null)
            {
                model.ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Eventos",
                    Rotulo = "Eventos"
                });

                model.ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Home",
                    Rotulo = "Home"
                }); 
            }

            #region -->> Callback
            //Identificar o outro site que contém o conteúdo e redirecionar usando Callback:
            if (model.Conteudo == null && _eventoId != 0)
            {
                int siteId = (model.SiteId == 1 ? 2 : 1);
                model.Conteudo = new DAL.PublicacaoDAL().Carregar(siteId, model.IdiomaId, _eventoId, model.UsuarioId).Publicacao;
                if (model.Conteudo != null)
                {
                    var _callbackPortal = new HttpCookie("CallbackPortal", Url.Content("~/Eventos/" + _eventoId + "/" + titulo)) { HttpOnly = true };
                    Response.AppendCookie(_callbackPortal);
                    HttpContext.Request.Cookies.Set(_callbackPortal);

                    Response.RedirectPermanent(Url.Content("~/Home/SessionSite?_site=" + siteId));
                    return null;
                }
            }
            else
            {
                var _callbackPortal = new HttpCookie("CallbackPortal", null) { HttpOnly = true };
                Response.AppendCookie(_callbackPortal);
                HttpContext.Request.Cookies.Set(_callbackPortal);
            }
            #endregion

            return View(model);
        }

    }
}
