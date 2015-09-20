using DAL;
using DTO.Publicacao;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class EditoriaController : Controller
    {
        public ActionResult CadEditoria()
        {
            return View();
        }

        public ActionResult ListarEditoria()
        {
            int SiteId = GetCurrentSite();
            int IdiomaId = 1;

            EditoriaDAL editoriaDAL = new EditoriaDAL();
            List<Editoria> listaEditoria = editoriaDAL.ListarEditoria(SiteId, IdiomaId);

            return Json(listaEditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarEditoria(int EditoriaId, int IdiomaId)
        {
            int SiteId = GetCurrentSite();

            EditoriaDAL editoriaDAL = new EditoriaDAL();
            var resposta = editoriaDAL.Carregar(EditoriaId, IdiomaId, false);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarEditoria(string Editoria, string EditoriaOld)
        {
            var form = (JObject)JsonConvert.DeserializeObject(Editoria);

            Editoria _anterior = new Editoria();
            Editoria _novo = new Editoria();

            _novo.EditoriaId = (int)Util.GetValue<int>(form, "EditoriaId");
            _novo.SiteId = GetCurrentSite();

            _novo.Detalhe.IdiomaId = (int)Util.IDIOMA.PORTUGUES;
            _novo.Detalhe.Descricao = (string)Util.GetValue<string>(form, "Descricao");

            #region --> Validação
            EditoriaResponse resp = new EditoriaResponse();
            if (String.IsNullOrEmpty(_novo.Detalhe.Descricao))
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

            List<EditoriaIdiomaExcecao> Extras = new List<EditoriaIdiomaExcecao>();
            List<EditoriaIdiomaExcecao> ExtrasOld = new List<EditoriaIdiomaExcecao>();

            #region -> Idiomas Extras
            //-- EN
            EditoriaIdiomaExcecao ExtraEN = new EditoriaIdiomaExcecao();
            ExtraEN.IdiomaId = (int)Util.IDIOMA.ENGLISH;
            ExtraEN.Descricao = (string)Util.GetValue<string>(form, "DescricaoEN");
            Extras.Add(ExtraEN);
            //-- ES
            EditoriaIdiomaExcecao ExtraES = new EditoriaIdiomaExcecao();
            ExtraES.IdiomaId = (int)Util.IDIOMA.ESPANHOL;
            ExtraES.Descricao = (string)Util.GetValue<string>(form, "DescricaoES");
            Extras.Add(ExtraES);
            //-- FR
            EditoriaIdiomaExcecao ExtraFR = new EditoriaIdiomaExcecao();
            ExtraFR.IdiomaId = (int)Util.IDIOMA.FRANCES;
            ExtraFR.Descricao = (string)Util.GetValue<string>(form, "DescricaoFR");
            Extras.Add(ExtraFR);
            #endregion

            return Json(new EditoriaDAL().Gravar(_novo, _anterior, Extras), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirBanner(int EditoriaId)
        {
            return Json(new EditoriaDAL().ExcluirEditoria(EditoriaId), JsonRequestBehavior.AllowGet);
        }

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }
    }
}
