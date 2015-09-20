using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Regra
{
    public class Regra
    {
        public int RegraId { get; set; }
        public int RegraTipoId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public int SiteId { get; set; }

        public RegraTipo RegraTipo { get; set; }

        public List<RegraPasso> RegraPasso { get; set; }

        public Regra()
        {
            RegraTipo = new RegraTipo();
        }
    }

    public class RegraTipo {
        public int RegraTipoId { get; set; }
        public string Descricao { get; set; }
    }

    public class RegraPasso
    {
        public int RegraPassoId { get; set; }
        public int RegraId { get; set; }
        public int Sequencia { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeMinimaUsuariosDoGrupo { get; set; }

        public Complemento Detalhes { get; set; }

        public RegraPasso()
        {
            Detalhes = new Complemento();
        }

        public class Complemento {
            public int TotalUsuarios { get; set; }
            public bool? Resultado { get; set; }
        }
    }

    public class RegraUsuarioElegivel{
        public bool? Ok_Usuario { get; set; }
        public bool? Ok_UsuarioGrupo { get; set; }
        public bool? UsuarioElegivel { get; set; }
        public bool? Liberado { get; set; }
        public DateTime? DataAprovacao { get; set; }

        public string Data { get; set; }
        public string Hora { get; set; }
    }

    public class RegraPassoCondicao
    {
        public int RegraPassoCondicaoId { get; set; }
        public int RegraPassoId { get; set; }
        public int? UsuarioGrupoId { get; set; }
        public int? UsuarioId { get; set; }
        public int? QuantidadeMinimaUsuarios { get; set; }

        public Complemento Detalhes { get; set; }

        public RegraPassoCondicao()
        {
            Detalhes = new Complemento();
        }

        public class Complemento {
            public string Usuario { get; set; }
            public string UsuarioGrupo { get; set; }
        }
    }

    public class PublicacaoTipoRegra
    {
        public int PublicacaoTipoRegraId { get; set; }
		public int PublicacaoTipoId { get; set; }
		public int RegraId { get; set; }
        public bool Privado { get; set; }

        public Complemento Detalhes { get; set; }

        public PublicacaoTipoRegra()
        {
            Detalhes = new Complemento();
        }

        public class Complemento {
            public string PublicacaoTipo { get; set; }
            public string Regra { get; set; }
        }
    }
}
