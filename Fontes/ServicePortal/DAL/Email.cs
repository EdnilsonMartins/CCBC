using DTO.EmailTemplate;
using DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace DAL
{
    public class Email
    {
        public bool EnviarMensagem(string Assunto, string Corpo, int SiteId, string EmailDestinatario, bool EnviarParaAdministrativoTI = false)
        {
            bool retorno = false;

            try
            {
                ConfiguracaoDAL config = new ConfiguracaoDAL();
                var c = config.CarregarConfiguracao(SiteId);

                var enviaMensagem = Corpo;
                var enviaAssunto = Assunto;

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Host = c.EmailHost;// "smtp.tendenza.com.br";
                //client.EnableSsl = true;
                client.Port = c.EmailPorta;
                client.Credentials = new System.Net.NetworkCredential(c.EmailUsername, c.EmailPassword);
                MailMessage mail = new MailMessage();
                mail.Sender = new System.Net.Mail.MailAddress(c.EmailUsername, c.EmailDisplayName);
                mail.From = new MailAddress("no-reply@ccbc.org.br", c.EmailDisplayName);
                
                if (EnviarParaAdministrativoTI)
                {
                    EmailDestinatario = c.EmailDestinoAdministrativoTI;
                }

                if (!string.IsNullOrEmpty(EmailDestinatario))
                {
                    if (EmailDestinatario.IndexOf(";") < 0)
                    {
                        mail.To.Add(new MailAddress(EmailDestinatario));
                    }
                    else
                    {
                        string[] EmailDestinatarios = EmailDestinatario.Split(';');
                        foreach (string dest in EmailDestinatarios)
                        {
                            if (!string.IsNullOrEmpty(dest))
                            {
                                mail.To.Add(new MailAddress(dest));
                            }
                        }
                    }
                }
                
                mail.Subject = enviaAssunto;
                mail.Body = enviaMensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                try
                {
                    client.Send(mail);
                    retorno = true;
                }
                catch (System.Exception erro)
                {
                    throw;
                }
                finally
                {
                    mail = null;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.InnerException.ToString();
            }

            return retorno;

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



        #region --> Fluxo: Notificação para Atualizar/Completar cadastro de Usuário

        public bool Enviar_NotificacaoPreCadastro_WebFull(int UsuarioId, int SiteId, int EmailTemplateId, bool EnviarParaAdministrativoTI = false)
        {
            bool retorno = false;

            UsuarioDTO usuario = new UsuarioDAL().Carregar(UsuarioId).Usuario;
            EmailTemplate emailTemplate = new EmailTemplateDAL().Carregar(SiteId, EmailTemplateId).EmailTemplate;
            if (emailTemplate.Corpo == null)
            {
                retorno = false;
            } else {
                List<EmailTemplateCampo> campos = new EmailTemplateDAL().ListarEmailTemplateCampo(EmailTemplateId);

                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(usuario.UsuarioId.ToString());
                string ID = System.Convert.ToBase64String(plainTextBytes);

                #region --> CORPO DO EMAIL
                string corpo = emailTemplate.Corpo;
                corpo = corpo.Replace("&lt;%Nome%&gt;", usuario.Nome);
                corpo = corpo.Replace("<%Nome%>", usuario.Nome);

                corpo = corpo.Replace("&lt;%Link_MinhaConta%&gt;", string.Format("http://ccbc.org.br/MinhaConta/{0}", ID));
                corpo = corpo.Replace("<%Link_MinhaConta%>", string.Format("http://ccbc.org.br/MinhaConta/{0}", ID));

                corpo = corpo.Replace("&lt;%Link_LembrarSenha%&gt;", string.Format("http://ccbc.org.br/MinhaConta/{0}/2", ID));
                corpo = corpo.Replace("<%Link_LembrarSenha%>", string.Format("http://ccbc.org.br/MinhaConta/{0}/2", ID));

                corpo = corpo.Replace("&lt;%Login_WebFull%&gt;", usuario.TedescoUsuario);
                corpo = corpo.Replace("<%Login_WebFull%>", usuario.TedescoUsuario);

                corpo = corpo.Replace("&lt;%Email_WebFull%&gt;", usuario.TedescoEmail);
                corpo = corpo.Replace("<%Email_WebFull%>", usuario.TedescoEmail);
                #endregion

                #region --> ASSUNTO DO EMAIL
                string assunto = emailTemplate.Assunto;
                assunto = assunto.Replace("&lt;%Nome%&gt;", usuario.Nome);
                assunto = assunto.Replace("<%Nome%>", usuario.Nome);

                assunto = assunto.Replace("&lt;%Link_MinhaConta%&gt;", string.Format("http://ccbc.org.br/MinhaConta/{0}", ID));
                assunto = assunto.Replace("<%Link_MinhaConta%>", string.Format("http://ccbc.org.br/MinhaConta/{0}", ID));

                assunto = assunto.Replace("&lt;%Link_LembrarSenha%&gt;", string.Format("http://ccbc.org.br/MinhaConta/{0}/2", ID));
                assunto = assunto.Replace("<%Link_LembrarSenha%>", string.Format("http://ccbc.org.br/MinhaConta/{0}/2", ID));

                assunto = assunto.Replace("&lt;%Login_WebFull%&gt;", usuario.TedescoUsuario);
                assunto = assunto.Replace("<%Login_WebFull%>", usuario.TedescoUsuario);

                assunto = assunto.Replace("&lt;%Email_WebFull%&gt;", usuario.TedescoEmail);
                assunto = assunto.Replace("<%Email_WebFull%>", usuario.TedescoEmail);
                #endregion

                string destinatario = usuario.TedescoEmail;

                #region --> Envio do E-mail
                try
                {
                    if (Email.ValidaEnderecoEmail(destinatario))
                    {
                        retorno = EnviarMensagem(assunto, corpo, SiteId, destinatario, EnviarParaAdministrativoTI);
                    }
                    else
                    {
                        retorno = false;
                    }
                }
                catch (Exception ex)
                {
                    retorno = false;
                }
                #endregion
            }

            return retorno;
        }

        #endregion


    }
}
