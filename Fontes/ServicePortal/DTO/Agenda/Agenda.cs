using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Agenda
{
    public class Agenda
    {
        public int AgendaId { get; set; }
        public DateTime Data { get; set; }
        public string Rotulo { get; set; }
        public string LinkURL { get; set; }
        public bool Restrito { get; set; }
    }
}
