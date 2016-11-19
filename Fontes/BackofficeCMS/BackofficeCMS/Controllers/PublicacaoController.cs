using BackofficeCMS.Models;
using DAL;
using DTO.Arquivo;
using DTO.Publicacao;
using DTO.Regra;
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
    public class PublicacaoController : Controller
    {
        
        public ActionResult ListaPublicacao()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Publicações";
            model.NavegacaoBarra.Resumo = "eventos, notícias e muito mais...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Publicacao/ListaPublicacao", Rotulo = "Publicações" });
            
            return View(model);
        }

        public ActionResult CadPublicacao(int PublicacaoId)
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Cadastro de Publicação";
            model.NavegacaoBarra.Resumo = "eventos, notícias e muito mais...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Publicacao/ListaPublicacao", Rotulo = "Publicações" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "#", Rotulo = "Cadastro" });

            ViewBag.PublicacaoId = PublicacaoId;
            return View(model);
        }

        public ActionResult CadArquivo(int ArquivoId, int OwnerId)
        {
            ViewBag.ArquivoId = ArquivoId;
            ViewBag.OwnerId = OwnerId;

            return View();
        }

        
        [HttpGet]
        public ActionResult ListarPublicacao()
        {

            int SiteId = GetCurrentSite();
            int UsuarioId = 1;
            int IdiomaId = 1;

            //Eventos
            PublicacaoDAL publicacaoDAL = new PublicacaoDAL();
            List<Publicacao> listaEventos = publicacaoDAL.ListarPublicacao(SiteId, null, null, null, null, Convert.ToInt32(UsuarioId), IdiomaId, false, false);

            //foreach (var a in listaEventos)
            //{
            //    a.Resumo = "";
            //   // a.Complemento = "";
            //}
            
            //var jsonResult = Json(listaEventos.FindAll(x=>x.PublicacaoId<1400), JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;


            return Json(listaEventos.FindAll(x=>x.PublicacaoId!=2976), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarPublicacao(int PublicacaoId, int IdiomaId)
        {
            PublicacaoDAL dal = new PublicacaoDAL();
            Publicacao publicacao = new Publicacao();

            int SiteId = GetCurrentSite();
            int UsuarioId = 1;
            //int IdiomaId = 1;

            //Eventos
            PublicacaoDAL publicacaoDAL = new PublicacaoDAL();
            var resposta = publicacaoDAL.Carregar(SiteId, IdiomaId, PublicacaoId, UsuarioId, false);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarPublicacaoRegraPasso(int PublicacaoId)
        {
            RegraResponse resposta = new RegraResponse();
            
            resposta.Regra.RegraPasso = new RegraDAL().ListarPublicacaoRegraPasso(PublicacaoId);
            resposta.Resposta.Erro = false;
            resposta.Resposta.Mensagem = "";

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPublicacaoAprovacaoHistorico(int PublicacaoId)
        {
            List<Publicacao.PublicacaoHistoricoItem> lista = new PublicacaoDAL().ListarPublicacaoAprovacaoHistorico(PublicacaoId);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VerificarUsuarioElegivel(int PublicacaoId)
        {
            int? UsuarioId = HttpContext.Request.Cookies["CMS_UsuarioId"] != null ? Convert.ToInt32(HttpContext.Request.Cookies["CMS_UsuarioId"].Value) : new Nullable<int>();

            return Json(new RegraDAL().VerificarUsuarioElegivel_Publicacao(PublicacaoId, UsuarioId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LiberarPublicacao(int PublicacaoId, bool Liberado)
        {
            //return Convert.ToInt32(HttpContext.Request.Cookies["CMS_UsuarioId"] != null ? HttpContext.Request.Cookies["CMS_UsuarioId"].Value : "0");
            int? UsuarioId = HttpContext.Request.Cookies["CMS_UsuarioId"] != null ? Convert.ToInt32(HttpContext.Request.Cookies["CMS_UsuarioId"].Value) : new Nullable<int>();
            return Json(new PublicacaoDAL().LiberarPublicacao(PublicacaoId, UsuarioId, Liberado), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarPublicacao(string Publicacao, string PublicacaoOld, string ListaUsuarioGrupo, string ListaUsuario)
        {
            NumberFormatInfo provider = NumberFormatInfo.CurrentInfo;
            
            var form = (JObject)JsonConvert.DeserializeObject(Publicacao);

            Publicacao _anterior = new Publicacao();
            Publicacao _novo = new Publicacao();

            _novo.PublicacaoId = (int)Util.GetValue<int>(form, "PublicacaoId");
            _novo.SiteId = GetCurrentSite();
            _novo.PublicacaoTipoId = (int)Util.GetValue<int>(form, "PublicacaoTipo");
            var data = (String)Util.GetValue<String>(form, "Data");
            if (!String.IsNullOrEmpty(data)){
                CultureInfo provider2 = new CultureInfo("pt-BR");
                _novo.Data = Convert.ToDateTime(data, provider2);
            }
            
            var dataValidade = (String)Util.GetValue<String>(form, "DataValidade");
            if (!String.IsNullOrEmpty(dataValidade)){
                CultureInfo provider2 = new CultureInfo("pt-BR");
                _novo.DataValidade = Convert.ToDateTime(dataValidade, provider2);
            }

            _novo.Detalhe.IdiomaId = 1;
            _novo.Detalhe.Titulo = (string)Util.GetValue<string>(form, "Titulo");
            _novo.Detalhe.Resumo = (string)Util.GetValue<string>(form, "Resumo");
            _novo.Detalhe.Conteudo = (string)Util.GetValue<string>(form, "PublicacaoConteudo");
            _novo.Ativo = Convert.ToBoolean((int?)Util.GetValue<int?>(form, "Status"));

            _novo.Destaque = Convert.ToBoolean((int?)Util.GetValue<int?>(form, "ExibirHome"));
            _novo.Posicao = (int?)Util.GetValue<int?>(form, "Posicao");
            _novo.EditoriaId = (int)Util.GetValue<int>(form, "Editoria", 0);
            _novo.Detalhe.Fonte = (string)Util.GetValue<string>(form, "Fonte");
            _novo.Tags = (string)Util.GetValue<string>(form, "tags_1");
            _novo.LinkURL = (string)Util.GetValue<string>(form, "LinkURLRevista");
            _novo.TargetId = (int?)Util.GetValue<int?>(form, "Target");

            _novo.Complemento.Privado = Convert.ToBoolean((int?)Util.GetValue<int?>(form, "Privado"));

            //var grupos = (string)Util.GetValue<string>(form, "UsuarioGrupo");

            #region --> Validação
            PublicacaoResponse resp = new PublicacaoResponse();
            if (_novo.PublicacaoTipoId == null || _novo.PublicacaoTipoId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar o Tipo de Publicação.";
            }
            if (Util.GetValue<int?>(form, "Status") == null)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar o Status: Ativo / Inativo.";
            }
            if (_novo.Complemento.Privado == true && string.IsNullOrEmpty(ListaUsuarioGrupo) && string.IsNullOrEmpty(ListaUsuario))
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Informar pelo menos um Usuário ou Grupo para publicação privada.";
            }
            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion

            List<PublicacaoIdiomaExcecao> Extras = new List<PublicacaoIdiomaExcecao>();
            List<PublicacaoIdiomaExcecao> ExtrasOld = new List<PublicacaoIdiomaExcecao>();
            
            #region -> Idiomas Extras
            //-- EN
            PublicacaoIdiomaExcecao ExtraEN = new PublicacaoIdiomaExcecao();
            ExtraEN.IdiomaId = (int)Util.IDIOMA.ENGLISH;
            ExtraEN.Titulo = (string)Util.GetValue<string>(form, "TituloEN");
            ExtraEN.Resumo = (string)Util.GetValue<string>(form, "ResumoEN");
            ExtraEN.Conteudo = (string)Util.GetValue<string>(form, "PublicacaoConteudoEN");
            //ExtraEN.Editora = (string)Util.GetValue<string>(form, "EditoriaEN");
            ExtraEN.Fonte = (string)Util.GetValue<string>(form, "FonteEN");
            Extras.Add(ExtraEN);
            //-- ES
            PublicacaoIdiomaExcecao ExtraES = new PublicacaoIdiomaExcecao();
            ExtraES.IdiomaId = (int)Util.IDIOMA.ESPANHOL;
            ExtraES.Titulo = (string)Util.GetValue<string>(form, "TituloES");
            ExtraES.Resumo = (string)Util.GetValue<string>(form, "ResumoES");
            ExtraES.Conteudo = (string)Util.GetValue<string>(form, "PublicacaoConteudoES");
            ExtraES.Editora = (string)Util.GetValue<string>(form, "EditoriaES");
            //ExtraES.Fonte = (string)Util.GetValue<string>(form, "FonteES");
            Extras.Add(ExtraES);
            //-- FR
            PublicacaoIdiomaExcecao ExtraFR = new PublicacaoIdiomaExcecao();
            ExtraFR.IdiomaId = (int)Util.IDIOMA.FRANCES;
            ExtraFR.Titulo = (string)Util.GetValue<string>(form, "TituloFR");
            ExtraFR.Resumo = (string)Util.GetValue<string>(form, "ResumoFR");
            ExtraFR.Conteudo = (string)Util.GetValue<string>(form, "PublicacaoConteudoFR");
            ExtraFR.Editora = (string)Util.GetValue<string>(form, "EditoriaFR");
            //ExtraFR.Fonte = (string)Util.GetValue<string>(form, "FonteFR");
            Extras.Add(ExtraFR);
            #endregion

            if (PublicacaoOld != null && PublicacaoOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(PublicacaoOld);

                _anterior.PublicacaoId = (int)Util.GetValue<int>(formOld, "UsuarioId");
                _anterior.Titulo = (string)Util.GetValue<string>(formOld, "Nome");
                _anterior.Resumo = (string)Util.GetValue<string>(formOld, "Login");
                //_anterior.Data = (DateTime)Util.GetValue<string>(form, "Data");

                #region -> Idiomas Extras (Dados anterior a alteração)
                //-- EN
                PublicacaoIdiomaExcecao ExtraENOld = new PublicacaoIdiomaExcecao();
                ExtraENOld.IdiomaId = (int)Util.IDIOMA.ENGLISH;
                ExtraENOld.Titulo = (string)Util.GetValue<string>(form, "TituloEN");
                ExtraENOld.Resumo = (string)Util.GetValue<string>(form, "ResumoEN");
                ExtraENOld.Conteudo = (string)Util.GetValue<string>(form, "PublicacaoConteudoEN");
                //ExtraENOld.Editora = (string)Util.GetValue<string>(form, "EditoriaEN");
                ExtraENOld.Fonte = (string)Util.GetValue<string>(form, "FonteEN");
                ExtrasOld.Add(ExtraENOld);
                //-- ES
                PublicacaoIdiomaExcecao ExtraESOld = new PublicacaoIdiomaExcecao();
                ExtraESOld.IdiomaId = (int)Util.IDIOMA.ESPANHOL;
                ExtraESOld.Titulo = (string)Util.GetValue<string>(form, "TituloES");
                ExtraESOld.Resumo = (string)Util.GetValue<string>(form, "ResumoES");
                ExtraESOld.Conteudo = (string)Util.GetValue<string>(form, "PublicacaoConteudoES");
                //ExtraESOld.Editora = (string)Util.GetValue<string>(form, "EditoriaES");
                ExtraESOld.Fonte = (string)Util.GetValue<string>(form, "FonteES");
                ExtrasOld.Add(ExtraESOld);
                //-- FR
                PublicacaoIdiomaExcecao ExtraFROld = new PublicacaoIdiomaExcecao();
                ExtraFROld.IdiomaId = (int)Util.IDIOMA.FRANCES;
                ExtraFROld.Titulo = (string)Util.GetValue<string>(form, "TituloFR");
                ExtraFROld.Resumo = (string)Util.GetValue<string>(form, "ResumoFR");
                ExtraFROld.Conteudo = (string)Util.GetValue<string>(form, "PublicacaoConteudoFR");
                //ExtraFROld.Editora = (string)Util.GetValue<string>(form, "EditoriaFR");
                ExtraFROld.Fonte = (string)Util.GetValue<string>(form, "FonteFR");
                ExtrasOld.Add(ExtraFROld);
                #endregion
            }

            return Json(new PublicacaoDAL().Gravar(_novo, _anterior, Extras, ExtrasOld, ListaUsuarioGrupo, ListaUsuario), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirPublicacao(int PublicacaoId)
        {
            return Json(new PublicacaoDAL().ExcluirPublicacao(PublicacaoId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GerarURLAmigavel(string PublicacaoTipoId, string PublicacaoId, string Titulo)
        {
            int _publicacaoTipoId;
            int.TryParse(PublicacaoTipoId, out _publicacaoTipoId);
            string rota = "";

            if (_publicacaoTipoId == (int)Util.TIPOPUBLICACAO.ARTIGO)
            {
                rota = "Artigo/";
            }
            else if (_publicacaoTipoId == (int)Util.TIPOPUBLICACAO.EVENTO)
            {
                rota = "Evento/";
            }
            else if (_publicacaoTipoId == (int)Util.TIPOPUBLICACAO.MATERIA)
            {
                rota = "Materia/";
            }
            else if (_publicacaoTipoId == (int)Util.TIPOPUBLICACAO.NOTICIA)
            {
                rota = "Noticias/";
            } else if (_publicacaoTipoId == (int)Util.TIPOPUBLICACAO.PAGINA)
            {
                rota = "Interna/";
            }

            ConfiguracaoDAL configDAL = new ConfiguracaoDAL();
            var SiteId = GetCurrentSite();
            var config = configDAL.CarregarConfiguracao(SiteId);

            var baseURL = config.HostBase;
            var retorno = "";
            if (!String.IsNullOrEmpty(PublicacaoId) && PublicacaoId != "" && PublicacaoId != "0") retorno = baseURL + rota + PublicacaoId + "/" + DAL.Util.GerarURLAmigavel(Titulo);
            return Json(retorno, JsonRequestBehavior.DenyGet);
        }

        #region -> Galeria

        public ActionResult CadGaleria(int PublicacaoId)
        {
            return View();
        }

        //[HttpPost]
        //public ContentResult UploadFiles()
        public ActionResult UploadFiles()
        {
            var r = new List<Arquivo>();

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                //string savedFileName = Path.Combine(Server.MapPath("~/App_Data"), Path.GetFileName(hpf.FileName));
                //hpf.SaveAs(savedFileName);

                byte[] image = new byte[hpf.ContentLength];
                hpf.InputStream.Read(image, 0, image.Length);

                r.Add(new Arquivo()
                {
                    FileName = hpf.FileName,
                    Tamanho = hpf.ContentLength,
                    Tipo = hpf.ContentType,
                    Content = image,
                    Legenda = "XXXX"
                });


            }

            long ArquivoId = 0;
            //Gravação no banco de dados.
            foreach (var a in r)
            {
                ArquivoDAL dal = new ArquivoDAL();
                var arquivo = dal.GravarArquivo(a, false);
                ArquivoId = arquivo.Arquivo.ArquivoId;
            }



            //return Content("{\"name\":\"" + r[0].FileName + "\",\"type\":\"" + r[0].Tipo + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Tamanho) + "\",\"ArquivoId\":\"" + ArquivoId + "\"}", "application/json");
            //return Json(new { isUploaded = true, message = "gravado!" }, "text/html", JsonRequestBehavior.AllowGet);

            return Json(new
            {
                statusCode = 200,
                status = "Image uploaded successfully.",
                file = new
                {
                    Id = 1055,
                    Name = "P1130142_ee0bbf3639aa4d06a7140839f33eb90d.JPG",
                    DisplayName = "P1130142.JPG",
                    Url = "D:\\....\\Files\\Content\\2014\\1024\\P1130142_ee0bbf3639aa4d06a7140839f33eb90d.JPG",
                    ThumbnailUrl = "D:\\....\\Files\\Content\\2014\\150\\P1130142_ee0bbf3639aa4d06a7140839f33eb90d.JPG",
                    OriginalUrl = "D:\\....\\Files\\Content\\2014\\P1130142_ee0bbf3639aa4d06a7140839f33eb90d.JPG",
                    MimeType = "image/jpeg",
                    Order = 0,
                    Size = 3657508,
                }
            }
                                , "text/html", JsonRequestBehavior.AllowGet);

        #endregion

        }


        public ActionResult CarregarArquivo(int ArquivoId)
        {
            ArquivoDAL model = new ArquivoDAL();
            //return Json(model.CarregarArquivo(ArquivoId), JsonRequestBehavior.DenyGet);
            return Json(model.ListarArquivos(null), JsonRequestBehavior.DenyGet);
        }


        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }
    }
}
