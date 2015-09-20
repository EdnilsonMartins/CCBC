using BackofficeCMS.Models;
using DAL;
using DTO.Arquivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            
            var CMS_UsuarioId = HttpContext.Request.Cookies["CMS_UsuarioId"];

            if (CMS_UsuarioId == null || (CMS_UsuarioId != null && String.IsNullOrEmpty(CMS_UsuarioId.Value)))
            {
                return RedirectPermanent("~/Login");
            }
            else
            {
                CMS model = new CMS().CarregarModel();

                model.MenuId = 1;
                model.NavegacaoBarra.ExibirNavegacao = true;
                model.NavegacaoBarra.Titulo = "Dashboard";
                model.NavegacaoBarra.Resumo = "";
                model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
                model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "#", Rotulo = "Dashboard" });

                model.ListaPublicacao = model.CarrgearPublicacaoPendente();

                return View(model);
            }
            
        }

        public ActionResult SessionCulture(string lang)
        {
            var langCookie = new HttpCookie("lang", lang) { HttpOnly = true };
            Response.AppendCookie(langCookie);

            ConfigurarDadosDeCultura(lang);

            return RedirectToAction("Index", "Login", new { culture = lang });
        }

        public ActionResult SessionSite(int SiteId, string SiteNome)
        {
            var siteId = new HttpCookie("CMS_SiteId", SiteId.ToString()) { HttpOnly = true };
            Response.AppendCookie(siteId);

            var siteNome = new HttpCookie("CMS_SiteNome", SiteNome) { HttpOnly = true };
            Response.AppendCookie(siteNome);

            return RedirectToAction("Index", "Home");
        }

        private void ConfigurarDadosDeCultura(string lang)
        {
            var currentCulture = HttpContext.Request.Cookies["lang"] != null
                ? HttpContext.Request.Cookies["lang"].Value
                : "en-US";
        }

    }
}
