using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Mailing
{
    public class MailingResponse
    {
        public Resposta Resposta { get; set; }

        public MailingDTO Mailing { get; set; }

        public MailingResponse()
        {
            Resposta = new Resposta();
            Mailing = new MailingDTO();
        }
    }
}
