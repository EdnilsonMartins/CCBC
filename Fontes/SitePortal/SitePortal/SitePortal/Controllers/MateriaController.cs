using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class MateriaController : Controller
    {

        public ActionResult Index(string materiaid = "", string titulo = "")
        {
            int _materiaid;
            Int32.TryParse(materiaid, out _materiaid);

            Portal model = new Portal().CarregarModel();

            model.Conteudo = new DTO.Publicacao.Publicacao();
            model.Conteudo = model.Materias.Find(x => x.PublicacaoId == _materiaid);

            if (model.Conteudo == null && _materiaid != 0)
            {
                string site = "1";
                var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                Response.AppendCookie(siteCookie);
                HttpContext.Request.Cookies.Set(siteCookie);

                model = new Portal().CarregarModel();
                model.Conteudo = new DTO.Publicacao.Publicacao();
                model.Conteudo = model.Materias.Find(x => x.PublicacaoId == _materiaid);
            }
            if (model.Conteudo == null && _materiaid != 0)
            {
                string site = "2";
                var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                Response.AppendCookie(siteCookie);
                HttpContext.Request.Cookies.Set(siteCookie);

                model = new Portal().CarregarModel();
                model.Conteudo = new DTO.Publicacao.Publicacao();
                model.Conteudo = model.Materias.Find(x => x.PublicacaoId == _materiaid);
            }

            #region -->> Callback
            //Identificar o outro site que contém o conteúdo e redirecionar usando Callback:
            if (model.Conteudo == null && _materiaid != 0)
            {
                int siteId = (model.SiteId == 1 ? 2 : 1);
                model.Conteudo = new DAL.PublicacaoDAL().Carregar(siteId, model.IdiomaId, _materiaid, model.UsuarioId).Publicacao;
                if (model.Conteudo != null)
                {
                    var _callbackPortal = new HttpCookie("CallbackPortal", Url.Content("~/Materia/" + materiaid + "/" + titulo)) { HttpOnly = true };
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
