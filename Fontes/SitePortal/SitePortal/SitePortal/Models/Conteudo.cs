using DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitePortal.Models
{
    public class Conteudo
    {
        public ResponseLogin Login { get; set; }

        public Conteudo()
        {
            Login = new ResponseLogin();
        }
    }
}