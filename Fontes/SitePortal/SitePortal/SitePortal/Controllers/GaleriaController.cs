using DAL;
using DTO;
using DTO.Menu;
using DTO.Publicacao;
using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class GaleriaController : Controller
    {
        //
        // GET: /Busca/

        public ActionResult Index()
        {
            //Novo callback usado para marcar a página anterior: Será utilizado qdo usuario trocar idioma.
            var _callbackPortal_Anterior = new HttpCookie("CallbackPortal_Anterior", Url.Content("~/Galeria")) { HttpOnly = true };
            Response.AppendCookie(_callbackPortal_Anterior);
            HttpContext.Request.Cookies.Set(_callbackPortal_Anterior);

            
            DateTime inicio = DateTime.Now;

            string eventoid = null;

            int _eventoId;
            Int32.TryParse(eventoid, out _eventoId);

            var currentSite = HttpContext.Request.Cookies["site"] != null ? HttpContext.Request.Cookies["site"].Value : "0";
            if (string.IsNullOrEmpty(currentSite)) currentSite = "0";
            int SiteId = Convert.ToInt32(currentSite);
            int SiteId_Outro = SiteId == 1 ? 2 : 1;

            #region --> Galerias de Foto do Site Ativo
            Portal model = new Portal().CarregarModel(false, SiteId);

            model.Noticias.ForEach(delegate(Publicacao item){
                if(item.ArquivoGaleriaId != 0){
                    model.ResultaBusca.Add(item);
                }
            });

            model.Eventos.ForEach(delegate(Publicacao item)
            {
                if (item.ArquivoGaleriaId != 0)
                {
                    model.ResultaBusca.Add(item);
                }
            });

            model.Materias.ForEach(delegate(Publicacao item)
            {
                if (item.ArquivoGaleriaId != 0)
                {
                    model.ResultaBusca.Add(item);
                }
            });

            model.Artigos.ForEach(delegate(Publicacao item)
            {
                if (item.ArquivoGaleriaId != 0)
                {
                    model.ResultaBusca.Add(item);
                }
            });

            model.Paginas.ForEach(delegate(Publicacao item)
            {
                if (item.ArquivoGaleriaId != 0)
                {
                    model.ResultaBusca.Add(item);
                }
            });
            #endregion

            #region --> Galerias de Foto do Outro Site
            Portal model2 = new Portal().CarregarModel(false, SiteId_Outro);
            model2.Noticias.ForEach(delegate(Publicacao item)
            {
                if (item.ArquivoGaleriaId != 0)
                {
                    model.ResultaBusca.Add(item);
                }
            });

            model2.Eventos.ForEach(delegate(Publicacao item)
            {
                if (item.ArquivoGaleriaId != 0)
                {
                    model.ResultaBusca.Add(item);
                }
            });

            model2.Materias.ForEach(delegate(Publicacao item)
            {
                if (item.ArquivoGaleriaId != 0)
                {
                    model.ResultaBusca.Add(item);
                }
            });

            model2.Artigos.ForEach(delegate(Publicacao item)
            {
                if (item.ArquivoGaleriaId != 0)
                {
                    model.ResultaBusca.Add(item);
                }
            });

            model2.Paginas.ForEach(delegate(Publicacao item)
            {
                if (item.ArquivoGaleriaId != 0)
                {
                    model.ResultaBusca.Add(item);
                }
            });
            #endregion


            if (model.Conteudo == null)
            {
                model.ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "#",
                    Rotulo = "Galeria de Fotos"
                });

                model.ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Home",
                    Rotulo = "Home"
                });
            }

            
            

            
            DateTime fim = DateTime.Now;
            TimeSpan TempoTotal = fim.Subtract(inicio);
            model.TempoPesquisa = TempoTotal.TotalSeconds.ToString("0.000");
            return View(model);
        }
    }
}

