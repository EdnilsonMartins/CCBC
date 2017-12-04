using BackofficeCMS.Models;
using DAL;
using DTO.Podcast;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class PodcastController : Controller
    {

        public ActionResult ListaPodcast(string Tipo)
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Podcast";
            model.NavegacaoBarra.Resumo = "cadastro e manutenção de Podcasts...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Podcast/ListaPodcast", Rotulo = "Podcasts" });

            ViewBag.Tipo = Tipo;

            return View(model);
        }

        public ActionResult CadPodcast(int PodcastId, string Tipo)
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Cadastro de Podcast";
            model.NavegacaoBarra.Resumo = "cadastro e manutenção de Podcasts...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Podcast/ListaPodcast", Rotulo = "Podcasts" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "#", Rotulo = "Cadastro" });

            ViewBag.PodcastId = PodcastId;
            ViewBag.Tipo = Tipo;

            return View(model);
        }

        public ActionResult ListarPodcast(string Tipo)
        {

            int SiteId = GetCurrentSite();
            int UsuarioId = 1;
            int IdiomaId = 1;

            PodcastDAL podcastDAL = new PodcastDAL();
            List<Podcast> listaBanner = podcastDAL.ListarPodcast(SiteId, null, null, IdiomaId, true, false);//(SiteId, null, null, null, Convert.ToInt32(UsuarioId), IdiomaId, false, false, null, false);

            if (!string.IsNullOrEmpty(Tipo) && Tipo == "1")
            {
                listaBanner = listaBanner.FindAll(x => x.PodcastPaiId == null);
            } 
            else if (!string.IsNullOrEmpty(Tipo) && Tipo == "2")
            {
                listaBanner = listaBanner.FindAll(x => x.PodcastPaiId != null);
            }
            else
            {
                listaBanner = new List<Podcast>();
            }

            return Json(listaBanner, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarPodcast(int PodcastId, int IdiomaId)
        {
            int SiteId = GetCurrentSite();
            int UsuarioId = 1;

            PodcastDAL podcastDAL = new PodcastDAL();
            var resposta = podcastDAL.Carregar(SiteId, IdiomaId, PodcastId, UsuarioId, false);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarPodcast(string Podcast, string PodcastOld, string ListaUsuarioGrupo, string ListaUsuario)
        {
            NumberFormatInfo provider = NumberFormatInfo.CurrentInfo;

            var form = (JObject)JsonConvert.DeserializeObject(Podcast);

            Podcast _anterior = new Podcast();
            Podcast _novo = new Podcast();

            _novo.PodcastId = (int)Util.GetValue<int>(form, "PodcastId");
            _novo.SiteId = GetCurrentSite();
            _novo.PodcastGrupoId = (int)Util.GetValue<int>(form, "PodcastGrupo");
            var DataPublicacao = (String)Util.GetValue<String>(form, "DataPublicacao");
            if (!String.IsNullOrEmpty(DataPublicacao))
            {
                _novo.DataPublicacao = Convert.ToDateTime(DataPublicacao, provider);
            }

            _novo.PodcastPaiId = (int?)Util.GetValue<int?>(form, "PodcastPaiId");

            _novo.Autor = (string)Util.GetValue<string>(form, "Autor");
            //_novo.Titulo = (string)Util.GetValue<string>(form, "Titulo");
            _novo.LinkURL = (string)Util.GetValue<string>(form, "LinkURL");
            _novo.LinkURLImagem = (string)Util.GetValue<string>(form, "LinkURLImagem");
            _novo.LinkURLAudio = (string)Util.GetValue<string>(form, "LinkURLAudio");
            _novo.Keywords = (string)Util.GetValue<string>(form, "Keywords");
            //_novo.Descricao = (string)Util.GetValue<string>(form, "Descricao");
            //_novo.TituloEpisodio = (string)Util.GetValue<string>(form, "TituloEpisodio");
            _novo.DireitosAutorais = (string)Util.GetValue<string>(form, "DireitosAutorais");
            _novo.ProprietarioNome = (string)Util.GetValue<string>(form, "ProprietarioNome");
            _novo.ProprietarioEmail = (string)Util.GetValue<string>(form, "ProprietarioEmail");
            //_novo.DataPublicacao = (string)Util.GetValue<string>(form, "DataPublicacao");
            _novo.Duracao = (int?)Util.GetValue<int?>(form, "Duracao");
            //_novo.SubTitulo = (string)Util.GetValue<string>(form, "SubTitulo");
            _novo.Tamanho = (string)Util.GetValue<string>(form, "Tamanho");





            _novo.Detalhe.IdiomaId = 1;
            _novo.Detalhe.Titulo = (string)Util.GetValue<string>(form, "Titulo");
            _novo.Detalhe.SubTitulo = (string)Util.GetValue<string>(form, "SubTitulo");
            _novo.Detalhe.TituloEpisodio = (string)Util.GetValue<string>(form, "TituloEpisodio");
            _novo.Detalhe.Descricao = (string)Util.GetValue<string>(form, "Descricao");

            //_novo.Detalhe.Resumo = (string)Util.GetValue<string>(form, "Resumo");
            //_novo.Detalhe.Descricao = (string)Util.GetValue<string>(form, "Descricao");
            //_novo.Ativo = Convert.ToBoolean((int?)Util.GetValue<int?>(form, "Status"));
            //_novo.Posicao = (int?)Util.GetValue<int?>(form, "Posicao");
            //_novo.TargetId = (int)Util.GetValue<int>(form, "Target");
            //_novo.LinkURL = (string)Util.GetValue<string>(form, "LinkURL");
            //
            //_novo.Complemento.Privado = Convert.ToBoolean((int?)Util.GetValue<int?>(form, "Privado"));

            #region --> Validação
            int Tipo = (int)Util.GetValue<int>(form, "Tipo");
            PodcastResponse resp = new PodcastResponse();
            if (Tipo == 1)
            {
                if (_novo.PodcastGrupoId == null || _novo.PodcastGrupoId == 0)
                {
                    resp.Resposta.Erro = true;
                    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                    resp.Resposta.Mensagem += "- Selecionar a Categoria do Podcast";
                }
            }
            if (Tipo == 2)
            {
                if (_novo.PodcastPaiId == null || _novo.PodcastPaiId == 0)
                {
                    resp.Resposta.Erro = true;
                    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                    resp.Resposta.Mensagem += "- Informe o Canal do Podcast que deseja vincular este episódio";
                }
            }
            
            //if (_novo.TargetId == null || _novo.TargetId == 0)
            //{
            //    resp.Resposta.Erro = true;
            //    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
            //    resp.Resposta.Mensagem += "- Selecionar o Target";
            //}
            //if (Util.GetValue<int?>(form, "Status") == null)
            //{
            //    resp.Resposta.Erro = true;
            //    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
            //    resp.Resposta.Mensagem += "- Selecionar o Status: Ativo / Inativo.";
            //}
            //if (_novo.Complemento.Privado == true && string.IsNullOrEmpty(ListaUsuarioGrupo) && string.IsNullOrEmpty(ListaUsuario))
            //{
            //    resp.Resposta.Erro = true;
            //    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
            //    resp.Resposta.Mensagem += "- Informar pelo menos um Usuário ou Grupo para publicação privada.";
            //}
            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion

            List<PodcastIdiomaExcecao> Extras = new List<PodcastIdiomaExcecao>();
            List<PodcastIdiomaExcecao> ExtrasOld = new List<PodcastIdiomaExcecao>();

            #region -> Idiomas Extras
            //-- EN
            PodcastIdiomaExcecao ExtraEN = new PodcastIdiomaExcecao();
            ExtraEN.IdiomaId = (int)Util.IDIOMA.ENGLISH;
            ExtraEN.Titulo = (string)Util.GetValue<string>(form, "TituloEN");
            ExtraEN.SubTitulo = (string)Util.GetValue<string>(form, "SubTituloEN");
            ExtraEN.TituloEpisodio = (string)Util.GetValue<string>(form, "TituloEpisodioEN");
            ExtraEN.Descricao = (string)Util.GetValue<string>(form, "DescricaoEN");
            Extras.Add(ExtraEN);
            //-- ES
            PodcastIdiomaExcecao ExtraES = new PodcastIdiomaExcecao();
            ExtraES.IdiomaId = (int)Util.IDIOMA.ESPANHOL;
            ExtraES.Titulo = (string)Util.GetValue<string>(form, "TituloES");
            ExtraES.SubTitulo = (string)Util.GetValue<string>(form, "SubTituloES");
            ExtraES.TituloEpisodio = (string)Util.GetValue<string>(form, "TituloEpisodioES");
            ExtraES.Descricao = (string)Util.GetValue<string>(form, "DescricaoES");
            Extras.Add(ExtraES);
            //-- FR
            PodcastIdiomaExcecao ExtraFR = new PodcastIdiomaExcecao();
            ExtraFR.IdiomaId = (int)Util.IDIOMA.FRANCES;
            ExtraFR.Titulo = (string)Util.GetValue<string>(form, "TituloFR");
            ExtraFR.SubTitulo = (string)Util.GetValue<string>(form, "SubTituloFR");
            ExtraFR.TituloEpisodio = (string)Util.GetValue<string>(form, "TituloEpisodioFR");
            ExtraFR.Descricao = (string)Util.GetValue<string>(form, "DescricaoFR");
            Extras.Add(ExtraFR);
            #endregion

            if (PodcastOld != null && PodcastOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(PodcastOld);

                _anterior.PodcastId = (int)Util.GetValue<int>(formOld, "UsuarioId");
                _anterior.Detalhe.Titulo = (string)Util.GetValue<string>(formOld, "Titulo");
                _anterior.Detalhe.SubTitulo = (string)Util.GetValue<string>(formOld, "SubTitulo");
                _anterior.Detalhe.TituloEpisodio = (string)Util.GetValue<string>(formOld, "TituloEpisodio");
                _anterior.Detalhe.Descricao = (string)Util.GetValue<string>(formOld, "Descricao");


                #region -> Idiomas Extras (Dados anterior a alteração)
                //-- EN
                PodcastIdiomaExcecao ExtraENOld = new PodcastIdiomaExcecao();
                ExtraENOld.IdiomaId = (int)Util.IDIOMA.ENGLISH;
                ExtraENOld.Titulo = (string)Util.GetValue<string>(form, "TituloEN");
                ExtraENOld.SubTitulo = (string)Util.GetValue<string>(form, "SubTituloEN");
                ExtraENOld.TituloEpisodio = (string)Util.GetValue<string>(form, "TituloEpisodioEN");
                ExtraENOld.Descricao = (string)Util.GetValue<string>(form, "DescricaoEN");
                ExtrasOld.Add(ExtraENOld);
                //-- ES
                PodcastIdiomaExcecao ExtraESOld = new PodcastIdiomaExcecao();
                ExtraESOld.IdiomaId = (int)Util.IDIOMA.ESPANHOL;
                ExtraESOld.Titulo = (string)Util.GetValue<string>(form, "TituloES");
                ExtraESOld.SubTitulo = (string)Util.GetValue<string>(form, "SubTituloES");
                ExtraESOld.TituloEpisodio = (string)Util.GetValue<string>(form, "TituloEpisodioES");
                ExtraESOld.Descricao = (string)Util.GetValue<string>(form, "DescricaoES");
                ExtrasOld.Add(ExtraESOld);
                //-- FR
                PodcastIdiomaExcecao ExtraFROld = new PodcastIdiomaExcecao();
                ExtraFROld.IdiomaId = (int)Util.IDIOMA.FRANCES;
                ExtraFROld.Titulo = (string)Util.GetValue<string>(form, "TituloFR");
                ExtraFROld.SubTitulo = (string)Util.GetValue<string>(form, "SubTituloFR");
                ExtraFROld.TituloEpisodio = (string)Util.GetValue<string>(form, "TituloEpisodioFR");
                ExtraFROld.Descricao = (string)Util.GetValue<string>(form, "DescricaoFR");
                ExtrasOld.Add(ExtraFROld);
                #endregion
            }

            return Json(new PodcastDAL().Gravar(_novo, _anterior, Extras, ExtrasOld, ListaUsuarioGrupo, ListaUsuario), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirPodcast(int PodcastId)
        {
            return Json(new PodcastDAL().ExcluirPodcast(PodcastId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GerarURLAmigavel(string PodcastId)
        {
            
            ConfiguracaoDAL configDAL = new ConfiguracaoDAL();
            var SiteId = GetCurrentSite();
            var config = configDAL.CarregarConfiguracao(SiteId);

            var baseURL = config.HostBase;
            var retorno = "";
            if (!String.IsNullOrEmpty(PodcastId) && PodcastId != "" && PodcastId != "0") retorno = baseURL + "admin/Podcast?Id=" + PodcastId;
            return Json(retorno, JsonRequestBehavior.DenyGet);
        }

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }

    }
}
