using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.EmailTemplate
{
    public class EmailTemplateCampo
    {
        public long EmailTemplateCampoId { get; set; }
        public int EmailTemplateId { get; set; }
        public string Nome { get; set; }
        public string Tag { get; set; }
    }
}
