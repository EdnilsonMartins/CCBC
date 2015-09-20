using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Banner
{
    public class Banner
    {
        public int BannerId { get; set; }
        public int SiteId { get; set; }
        public int BannerTipoId { get; set; }
        public int TargetId { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? DataValidade { get; set; }
        public int? Posicao { get; set; }
        public string LinkURL { get; set; }
        public long ArquivoId_Primaria { get; set; }
        public long ArquivoId_Secundaria { get; set; }
        public string Referencia { get; set; }
        public bool? Ativo { get; set; }

        public int TotalVisualizacao { get; set; }
        public int TotalClique { get; set; }

        public BannerIdiomaExcecao Detalhe { get; set; }
        public BannerComplemento Complemento { get; set; }

        public class BannerComplemento
        {
            public bool Privado { get; set; }
            public bool Liberado { get; set; }
            public string ListaUsuarioGrupo { get; set; }
            public string ListaUsuario { get; set; }
            public string BannerTipo { get; set; }
        }

        public Banner()
        {
            Detalhe = new BannerIdiomaExcecao();
            Complemento = new BannerComplemento();
        }
    }
}
