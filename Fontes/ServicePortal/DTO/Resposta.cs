using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Resposta
    {
        public bool Erro { get; set; }
        public string Mensagem { get; set; }

        public Resposta()
        {
            Mensagem = "";
        }
    }
}
