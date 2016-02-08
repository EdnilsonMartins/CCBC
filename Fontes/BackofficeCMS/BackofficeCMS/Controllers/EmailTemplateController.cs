using BackofficeCMS.Models;
using DAL;
using DTO.EmailTemplate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class EmailTemplateController : Controller
    {

        public ActionResult ListaEmailTemplate()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 300; // CONFIGURAÇÕES
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "E-mail Administrativo";
            model.NavegacaoBarra.Resumo = "gerencia os templates de e-mails utilizados pelo sistema";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/EmailTemplate/ListaEmailTemplate", Rotulo = "E-mail Administrativo" });

            return View(model);
        }

        public ActionResult CadEmailTemplate(int EmailTemplateId)
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 300; // CONFIGURAÇÕES
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "E-mail Administrativo";
            model.NavegacaoBarra.Resumo = "gerencia os templates de e-mails utilizados pelo sistema";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/EmailTemplate/ListaEmailTemplate", Rotulo = "E-mail Administrativo" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "#", Rotulo = "Cadastro" });

            ViewBag.EmailTemplateId = EmailTemplateId;
            return View(model);
        }

        public ActionResult ListarEmailTemplate()
        {

            int SiteId = GetCurrentSite();

            EmailTemplateDAL emailTemplateDAL = new EmailTemplateDAL();
            List<EmailTemplate> listaAssociado = emailTemplateDAL.ListarEmailTemplate(SiteId, (int)Util.EMAIL_TEMPLATE.ADMINISTRATIVO);

            return Json(listaAssociado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarEmailTemplate(int EmailTemplateId)
        {
            int SiteId = GetCurrentSite();

            EmailTemplateDAL emailTemplateDAL = new EmailTemplateDAL();
            var resposta = emailTemplateDAL.Carregar(SiteId, EmailTemplateId);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarEmailTemplate(string EmailTemplate, string EmailTemplateOld)
        {
            NumberFormatInfo provider = NumberFormatInfo.CurrentInfo;

            var form = (JObject)JsonConvert.DeserializeObject(EmailTemplate);

            EmailTemplate _anterior = new EmailTemplate();
            EmailTemplate _novo = new EmailTemplate();

            _novo.SiteId = GetCurrentSite();
            _novo.EmailTemplateId = (int)Util.GetValue<int>(form, "EmailTemplateId");
            _novo.EmailGrupoTemplateId = (int)Util.GetValue<int>(form, "EmailGrupoTemplate");
            _novo.Comentario = (string)Util.GetValue<string>(form, "Comentario");
            _novo.Assunto = (string)Util.GetValue<string>(form, "Assunto");
            _novo.Corpo = (string)Util.GetValue<string>(form, "EmailTemplateCorpo");

            #region --> Validação
            EmailTemplateResponse resp = new EmailTemplateResponse();
            if (String.IsNullOrEmpty(_novo.Comentario))
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Informe um comentário";
            }
            if (String.IsNullOrEmpty(_novo.Assunto))
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Informe um assunto";
            }
            if (String.IsNullOrEmpty(_novo.Corpo))
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Preencha o corpo do template de e-mail";
            }
            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion

            if (EmailTemplateOld != null && EmailTemplateOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(EmailTemplateOld);

                _anterior.EmailTemplateId = (int)Util.GetValue<int>(formOld, "EmailTemplateId");
                _anterior.SiteId = GetCurrentSite();
                _anterior.Comentario = (string)Util.GetValue<string>(formOld, "Comentario");
                _anterior.Assunto = (string)Util.GetValue<string>(formOld, "Assunto");
                _anterior.Corpo = (string)Util.GetValue<string>(formOld, "Corpo");

            }

            return Json(new EmailTemplateDAL().Gravar(_novo, _anterior), JsonRequestBehavior.AllowGet);
        }

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }
    }
}
