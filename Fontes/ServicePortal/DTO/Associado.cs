using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Associado
    {
        public int AssociadoId { get; set; }
        public int SiteId { get; set; }
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public int AssociadoCategoriaId { get; set; }
        public int TipoPessoaId { get; set; }
        public int PaisId { get; set; }

        public AssociadoComplemento Detalhe { get; set; }

        public Associado()
        {
            Detalhe = new AssociadoComplemento();
        }

        public class AssociadoComplemento
        {
            public string TipoPessoa { get; set; }
            public string AssociadoCategoria { get; set; }
            public string Pais { get; set; }
        }
    }
}
