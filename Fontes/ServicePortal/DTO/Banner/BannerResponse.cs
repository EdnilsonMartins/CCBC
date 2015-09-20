using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Banner
{
    public class BannerResponse
    {
        public Resposta Resposta { get; set; }

        public Banner Banner { get; set; }

        public BannerResponse()
        {
            Resposta = new Resposta();
            Banner = new Banner();
        }
    }
}
