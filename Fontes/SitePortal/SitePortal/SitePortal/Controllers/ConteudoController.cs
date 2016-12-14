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

        public ActionResult Calcular2015(string Valor = "0")
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

        public ActionResult RevistasNewsletter(int Id)
        {



            int _id = 0;
            string _LinkURL = "";
            
            Portal model = new Portal().CarregarModel(false);
            model.Conteudo = new DTO.Publicacao.Publicacao();


            model.RevistaNewsletterTipo = Id;
            if (Id == 1){
                model.RevistasNewsletter = model.Revistas;
                //_id = 1182; // 1182;
                _id = model.Materias[0].PublicacaoId;

                model.Conteudo = model.Materias.Find(x => x.PublicacaoId == _id);
                _LinkURL = "/Revistas";


            }
            else if (Id == 2)
            {
                model.RevistasNewsletter = model.Newsletter;
                //_id = 1678;
                _id = model.Materias[0].PublicacaoId;

                //model.Conteudo = model.Noticias.Find(x => x.PublicacaoId == _id);//model.RevistasNewsletter[0].PublicacaoId
                model.Conteudo = model.Materias.Find(x => x.PublicacaoId == _id);
                _LinkURL = "/NewsLetter";
            }
            else
            {
                if (model.Revistas.Any())
                    model.RevistasNewsletter.AddRange(model.Revistas.OrderByDescending(d => d.Data).ToList().GetRange(0,3));
                if (model.Newsletter.Any())
                    model.RevistasNewsletter.AddRange(model.Newsletter.OrderByDescending(d => d.Data).ToList().GetRange(0, 3));
                //_id = 1649;
                _id = model.Materias[0].PublicacaoId;

                model.Conteudo = model.Materias.Find(x => x.PublicacaoId == _id);
                _LinkURL = "/Publicacoes";
            }

            //Novo callback usado para marcar a página anterior: Será utilizado qdo usuario trocar idioma.
            var _callbackPortal_Anterior = new HttpCookie("CallbackPortal_Anterior", Url.Content("~" + _LinkURL)) { HttpOnly = true };
            Response.AppendCookie(_callbackPortal_Anterior);
            HttpContext.Request.Cookies.Set(_callbackPortal_Anterior);

            
            

            var siteId_Entrada = model.SiteId;
            if (model.SiteId == 0) model.SiteId = 2;

            if (model.Conteudo == null && _id != 0 && model.SiteId == 2)
            {
                string site = "1";
                var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                Response.AppendCookie(siteCookie);
                HttpContext.Request.Cookies.Set(siteCookie);

                model = new Portal().CarregarModel(false);
                model.Conteudo = new DTO.Publicacao.Publicacao();
                model.Conteudo = model.Noticias.Find(x => x.PublicacaoId == _id);
            }
            if (model.Conteudo == null && _id != 0 && model.SiteId == 1)
            {
                string site = "2";
                var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
                Response.AppendCookie(siteCookie);
                HttpContext.Request.Cookies.Set(siteCookie);

                model = new Portal().CarregarModel(false);
                model.Conteudo = new DTO.Publicacao.Publicacao();
                model.Conteudo = model.Noticias.Find(x => x.PublicacaoId == _id);
            }


            if (_id != null && model.Conteudo != null)
            {
                if (_id == model.Conteudo.PublicacaoId)
                {
                    model.CarregarMenuInterna((int)_id, _LinkURL);
                }
                //if (model.Conteudo != null) model.CarregarMenuTree(_id);
                //model.CarregarBannerInterna((int)_id);
            }

            model.ListaMenuTree = new List<Menu>();

            model.ListaMenuTree.Add(new Menu()
            {
                MenuTipoAcaoId = 1,
                LinkURL = "Publicacoes",
                Rotulo = "Publicações"
            });

            model.ListaMenuTree.Add(new Menu()
            {
                MenuTipoAcaoId = 1,
                LinkURL = "Home",
                Rotulo = "Home"
            });


            return View(model);
        }
    
    
    }
}
