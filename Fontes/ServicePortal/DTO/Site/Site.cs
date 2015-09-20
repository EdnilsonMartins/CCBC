using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Site
{
    public class Site
    {
        public int SiteId { get; set; }
        public string Descricao {get; set;}
        public string Tags { get; set; }

        public Configuracao.Configuracao Configuracao { get; set; }

        public Site()
        {
            Configuracao = new Configuracao.Configuracao();
        }
    }
}
