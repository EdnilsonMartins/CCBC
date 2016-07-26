﻿using DTO.Menu;
using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class InternaController : Controller
    {

        public ActionResult Index(string internaid = "", string titulo = "", string lang = "")
        {
            if (!String.IsNullOrEmpty(lang))
            {
                var langCookie = new HttpCookie("lang", lang) { HttpOnly = true };
                Response.AppendCookie(langCookie);
                HttpContext.Request.Cookies.Set(langCookie);

                ConfigurarDadosDeCultura(lang);
            } 
            
            
            var UsuarioId = "";
            var UsuarioNome = "";
            if (Session["UsuarioId"] != null)
                UsuarioId = Session["UsuarioId"].ToString();
            if (Session["UsuarioNome"] != null)
                UsuarioNome = Session["UsuarioNome"].ToString();

            int _internaid;
            Int32.TryParse(internaid, out _internaid);

            Portal model = new Portal().CarregarModel(false);
            model.Conteudo = new DTO.Publicacao.Publicacao();
            model.Conteudo = model.Paginas.Find(x => x.PublicacaoId == _internaid);

            var siteId_Entrada = model.SiteId;
            if (model.SiteId == 0) model.SiteId = 2;

            if (model.Conteudo == null && _internaid != 0 && model.SiteId == 2)
            {
                string site = "1";
                var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                Response.AppendCookie(siteCookie);
                HttpContext.Request.Cookies.Set(siteCookie);

                model = new Portal().CarregarModel(false);
                model.Conteudo = new DTO.Publicacao.Publicacao();
                model.Conteudo = model.Paginas.Find(x => x.PublicacaoId == _internaid);
            }
            if (model.Conteudo == null && _internaid != 0 && model.SiteId == 1){
                string site = "2";
                var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                Response.AppendCookie(siteCookie);
                HttpContext.Request.Cookies.Set(siteCookie);

                model = new Portal().CarregarModel(false);
                model.Conteudo = new DTO.Publicacao.Publicacao();
                model.Conteudo = model.Paginas.Find(x => x.PublicacaoId == _internaid);
            }


            if (model.Conteudo == null)
            {
                model.ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Internas",
                    Rotulo = "Internas"
                });

                model.ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Home",
                    Rotulo = "Home"
                });


                //Default
                if (_internaid != 0)
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
            if (model.Conteudo == null && _internaid != 0)
            {
                int siteId = (model.SiteId == 1 ? 2 : 1);
                model.Conteudo = new DAL.PublicacaoDAL().Carregar(siteId, model.IdiomaId, _internaid, model.UsuarioId).Publicacao;
                if (model.Conteudo != null)
                {
                    var _callbackPortal = new HttpCookie("CallbackPortal", Url.Content("~/Interna/" + _internaid + "/" + titulo)) { HttpOnly = true };
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

                //Novo callback usado para marcar a página anterior: Será utilizado qdo usuario trocar idioma.
                if (model.Conteudo != null)
                {
                    var _callbackPortal_Anterior = new HttpCookie("CallbackPortal_Anterior", Url.Content("~/Interna/" + _internaid + "/" + titulo)) { HttpOnly = true };
                    Response.AppendCookie(_callbackPortal_Anterior);
                    HttpContext.Request.Cookies.Set(_callbackPortal_Anterior);
                }
            }
            #endregion


            if (_internaid != null && model.Conteudo != null)
            {
                if (_internaid == model.Conteudo.PublicacaoId)
                {
                    model.CarregarMenuInterna((int)_internaid);
                }
                if (model.Conteudo != null) model.CarregarMenuTree(_internaid);
                model.CarregarBannerInterna((int)_internaid);
            }


            return View(model);
        }

        private void ConfigurarDadosDeCultura(string lang)
        {
            var currentCulture = HttpContext.Request.Cookies["lang"] != null
                ? HttpContext.Request.Cookies["lang"].Value
                : "en-US";

            //if (currentCulture != lang)
            //{
            //    ConfigurarUrlDeConsulta();
            //}
        }
    }
}
