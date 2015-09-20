using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Publicacao
{
    public class EditoriaIdiomaExcecao
    {
        public int EditoriaIdiomaExcecaoId { get; set; }
        public int EditoriaId { get; set; }
        public int IdiomaId { get; set; }
        public string Descricao { get; set; }
    }
}
