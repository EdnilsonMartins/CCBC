using BackofficeCMS.Models;
using DAL;
using DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class LoginController : Controller
    {
        
        public ActionResult Index(LoginModel login)
        {
            ResponseLogin resp = new ResponseLogin();

            resp = new LoginDAL().EfetuarLogin(login.username, login.password);
            if (resp.Resposta.Erro)
            {
                login.Autenticado = false;
                login.Mensagem = "Login inválido. Acesso Negado!";
            }
            else
            {
                var usuarioCookie = new HttpCookie("CMS_UsuarioId", resp.UsuarioId.ToString()) { HttpOnly = true };
                Response.AppendCookie(usuarioCookie);
                var usuarioNomeCookie = new HttpCookie("CMS_UsuarioNome", resp.Nome.ToString()) { HttpOnly = true };
                Response.AppendCookie(usuarioNomeCookie);
                var permanecerConectadoCookie = new HttpCookie("CMS_PermanecerConectado", login.PermanecerConectado.ToString()) { HttpOnly = true };
                Response.AppendCookie(permanecerConectadoCookie);

                #region --> Verifica site Padrão

                List<Funcionalidade> listaFuncionalidades = new UsuarioDAL().CarregarUsuarioFuncionalidade(resp.UsuarioId);
                string SiteId = "0";
                string SiteNome = "";

                if (listaFuncionalidades.Any(x => x.FuncionalidadeId == 10))
                {
                    SiteId = "1";
                    SiteNome = "CCBC";
                } else if (listaFuncionalidades.Any(x => x.FuncionalidadeId == 20))
                {
                    SiteId = "2";
                    SiteNome = "CAM-CCBC";
                }
                else
                {
                    resp.Resposta.Erro = true;
                    login.Autenticado = false;
                    login.Mensagem = "Acesso restrito ao CMS!";
                }

                var siteId = new HttpCookie("CMS_SiteId", SiteId) { HttpOnly = true };
                Response.AppendCookie(siteId);

                var siteNome = new HttpCookie("CMS_SiteNome", SiteNome) { HttpOnly = true };
                Response.AppendCookie(siteNome);

                #endregion

                if (!resp.Resposta.Erro)
                {
                    return RedirectPermanent("~/");
                }
            }

            return View(login);
        }

        public ActionResult RecuperarSenha(string Email)
        {
            UsuarioResponse resp = new UsuarioResponse();

            resp = new UsuarioDAL().Carregar(Email);
            if (resp.Resposta.Erro)
            {
                //Retornar mensagem de erro.
            }
            else
            {
                if (resp.Usuario.UsuarioId != 0)
                {
                    //Enviar e-mail.
                }
                else
                {
                    resp.Resposta.Erro = true;
                    resp.Resposta.Mensagem = "Usuário não localizado, verifique o endereço de e-mail informado!";
                }
            }

            return Json(resp, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult EfetuarLogoff()
        {
            var usuarioCookie = new HttpCookie("CMS_UsuarioId", null) { HttpOnly = true };
            Response.AppendCookie(usuarioCookie);
            var usuarioNomeCookie = new HttpCookie("CMS_UsuarioNome", null) { HttpOnly = true };
            Response.AppendCookie(usuarioNomeCookie);
            var permanecerConectadoCookie = new HttpCookie("CMS_PermanecerConectado", null) { HttpOnly = true };
            Response.AppendCookie(permanecerConectadoCookie);

            var siteIdCookie = new HttpCookie("CMS_SiteId", null) { HttpOnly = true };
            Response.AppendCookie(siteIdCookie);
            var siteNomeCookie = new HttpCookie("CMS_SiteNome", null) { HttpOnly = true };
            Response.AppendCookie(siteNomeCookie);

            return RedirectPermanent("~/Login");

            //return Json("", JsonRequestBehavior.DenyGet);
        }
    }
}
