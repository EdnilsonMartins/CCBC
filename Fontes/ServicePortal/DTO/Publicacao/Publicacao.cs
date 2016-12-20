using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Publicacao
{
    public class Publicacao
    {
        public int PublicacaoId { get; set; }
        public int SiteId { get; set; }
        public int PublicacaoTipoId { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? DataValidade { get; set; }
        public int? Posicao { get; set; }
        public bool? Destaque { get; set; }
        public bool? Ativo { get; set; }
        public int? EditoriaId { get; set; }

        public PublicacaoIdiomaExcecao Detalhe { get; set; }
        public PublicacaoComplemento Complemento { get; set; }
        public List<PublicacaoHistoricoItem> AprovacaoHistorico { get; set; }
        
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string Conteudo { get; set; }
        //public string Editora { get; set; }
        public string Fonte { get; set; }
        public string Tags { get; set; }
        public long ArquivoDestaqueId { get; set; }
        public long ArquivoGaleriaId { get; set; }

        public bool? UsuarioElegivel { get; set; }
        public DateTime? DataAprovacao { get; set; }

        public string LinkURL { get; set; }
        public int? TargetId { get; set; }

        public bool? ExibirLateralEsquerda { get; set; }
        public bool? ExibirLateralDireita { get; set; }
        
        public class PublicacaoComplemento
        {
            public bool? Privado { get; set; }
            public bool? Liberado { get; set; }
            public DateTime? DataLiberado { get; set; }
            public string ListaUsuarioGrupo { get; set; }
            public string ListaUsuario { get; set; }
            public string PublicacaoTipo { get; set; }
            public string Editoria { get; set; }

            public string Data { get; set; }
            public string Hora { get; set; }
        }

        public class PublicacaoHistoricoItem
        {
            public DateTime? DataLiberacao { get; set; }
            public bool? Liberado { get; set; }
            public string Descricao { get; set; }
            public int? UsuarioId { get; set; }
            public string NomeUsuario { get; set; }

            public string Data { get; set; }
            public string Hora { get; set; }
        }

        public Publicacao()
        {
            Detalhe = new PublicacaoIdiomaExcecao();
            Complemento = new PublicacaoComplemento();
            AprovacaoHistorico = new List<PublicacaoHistoricoItem>();
        }
    }
}
