using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Pasta
{
    public class PastaResponse
    {
        public Resposta Resposta { get; set; }

        public Pasta Pasta { get; set; }

        public PastaResponse()
        {
            Resposta = new Resposta();
            Pasta = new Pasta();
        }
    }
}
