using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Mailing
{
    public class MailingDTO
    {
        public int MailingId {get; set;}
        public int SiteId {get; set;}
        public string Nome {get; set;}
        public string Email {get; set;}
        public string Segmento {get; set;}
        public DateTime DataInclusao {get; set;}
        public bool Ativo { get; set; }
    }
}
