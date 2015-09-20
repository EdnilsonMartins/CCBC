using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class PublicacaoController : Controller
    {
        //
        // GET: /Publicacao/

        public ActionResult Index(int PublicacaoId)
        {
            return View();
        }

    }
}
