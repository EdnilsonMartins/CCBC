using BackofficeCMS.Models;
using DAL;
using DTO.Usuario;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class UsuarioController : Controller
    {

        public ActionResult ListaUsuario()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 300; // CONFIGURAÇÕES
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Usuários";
            model.NavegacaoBarra.Resumo = "controle de usuários e grupos...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Usuario/ListaUsuario", Rotulo = "Usuários" });


            return View(model);
        }

        public ActionResult CadUsuario()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 300; // CONFIGURAÇÕES
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Usuários";
            model.NavegacaoBarra.Resumo = "controle de usuários e grupos...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Usario/ListaUsuario", Rotulo = "Usuários" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Usario/CadUsuario", Rotulo = "Cadastro" });

            return View(model);
        }

        public ActionResult MinhaConta(int UsuarioId)
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 300; // CONFIGURAÇÕES
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Usuários";
            model.NavegacaoBarra.Resumo = "controle de usuários e grupos...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Usuario/ListaUsuario", Rotulo = "Usuários" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Usuario/MinhaConta", Rotulo = "Minha Conta" });

            ViewBag.UsuarioId = UsuarioId;
            return View(model);
        }

        public ActionResult ListarUsuario()
        {
            int SiteId = GetCurrentSite();
            return Json(new UsuarioDAL().ListarUsuario(SiteId).OrderBy(x => x.Nome), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarUsuario(int UsuarioId)
        {
            return Json(new UsuarioDAL().Carregar(UsuarioId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GravarUsuario(string Usuario, string UsuarioOld, string ListaUsuarioGrupo)
        {
            var form = (JObject)JsonConvert.DeserializeObject(Usuario);

            UsuarioDTO _anterior = new UsuarioDTO();
            UsuarioDTO _novo = new UsuarioDTO();

            _novo.UsuarioId = (int)Util.GetValue<int>(form, "UsuarioId");
            _novo.Nome = (string)Util.GetValue<string>(form, "Nome");
            _novo.Email = (string)Util.GetValue<string>(form, "Email");
            _novo.Login = (string)Util.GetValue<string>(form, "Login");
            _novo.Senha = (string)Util.GetValue<string>(form, "Password");
            _novo.Ativo = Convert.ToBoolean((int?)Util.GetValue<int?>(form, "Ativo"));
            _novo.SiteId = GetCurrentSite();
            _novo.TedescoUsuario = (string)Util.GetValue<string>(form, "TedescoUsuario");
            _novo.TedescoEmail = (string)Util.GetValue<string>(form, "TedescoEmail");

            if (UsuarioOld != null && UsuarioOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(UsuarioOld);

                _anterior.UsuarioId = (int)Util.GetValue<int>(formOld, "UsuarioId");
                _anterior.Nome = (string)Util.GetValue<string>(formOld, "Nome");
                _anterior.Email = (string)Util.GetValue<string>(formOld, "Email");
                _anterior.Login = (string)Util.GetValue<string>(formOld, "Login");
                _anterior.Ativo = Convert.ToBoolean((int?)Util.GetValue<int?>(formOld, "Ativo"));
                _anterior.TedescoUsuario = (string)Util.GetValue<string>(formOld, "TedescoUsuario");
                _anterior.TedescoEmail = (string)Util.GetValue<string>(formOld, "TedescoEmail");

            }

            List<Funcionalidade> funcs = new UsuarioDAL().ListarFuncionalidades(1);
            funcs.ForEach(delegate(Funcionalidade f)
            {
                ValidarFuncionalidade(form, f.FuncionalidadeId, ref _novo);
            });

            return Json(new UsuarioDAL().Gravar(_novo, _anterior, ListaUsuarioGrupo), JsonRequestBehavior.AllowGet);
        }

        private void ValidarFuncionalidade(JObject form, int FuncId, ref UsuarioDTO _novo)
        {
            bool? optFuncionalidade = (bool?)Util.GetValue<bool?>(form, "optFunc_" + FuncId, 1);
            if (optFuncionalidade == true) _novo.Funcionalidades.Add(new Funcionalidade() { FuncionalidadeId = FuncId, Ativo = true, Parametro = null });
        }

        public ActionResult ExcluirUsuario(int UsuarioId)
        {
            return Json(new UsuarioDAL().ExcluirUsuario(UsuarioId), JsonRequestBehavior.AllowGet);
        }


        #region --> Grupo de Usuário

        public ActionResult ListaGrupoUsuario()
        {

            CMS model = new CMS().CarregarModel();

            model.MenuId = 300; // CONFIGURAÇÕES
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Grupo de Usuários";
            model.NavegacaoBarra.Resumo = "cadastro de grupo de usuários...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Usuario/ListaGrupoUsuario", Rotulo = "Grupo de Usuários" });


            return View(model);

        }

        public ActionResult CadUsuarioGrupo(int UsuarioGrupoId, int? UsuarioGrupoPaiId)
        {
            ViewBag.UsuarioGrupoId = UsuarioGrupoId;
            ViewBag.UsuarioGrupoPaiId = UsuarioGrupoPaiId;

            CMS model = new CMS().CarregarModel();

            model.MenuId = 300; // CONFIGURAÇÕES
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Grupo de Usuários";
            model.NavegacaoBarra.Resumo = "cadastro de grupo de usuários...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Usario/ListaGrupoUsuario", Rotulo = "Grupo de Usuários" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Usario/CadGrupoUsuario", Rotulo = "Cadastro" });

            

            return View(model);
        }

        public ActionResult ListarUsuarioGrupo()
        {
            int SiteId = GetCurrentSite();
            return Json(new UsuarioDAL().ListarUsuarioGrupo(SiteId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarUsuarioGrupo(int UsuarioGrupoId)
        {
            return Json(new UsuarioDAL().CarregarUsuarioGrupo(UsuarioGrupoId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GravarUsuarioGrupo(string UsuarioGrupo, string UsuarioGrupoOld)
        {
            var form = (JObject)JsonConvert.DeserializeObject(UsuarioGrupo);

            UsuarioGrupoDTO _anterior = new UsuarioGrupoDTO();
            UsuarioGrupoDTO _novo = new UsuarioGrupoDTO();

            _novo.UsuarioGrupoId = (int)Util.GetValue<int>(form, "UsuarioGrupoId");
            _novo.UsuarioGrupoPaiId = (int)Util.GetValue<int>(form, "UsuarioGrupoPaiId");
            _novo.Nome = (string)Util.GetValue<string>(form, "Nome");
            _novo.SiteId = GetCurrentSite();

            if (UsuarioGrupoOld != null && UsuarioGrupoOld != "null")
            {
                var formOld = (JObject)JsonConvert.DeserializeObject(UsuarioGrupoOld);

                _anterior.UsuarioGrupoId = (int)Util.GetValue<int>(formOld, "UsuarioGrupoId");
                _anterior.Nome = (string)Util.GetValue<string>(formOld, "Nome");
            }

            return Json(new UsuarioDAL().GravarUsuarioGrupo(_novo, _anterior), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcluirUsuarioGrupo(int UsuarioGrupoId)
        {
            return Json(new UsuarioDAL().ExcluirUsuarioGrupo(UsuarioGrupoId), JsonRequestBehavior.AllowGet);
        }

        #endregion

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }
    }
}
