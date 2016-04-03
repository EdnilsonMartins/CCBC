using BackofficeCMS.Models;
using DAL;
using DTO;
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

        public ActionResult MinhaConta(int UsuarioId, int Fluxo = 0)
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
            ViewBag.Fluxo = Fluxo; //Fluxo de Pré-Cadastro Tedesco WebFull.
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
            int SiteId = GetCurrentSite();

            int fluxo = 0;

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

            fluxo = (int)Util.GetValue<int>(form, "Fluxo");

            #region --> Validação
            UsuarioResponse resp = new UsuarioResponse();

            //if (String.IsNullOrEmpty(_novo.Nome))
            //{
            //    resp.Resposta.Erro = true;
            //    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
            //    resp.Resposta.Mensagem += "- Informar o campo Nome";
            //}

            if (String.IsNullOrEmpty(_novo.Email))
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Informar o campo E-mail";
            }
            else
            {
                UsuarioDAL dalValidaEmail = new UsuarioDAL();
                UsuarioDTO userValidaEmail = dalValidaEmail.Carregar(_novo.Email).Usuario;
                if (userValidaEmail.UsuarioId != _novo.UsuarioId)
                {
                    resp.Resposta.Erro = true;
                    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                    resp.Resposta.Mensagem += "- O endereço de e-mail informado já está em uso";
                }
            }

            if (String.IsNullOrEmpty(ListaUsuarioGrupo))
            {
                resp.Resposta.Erro = true;
                if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                resp.Resposta.Mensagem += "- Selecionar o Grupo.";
            }

            if (fluxo == 1 && SiteId == 2)
            {
                if (String.IsNullOrEmpty(_novo.TedescoUsuario))
                {
                    resp.Resposta.Erro = true;
                    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                    resp.Resposta.Mensagem += "- Informar o campo Login de acesso ao WebFull";
                }
                if (String.IsNullOrEmpty(_novo.TedescoEmail))
                {
                    resp.Resposta.Erro = true;
                    if (resp.Resposta.Mensagem.Length > 0) resp.Resposta.Mensagem += "<br />";
                    resp.Resposta.Mensagem += "- Informar o campo E-mail de acesso ao WebFull";
                }
            }

            if (resp.Resposta.Erro)
            {
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            #endregion

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

            if (fluxo == 1)
            {
                _novo.TedescoStatusId = (int)Util.TEDESCO_STATUS.PRE_CADASTRO;
            }

            UsuarioResponse usuarioResponse = new UsuarioDAL().Gravar(_novo, _anterior, ListaUsuarioGrupo);

            if (fluxo == 1 || SiteId == 1)
            {
                Resposta resposta = ExecutaNotificarUsuario(usuarioResponse.Usuario.UsuarioId);
                usuarioResponse.Resposta = resposta;
            }


            return Json(usuarioResponse, JsonRequestBehavior.AllowGet);
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

        public ActionResult NotificarUsuario(int UsuarioId)
        {
            Resposta resposta = ExecutaNotificarUsuario(UsuarioId);

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        private Resposta ExecutaNotificarUsuario(int UsuarioId)
        {
            Email email = new Email();

            Resposta resposta = new Resposta();

            int SiteId = GetCurrentSite();
            int EmailTemplateId = 1;
            if (SiteId == 1) EmailTemplateId = 5;

            bool enviado = email.Enviar_NotificacaoPreCadastro_WebFull(UsuarioId, GetCurrentSite(), EmailTemplateId);
            if (enviado)
            {
                new UsuarioDAL().NotificarUsuario(UsuarioId);
            }
            resposta.Erro = !enviado;

            return resposta;
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
