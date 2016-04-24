using DTO.Menu;
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
            //var UsuarioId = "";
            //var UsuarioNome = "";
            //if (Session["UsuarioId"] != null)
            //    UsuarioId = Session["UsuarioId"].ToString();
            //if (Session["UsuarioNome"] != null)
            //    UsuarioNome = Session["UsuarioNome"].ToString();

            int _materiaid;
            Int32.TryParse(materiaid, out _materiaid);

            Portal model = new Portal().CarregarModel(false);
            model.Conteudo = new DTO.Publicacao.Publicacao();
            model.Conteudo = model.Materias.Find(x => x.PublicacaoId == _materiaid);

            var siteId_Entrada = model.SiteId;
            if (model.SiteId == 0) model.SiteId = 2;

            if (model.Conteudo == null && _materiaid != 0 && model.SiteId == 2)
            {
                string site = "1";
                var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                Response.AppendCookie(siteCookie);
                HttpContext.Request.Cookies.Set(siteCookie);

                model = new Portal().CarregarModel(false);
                model.Conteudo = new DTO.Publicacao.Publicacao();
                model.Conteudo = model.Materias.Find(x => x.PublicacaoId == _materiaid);
            }
            if (model.Conteudo == null && _materiaid != 0 && model.SiteId == 1){
                string site = "2";
                var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                Response.AppendCookie(siteCookie);
                HttpContext.Request.Cookies.Set(siteCookie);

                model = new Portal().CarregarModel(false);
                model.Conteudo = new DTO.Publicacao.Publicacao();
                model.Conteudo = model.Materias.Find(x => x.PublicacaoId == _materiaid);
            }


            if (model.Conteudo == null)
            {
                
                model.ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Home",
                    Rotulo = "Home"
                });


                //Default
                if (_materiaid != 0)
                {
                    string site = siteId_Entrada.ToString();
                    var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                    Response.AppendCookie(siteCookie);
                    HttpContext.Request.Cookies.Set(siteCookie);

                    model = new Portal().CarregarModel(false);

                    if (model.SiteId == 1)
                    {
                        int paginaPadrao = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ConteudoRestritoIDCCBC"].ToString());
                        model.Conteudo = new DTO.Publicacao.Publicacao();
                        model.Conteudo = model.Paginas.Find(x => x.PublicacaoId == paginaPadrao);
                    }

                    if (model.SiteId == 2)
                    {
                        int paginaPadrao = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ConteudoRestritoIDCAM"].ToString());
                        model.Conteudo = new DTO.Publicacao.Publicacao();
                        model.Conteudo = model.Paginas.Find(x => x.PublicacaoId == paginaPadrao);
                    }
                }
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


            if (_materiaid != null && model.Conteudo != null)
            {
                if (_materiaid == model.Conteudo.PublicacaoId)
                {
                    model.CarregarMenuInterna((int)_materiaid);
                }
                if (model.Conteudo != null) model.CarregarMenuTree(_materiaid);
                model.CarregarBannerInterna((int)_materiaid);
            }


            return View(model);
        }

    }
}
