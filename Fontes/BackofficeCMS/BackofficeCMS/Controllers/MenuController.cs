using BackofficeCMS.Models;
using DAL;
using DTO.Menu;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class MenuController : Controller
    {

        public ActionResult ListaMenu()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 100; //CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Menus do Portal";
            model.NavegacaoBarra.Resumo = "gerenciamento do menu superior, quick e inferior...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Menu/ListaMenu", Rotulo = "Menus" });

            return View(model);
        }

        public ActionResult ListarMenu(int MenuTipoId)
        {

            int SiteId = GetCurrentSite();
            int UsuarioId = 1;
            int IdiomaId = 1;

            //Eventos
            MenuDAL menuDAL = new MenuDAL();
            List<Menu> listaMenu = menuDAL.ListarMenu(SiteId, MenuTipoId, (int)Util.IDIOMA.PORTUGUES, null, true, Convert.ToInt32(UsuarioId), false);


            return Json(listaMenu, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CadMenu(int MenuId, int? MenuPaiId, int? MenuTipoId)
        {
            CMS model = new CMS().CarregarModel();
            ViewBag.MenuId = MenuId;
            ViewBag.MenuPaiId = MenuPaiId;
            ViewBag.MenuTipoId = MenuTipoId;

            model.MenuId = 100; // CMS
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Item do Menu";
            model.NavegacaoBarra.Resumo = "direcionamento de conteúdo, caminhos e ações";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Menu/ListaMenu", Rotulo = "Menus" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Menu/CadMenu/" + MenuId, Rotulo = "Cadastro" });


            return View(model);
        }

        public ActionResult CarregarMenu(int MenuId, int IdiomaId)
        {
            MenuDAL dal = new MenuDAL();
            Menu menu = new Menu();

            int SiteId = 2;
            int UsuarioId = 1;
            //int IdiomaId = 1;

            var resposta = dal.Carregar(MenuId, IdiomaId, false);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarMenu(string Menu, string MenuOld, string ListaUsuarioGrupo, string ListaUsuario)
        {
           
            var form = (JObject)JsonConvert.DeserializeObject(Menu);

            Menu _anterior = new Menu();
            Menu _novo = new Menu();

            _novo.MenuId = (int)Util.GetValue<int>(form, "MenuId");
            _novo.MenuPaiId = (int?)Util.GetValue<int?>(form, "MenuPaiId");
            _novo.SiteId = GetCurrentSite();
            _novo.MenuTipoAcaoId = (int)Util.GetValue<int>(form, "MenuTipoAcao"); ;
            _novo.MenuTipoId = (int)Util.GetValue<int>(form, "MenuTipo");
            _novo.TargetId = (int)Util.GetValue<int>(form, "Target");
            _novo.LinkURL = (string)Util.GetValue<string>(form, "LinkURL");
            _novo.Detalhe.IdiomaId = (int)Util.IDIOMA.PORTUGUES;
            _novo.Detalhe.Rotulo = (string)Util.GetValue<string>(form, "Rotulo");

            _novo.Complemento.Privado = Convert.ToBoolean((int?)Util.GetValue<int?>(form, "Privado"));

            #region --> Validação
            MenuResponse resp = new MenuResponse();
            if (string.IsNullOrEmpty(_novo.Detalhe.Rotulo) || string.IsNullOrWhiteSpace(_novo.Detalhe.Rotulo))
            {
                resp.Resposta.Erro = true;
                resp.Resposta.Mensagem += "- Informar um Rótulo";
            }
            if (_novo.MenuTipoAcaoId == null || _novo.MenuTipoAcaoId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar o Tipo de Ação";
            }
            if (_novo.TargetId == null || _novo.TargetId == 0)
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar o Target";
            }
            if (_novo.MenuTipoAcaoId == 2) // 2-Conteúdo = Link ou Publicacao!
            {
                if (string.IsNullOrEmpty(_novo.LinkURL) || string.IsNullOrEmpty(_novo.LinkURL.Trim()))
                {
                    resp.Resposta.Erro = true;
                    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                    resp.Resposta.Mensagem += "- Informar o link";
                }
            }
            if (_novo.Complemento.Privado == true && string.IsNullOrEmpty(ListaUsuarioGrupo) && string.IsNullOrEmpty(ListaUsuario))
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Informar pelo menos um Usuário ou Grupo para publicação privada.";
            }
            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion

            #region --> PublicacaoId
            if (_novo.MenuTipoAcaoId == 2) // 2-Conteúdo = Link ou Publicacao!
            {
                var _index = -1;
                int _publicacaoID = 0;

                if (_novo.LinkURL.IndexOf("/Eventos/") > -1)
                {
                    _index = _novo.LinkURL.IndexOf("/Eventos/");
                    var _posIniID = _index + ("/Eventos/").Length;
                    var _posFimID = _novo.LinkURL.IndexOf("/", (_index + ("/Eventos/").Length));
                    int.TryParse(_novo.LinkURL.Substring(_posIniID, _posFimID - _posIniID), out _publicacaoID);
                }
                else if (_novo.LinkURL.IndexOf("/Noticias/") > -1)
                {
                    _index = _novo.LinkURL.IndexOf("/Noticias/");
                    var _posIniID = _index + ("/Noticias/").Length;
                    var _posFimID = _novo.LinkURL.IndexOf("/", (_index + ("/Noticias/").Length));
                    int.TryParse(_novo.LinkURL.Substring(_posIniID, _posFimID - _posIniID), out _publicacaoID);
                }
                else if (_novo.LinkURL.IndexOf("/Materia/") > -1)
                {
                    _index = _novo.LinkURL.IndexOf("/Materia/");
                    var _posIniID = _index + ("/Materia/").Length;
                    var _posFimID = _novo.LinkURL.IndexOf("/", (_index + ("/Materia/").Length));
                    int.TryParse(_novo.LinkURL.Substring(_posIniID, _posFimID - _posIniID), out _publicacaoID);
                }
                else if (_novo.LinkURL.IndexOf("/Interna/") > -1)
                {
                    _index = _novo.LinkURL.IndexOf("/Interna/");
                    var _posIniID = _index + ("/Interna/").Length;
                    var _posFimID = _novo.LinkURL.IndexOf("/", (_index + ("/Interna/").Length));
                    int.TryParse(_novo.LinkURL.Substring(_posIniID, _posFimID - _posIniID), out _publicacaoID);
                }
                
                _novo.PublicacaoId = _publicacaoID == 0 ? new Nullable<int>(): _publicacaoID;

            }
            #endregion


            List<MenuIdiomaExcecao> Extras = new List<MenuIdiomaExcecao>();
            List<MenuIdiomaExcecao> ExtrasOld = new List<MenuIdiomaExcecao>();

            #region -> Idiomas Extras
            //-- EN
            MenuIdiomaExcecao ExtraEN = new MenuIdiomaExcecao();
            ExtraEN.IdiomaId = (int)Util.IDIOMA.ENGLISH;
            ExtraEN.Rotulo = (string)Util.GetValue<string>(form, "RotuloEN");
            Extras.Add(ExtraEN);
            //-- ES
            MenuIdiomaExcecao ExtraES = new MenuIdiomaExcecao();
            ExtraES.IdiomaId = (int)Util.IDIOMA.ESPANHOL;
            ExtraES.Rotulo = (string)Util.GetValue<string>(form, "RotuloES");
            Extras.Add(ExtraES);
            //-- FR
            MenuIdiomaExcecao ExtraFR = new MenuIdiomaExcecao();
            ExtraFR.IdiomaId = (int)Util.IDIOMA.FRANCES;
            ExtraFR.Rotulo = (string)Util.GetValue<string>(form, "RotuloFR");
            Extras.Add(ExtraFR);
            #endregion

            if (MenuOld != null && MenuOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(MenuOld);

                _anterior.MenuId = (int)Util.GetValue<int>(formOld, "MenuId");
                _anterior.MenuPaiId = (int)Util.GetValue<int>(formOld, "MenuPaiId");
                _anterior.MenuTipoId = (int)Util.GetValue<int>(formOld, "MenuTipo");
                _anterior.LinkURL = (string)Util.GetValue<string>(formOld, "LinkURL");
                _anterior.Detalhe.IdiomaId = (int)Util.IDIOMA.PORTUGUES;
                _anterior.Detalhe.Rotulo = (string)Util.GetValue<string>(formOld, "Rotulo");

                #region -> Idiomas Extras (Dados anterior a alteração)
                //-- EN
                MenuIdiomaExcecao ExtraENOld = new MenuIdiomaExcecao();
                ExtraENOld.IdiomaId = (int)Util.IDIOMA.ENGLISH;
                ExtraENOld.Rotulo = (string)Util.GetValue<string>(formOld, "RotuloEN");
                ExtrasOld.Add(ExtraENOld);
                //-- ES
                MenuIdiomaExcecao ExtraESOld = new MenuIdiomaExcecao();
                ExtraESOld.IdiomaId = (int)Util.IDIOMA.ESPANHOL;
                ExtraESOld.Rotulo = (string)Util.GetValue<string>(formOld, "RotuloES");
                ExtrasOld.Add(ExtraESOld);
                //-- FR
                MenuIdiomaExcecao ExtraFROld = new MenuIdiomaExcecao();
                ExtraFROld.IdiomaId = (int)Util.IDIOMA.FRANCES;
                ExtraFROld.Rotulo = (string)Util.GetValue<string>(formOld, "RotuloFR");
                ExtrasOld.Add(ExtraFROld);
                #endregion
            }

            return Json(new MenuDAL().Gravar(_novo, _anterior, Extras, ExtrasOld, ListaUsuarioGrupo, ListaUsuario), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirMenu(int MenuId)
        {
            return Json(new MenuDAL().Excluir(MenuId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReposicionarMenu(int MenuId, int MenuPaiId, int Posicao)
        {
            MenuDAL dal = new MenuDAL();
            Menu menu = new Menu();
            menu.MenuId = MenuId;
            menu.MenuPaiId = MenuPaiId == 0 ? new Nullable<int>() : MenuPaiId;
            menu.Posicao = Posicao + 1;
            return Json(dal.Reposicionar(menu), JsonRequestBehavior.AllowGet);
        }

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }
    }
}
