using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Usuario
{
    public class ResponseLogin
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public Resposta Resposta { get; set; }

        public ResponseLogin()
        {
            this.Resposta = new Resposta();
        }
    }
}
