using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Arquivo
{
    public class ArquivoResponse
    {
        public Resposta Resposta { get; set; }

        public Arquivo Arquivo { get; set; }

        public ArquivoResponse()
        {
            Resposta = new Resposta();
            Arquivo = new Arquivo();
        }
    }
}
