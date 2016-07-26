using DAL;
using DTO.Menu;
using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class ContatoController : Controller
    {
        
        public ActionResult Index(FormCollection form)
        {
            //Limpa callback da página anterior!
            var _callbackPortal = new HttpCookie("CallbackPortal_Anterior", null) { HttpOnly = true };
            Response.AppendCookie(_callbackPortal);
            HttpContext.Request.Cookies.Set(_callbackPortal);
            //=======

            //WCFIntegracaoPortal.IntegracaoPortalClient wcf = new WCFIntegracaoPortal.IntegracaoPortalClient();
            //List<SitePortal.WCFIntegracaoPortal.Menu> listaMenuPrincipal = wcf.ListarMenu(1, null).Menus.ToList();
            //List<SitePortal.WCFIntegracaoPortal.Menu> listaMenuQuick = wcf.ListarMenu(2, null).Menus.ToList();

            Portal model = new Portal().CarregarModel();

            model.ListaMenuTree.Add(new Menu()
            {
                MenuTipoAcaoId = 1,
                LinkURL = "Contato",
                Rotulo = "Contato"
            });

            model.ListaMenuTree.Add(new Menu()
            {
                MenuTipoAcaoId = 1,
                LinkURL = "Home",
                Rotulo = "Home"
            });


            if (!String.IsNullOrEmpty(Request.Form["email"]))
            {
                model.NrProtocoloContato = DateTime.Now.ToString("yyMMddHHmmssCfff");

                string Assunto = Request.Form["assunto"];
                string enviaMensagem = Request.Form["Mensagem"];
                string nome = Request.Form["nome"];
                string telefone = Request.Form["telefone"];
                string departamento = Request.Form["departamento"];
                string email = Request.Form["email"];
                string assunto = Request.Form["assunto"];


                try
                {
                    ConfiguracaoDAL config = new ConfiguracaoDAL();
                    var c = config.CarregarConfiguracao(model.SiteId);


                    var SiteNome = "";
                    if (model.SiteId == 1) SiteNome = "CCBC";
                    if (model.SiteId == 2) SiteNome = "CAM-CCBC";



                    enviaMensagem = "<b>Contato via Portal " + SiteNome + "</b><br /><br />" +
                                    "Nome: " + nome + "<br />" +
                                    "Telefone: " + telefone + "<br />" +
                                    "Departamento: " + departamento + "<br />" +
                                    "Email: " + email + "<br /><br />" +
                                    "Nr Controle: <b>" + model.NrProtocoloContato + "</b><br /><br />" +
                                    "Assunto: " + assunto + "<br /><br />" +
                                    "Mensagem: <br>" +
                                    enviaMensagem;



                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                    client.Host = c.EmailHost;// "smtp.tendenza.com.br";
                    client.EnableSsl = true;
                    client.Port = c.EmailPorta;
                    client.Credentials = new System.Net.NetworkCredential(c.EmailUsername, c.EmailPassword); //"ed.martins@tendenza.com.br", "123Mudar#");
                    MailMessage mail = new MailMessage();
                    mail.Sender = new System.Net.Mail.MailAddress(c.EmailUsername, c.EmailDisplayName);
                    mail.From = new MailAddress("no-reply@ccbc.org.br", c.EmailDisplayName);
                    mail.To.Add(new MailAddress(c.EmailDestino));
                    mail.Subject = "Contato Portal";
                    mail.Body = enviaMensagem;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    try
                    {
                        client.Send(mail);
                    }
                    catch (System.Exception erro)
                    {
                        //trata erro
                    }
                    finally
                    {
                        mail = null;
                    }
                }
                catch (Exception ex)
                {
                    string erro = ex.InnerException.ToString();
                    //return ex.Message.ToString() + erro;
                }




                

            }

            model.Titulo = Resources.Portal.Contato_Titulo_1 + " " + Resources.Portal.Contato_Titulo_2;

            return View(model);
        }

        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                //define a expressão regulara para validar o email
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                // testa o email com a expressão
                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    // o email é valido
                    return true;
                }
                else
                {
                    // o email é inválido
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
