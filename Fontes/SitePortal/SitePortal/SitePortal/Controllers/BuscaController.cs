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
    public class BuscaController : Controller
    {
        //
        // GET: /Busca/

        public ActionResult Index()
        {
            Portal model = new Portal().CarregarModel();

            return View(model);
        }

        public ActionResult Resultado(FormCollection form)
        {
            DateTime inicio = DateTime.Now;

            string Palavra = Request.Form["txtBusca"];
            if (Palavra == null) Palavra = "";
            

            string eventoid = null;

            int _eventoId;
            Int32.TryParse(eventoid, out _eventoId);

            Portal model = new Portal().CarregarModel();

            var socios = new AssociadoDAL().ListarAssociado(model.SiteId, 1, "", false);
            var arbitragem = new AssociadoDAL().ListarAssociado(model.SiteId, 2, "", false);
            var mediadores = new AssociadoDAL().ListarAssociado(model.SiteId, 3, "", false);

            List<string> palavras = Palavra.Split(new char[] { ' ' }).ToList<string>();

            palavras.ForEach(delegate(string p){
                p = p.ToLower();
                model.Eventos.ForEach(delegate(Publicacao item)
                {
                    bool vai = false;
                    if (item.Detalhe.Titulo != null && item.Detalhe.Titulo.ToLower().Contains(p)) vai = true;
                    if (item.Detalhe.Resumo != null && item.Detalhe.Resumo.ToLower().Contains(p)) vai = true;
                    if (item.Detalhe.Conteudo != null && item.Detalhe.Conteudo.ToLower().Contains(p)) vai = true;
                    if (vai) model.ResultaBusca.Add(item);
                });
                model.Materias.ForEach(delegate(Publicacao item)
                {
                    bool vai = false;
                    if (item.Detalhe.Titulo != null && item.Detalhe.Titulo.ToLower().Contains(p)) vai = true;
                    if (item.Detalhe.Resumo != null && item.Detalhe.Resumo.ToLower().Contains(p)) vai = true;
                    if (item.Detalhe.Conteudo != null && item.Detalhe.Conteudo.ToLower().Contains(p)) vai = true;
                    if (vai) model.ResultaBusca.Add(item);
                });
                model.Noticias.ForEach(delegate(Publicacao item)
                {
                    bool vai = false;
                    if (item.Detalhe.Titulo != null && item.Detalhe.Titulo.ToLower().Contains(p)) vai = true;
                    if (item.Detalhe.Resumo != null && item.Detalhe.Resumo.ToLower().Contains(p)) vai = true;
                    if (item.Detalhe.Conteudo != null && item.Detalhe.Conteudo.ToLower().Contains(p)) vai = true;
                    if (vai) model.ResultaBusca.Add(item);
                });

                //Associados
                List<Associado> socio = socios.FindAll(x => x.Nome.ToLower().Contains(p) || (x.Resumo + "").ToLower().Contains(p));
                foreach(Associado a in socio){
                    model.ResultaBusca.Add(new Publicacao(){
                        PublicacaoTipoId = 99,
                        Detalhe = new PublicacaoIdiomaExcecao() { Resumo = string.IsNullOrEmpty(a.Resumo)? "Cadastro de Associados" : a.Resumo, Titulo = a.Nome },
                        Complemento = new Publicacao.PublicacaoComplemento() { Privado = false },
                        PublicacaoId = 0,

                    });
                }

                //Arbitragem
                List<Associado> arbitros = arbitragem.FindAll(x => x.Nome.ToLower().Contains(p) || (x.Resumo + "").ToLower().Contains(p));
                foreach (Associado a in arbitros)
                {
                    model.ResultaBusca.Add(new Publicacao()
                    {
                        PublicacaoTipoId = 98,
                        Detalhe = new PublicacaoIdiomaExcecao() { Resumo = string.IsNullOrEmpty(a.Resumo) ? "Corpo de Árbitros" : a.Resumo, Titulo = a.Nome },
                        Complemento = new Publicacao.PublicacaoComplemento() { Privado = false },
                        PublicacaoId = 0,

                    });
                }

                //Mediadores
                List<Associado> mediador = mediadores.FindAll(x => x.Nome.ToLower().Contains(p) || (x.Resumo + "").ToLower().Contains(p));
                foreach (Associado a in mediador)
                {
                    model.ResultaBusca.Add(new Publicacao()
                    {
                        PublicacaoTipoId = 97,
                        Detalhe = new PublicacaoIdiomaExcecao() { Resumo = string.IsNullOrEmpty(a.Resumo) ? "Mediação" : a.Resumo, Titulo = a.Nome },
                        Complemento = new Publicacao.PublicacaoComplemento() { Privado = false },
                        PublicacaoId = 0,

                    });
                }
            });

            //model.Conteudo = new DTO.Publicacao.Publicacao();
            //model.Conteudo = model.Eventos.Find(x => x.PublicacaoId == _eventoId);
            //model.Conteudo = model.Eventos.Find(x => x.Detalhe.Conteudo != null && x.Detalhe.Conteudo.Contains(Palavra));

            
            if (model.Conteudo == null)
            {
                model.ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "#",
                    Rotulo = "Resultados da Busca"
                });

                model.ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Home",
                    Rotulo = "Home"
                });
            }

            
            

            model.PalavraBusca = Palavra;

            DateTime fim = DateTime.Now;
            TimeSpan TempoTotal = fim.Subtract(inicio);
            model.TempoPesquisa = TempoTotal.TotalSeconds.ToString("0.000");
            return View(model);
        }
    }
}
