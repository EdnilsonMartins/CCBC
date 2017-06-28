using DAL;
using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class BuscaSocioController : Controller
    {
        
        public ActionResult Index(int AssociadoCategoriaId)
        {
            
            //Novo callback usado para marcar a página anterior: Será utilizado qdo usuario trocar idioma.
            var _callbackPortal_Anterior = new HttpCookie("CallbackPortal_Anterior", Url.Content("~/BuscaSocio?AssociadoCategoriaId=" + AssociadoCategoriaId)) { HttpOnly = true };
            Response.AppendCookie(_callbackPortal_Anterior);
            HttpContext.Request.Cookies.Set(_callbackPortal_Anterior);

            Portal model = new Portal().CarregarModel();

            model.AssociadoCategoriaId = AssociadoCategoriaId;

            if (AssociadoCategoriaId == 1) model.AssociadoCategoria = "Associados";
            if (AssociadoCategoriaId == 2) model.AssociadoCategoria = "Árbitros";
            if (AssociadoCategoriaId == 3) model.AssociadoCategoria = "Mediadores";

            model.Titulo = model.AssociadoCategoria;

            return View(model);
        }


        public ActionResult ListarAssociado(int SiteId, int AssociadoCategoriaId, string LetraInicial)
        {

            var lista = new AssociadoDAL().ListarAssociado(SiteId, AssociadoCategoriaId, LetraInicial, false);
            
            

            var currentCulture = HttpContext.Request.Cookies["lang"] != null ? HttpContext.Request.Cookies["lang"].Value : "pt-BR";
            if (string.IsNullOrEmpty(currentCulture)) currentCulture = "pt-BR";
            
            foreach(var item in lista){
                if (item.Detalhe != null && item.Detalhe.DataAtualizacao !=null){

                    if(Util.GetIdiomaId(currentCulture) == (int)Util.IDIOMA.PORTUGUES){
                        item.Detalhe.DataAtualizacao = Resources.Portal.Associado_AtualizadoEm + " " + string.Format("{0} de {1} de {2}", ((DateTime)item.DataAtualizacao).ToString("dd"), ((DateTime)item.DataAtualizacao).ToString("MMMM"), ((DateTime)item.DataAtualizacao).ToString("yyyy"));

                    }else if(Util.GetIdiomaId(currentCulture) == (int)Util.IDIOMA.ENGLISH){
                        item.Detalhe.DataAtualizacao = Resources.Portal.Associado_AtualizadoEm + " " + ((DateTime)item.DataAtualizacao).ToString("MMMM dd, yyyy") ;

                    }else if(Util.GetIdiomaId(currentCulture) == (int)Util.IDIOMA.ESPANHOL){
                        item.Detalhe.DataAtualizacao = Resources.Portal.Associado_AtualizadoEm + " " + string.Format("{0} de {1} de {2}", ((DateTime)item.DataAtualizacao).ToString("dd"), ((DateTime)item.DataAtualizacao).ToString("MMMM"), ((DateTime)item.DataAtualizacao).ToString("yyyy"));

                    }else if(Util.GetIdiomaId(currentCulture) == (int)Util.IDIOMA.FRANCES){

                    }

                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

    }
}
