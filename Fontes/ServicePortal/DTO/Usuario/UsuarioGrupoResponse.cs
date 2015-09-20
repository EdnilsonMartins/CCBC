using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Usuario
{
    public class UsuarioGrupoResponse
    {
        public Resposta Resposta { get; set; }

        public UsuarioGrupoDTO UsuarioGrupo { get; set; }

        public UsuarioGrupoResponse()
        {
            Resposta = new Resposta();
            UsuarioGrupo = new UsuarioGrupoDTO();
        }
    }
}
