using BackofficeCMS.Models;
using DAL;
using DTO;
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
    public class AssociadoController : Controller
    {

        public ActionResult ListaAssociado()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Pessoa";
            model.NavegacaoBarra.Resumo = "cadastro e manutenção de pessoas (Associados)...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Associado/ListaAssociado", Rotulo = "Pessoas" });

            return View(model);
        }

        public ActionResult CadAssociado(int AssociadoId)
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Cadastro de Pessoa";
            model.NavegacaoBarra.Resumo = "cadastro e manutenção de pessoas (Associados)...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Associado/ListaAssociado", Rotulo = "Pessoas" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "#", Rotulo = "Cadastro" });

            ViewBag.AssociadoId = AssociadoId;
            return View(model);
        }

        public ActionResult ListarAssociado()
        {

            int SiteId = GetCurrentSite();

            AssociadoDAL associadoDAL = new AssociadoDAL();
            List<Associado> listaAssociado = associadoDAL.ListarAssociado(SiteId, null, null, true);

            return Json(listaAssociado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarAssociado(int AssociadoId)
        {
            int SiteId = GetCurrentSite();

            AssociadoDAL associadoDAL = new AssociadoDAL();
            var resposta = associadoDAL.Carregar(AssociadoId);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarAssociado(string Associado, string AssociadoOld)
        {
            NumberFormatInfo provider = NumberFormatInfo.CurrentInfo;

            var form = (JObject)JsonConvert.DeserializeObject(Associado);

            Associado _anterior = new Associado();
            Associado _novo = new Associado();

            _novo.SiteId = GetCurrentSite();
            _novo.AssociadoId = (int)Util.GetValue<int>(form, "AssociadoId");
            _novo.AssociadoCategoriaId = (int)Util.GetValue<int>(form, "AssociadoCategoria");
            _novo.Nome = (string)Util.GetValue<string>(form, "Nome");
            _novo.Resumo = (string)Util.GetValue<string>(form, "Resumo");
            _novo.PaisId = (int)Util.GetValue<int>(form, "Pais");
            _novo.TipoPessoaId = (int)Util.GetValue<int>(form, "TipoPessoa");

            #region --> Validação
            AssociadoResponse resp = new AssociadoResponse();
            if (String.IsNullOrEmpty(_novo.Nome))
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Informe o Nome";
            }
            if (_novo.AssociadoCategoriaId == null || _novo.AssociadoCategoriaId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecione a Categoria";
            }
            if (_novo.TipoPessoaId == null || _novo.TipoPessoaId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecione o Tipo de Pessoa (Física / Jurídica)";
            }
            if (_novo.PaisId == null || _novo.PaisId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecione o País de origem";
            }
            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion

            if (AssociadoOld != null && AssociadoOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(AssociadoOld);

                _anterior.AssociadoId = (int)Util.GetValue<int>(formOld, "AssociadoId");
                _anterior.SiteId = GetCurrentSite();
                _anterior.AssociadoCategoriaId = (int)Util.GetValue<int>(formOld, "AssociadoCategoria");
                _anterior.Nome = (string)Util.GetValue<string>(formOld, "Nome");
                _anterior.Resumo = (string)Util.GetValue<string>(formOld, "Resumo");
                _anterior.PaisId = (int)Util.GetValue<int>(formOld, "Pais");
                _anterior.TipoPessoaId = (int)Util.GetValue<int>(formOld, "TipoPessoa");

            }

            return Json(new AssociadoDAL().Gravar(_novo, _anterior), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirAssociado(int AssociadoId)
        {
            return Json(new AssociadoDAL().ExcluirAssociado(AssociadoId), JsonRequestBehavior.AllowGet);
        }

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }
    }
}
