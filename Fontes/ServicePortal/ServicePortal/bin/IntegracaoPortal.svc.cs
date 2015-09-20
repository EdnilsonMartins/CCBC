using DAL;
using DTO.Arquivo;
using DTO.Banner;
using DTO.Configuracao;
using DTO.Menu;
using DTO.Publicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServicePortal
{
    public class IntegracaoPortal : IIntegracaoPortal
    {
        
        public ListaMenuResponse ListarMenu(int SiteId, int MenuTipoId, int IdiomaId, int? PublicacaoId)
        {
            MenuDAL dal = new MenuDAL();
            var lista = dal.ListarMenu(SiteId, MenuTipoId, IdiomaId, PublicacaoId);

            ListaMenuResponse resposta = new ListaMenuResponse();
            resposta.Menus = lista;

            return resposta;
        }

        public Publicacao CarregarPublicacao(int SiteId, int PublicacaoId, int IdiomaId)
        {
            PublicacaoDAL publicacaoDAL = new PublicacaoDAL();
            Publicacao publicacao = new Publicacao();

            List<Publicacao> lista = publicacaoDAL.ListarPublicacao(SiteId, PublicacaoId, (int)Util.TIPOPUBLICACAO.PAGINA, null, null, null, IdiomaId);
            if (lista.Any())
            {
                publicacao = lista[0];
            }
            else
            {
                lista = publicacaoDAL.ListarPublicacao(SiteId, PublicacaoId, (int)Util.TIPOPUBLICACAO.MATERIA, null, null, null, IdiomaId);
                if (lista.Any())
                {
                    publicacao = lista[0];
                }
                else
                {
                    lista = publicacaoDAL.ListarPublicacao(SiteId, PublicacaoId, (int)Util.TIPOPUBLICACAO.NOTICIA, null, null, null, IdiomaId);
                    if (lista.Any())
                    {
                        publicacao = lista[0];
                    }
                    else
                    {
                        lista = publicacaoDAL.ListarPublicacao(SiteId, PublicacaoId, (int)Util.TIPOPUBLICACAO.EVENTO, null, null, null, IdiomaId);
                        if (lista.Any())
                        {
                            publicacao = lista[0];
                        }
                        else
                        {
                            lista = publicacaoDAL.ListarPublicacao(SiteId, PublicacaoId, (int)Util.TIPOPUBLICACAO.ARTIGO, null, null, null, IdiomaId);
                            if (lista.Any())
                            {
                                publicacao = lista[0];
                            }
                        }
                    }
                }
            }

            return publicacao;
        }

        public Configuracao CarregarConfiguracao(int SiteId)
        {
            ConfiguracaoDAL configDAL = new ConfiguracaoDAL();
            Configuracao configuracao = configDAL.CarregarConfiguracao(SiteId);

            return configuracao;
        }

        public string CarregarTagSite(int SiteId)
        {
            Config.tipo = typeof(System.Web.HttpApplication);
            return new SiteDAL().CarregarSite(SiteId).Site.Tags;
        }

        public Banner CarregarBanner(int SiteId, int PublicacaoId, int BannetTipoId, int IdiomaId)
        {
            Config.tipo = typeof(System.Web.HttpApplication);
            Banner banner = new Banner();
            List<Banner> listaSuperiorInterna = new BannerDAL().ListarBanner(SiteId, null, BannetTipoId, null, null, IdiomaId, Apenas1: true, PublicacaoId: PublicacaoId);
            if (listaSuperiorInterna.Any())
            {
                banner = listaSuperiorInterna[0];
                new BannerDAL().GravarEvento(Util.BANNER_EVENTO_TIPO.VISUALIZACAO, banner.BannerId, banner.ArquivoId_Primaria);
            }
            return banner;
        }

        public Arquivo CarregarArquivo(int ArquivoId)
        {
            ArquivoDAL model = new ArquivoDAL();
            Arquivo arquivo = model.CarregarArquivo(ArquivoId);
            return arquivo;
        }

        public List<Arquivo> ListarArquivos(int? OwnerId, int? ArquivoCategoriaId, int ArquivoCategoriaTipoId)
        {
            ArquivoDAL model = new ArquivoDAL();
            return model.ListarArquivosGaleria(OwnerId, ArquivoCategoriaId, ArquivoCategoriaTipoId);
        }

        public Publicacao CarregarHome(int SiteId, int IdiomaId)
        {
            PublicacaoDAL publicacaoDAL = new PublicacaoDAL();
            Publicacao publicacao = new Publicacao();

            List<Publicacao> lista = publicacaoDAL.ListarPublicacao(SiteId, null, (int)Util.TIPOPUBLICACAO.HOME, null, null, null, IdiomaId);
            if (lista.Any())
            {
                publicacao = lista[0];
            }

            return publicacao;
        }
    
    }
}
