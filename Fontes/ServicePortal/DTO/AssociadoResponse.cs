using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class AssociadoResponse
    {
        public Resposta Resposta { get; set; }

        public Associado Associado { get; set; }

        public AssociadoResponse()
        {
            Resposta = new Resposta();
            Associado = new Associado();
        }
    }
}
