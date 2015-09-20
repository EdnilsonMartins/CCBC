using DAL;
using DTO.Menu;
using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class ConteudoController : Controller
    {
        //
        // GET: /Conteudo/

        public ActionResult Index(int? Id)
        {
            Portal model = new Portal().CarregarModel();

            if (Id != null)
            {
                model.Conteudo = model.Eventos.Find(x => x.PublicacaoId == Id);
                if (model.Conteudo == null)
                    model.Conteudo = model.Noticias.Find(x => x.PublicacaoId == Id);
                if (model.Conteudo == null)
                    model.Conteudo = model.Materias.Find(x => x.PublicacaoId == Id);
                if (model.Conteudo == null)
                    model.Conteudo = model.Artigos.Find(x => x.PublicacaoId == Id);
                if (model.Conteudo == null)
                    model.Conteudo = model.Paginas.Find(x => x.PublicacaoId == Id);

                model.CarregarMenuInterna((int)Id);
                if (model.Conteudo != null) model.CarregarMenuTree((int)Id);

                model.CarregarBannerInterna((int)Id);
            }

            return View(model);
        }

        public ActionResult CarregarConteudo(int ConteudoId)
        {
            ConteudoDAL model = new ConteudoDAL();
            return Json(model.CarregarConteudo(ConteudoId), JsonRequestBehavior.DenyGet);
        }

        public ActionResult CarregarArquivo(int ArquivoId)
        {
            ArquivoDAL model = new ArquivoDAL();
            //return Json(model.CarregarArquivo(ArquivoId), JsonRequestBehavior.DenyGet);
            return Json(model.ListarArquivos(null), JsonRequestBehavior.DenyGet);
        }

        public ActionResult ListarArquivosGaleria(int PublicacaoId, int ArquivoCategoriaId)
        {
            ArquivoDAL model = new ArquivoDAL();
            return Json(model.ListarArquivosGaleria(PublicacaoId, ArquivoCategoriaId, (int)Util.ARQUIVO_CATEGORIA_TIPO.PUBLICACAO), JsonRequestBehavior.DenyGet);
        }

        public ActionResult Calculadora2015()
        {
            return View();
        }

        public ActionResult Calcular2015(double Valor = 0)
        {
            var calculo = new CalculadoraDAL().Calcular(Valor);
            return Json(calculo, JsonRequestBehavior.DenyGet);
        }

        public ActionResult RegistrarClick(string BannerId, string ArquivoId)
        {
            int bannerId;
            int.TryParse(BannerId, out bannerId);

            long arquivoId;
            long.TryParse(ArquivoId, out arquivoId);

            new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.CLIQUE, bannerId, arquivoId);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        public ActionResult QuickMenu(Portal model)
        {
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Agenda(Portal model)
        {
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View();
            }
        }

        public ActionResult NewsLetter(Portal model)
        {
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View();
            }
        }

        public ActionResult BannerPublicidadeRotativo(Portal model)
        {
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Calendario(int inc)
        {
            ViewBag.inc = inc;
            return PartialView("Calendario");
        }
    }
}
