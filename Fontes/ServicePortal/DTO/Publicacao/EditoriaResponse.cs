using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Publicacao
{
    public class EditoriaResponse
    {
        public Resposta Resposta { get; set; }

        public Editoria Editoria { get; set; }

        public EditoriaResponse()
        {
            Resposta = new Resposta();
            Editoria = new Editoria();
        }
    }
}
