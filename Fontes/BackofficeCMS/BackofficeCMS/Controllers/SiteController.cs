using BackofficeCMS.Models;
using DAL;
using DTO.Site;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class SiteController : Controller
    {
        public ActionResult CadSite(int SiteId)
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 200; // CRM
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Site";
            model.NavegacaoBarra.Resumo = "relação de sites da plataforma...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Site/ListaSite", Rotulo = "Site" });

            ViewBag.SiteId = SiteId;

            return View(model);
        }

        public ActionResult ListaSite()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 200; // CRM
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Site";
            model.NavegacaoBarra.Resumo = "relação de sites da plataforma...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Site/ListaSite", Rotulo = "Site" });


            return View(model);
        }

        public ActionResult ListarSite()
        {

            SiteDAL siteDAL = new SiteDAL();
            List<Site> listaSites = siteDAL.ListarSite(null);

            return Json(listaSites, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarSite(int SiteId)
        {
            int _SiteId = GetCurrentSite();

            SiteDAL siteDAL = new SiteDAL();
            var resposta = siteDAL.CarregarSite(SiteId);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarSite(string Site, string SiteOld)
        {
            var form = (JObject)JsonConvert.DeserializeObject(Site);

            Site _anterior = new Site();
            Site _novo = new Site();

            _novo.SiteId = (int)Util.GetValue<int>(form, "SiteId");
            _novo.Descricao = (string)Util.GetValue<string>(form, "Descricao");
            _novo.Tags = (string)Util.GetValue<string>(form, "tags_1");

            _novo.Configuracao.ContatoTelefonePrincipal = (string)Util.GetValue<string>(form, "TelefonePrincipal");
            _novo.Configuracao.ContatoEmailPrincipal = (string)Util.GetValue<string>(form, "EmailPrincipal");
            _novo.Configuracao.Localizacao = (string)Util.GetValue<string>(form, "Localizacao");
            _novo.Configuracao.LocalizacaoComplemento = (string)Util.GetValue<string>(form, "LocalizacaoComplemento");
            _novo.Configuracao.EmailHost = (string)Util.GetValue<string>(form, "Host");
            _novo.Configuracao.EmailUsername = (string)Util.GetValue<string>(form, "Username");
            _novo.Configuracao.EmailPassword = (string)Util.GetValue<string>(form, "Password");
            _novo.Configuracao.EmailDisplayName = (string)Util.GetValue<string>(form, "DisplayName");
            _novo.Configuracao.LinkMapa = (string)Util.GetValue<string>(form, "LinkMapa");
            _novo.Configuracao.HostBase = (string)Util.GetValue<string>(form, "HostBase");
            _novo.Configuracao.EmailDestino = (string)Util.GetValue<string>(form, "EmailDestino");
            _novo.Configuracao.EmailPorta = (int)Util.GetValue<int>(form, "EmailPorta");


            #region --> Validação
            SiteResponse resp = new SiteResponse();
            if (String.IsNullOrEmpty(_novo.Descricao))
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Informar uma descrição.";
            }

            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion

            return Json(new SiteDAL().GravarSite(_novo, _anterior), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirSite(int SiteId)
        {
            return Json(new SiteDAL().ExcluirSite(SiteId), JsonRequestBehavior.AllowGet);
        }

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }
    }
}
