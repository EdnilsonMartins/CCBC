using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Publicacao
{
    public class PublicacaoIdiomaExcecao
    {
        public int PublicacaoIdiomaExcecaoId { get; set; }
        public int PublicacaoId { get; set; }
        public int IdiomaId { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string Conteudo { get; set; }
        public string Editora { get; set; }
        public string Fonte { get; set; }
        public string Tags { get; set; }
        public long? ArquivoDestaqueId { get; set; }
    }
}
