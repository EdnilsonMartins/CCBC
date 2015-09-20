using BackofficeCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class MediaCenterController : Controller
    {

        public ActionResult Explorer()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Media Center";
            model.NavegacaoBarra.Resumo = "arquivos, fotos, vídeos...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/MediaCenter/MediaCenter", Rotulo = "Media Center" });

            return View(model);
        }

        public ActionResult CadMediaCenter()
        {
            return View();
        }

        public ActionResult FormMediaCenter(){
            return View();
        }
    }
}
