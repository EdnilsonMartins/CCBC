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
            //Limpa callback da página anterior!
            var _callbackPortal = new HttpCookie("CallbackPortal_Anterior", null) { HttpOnly = true };
            Response.AppendCookie(_callbackPortal);
            HttpContext.Request.Cookies.Set(_callbackPortal);
            //=======

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
            return Json(new AssociadoDAL().ListarAssociado(SiteId, AssociadoCategoriaId, LetraInicial, false), JsonRequestBehavior.AllowGet);
        }

    }
}
