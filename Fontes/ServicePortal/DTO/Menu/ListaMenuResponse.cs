using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Menu
{
    public class ListaMenuResponse
    {
        public List<Menu> Menus { get; set; }

        public ListaMenuResponse()
        {
            Menus = new List<Menu>();
        }
    }
}
