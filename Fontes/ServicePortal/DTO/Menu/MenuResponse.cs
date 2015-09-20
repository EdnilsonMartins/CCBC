using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Menu
{
    public class MenuResponse
    {
        public Resposta Resposta { get; set; }

        public Menu Menu { get; set; }

        public MenuResponse()
        {
            Resposta = new Resposta();
            Menu = new Menu();
        }
    }
}
