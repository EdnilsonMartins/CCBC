using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Arquivo
{
    public class Arquivo
    {
        public long ArquivoId { get; set; }
        public byte[] Content { get; set; }
        public string Legenda { get; set; }
        public int? IdiomaId { get; set; }
        public string Base64 { get; set; }
        public string ListaCategoria { get; set; }

        public string FileName { get; set; }
        public string Tipo { get; set; }
        public int Tamanho { get; set; }

        public int? PastaId { get; set; }

    }
}
