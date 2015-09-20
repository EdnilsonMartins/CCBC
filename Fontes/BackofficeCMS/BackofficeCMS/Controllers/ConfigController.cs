using BackofficeCMS.Models;
using DAL;
using DTO.Regra;
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
    public class ConfigController : Controller
    {
        //
        // GET: /Config/

        public ActionResult Setup()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 300; // CONFIGURAÇÕES
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Configurações";
            model.NavegacaoBarra.Resumo = "ajuste de diversos parâmetros da plataforma...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Config/Setup", Rotulo = "Setup" });

            return View(model);
        }

        #region --> Regras
        public ActionResult CadRegra(int RegraId)
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Cadastro de Modelo de Regra";
            model.NavegacaoBarra.Resumo = "conjunto de condições para controlar a aprovação de publicações...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Config/ListaRegra", Rotulo = "Modelos de Regra" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "#", Rotulo = "Cadastro" });

            ViewBag.RegraId = RegraId;
            return View(model);
        }

        public ActionResult CadPublicacaoTipoRegra(int PublicacaoTipoRegraId)
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 300; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Associação de Regras";
            model.NavegacaoBarra.Resumo = "configuração de regras das publicações...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Config/ListaPublicacaoTipoRegra", Rotulo = "Associação de Regra" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "#", Rotulo = "Cadastro" });

            ViewBag.PublicacaoTipoRegraId = PublicacaoTipoRegraId;
            return View(model);
        }


        public ActionResult ListaRegra()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Modelos de Regra";
            model.NavegacaoBarra.Resumo = "conjunto de condições para controlar a aprovação de publicações...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Config/ListaRegra", Rotulo = "Modelos de Regra" });

            return View(model);
        }

        public ActionResult ListaPublicacaoTipoRegra()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 300; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Associação de Regras";
            model.NavegacaoBarra.Resumo = "configuração de regras das publicações...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Config/ListaPublicacaoTipoRegra", Rotulo = "Associação de Regra" });

            return View(model);
        }


        public ActionResult ListarRegra()
        {

            int SiteId = GetCurrentSite();
            //int UsuarioId = 1;
            //int IdiomaId = 1;

            RegraDAL regraDAL = new RegraDAL();
            List<Regra> lista = regraDAL.ListarRegra(SiteId);


            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarRegraPasso(int RegraId)
        {
            RegraDAL regraDAL = new RegraDAL();
            List<RegraPasso> lista = regraDAL.ListarRegraPasso(RegraId);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarRegraPassoCondicao(int RegraPassoId)
        {
            RegraDAL regraDAL = new RegraDAL();
            List<RegraPassoCondicao> lista = regraDAL.ListarRegraPassoCondicao(RegraPassoId);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPublicacaoTipoRegra()
        {

            int SiteId = GetCurrentSite();

            RegraDAL regraDAL = new RegraDAL();
            List<PublicacaoTipoRegra> lista = regraDAL.ListarPublicacaoTipoRegra(null, SiteId);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }



        public ActionResult CarregarRegra(int RegraId)
        {
            RegraDAL dal = new RegraDAL();
            Regra dto = new Regra();

            int SiteId = GetCurrentSite();
            int UsuarioId = 1;
            //int IdiomaId = 1;

            //Eventos
            RegraDAL regraDAL = new RegraDAL();
            var resposta = regraDAL.Carregar(SiteId, 0, RegraId, UsuarioId);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarRegraPasso(int RegraPassoId, int RegraId)
        {
            RegraDAL dal = new RegraDAL();
            RegraPasso dto = new RegraPasso();

            RegraDAL regraDAL = new RegraDAL();
            var resposta = regraDAL.CarregarRegraPasso(RegraPassoId, RegraId);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarRegraPassoCondicao(int RegraPassoCondicaoId, int RegraPassoId)
        {
            RegraDAL dal = new RegraDAL();
            RegraPassoCondicao dto = new RegraPassoCondicao();

            RegraDAL regraDAL = new RegraDAL();
            var resposta = regraDAL.CarregarRegraPassoCondicao(RegraPassoCondicaoId, RegraPassoId);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarPublicacaoTipoRegra(int PublicacaoTipoRegraId)
        {
            int SiteId = GetCurrentSite();

            RegraDAL regraDAL = new RegraDAL();
            var resposta = regraDAL.CarregarPublicacaoTipoRegra(PublicacaoTipoRegraId, SiteId);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarRegra(string Regra, string RegraOld)
        {
            NumberFormatInfo provider = NumberFormatInfo.CurrentInfo;

            var form = (JObject)JsonConvert.DeserializeObject(Regra);

            Regra _anterior = new Regra();
            Regra _novo = new Regra();

            _novo.RegraId = (int)Util.GetValue<int>(form, "RegraId");
            _novo.SiteId = GetCurrentSite();
            _novo.Descricao = (String)Util.GetValue<String>(form, "Descricao");
            _novo.RegraTipoId = (int)Util.GetValue<int>(form, "RegraTipo");
            
            #region --> Validação
            RegraResponse resp = new RegraResponse();
            if (_novo.RegraTipoId == null || _novo.RegraTipoId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar o Tipo da Regra";
            }
            
            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion


            if (RegraOld != null && RegraOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(RegraOld);

                _anterior.RegraId = (int)Util.GetValue<int>(formOld, "RegraId");
                _anterior.Descricao = (string)Util.GetValue<string>(formOld, "Descricao");
                _anterior.RegraTipoId = (int)Util.GetValue<int>(formOld, "RegraTipoId");
            }

            return Json(new RegraDAL().Gravar(_novo, _anterior), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarRegraPasso(int RegraPassoId, int RegraId, string Regra, string RegraOld)
        {
            NumberFormatInfo provider = NumberFormatInfo.CurrentInfo;

            var form = (JObject)JsonConvert.DeserializeObject(Regra);

            RegraPasso _anterior = new RegraPasso();
            RegraPasso _novo = new RegraPasso();

            _novo.RegraPassoId = RegraPassoId;
            _novo.RegraId = RegraId;
            _novo.Sequencia = (int)Util.GetValue<int>(form, "Sequencia");
            _novo.Descricao = (string)Util.GetValue<string>(form, "Descricao");
            _novo.QuantidadeMinimaUsuariosDoGrupo = (int)Util.GetValue<int>(form, "QtdAprovacao");

            #region --> Validação
            //RegraResponse resp = new RegraResponse();
            //if (_novo.RegraTipoId == null || _novo.RegraTipoId == 0)
            //{
            //    resp.Resposta.Erro = true;
            //    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
            //    resp.Resposta.Mensagem += "- Selecionar o Tipo da Regra";
            //}

            //if (resp.Resposta.Erro)
            //{
            //    return Json(resp, JsonRequestBehavior.AllowGet);
            //}
            #endregion


            if (RegraOld != null && RegraOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(RegraOld);

                _anterior.Sequencia = (int)Util.GetValue<int>(formOld, "Sequencia");
                _anterior.Descricao = (string)Util.GetValue<string>(formOld, "Descricao");
                _anterior.QuantidadeMinimaUsuariosDoGrupo = (int)Util.GetValue<int>(formOld, "QtdAprovacao");
            }

            return Json(new RegraDAL().Gravar(_novo, _anterior), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarRegraPassoCondicao(int RegraPassoCondicaoId, int RegraPassoId, int? UsuarioGrupoId, int? UsuarioId, string Regra, string RegraOld)
        {
            NumberFormatInfo provider = NumberFormatInfo.CurrentInfo;

            var form = (JObject)JsonConvert.DeserializeObject(Regra);

            RegraPassoCondicao _anterior = new RegraPassoCondicao();
            RegraPassoCondicao _novo = new RegraPassoCondicao();

            _novo.RegraPassoCondicaoId = RegraPassoCondicaoId;
            _novo.RegraPassoId = RegraPassoId;
            _novo.UsuarioGrupoId = UsuarioGrupoId;
            _novo.UsuarioId = UsuarioId;
            _novo.QuantidadeMinimaUsuarios = (int)Util.GetValue<int>(form, "QtdAprovacao");

            #region --> Validação
            //RegraResponse resp = new RegraResponse();
            //if (_novo.RegraTipoId == null || _novo.RegraTipoId == 0)
            //{
            //    resp.Resposta.Erro = true;
            //    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
            //    resp.Resposta.Mensagem += "- Selecionar o Tipo da Regra";
            //}

            //if (resp.Resposta.Erro)
            //{
            //    return Json(resp, JsonRequestBehavior.AllowGet);
            //}
            #endregion


            if (RegraOld != null && RegraOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(RegraOld);

                //_anterior.Sequencia = (int)Util.GetValue<int>(formOld, "Sequencia");
                //_anterior.Descricao = (string)Util.GetValue<string>(formOld, "Descricao");
                //_anterior.QuantidadeMinimaUsuariosDoGrupo = (int)Util.GetValue<int>(formOld, "QtdAprovacao");
            }

            return Json(new RegraDAL().Gravar(_novo, _anterior), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarPublicacaoTipoRegra(string Regra, string RegraOld)
        {

            NumberFormatInfo provider = NumberFormatInfo.CurrentInfo;

            var form = (JObject)JsonConvert.DeserializeObject(Regra);

            PublicacaoTipoRegra _anterior = new PublicacaoTipoRegra();
            PublicacaoTipoRegra _novo = new PublicacaoTipoRegra();

            _novo.PublicacaoTipoRegraId = (int)Util.GetValue<int>(form, "PublicacaoTipoRegraId");
            _novo.PublicacaoTipoId = (int)Util.GetValue<int>(form, "TipoPublicacao");
            _novo.RegraId = (int)Util.GetValue<int>(form, "Regra");
            _novo.Privado = Convert.ToBoolean((int?)Util.GetValue<int?>(form, "Privado"));


            #region --> Validação
            PublicacaoTipoRegraResponse resp = new PublicacaoTipoRegraResponse();
            if (_novo.PublicacaoTipoId == null || _novo.PublicacaoTipoId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar o Tipo da Publicação";
            }
            if (_novo.RegraId == null || _novo.RegraId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar a Regra que será utilizada";
            }

            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion


            if (RegraOld != null && RegraOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(RegraOld);

                //_anterior.Sequencia = (int)Util.GetValue<int>(formOld, "Sequencia");
                //_anterior.Descricao = (string)Util.GetValue<string>(formOld, "Descricao");
                //_anterior.QuantidadeMinimaUsuariosDoGrupo = (int)Util.GetValue<int>(formOld, "QtdAprovacao");
            }

            return Json(new RegraDAL().Gravar(_novo, _anterior), JsonRequestBehavior.AllowGet);
        }



        public ActionResult ExcluirRegra(int RegraId)
        {
            return Json(new RegraDAL().ExcluirRegra(RegraId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirRegraPasso(int RegraPassoId)
        {
            return Json(new RegraDAL().ExcluirRegraPasso(RegraPassoId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirRegraPassoCondicao(int RegraPassoCondicaoId)
        {
            return Json(new RegraDAL().ExcluirRegraPassoCondicao(RegraPassoCondicaoId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirPublicacaoTipoRegra(int PublicacaoTipoRegraId)
        {
            return Json(new RegraDAL().ExcluirPublicacaoTipoRegra(PublicacaoTipoRegraId), JsonRequestBehavior.AllowGet);
        }
        #endregion




        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }
    }
}
