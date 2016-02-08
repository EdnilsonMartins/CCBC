using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.EmailTemplate
{
    public class EmailTemplate
    {
        public long EmailTemplateId { get; set; }
        public int SiteId { get; set; }
        public long EmailGrupoTemplateId { get; set; }
        public string Comentario { get; set; }
        public string Assunto { get; set; }
        public string Corpo { get; set; }

        public EmailTemplate()
        {

        }
    }
}
