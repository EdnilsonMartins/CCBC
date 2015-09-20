using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Site
{
    public class SiteResponse
    {
        public Resposta Resposta { get; set; }

        public Site Site { get; set; }

        public SiteResponse()
        {
            Resposta = new Resposta();
            Site = new Site();
        }
    }
}
