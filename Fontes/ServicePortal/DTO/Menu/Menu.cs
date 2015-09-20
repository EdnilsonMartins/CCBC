using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Menu
{
    public class Menu
    {
        public int MenuId { get; set; }
        public int? MenuPaiId { get; set; }
        public int? SiteId { get; set; }
        public int? MenuTipoId { get; set; }
        public int? MenuTipoAcaoId { get; set; }
        public int? PublicacaoId { get; set; }
        public string Rotulo { get; set; }
        public string LinkURL { get; set; }
        public string ImageURL { get; set; }
        public int Posicao { get; set; }
        public int TargetId { get; set; }
        public string IconClass { get; set; }

        public MenuIdiomaExcecao Detalhe { get; set; }
        public MenuComplemento Complemento { get; set; }

        public class MenuComplemento
        {
            public bool Privado { get; set; }
            //public bool Liberado { get; set; }
            public string ListaUsuarioGrupo { get; set; }
            public string ListaUsuario { get; set; }
            //public string BannerTipo { get; set; }
        }

        public Menu(){
            Detalhe = new MenuIdiomaExcecao();
            Complemento = new MenuComplemento();
        }
    }
}
