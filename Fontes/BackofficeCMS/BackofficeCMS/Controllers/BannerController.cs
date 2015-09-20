using BackofficeCMS.Models;
using DAL;
using DTO.Banner;
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
    public class BannerController : Controller
    {
        
        public ActionResult ListaBanner()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Banner";
            model.NavegacaoBarra.Resumo = "cadastro e manutenção de banners...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Banner/ListaBanner", Rotulo = "Banners" });

            return View(model);
        }

        public ActionResult CadBanner(int BannerId)
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Cadastro de Banner";
            model.NavegacaoBarra.Resumo = "cadastro e manutenção de banners...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Banner/ListaBanner", Rotulo = "Banners" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "#", Rotulo = "Cadastro" });

            ViewBag.BannerId = BannerId;
            return View(model);
        }

        public ActionResult ListarBanner()
        {

            int SiteId = GetCurrentSite();
            int UsuarioId = 1;
            int IdiomaId = 1;

            BannerDAL bannerDAL = new BannerDAL();
            List<Banner> listaBanner = bannerDAL.ListarBanner(SiteId, null, null, null, Convert.ToInt32(UsuarioId), IdiomaId, false, false, null, false);

            return Json(listaBanner, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarBanner(int BannerId, int IdiomaId)
        {
            int SiteId = GetCurrentSite();
            int UsuarioId = 1;

            BannerDAL bannerDAL = new BannerDAL();
            var resposta = bannerDAL.Carregar(SiteId, IdiomaId, BannerId, UsuarioId, false);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarBanner(string Banner, string BannerOld, string ListaUsuarioGrupo, string ListaUsuario)
        {
            NumberFormatInfo provider = NumberFormatInfo.CurrentInfo;

            var form = (JObject)JsonConvert.DeserializeObject(Banner);

            Banner _anterior = new Banner();
            Banner _novo = new Banner();

            _novo.BannerId = (int)Util.GetValue<int>(form, "BannerId");
            _novo.SiteId = GetCurrentSite();
            _novo.BannerTipoId = (int)Util.GetValue<int>(form, "BannerTipo");
            var data = (String)Util.GetValue<String>(form, "Data");
            if (!String.IsNullOrEmpty(data))
            {
                _novo.Data = Convert.ToDateTime(data, provider);
            }

            var dataValidade = (String)Util.GetValue<String>(form, "DataValidade");
            if (!String.IsNullOrEmpty(dataValidade))
            {
                _novo.DataValidade = Convert.ToDateTime(dataValidade, provider);
            }
            _novo.Referencia = (string)Util.GetValue<string>(form, "Referencia");

            _novo.Detalhe.IdiomaId = 1;
            _novo.Detalhe.Titulo = (string)Util.GetValue<string>(form, "Titulo");
            _novo.Detalhe.Resumo = (string)Util.GetValue<string>(form, "Resumo");
            _novo.Detalhe.Descricao = (string)Util.GetValue<string>(form, "Descricao");
            _novo.Ativo = Convert.ToBoolean((int?)Util.GetValue<int?>(form, "Status"));
            _novo.Posicao = (int?)Util.GetValue<int?>(form, "Posicao");
            _novo.TargetId = (int)Util.GetValue<int>(form, "Target");
            _novo.LinkURL = (string)Util.GetValue<string>(form, "LinkURL");

            _novo.Complemento.Privado = Convert.ToBoolean((int?)Util.GetValue<int?>(form, "Privado"));

            #region --> Validação
            BannerResponse resp = new BannerResponse();
            if (_novo.BannerTipoId == null || _novo.BannerTipoId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar o Tipo de Banner";
            }
            if (_novo.TargetId == null || _novo.TargetId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar o Target";
            }
            if (Util.GetValue<int?>(form, "Status") == null)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar o Status: Ativo / Inativo.";
            }
            if (_novo.Complemento.Privado == true && string.IsNullOrEmpty(ListaUsuarioGrupo) && string.IsNullOrEmpty(ListaUsuario))
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Informar pelo menos um Usuário ou Grupo para publicação privada.";
            }
            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion

            List<BannerIdiomaExcecao> Extras = new List<BannerIdiomaExcecao>();
            List<BannerIdiomaExcecao> ExtrasOld = new List<BannerIdiomaExcecao>();

            #region -> Idiomas Extras
            //-- EN
            BannerIdiomaExcecao ExtraEN = new BannerIdiomaExcecao();
            ExtraEN.IdiomaId = (int)Util.IDIOMA.ENGLISH;
            ExtraEN.Titulo = (string)Util.GetValue<string>(form, "TituloEN");
            ExtraEN.Resumo = (string)Util.GetValue<string>(form, "ResumoEN");
            ExtraEN.Descricao = (string)Util.GetValue<string>(form, "DescricaoEN");
            Extras.Add(ExtraEN);
            //-- ES
            BannerIdiomaExcecao ExtraES = new BannerIdiomaExcecao();
            ExtraES.IdiomaId = (int)Util.IDIOMA.ESPANHOL;
            ExtraES.Titulo = (string)Util.GetValue<string>(form, "TituloES");
            ExtraES.Resumo = (string)Util.GetValue<string>(form, "ResumoES");
            ExtraES.Descricao = (string)Util.GetValue<string>(form, "DescricaoES");
            Extras.Add(ExtraES);
            //-- FR
            BannerIdiomaExcecao ExtraFR = new BannerIdiomaExcecao();
            ExtraFR.IdiomaId = (int)Util.IDIOMA.FRANCES;
            ExtraFR.Titulo = (string)Util.GetValue<string>(form, "TituloFR");
            ExtraFR.Resumo = (string)Util.GetValue<string>(form, "ResumoFR");
            ExtraFR.Descricao = (string)Util.GetValue<string>(form, "DescricaoFR");
            Extras.Add(ExtraFR);
            #endregion

            if (BannerOld != null && BannerOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(BannerOld);

                _anterior.BannerId = (int)Util.GetValue<int>(formOld, "UsuarioId");
                _anterior.Detalhe.Titulo = (string)Util.GetValue<string>(formOld, "Titulo");
                _anterior.Detalhe.Resumo = (string)Util.GetValue<string>(formOld, "Resumo");
                _anterior.Detalhe.Descricao = (string)Util.GetValue<string>(formOld, "Descricao");
                
                #region -> Idiomas Extras (Dados anterior a alteração)
                //-- EN
                BannerIdiomaExcecao ExtraENOld = new BannerIdiomaExcecao();
                ExtraENOld.IdiomaId = (int)Util.IDIOMA.ENGLISH;
                ExtraENOld.Titulo = (string)Util.GetValue<string>(form, "TituloEN");
                ExtraENOld.Resumo = (string)Util.GetValue<string>(form, "ResumoEN");
                ExtraENOld.Descricao = (string)Util.GetValue<string>(form, "DescricaoEN");
                ExtrasOld.Add(ExtraENOld);
                //-- ES
                BannerIdiomaExcecao ExtraESOld = new BannerIdiomaExcecao();
                ExtraESOld.IdiomaId = (int)Util.IDIOMA.ESPANHOL;
                ExtraESOld.Titulo = (string)Util.GetValue<string>(form, "TituloES");
                ExtraESOld.Resumo = (string)Util.GetValue<string>(form, "ResumoES");
                ExtraESOld.Descricao = (string)Util.GetValue<string>(form, "DescricaoES");
                ExtrasOld.Add(ExtraESOld);
                //-- FR
                BannerIdiomaExcecao ExtraFROld = new BannerIdiomaExcecao();
                ExtraFROld.IdiomaId = (int)Util.IDIOMA.FRANCES;
                ExtraFROld.Titulo = (string)Util.GetValue<string>(form, "TituloFR");
                ExtraFROld.Resumo = (string)Util.GetValue<string>(form, "ResumoFR");
                ExtraFROld.Descricao = (string)Util.GetValue<string>(form, "DescricaoFR");
                ExtrasOld.Add(ExtraFROld);
                #endregion
            }

            return Json(new BannerDAL().Gravar(_novo, _anterior, Extras, ExtrasOld, ListaUsuarioGrupo, ListaUsuario), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirBanner(int BannerId)
        {
            return Json(new BannerDAL().ExcluirBanner(BannerId), JsonRequestBehavior.AllowGet);
        }

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }
    }
}
