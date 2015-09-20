using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Configuracao
{
    public class ConfiguracaoResponse
    {
        public Resposta Resposta { get; set; }

        public Configuracao Configuracao { get; set; }

        public ConfiguracaoResponse()
        {
            Resposta = new Resposta();
            Configuracao = new Configuracao();
        }
    }
}
