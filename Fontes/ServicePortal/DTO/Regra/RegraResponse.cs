using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Regra
{
    public class RegraResponse
    {
        public Resposta Resposta { get; set; }

        public Regra Regra { get; set; }

        public RegraResponse()
        {
            Resposta = new Resposta();
            Regra = new Regra();
        }
    }


    public class RegraPassoResponse
    {
        public Resposta Resposta { get; set; }

        public RegraPasso RegraPasso { get; set; }

        public RegraPassoResponse()
        {
            Resposta = new Resposta();
            RegraPasso = new RegraPasso();
        }
    }


    public class RegraPassoCondicaoResponse
    {
        public Resposta Resposta { get; set; }

        public RegraPassoCondicao RegraPassoCondicao { get; set; }

        public RegraPassoCondicaoResponse()
        {
            Resposta = new Resposta();
            RegraPassoCondicao = new RegraPassoCondicao();
        }
    }

    public class PublicacaoTipoRegraResponse
    {
        public Resposta Resposta { get; set; }

        public PublicacaoTipoRegra PublicacaoTipoRegra { get; set; }

        public PublicacaoTipoRegraResponse()
        {
            Resposta = new Resposta();
            PublicacaoTipoRegra = new PublicacaoTipoRegra();
        }
    }
    

}
