using DAL;
using DTO;
using DTO.Configuracao;
using DTO.Usuario;
using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace SitePortal.Controllers
{
    public class MinhaContaController : Controller
    {
        
        public ActionResult Index(string ID, string Fluxo = "1")
        {
            Portal model = new Portal().CarregarModel();

            string FluxoID = Request.Form["Fluxo"];

            if (ID != null && FluxoID != null && (FluxoID == "1" || FluxoID == "2"))
            {

                string nome = Request.Form["nome"];
                string login = Request.Form["login"];
                string email = Request.Form["email"];
                string senha = Request.Form["senha"];

                byte[] data = Convert.FromBase64String(ID);
                string ID2 = Encoding.UTF8.GetString(data);

                UsuarioDTO usuario = new UsuarioDAL().Carregar(Convert.ToInt32(ID2)).Usuario;
                usuario.Nome = nome;
                usuario.Login = login;
                usuario.Email = email;
                usuario.Senha = senha;

                #region --> Validação
                Util.PASSWORD_LEVEL nivelSenha = new UsuarioDAL().ValidarSenha(usuario);

                model.CadastroUsuarioResponse = new UsuarioResponse();
                model.CadastroUsuarioResponse.Resposta = new Resposta();
                string msgValidacao = "";

                if (nivelSenha == Util.PASSWORD_LEVEL.REJECTED)
                {
                    Configuracao c = new ConfiguracaoDAL().CarregarConfiguracao(2);
                    msgValidacao = string.Format("- Digite uma senha válida, com no mínimo {0} caracteres.", c.TamanhoMinimoSenha);
                }

                if (string.IsNullOrEmpty(nome))
                {
                    if (msgValidacao.Length > 0) msgValidacao += "<br />";
                    msgValidacao += "- Informe o nome do usuário.";
                }
                if (string.IsNullOrEmpty(login))
                {
                    if (msgValidacao.Length > 0 )  msgValidacao += "<br />";
                    msgValidacao += "- Informe um login.";
                }
                if (string.IsNullOrEmpty(email))
                {
                    if (msgValidacao.Length > 0) msgValidacao += "<br />";
                    msgValidacao += "- Informe um endereço de e-mail.";
                }

                if (!string.IsNullOrEmpty(msgValidacao))
                {
                    model.CadastroUsuarioResponse.Resposta.Erro = true;
                    model.CadastroUsuarioResponse.Resposta.Mensagem = msgValidacao;
                }

                #endregion

                if (FluxoID == "1" && !model.CadastroUsuarioResponse.Resposta.Erro)
                {
                    
                    usuario.Ativo = true;

                    if (usuario.Funcionalidades == null || usuario.Funcionalidades.Count == 0)
                    {
                        var func = new List<Funcionalidade>();
                        func.Add(new Funcionalidade() { Ativo = true, FuncionalidadeId = 1 });
                        usuario.Funcionalidades = func;
                    }

                    usuario.TedescoStatusId = (int)Util.TEDESCO_STATUS.CONFIRMADO;
                    usuario.TedescoDataConfirmacao = DateTime.Now;


                    UsuarioResponse resp = new UsuarioDAL().Gravar(usuario, null, usuario.ListaUsuarioGrupo, true);
                    if (resp.Resposta.Erro == false)
                    {
                        // envio do e-mail ID = 2 (Instruções de Uso)
                        ExecutaNotificarUsuario(usuario.UsuarioId, 2, 2);

                        // envio do e-mail ID = 4 (Notificação ao Administrador de Usuário ok!)
                        ExecutaNotificarAdministrador(usuario.UsuarioId, 2, 4);

                        model.NrProtocoloContato = "Seu cadastro foi atualizado com sucesso!";
                    }
                }
                else if (FluxoID == "2" && model.CadastroUsuarioResponse.Resposta.Erro)
                {
                    
                    UsuarioResponse resp = new UsuarioDAL().Gravar(usuario, null, usuario.ListaUsuarioGrupo, true);

                    model.NrProtocoloContato = "Seu cadastro foi atualizado com sucesso!";
                }

                model.Usuario = usuario;
            }
            else
            {
                byte[] data = Convert.FromBase64String(ID);
                string ID2 = Encoding.UTF8.GetString(data);

                UsuarioDTO usuario = new UsuarioDAL().Carregar(Convert.ToInt32(ID2)).Usuario;
                model.Usuario = usuario;
            }

            @ViewBag.ID = ID;
            @ViewBag.Fluxo = Fluxo; //Deixar como parâmetro e mexer na rota.

            return View(model);
        }

        
        public ActionResult Atualizar(FormCollection form)
        {
            Portal model = new Portal().CarregarModel();
            

            string ID = Request.Form["ID"];
            string Fluxo = Request.Form["Fluxo"];

            if(ID != null && Fluxo !=null && Fluxo == "1"){

                string nome = Request.Form["nome"];
                string login = Request.Form["login"];
                string email = Request.Form["email"];
                string senha = Request.Form["senha"];

                byte[] data = Convert.FromBase64String(ID);
                string ID2 = Encoding.UTF8.GetString(data);

                UsuarioDTO usuario = new UsuarioDAL().Carregar(Convert.ToInt32(ID2)).Usuario;

                usuario.Nome = nome;
                usuario.Login = login;
                usuario.Email = email;
                usuario.Senha = senha;
                usuario.Ativo = true;

                var func = new List<Funcionalidade>();
                func.Add(new Funcionalidade() { Ativo = true, FuncionalidadeId = 1 });
                usuario.Funcionalidades = func;

                usuario.TedescoStatusId = (int)Util.TEDESCO_STATUS.CONFIRMADO;
                usuario.TedescoDataConfirmacao = DateTime.Now;

                UsuarioResponse resp = new UsuarioDAL().Gravar(usuario, null, "");
                if (resp.Resposta.Erro == false)
                {
                    // envio do e-mail ID = 2 (Instruções de Uso)
                    ExecutaNotificarUsuario(usuario.UsuarioId, 2, 2);

                    // envio do e-mail ID = 4 (Notificação ao Administrador de Usuário ok!)
                    ExecutaNotificarAdministrador(usuario.UsuarioId, 2, 4);
                }
                
                usuario = resp.Usuario;

                model.Usuario = usuario;
            }

            return View(model);
        }

        public ActionResult ValidarSenha(string ID, string senha)
        {
            UsuarioResponse resp = new UsuarioResponse();

            if (ID != null)
            {
                byte[] data = Convert.FromBase64String(ID);
                string ID2 = Encoding.UTF8.GetString(data);

                UsuarioDTO usuario = new UsuarioDAL().Carregar(Convert.ToInt32(ID2)).Usuario;
                usuario.Senha = senha;

                Util.PASSWORD_LEVEL nivelSenha = new UsuarioDAL().ValidarSenha(usuario);

                if (nivelSenha == Util.PASSWORD_LEVEL.REJECTED)
                {
                    Configuracao c = new ConfiguracaoDAL().CarregarConfiguracao(2);
                    resp = new UsuarioResponse() { Resposta = new Resposta() { Erro = true, Mensagem = string.Format("Digite uma senha válida, com no mínimo {0} caractere(s).", c.TamanhoMinimoSenha) } };
                }
                else
                {
                    resp.Resposta.Mensagem = ((int)nivelSenha).ToString();
                }
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        private Resposta ExecutaNotificarUsuario(int UsuarioId, int SiteId, int EmailTemplateId)
        {
            Email email = new Email();

            Resposta resposta = new Resposta();

            bool enviado = email.Enviar_NotificacaoPreCadastro_WebFull(UsuarioId, SiteId, EmailTemplateId);
            resposta.Erro = !enviado;

            return resposta;
        }

        private Resposta ExecutaNotificarAdministrador(int UsuarioId, int SiteId, int EmailTemplateId)
        {
            Email email = new Email();

            Resposta resposta = new Resposta();

            bool enviado = email.Enviar_NotificacaoPreCadastro_WebFull(UsuarioId, SiteId, EmailTemplateId, true);
            resposta.Erro = !enviado;

            return resposta;
        }
    }
}
