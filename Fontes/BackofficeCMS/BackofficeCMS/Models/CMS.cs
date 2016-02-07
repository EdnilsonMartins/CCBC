using DAL;
using DTO.Menu;
using DTO.Publicacao;
using DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackofficeCMS.Models
{
    public class CMS
    {
        public ResponseLogin Login { get; set; }
        public CMSPermissoes Permissoes { get; set; }
        public List<Menu> ListaMenu { get; set; }
        public Navegacao NavegacaoBarra { get; set; }
        public string SiteNome { get; set; }
        public int SiteId { get; set; }
        public int MenuId { get; set; }
        public List<Publicacao> ListaPublicacao { get; set; }

        public Totalizadores Resumo { get; set; }

        public List<Funcionalidade> ListaFuncionalidades { get; set; }

        public CMS()
        {
            Login = new ResponseLogin();
            Permissoes = new CMSPermissoes();
            ListaMenu = new List<Menu>();
            NavegacaoBarra = new Navegacao();
            Resumo = new Totalizadores();
            ListaPublicacao = new List<Publicacao>();
            ListaFuncionalidades = new List<Funcionalidade>();
        }

        public CMS CarregarModel()
        {
            CMS model = new CMS();

            model.Login.UsuarioId = Convert.ToInt32(HttpContext.Current.Request.Cookies["CMS_UsuarioId"] != null ? HttpContext.Current.Request.Cookies["CMS_UsuarioId"].Value : "0");
            model.Login.Nome = HttpContext.Current.Request.Cookies["CMS_UsuarioNome"] != null ? HttpContext.Current.Request.Cookies["CMS_UsuarioNome"].Value : "";

            model.ListaFuncionalidades = new UsuarioDAL().CarregarUsuarioFuncionalidade(model.Login.UsuarioId);

            model.SiteId = Convert.ToInt32(HttpContext.Current.Request.Cookies["CMS_SiteId"] != null ? (String.IsNullOrEmpty(HttpContext.Current.Request.Cookies["CMS_SiteId"].Value) ? "0" : HttpContext.Current.Request.Cookies["CMS_SiteId"].Value) : "0");
            model.SiteNome = HttpContext.Current.Request.Cookies["CMS_SiteNome"] != null ? HttpContext.Current.Request.Cookies["CMS_SiteNome"].Value : "";

            model.ListaMenu = CarregarMenu(model.ListaFuncionalidades);

            //Resumo
            model.Resumo.TotalMateria = new PublicacaoDAL().ListarPublicacao(model.SiteId, null, (int)Util.TIPOPUBLICACAO.MATERIA, null, null, 1, 1).Count;
            model.Resumo.TotalEventos = new PublicacaoDAL().ListarPublicacao(model.SiteId, null, (int)Util.TIPOPUBLICACAO.EVENTO, null, null, 1, 1).Count;
            model.Resumo.TotalNoticias = new PublicacaoDAL().ListarPublicacao(model.SiteId, null, (int)Util.TIPOPUBLICACAO.NOTICIA, null, null, 1, 1).Count;
            model.Resumo.TotalAssinantes = 0; // new PublicacaoDAL().ListarPublicacao(SiteId, null, (int)Util.TIPOPUBLICACAO.EVENTO, null, null, 1, 1).Count;

            

            return model;

        }

        public List<Menu> CarregarMenu(List<Funcionalidade> funcionalidades)
        {
            List<Menu> lista = new List<Menu>();

            Menu menu;

            #region --> Root

            menu = new Menu();
            menu.MenuId = 1;
            menu.MenuPaiId = null;
            menu.LinkURL = "";
            menu.Rotulo = "Dashboard";
            menu.IconClass = "icon-home";
            lista.Add(menu);

            menu = new Menu();
            menu.MenuId = 100;
            menu.MenuPaiId = null;
            menu.LinkURL = "";
            menu.Rotulo = "CMS";
            menu.IconClass = "icon-docs";
            lista.Add(menu);

            menu = new Menu();
            menu.MenuId = 200;
            menu.MenuPaiId = null;
            menu.LinkURL = "";
            menu.Rotulo = "CRM";
            menu.IconClass = "icon-briefcase";
            lista.Add(menu);

            menu = new Menu();
            menu.MenuId = 250;
            menu.MenuPaiId = null;
            menu.LinkURL = "X";
            menu.Rotulo = "GED";
            menu.IconClass = "icon-folder";
            lista.Add(menu);

            menu = new Menu();
            menu.MenuId = 300;
            menu.MenuPaiId = null;
            menu.LinkURL = "";
            menu.Rotulo = "Configurações";
            menu.IconClass = "icon-settings";
            lista.Add(menu);

            #endregion

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.SITE_LISTAR))
            {
                menu = new Menu();
                menu.MenuId = 8;
                menu.MenuPaiId = 100;
                menu.LinkURL = "Site/ListaSite";
                menu.Rotulo = "Site";
                menu.IconClass = "icon-globe";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.MENU_LISTAR))
            {
                menu = new Menu();
                menu.MenuId = 4;
                menu.MenuPaiId = 100;
                menu.LinkURL = "Menu/ListaMenu";
                menu.Rotulo = "Menus";
                menu.IconClass = "icon-anchor";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.BANNER_LISTAR))
            {
                menu = new Menu();
                menu.MenuId = 6;
                menu.MenuPaiId = 100;
                menu.LinkURL = "Banner/ListaBanner";
                menu.Rotulo = "Banners";
                menu.IconClass = "icon-map";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.PUBLICACAO_LISTAR))
            {
                menu = new Menu();
                menu.MenuId = 7;
                menu.MenuPaiId = 100;
                menu.LinkURL = "Publicacao/ListaPublicacao";
                menu.Rotulo = "Publicações";
                menu.IconClass = "icon-folder";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.MEDIACENTER_EXPLORAR))
            {
                menu = new Menu();
                menu.MenuId = 9;
                menu.MenuPaiId = 100;
                menu.LinkURL = "MediaCenter/Explorer";
                menu.Rotulo = "Media Center";
                menu.IconClass = "icon-camera";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.PESSOA_LISTAR))
            {
                menu = new Menu();
                menu.MenuId = 100100;
                menu.MenuPaiId = 100;
                menu.LinkURL = "Associado/ListaAssociado";
                menu.Rotulo = "Pessoas";
                menu.IconClass = "icon-user";
                lista.Add(menu);
            }

            #region --> Configurações

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.USUARIO_LISTAR))
            {
                menu = new Menu();
                menu.MenuId = 2;
                menu.MenuPaiId = 300;
                menu.LinkURL = "Usuario/ListaUsuario";
                menu.Rotulo = "Usuários";
                menu.IconClass = "icon-user";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.CONFIGURACAO_SETUP))
            {
                menu = new Menu();
                menu.MenuId = 3;
                menu.MenuPaiId = 300;
                menu.LinkURL = "Config/Setup";
                menu.Rotulo = "Configurações";
                menu.IconClass = "icon-settings";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.USUARIOGRUPO_LISTAR))
            {
                menu = new Menu();
                menu.MenuId = 10;
                menu.MenuPaiId = 300;
                menu.LinkURL = "Usuario/ListaGrupoUsuario";
                menu.Rotulo = "Grupo de Usuários";
                menu.IconClass = "icon-folder";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.CONFIGURACAO_WORKFLOW))
            {
                menu = new Menu();
                menu.MenuId = 3001;
                menu.MenuPaiId = 300;
                menu.LinkURL = "";
                menu.Rotulo = "Aprovação / Liberação";
                menu.IconClass = "icon-tag";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.CONFIGURACAO_WORKFLOW))
            {
                menu = new Menu();
                menu.MenuId = 300100;
                menu.MenuPaiId = 3001;
                menu.LinkURL = "Config/ListaRegra";
                menu.Rotulo = "Modelos de Regra";
                menu.IconClass = "";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.CONFIGURACAO_WORKFLOW))
            {
                menu = new Menu();
                menu.MenuId = 300101;
                menu.MenuPaiId = 3001;
                menu.LinkURL = "Config/ListaPublicacaoTipoRegra";
                menu.Rotulo = "Associação de Regra";
                menu.IconClass = "";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.USUARIO_ADICIONAR_PRECADASTRO_WEBFULL))
            {
                menu = new Menu();
                menu.MenuId = 295;
                menu.MenuPaiId = 300;
                menu.LinkURL = "";
                menu.Rotulo = "E-mail Administrativo";
                menu.IconClass = "icon-envelope";
                lista.Add(menu);
            }

            #endregion


            #region --> CRM

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.ATENDIMENTO_LISTARFILA))
            {
                menu = new Menu();
                menu.MenuId = 11;
                menu.MenuPaiId = 200;
                menu.LinkURL = "";
                menu.Rotulo = "Atendimento";
                menu.IconClass = "";
                lista.Add(menu);
            }

            menu = new Menu();
            menu.MenuId = 12;
            menu.MenuPaiId = 200;
            menu.LinkURL = "";
            menu.Rotulo = "E-marketing";
            menu.IconClass = "";
            lista.Add(menu);

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.EMARKETING_MAILING))
            {
                menu = new Menu();
                menu.MenuId = 5;
                menu.MenuPaiId = 12;
                menu.LinkURL = "Mailing/ListaMailing";
                menu.Rotulo = "Mailing";
                menu.IconClass = "icon-envelope";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.EMARKETING_TEMPLATES))
            {
                menu = new Menu();
                menu.MenuId = 13;
                menu.MenuPaiId = 12;
                menu.LinkURL = "X"; //Mailing/ListaTemplate
                menu.Rotulo = "Templates";
                menu.IconClass = "";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.EMARKETING_CAMPANHAS))
            {
                menu = new Menu();
                menu.MenuId = 14;
                menu.MenuPaiId = 12;
                menu.LinkURL = "X";//Mailing/ListaCampanha
                menu.Rotulo = "Campanhas";
                menu.IconClass = "";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.EMARKETING_ENVIOS))
            {
                menu = new Menu();
                menu.MenuId = 15;
                menu.MenuPaiId = 12;
                menu.LinkURL = "X"; //Mailing/ListaEnvio 
                menu.Rotulo = "Envios";
                menu.IconClass = "";
                lista.Add(menu);
            }

            if (funcionalidades.Any(x => x.FuncionalidadeId == (int)DAL.Util.FUNCIONALIDADES.EMARKETING_RELATORIOS))
            {
                menu = new Menu();
                menu.MenuId = 16;
                menu.MenuPaiId = 12;
                menu.LinkURL = "X"; //#
                menu.Rotulo = "Relatórios";
                menu.IconClass = "";
                lista.Add(menu);
            }

            #endregion

            return lista;
        }

        public List<Publicacao> CarrgearPublicacaoPendente()
        {
            List<Publicacao> lista = new List<Publicacao>();
            
            int SiteId = GetCurrentSite();
            int UsuarioId = Convert.ToInt32(HttpContext.Current.Request.Cookies["CMS_UsuarioId"] != null ? HttpContext.Current.Request.Cookies["CMS_UsuarioId"].Value : "0");
            int IdiomaId = 1;

            //Eventos
            PublicacaoDAL publicacaoDAL = new PublicacaoDAL();
            lista = publicacaoDAL.ListarPublicacao(SiteId, null, null, null, null, Convert.ToInt32(UsuarioId), IdiomaId, false, false).FindAll(x => (x.UsuarioElegivel == true) && x.DataAprovacao == null);

            var listaElegivel = lista.FindAll(x => (x.UsuarioElegivel == true) && x.DataAprovacao == null);

            return lista;
            
        }

        public class CMSPermissoes{
            
        }

        //public class CMSMenu
        //{
        //    public int MenuId { get; set; }
        //    public int? MenuPaiId { get; set; }
        //    public string URL { get; set; }
        //    public string Descrição { get; set; }
        //    public string IconClass { get; set; }
        //}
    
    
        public class Navegacao{
            
            public bool ExibirNavegacao { get; set; }
            public string Titulo { get; set; }
            public string Resumo {get; set; }
            public List<NavegacaoItem> ListaNavegacao { get; set; }

            public Navegacao()
            {
                ListaNavegacao = new List<NavegacaoItem>();
            }

            public class NavegacaoItem{
                public string URL { get; set; }
                public string Rotulo { get; set; }
            }

        }

        public class Totalizadores
        {
            public int TotalMateria { get; set; }
            public int TotalEventos { get; set; }
            public int TotalNoticias { get; set; }
            public int TotalAssinantes { get; set; }

        }

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Current.Request.Cookies["CMS_SiteId"] != null ? (String.IsNullOrEmpty(HttpContext.Current.Request.Cookies["CMS_SiteId"].Value) ? "0" : HttpContext.Current.Request.Cookies["CMS_SiteId"].Value) : "0");
        }
    }
}