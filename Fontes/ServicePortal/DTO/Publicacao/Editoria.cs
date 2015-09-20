using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Publicacao
{
    public class Editoria
    {
        public int EditoriaId { get; set; }
        public int SiteId { get; set; }

        public EditoriaIdiomaExcecao Detalhe { get; set; }

        public Editoria()
        {
            Detalhe = new EditoriaIdiomaExcecao();
        }
    }
}
