using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.EmailTemplate
{
    public class EmailTemplateResponse
    {
        public Resposta Resposta { get; set; }

        public EmailTemplate EmailTemplate { get; set; }

        public EmailTemplateResponse()
        {
            Resposta = new Resposta();
            EmailTemplate = new EmailTemplate();
        }
    }
}
