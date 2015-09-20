﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Usuario
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public string ListaUsuarioGrupo { get; set; }
        public int SiteId { get; set; }
        public string TedescoUsuario { get; set; }
        public string TedescoEmail { get; set; }

        public List<Funcionalidade> Funcionalidades { get; set; }

        public int UsuarioGrupoId { get; set; }

        public UsuarioDTO()
        {
            Funcionalidades = new List<Funcionalidade>();
        }
    }
}
