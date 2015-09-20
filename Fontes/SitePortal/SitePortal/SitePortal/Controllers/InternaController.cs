using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class InternaController : Controller
    {
        
        public ActionResult Index(string internaid = "")
        {
            int _internaid;
            Int32.TryParse(internaid, out _internaid);

            Portal model = new Portal().CarregarModel();

            model.Conteudo = new DTO.Publicacao.Publicacao();
            model.Conteudo = model.Paginas.Find(x => x.PublicacaoId == _internaid);

            return View(model);
        }

    }
}
