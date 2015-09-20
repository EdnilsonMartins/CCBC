using DAL;
using DTO.Pasta;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class PastaController : Controller
    {
        public ActionResult CadPasta()
        {

            return View();
        }

        public ActionResult ListarPasta()
        {
            int SiteId = GetCurrentSite();

            PastaDAL pastaDAL = new PastaDAL();
            List<Pasta> listaPasta = pastaDAL.ListarPasta(SiteId);

            return Json(listaPasta, JsonRequestBehavior.AllowGet);
        }
                
        public ActionResult CarregarPasta(int PastaId)
        {
            PastaDAL dal = new PastaDAL();
            Pasta pasta = new Pasta();

            var resposta = dal.Carregar(PastaId);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GravarPasta(string Pasta, string PastaOld)
        {

            var form = (JObject)JsonConvert.DeserializeObject(Pasta);

            Pasta _anterior = new Pasta();
            Pasta _novo = new Pasta();

            _novo.PastaId = (int)Util.GetValue<int>(form, "PastaId");
            _novo.PastaPaiId = (int?)Util.GetValue<int?>(form, "PastaPaiId");
            _novo.SiteId = GetCurrentSite();
            _novo.Descricao = (string)Util.GetValue<string>(form, "Descricao");

            #region --> Validação
            PastaResponse resp = new PastaResponse();
            if (string.IsNullOrEmpty(_novo.Descricao) || string.IsNullOrWhiteSpace(_novo.Descricao))
            {
                resp.Resposta.Erro = true;
                resp.Resposta.Mensagem += "- Informar uma Descrição";
            }
            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion

            return Json(new PastaDAL().Gravar(_novo, _anterior), JsonRequestBehavior.AllowGet);

        }

        public ActionResult ExcluirPasta(int PastaId)
        {
            return Json(new PastaDAL().ExcluirPasta(PastaId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReposicionarPasta(int PastaId, int PastaPaiId, int Posicao)
        {
            PastaDAL dal = new PastaDAL();
            Pasta pasta = new Pasta();
            pasta.PastaId = PastaId;
            pasta.PastaPaiId = PastaPaiId == 0 ? new Nullable<int>() : PastaPaiId;
            pasta.Posicao = Posicao + 1;
            return Json(dal.Reposicionar(pasta), JsonRequestBehavior.AllowGet);
        }

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }

    }
}
