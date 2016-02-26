using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class PortalController : Controller
    {
        //
        // GET: /Portal/

        public ActionResult Index()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult GetToken()
        {

            var UsuarioId = HttpContext.Request.Cookies["UsuarioId"] != null ? HttpContext.Request.Cookies["UsuarioId"].Value : "0";

            Portal model = new Portal();
            model.GetTedescoToken(ref model, UsuarioId);

            return Json(model.TedescoToken, JsonRequestBehavior.DenyGet);
        }
    }
}
