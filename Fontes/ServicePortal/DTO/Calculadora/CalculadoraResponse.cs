using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Calculadora
{
    public class CalculadoraResponse
    {
        public Resposta Resposta { get; set; }

        public Calculadora Calculadora { get; set; }

        public CalculadoraResponse()
        {
            Resposta = new Resposta();
            Calculadora = new Calculadora();
        }
    }
}
