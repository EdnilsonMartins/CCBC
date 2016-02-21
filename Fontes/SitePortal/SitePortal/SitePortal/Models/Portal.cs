using DAL;
using DTO.Banner;
using DTO.Configuracao;
using DTO.Menu;
using DTO.Publicacao;
using DTO.Usuario;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SitePortal.Models
{
    public class Portal
    {
        public ResponseLogin Login { get; set; }

        public List<Banner> BannerPrincipal { get; set; }
        public List<Banner> BannerMantenedores { get; set; }
        public List<Banner> BannerParceiras { get; set; }
        public List<Banner> BannerRedesSociais { get; set; }
        public Banner BannerLateral { get; set; }
        public Banner BannerInferiorEsquerda { get; set; }
        public Banner BannerInferior { get; set; }
        public Banner BannerSuperiorInterna { get; set; }
        public List<Banner> BannerInferiorRotativo { get; set; }
        public List<Banner> BannerInferiorEsquerdaRotativo { get; set; }
        
        public List<Publicacao> Eventos { get; set; }
        public List<Publicacao> Noticias { get; set; }
        public List<Publicacao> Materias { get; set; }
        public List<Publicacao> Artigos { get; set; }
        public List<Publicacao> Paginas { get; set; }
        public List<Publicacao> ResultaBusca { get; set; }

        public Publicacao Evento { get; set; }
        
        public Publicacao Conteudo { get; set; }

        public List<Menu> ListaMenuPrincipal { get; set; }
        public List<Menu> ListaMenuInterna { get; set; }
        public List<Menu> ListaMenuQuick { get; set; }
        public List<Menu> ListaMenuTree = new List<Menu>();
        public List<Menu> ListaMenuInferior { get; set; }

        public Configuracao Configuracao { get; set; }

        public int SiteId { get; set; }
        public bool isHome { get; set; }
        public int AssociadoCategoriaId { get; set; }
        public string AssociadoCategoria { get; set; }

        public string TempoPesquisa { get; set; }
        public string PalavraBusca { get; set; }

        public string NrProtocoloContato { get; set; }

        public int IdiomaId { get; set; }
        public int UsuarioId { get; set; }

        public string Titulo { get; set; }
        public string TagsSite { get; set; }

        public string TedescoURL { get; set; }
        public string TedescoLogin { get; set; }
        public string TedescoEmail { get; set; }
        public string TedescoToken { get; set; }

        public UsuarioDTO Usuario { get; set; }
        public UsuarioResponse CadastroUsuarioResponse { get; set; }

        public Portal()
        {
            Login = new ResponseLogin();
            
            BannerPrincipal = new List<Banner>();
            BannerMantenedores = new List<Banner>();
            BannerParceiras = new List<Banner>();
            BannerRedesSociais = new List<Banner>();

            Eventos = new List<Publicacao>();
            Noticias = new List<Publicacao>();
            Materias = new List<Publicacao>();
            Artigos = new List<Publicacao>();
            Paginas = new List<Publicacao>();
            ResultaBusca = new List<Publicacao>();

            ListaMenuPrincipal = new List<Menu>();
            ListaMenuInterna = new List<Menu>();
            ListaMenuQuick = new List<Menu>();
            ListaMenuTree = new List<Menu>();
            ListaMenuInferior = new List<Menu>();
        }

        public Portal CarregarModel(bool CarregarTodosBanner = false)
        {
            Portal model = new Portal();

            var currentCulture = HttpContext.Current.Request.Cookies["lang"] != null ? HttpContext.Current.Request.Cookies["lang"].Value : "pt-BR";
            if (string.IsNullOrEmpty(currentCulture)) currentCulture = "pt-BR";
            IdiomaId = Util.GetIdiomaId(currentCulture);

            var currentSite = HttpContext.Current.Request.Cookies["site"] != null ? HttpContext.Current.Request.Cookies["site"].Value : "0";
            if (string.IsNullOrEmpty(currentSite)) currentSite = "0";
            int SiteId = Convert.ToInt32(currentSite);
            model.SiteId = SiteId;

            var UsuarioId = HttpContext.Current.Request.Cookies["UsuarioId"] != null ? HttpContext.Current.Request.Cookies["UsuarioId"].Value : "0";
            var UsuarioNome = HttpContext.Current.Request.Cookies["UsuarioNome"] != null ? HttpContext.Current.Request.Cookies["UsuarioNome"].Value : "";

            int _usuarioId;
            int.TryParse(UsuarioId, out _usuarioId);
            this.UsuarioId = _usuarioId;

            //Login
            if (UsuarioId == "") UsuarioId = "0";
            model.Login.UsuarioId = Convert.ToInt32(UsuarioId);
            model.Login.Nome = UsuarioNome;


            //Menu 
            model.ListaMenuPrincipal = new MenuDAL().ListarMenu(SiteId, 1, IdiomaId, null, false, Convert.ToInt32(UsuarioId));
            model.ListaMenuQuick = new MenuDAL().ListarMenu(SiteId, 2, IdiomaId, null, false, Convert.ToInt32(UsuarioId));
            model.ListaMenuInferior = new MenuDAL().ListarMenu(SiteId, 3, IdiomaId, null, true, Convert.ToInt32(UsuarioId));

            #region --> Configuração Tedesco
            UsuarioDAL userDAL = new UsuarioDAL();
            var usuario = userDAL.Carregar(Convert.ToInt32(UsuarioId));
            if (usuario != null && usuario.Usuario.TedescoUsuario != null && usuario.Usuario.TedescoEmail != null)
            {
                try
                {

                    string urlTedesco = System.Configuration.ConfigurationManager.AppSettings["TedescoURL"].ToString();
                    string urlToken = string.Format(urlTedesco + "integracao/services/token?login={0}&email={1}",
                                                    usuario.Usuario.TedescoUsuario,
                                                    usuario.Usuario.TedescoEmail);
                    
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToken);
                    
                    request.MaximumAutomaticRedirections = 4;
                    request.MaximumResponseHeadersLength = 4;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream receiveStream = response.GetResponseStream())
                        {
                            using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                            {
                                string resposta = readStream.ReadToEnd();
                                
                                dynamic stuff = JsonConvert.DeserializeObject(resposta);
                                
                                model.TedescoURL = urlTedesco;
                                model.TedescoLogin = stuff.Login;
                                model.TedescoEmail = stuff.Email;
                                model.TedescoToken = stuff.Token;

                                response.Close();
                                readStream.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    model.TedescoURL = null;
                    model.TedescoLogin = null;
                    model.TedescoEmail = null;
                    model.TedescoToken = null;
                }
                
            }
            #endregion

            #region --> BANNERS
            //Banner Principal
            if (CarregarTodosBanner)
            {
                List<Banner> listaBanner = new BannerDAL().ListarBanner(SiteId, null, 1, null, Convert.ToInt32(UsuarioId), IdiomaId);
                model.BannerPrincipal = listaBanner;
                foreach (var banner in listaBanner)
                {
                    new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, banner.BannerId, banner.ArquivoId_Primaria);
                };
            }
            
            //Banner Mantenedores
            if (CarregarTodosBanner)
            {
                List<Banner> listaMantenedores = new BannerDAL().ListarBanner(SiteId, null, 3, null, Convert.ToInt32(UsuarioId), IdiomaId);
                model.BannerMantenedores = listaMantenedores;
                foreach (var banner in listaMantenedores)
                {
                    new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, banner.BannerId, banner.ArquivoId_Primaria);
                };
            }

            //Banner Parceiras
            if (CarregarTodosBanner)
            {
                List<Banner> listaParceiras = new BannerDAL().ListarBanner(SiteId, null, 4, null, Convert.ToInt32(UsuarioId), IdiomaId);
                model.BannerParceiras = listaParceiras;
                foreach (var banner in listaParceiras)
                {
                    new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, banner.BannerId, banner.ArquivoId_Primaria);
                };
            }

            //Banner Lateral
            List<Banner> listaLateral = new BannerDAL().ListarBanner(SiteId, null, 2, null, Convert.ToInt32(UsuarioId), IdiomaId, Apenas1: true);
            if (listaLateral.Any())
            {
                model.BannerLateral = listaLateral[0];
                new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, model.BannerLateral.BannerId, model.BannerLateral.ArquivoId_Primaria);
            }

            //Banner Redes Sociais
            if (CarregarTodosBanner)
            {
                List<Banner> listaRedesSociais = new BannerDAL().ListarBanner(SiteId, null, 8, null, Convert.ToInt32(UsuarioId), IdiomaId);
                model.BannerRedesSociais = listaRedesSociais;
                foreach (var banner in listaRedesSociais)
                {
                    new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, banner.BannerId, banner.ArquivoId_Primaria);
                };
            }

            //Banner InferiorEsquerda
            if (CarregarTodosBanner)
            {
                List<Banner> listaLateralIntefior = new BannerDAL().ListarBanner(SiteId, null, 6, null, Convert.ToInt32(UsuarioId), IdiomaId);
                if (listaLateralIntefior.Any())
                {
                    model.BannerInferiorEsquerda = listaLateralIntefior[0];
                    new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, model.BannerInferiorEsquerda.BannerId, model.BannerInferiorEsquerda.ArquivoId_Primaria);
                }
            }
        
            //Banner Inferior
            if (CarregarTodosBanner)
            {
                List<Banner> listaInferior = new BannerDAL().ListarBanner(SiteId, null, 5, null, Convert.ToInt32(UsuarioId), IdiomaId);
                if (listaInferior.Any())
                {
                    model.BannerInferior = listaInferior[0];
                    new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, model.BannerInferior.BannerId, model.BannerInferior.ArquivoId_Primaria);
                }
            }

            //Banner: Home Inferior Lateral Rotativo
            if (CarregarTodosBanner)
            {
                List<Banner> listaInferiorEsquerdaRotativo = new BannerDAL().ListarBanner(SiteId, null, 9, null, Convert.ToInt32(UsuarioId), IdiomaId);
                model.BannerInferiorEsquerdaRotativo = listaInferiorEsquerdaRotativo;
                foreach (var banner in listaInferiorEsquerdaRotativo)
                {
                    new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, banner.BannerId, banner.ArquivoId_Primaria);
                };
            }

            //Banner: Home Inferior Rotativo
            if (CarregarTodosBanner)
            {
                List<Banner> listaInferiorRotativo = new BannerDAL().ListarBanner(SiteId, null, 10, null, Convert.ToInt32(UsuarioId), IdiomaId);
                model.BannerInferiorRotativo = listaInferiorRotativo;
                foreach (var banner in listaInferiorRotativo)
                {
                    new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, banner.BannerId, banner.ArquivoId_Primaria);
                };
            }

            #endregion

            PublicacaoDAL publicacaoDAL = new PublicacaoDAL();

            //Eventos
            List<Publicacao> listaEventos = publicacaoDAL.ListarPublicacao(SiteId, null, (int)Util.TIPOPUBLICACAO.EVENTO, null, null, Convert.ToInt32(UsuarioId), IdiomaId);
            model.Eventos = listaEventos;
            if (listaEventos.Count == 0)
            {
                listaEventos = publicacaoDAL.ListarPublicacao(1, null, (int)Util.TIPOPUBLICACAO.EVENTO, null, null, Convert.ToInt32(UsuarioId), IdiomaId);
                model.Eventos = listaEventos;
            }

            //Noticias
            List<Publicacao> listaNoticias = publicacaoDAL.ListarPublicacao(SiteId, null, (int)Util.TIPOPUBLICACAO.NOTICIA, null, null, Convert.ToInt32(UsuarioId), IdiomaId);
            model.Noticias = listaNoticias;

            //Materias
            List<Publicacao> listaMaterias = publicacaoDAL.ListarPublicacao(SiteId, null, (int)Util.TIPOPUBLICACAO.MATERIA, null, null, Convert.ToInt32(UsuarioId), IdiomaId);
            model.Materias = listaMaterias;

            //Artigos
            List<Publicacao> listaArtigos = publicacaoDAL.ListarPublicacao(SiteId, null, (int)Util.TIPOPUBLICACAO.ARTIGO, null, null, Convert.ToInt32(UsuarioId), IdiomaId);
            model.Artigos = listaArtigos;
            
            //Paginas
            List<Publicacao> listaPaginas = publicacaoDAL.ListarPublicacao(SiteId, null, (int)Util.TIPOPUBLICACAO.PAGINA, null, null, Convert.ToInt32(UsuarioId), IdiomaId);
            model.Paginas = listaPaginas;

            #region --> Configuracao
            ConfiguracaoDAL configDAL = new ConfiguracaoDAL();
            model.Configuracao = configDAL.CarregarConfiguracao(SiteId);
            #endregion

            #region --> Site
            model.TagsSite = new SiteDAL().CarregarSite(SiteId).Site.Tags;
            #endregion

            return model;
        }

        public void CarregarBannerInterna(int PublicacaoId)
        {
            var currentCulture = HttpContext.Current.Request.Cookies["lang"] != null ? HttpContext.Current.Request.Cookies["lang"].Value : "pt-BR";
            if (string.IsNullOrEmpty(currentCulture)) currentCulture = "pt-BR";
            int IdiomaId = Util.GetIdiomaId(currentCulture);

            var currentSite = HttpContext.Current.Request.Cookies["site"] != null ? HttpContext.Current.Request.Cookies["site"].Value : "2";
            if (string.IsNullOrEmpty(currentSite)) currentSite = "0";
            int SiteId = Convert.ToInt32(currentSite);

            var UsuarioId = ((HttpContext.Current.Request.Cookies["UsuarioId"] != null) && (!String.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioId"].Value))) ? Convert.ToInt32(HttpContext.Current.Request.Cookies["UsuarioId"].Value) : new Nullable<int>();
            var UsuarioNome = HttpContext.Current.Request.Cookies["UsuarioNome"] != null ? HttpContext.Current.Request.Cookies["UsuarioNome"].Value : "";

            //Banner Superior Interna
            List<Banner> listaSuperiorInterna = new BannerDAL().ListarBanner(SiteId, null, 7, null, UsuarioId, IdiomaId, Apenas1: true, PublicacaoId: PublicacaoId);
            if (listaSuperiorInterna.Any())
            {
                BannerSuperiorInterna = listaSuperiorInterna[0];
                new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, BannerSuperiorInterna.BannerId, BannerSuperiorInterna.ArquivoId_Primaria);

            }

            //Banner Lateral
            List<Banner> listaLateral = new BannerDAL().ListarBanner(SiteId, null, 2, null, UsuarioId, IdiomaId, Apenas1: true, PublicacaoId: PublicacaoId);
            if (listaLateral.Any())
            {
                BannerLateral = listaLateral[0];
                new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, BannerLateral.BannerId, BannerLateral.ArquivoId_Primaria);
            }
        }

        public void CarregarMenuInterna(int PublicacaoId)
        {
            var currentCulture = HttpContext.Current.Request.Cookies["lang"] != null ? HttpContext.Current.Request.Cookies["lang"].Value : "pt-BR";
            if (string.IsNullOrEmpty(currentCulture)) currentCulture = "pt-BR";
            int IdiomaId = Util.GetIdiomaId(currentCulture);

            var currentSite = HttpContext.Current.Request.Cookies["site"] != null ? HttpContext.Current.Request.Cookies["site"].Value : "2";
            if (string.IsNullOrEmpty(currentSite)) currentSite = "0";
            int SiteId = Convert.ToInt32(currentSite);

            var UsuarioId = HttpContext.Current.Request.Cookies["UsuarioId"] != null ? HttpContext.Current.Request.Cookies["UsuarioId"].Value : "0";
            var UsuarioNome = HttpContext.Current.Request.Cookies["UsuarioNome"] != null ? HttpContext.Current.Request.Cookies["UsuarioNome"].Value : "";

            MenuDAL dal = new MenuDAL();

            ListaMenuInterna = dal.ListarMenu(SiteId, 1, IdiomaId, PublicacaoId);
        }

        public void CarregarMenuTree(int PublicacaoId)
        {
            var currentCulture = HttpContext.Current.Request.Cookies["lang"] != null ? HttpContext.Current.Request.Cookies["lang"].Value : "pt-BR";
            if (string.IsNullOrEmpty(currentCulture)) currentCulture = "pt-BR";
            int IdiomaId = Util.GetIdiomaId(currentCulture);

            var currentSite = HttpContext.Current.Request.Cookies["site"] != null ? HttpContext.Current.Request.Cookies["site"].Value : "2";
            if (string.IsNullOrEmpty(currentSite)) currentSite = "0";
            int SiteId = Convert.ToInt32(currentSite);

            var UsuarioId = HttpContext.Current.Request.Cookies["UsuarioId"] != null ? HttpContext.Current.Request.Cookies["UsuarioId"].Value : "0";
            var UsuarioNome = HttpContext.Current.Request.Cookies["UsuarioNome"] != null ? HttpContext.Current.Request.Cookies["UsuarioNome"].Value : "";

            MenuDAL dal = new MenuDAL();
            ListaMenuTree = new List<Menu>();

            if (this.ListaMenuInterna.Count > 0)
            {
                ListaMenuTree = dal.ListarMenuTree(IdiomaId, PublicacaoId);

                //Sempre exibir Home!
                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Home",
                    Rotulo = "Home"
                });
            } 
            else if (Conteudo.PublicacaoTipoId == (int)Util.TIPOPUBLICACAO.EVENTO)
            {
                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Eventos/" + Conteudo.PublicacaoId + "/" + Util.GerarURLAmigavel(Conteudo.Detalhe.Titulo),
                    Rotulo = Conteudo.Detalhe.Titulo
                });

                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Eventos",
                    Rotulo = "Eventos"
                });

                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Home",
                    Rotulo = "Home"
                });
            }
            else if (Conteudo.PublicacaoTipoId == (int)Util.TIPOPUBLICACAO.NOTICIA)
            {
                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Noticias/" + Conteudo.PublicacaoId + "/" + Util.GerarURLAmigavel(Conteudo.Detalhe.Titulo),
                    Rotulo = Conteudo.Detalhe.Titulo
                });

                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Noticias",
                    Rotulo = "Noticias"
                });

                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Home",
                    Rotulo = "Home"
                });
            }
            else if (Conteudo.PublicacaoTipoId == (int)Util.TIPOPUBLICACAO.MATERIA)
            {
                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Materia/" + Conteudo.PublicacaoId + "/" + Util.GerarURLAmigavel(Conteudo.Detalhe.Titulo),
                    Rotulo = Conteudo.Detalhe.Titulo
                });

                //ListaMenuTree.Add(new Menu()
                //{
                //    MenuTipoAcaoId = 1,
                //    LinkURL = "Matérias",
                //    Rotulo = "Matérias"
                //});

                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Home",
                    Rotulo = "Home"
                });
            }
            else if (Conteudo.PublicacaoTipoId == (int)Util.TIPOPUBLICACAO.PAGINA)
            {

                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Interna/" + Conteudo.PublicacaoId + "/" + Util.GerarURLAmigavel(Conteudo.Detalhe.Titulo),
                    Rotulo = Conteudo.Detalhe.Titulo
                });

                ListaMenuTree.Add(new Menu()
                {
                    MenuTipoAcaoId = 1,
                    LinkURL = "Home",
                    Rotulo = "Home"
                });
            }
            else
            {
                ListaMenuTree = dal.ListarMenuTree(IdiomaId, PublicacaoId);
            }

        }
    }
}