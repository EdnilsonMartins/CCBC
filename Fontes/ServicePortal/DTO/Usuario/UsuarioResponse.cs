using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Usuario
{
    public class UsuarioResponse
    {
        public Resposta Resposta { get; set; }

        public UsuarioDTO Usuario { get; set; }

        public UsuarioResponse()
        {
            Resposta = new Resposta();
            Usuario = new UsuarioDTO();
        }
    }
}
