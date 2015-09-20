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
    [ServiceContract]
    public interface IIntegracaoPortal
    {

        [OperationContract]
        ListaMenuResponse ListarMenu(int SiteId, int MenuTipoId, int IdiomaId, int? PublicacaoId);

        [OperationContract]
        List<Arquivo> ListarArquivos(int? OwnerId, int? ArquivoCategoriaId, int ArquivoCategoriaTipoId);

        [OperationContract]
        Publicacao CarregarPublicacao(int SiteId, int PublicacaoId, int IdiomaId);

        [OperationContract]
        Configuracao CarregarConfiguracao(int SiteId);

        [OperationContract]
        String CarregarTagSite(int SiteId);

        [OperationContract]
        Banner CarregarBanner(int SiteId, int PublicacaoId, int BannetTipoId, int IdiomaId);

        [OperationContract]
        Arquivo CarregarArquivo(int ArquivoId);

        [OperationContract]
        Publicacao CarregarHome(int SiteId, int IdiomaId);
    }

}
