using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Menu
{
    public class MenuIdiomaExcecao
    {
        public int MenuIdiomaExcecaoId { get; set; }
        public int MenuId { get; set; }
        public int IdiomaId { get; set; }
        public string Rotulo { get; set; }
    }
}
