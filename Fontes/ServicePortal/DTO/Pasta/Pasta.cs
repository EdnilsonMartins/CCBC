using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Pasta
{
    public class Pasta
    {
        public int PastaId { get; set; } 
        public int? PastaPaiId  { get; set; } 
        public int SiteId  { get; set; }
        public string Descricao { get; set; }
        public int Posicao { get; set; }
    }
}
