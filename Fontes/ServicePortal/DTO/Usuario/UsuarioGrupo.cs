using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Usuario
{
    public class UsuarioGrupoDTO
    {
        public int UsuarioGrupoId { get; set; }
        public string Nome { get; set; }
        public int SiteId { get; set; }
        public int? UsuarioGrupoPaiId { get; set; }
    }
}
