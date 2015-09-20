using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Publicacao
{
    public class PublicacaoResponse
    {
        public Resposta Resposta { get; set; }

        public Publicacao Publicacao { get; set; }

        public PublicacaoResponse()
        {
            Resposta = new Resposta();
            Publicacao = new Publicacao();
        }
    }
}
