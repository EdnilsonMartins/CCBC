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
        public void EnviarMensagem(string Assunto, string Remetente, string enviaMensagem)
        {
            string Destinatario = "ed.martins@tendenza.com.br";
                try
                {
                    // valida o email
                    bool bValidaEmail = ValidaEnderecoEmail(Destinatario);

                    // Se o email não é validao retorna uma mensagem
                    //if (bValidaEmail == false)
                        //return "Email do destinatário inválido: " + Destinatario;

                    
                    // cria uma mensagem
                    MailMessage mensagemEmail = new MailMessage();

                    SmtpClient client = new SmtpClient("smtp.tendenza.com", 587);
                    client.EnableSsl = true;
                    NetworkCredential cred = new NetworkCredential("SEU_EMAIL@gmail.com", "SUA_SENHA");
                    client.Credentials = cred;

                    // inclui as credenciais
                    client.UseDefaultCredentials = true;

                    // envia a mensagem
                    client.Send(mensagemEmail);

                    //return "Mensagem enviada para  " + Destinatario + " às " + DateTime.Now.ToString() + ".";
                }
                catch (Exception ex)
                {
                    string erro = ex.InnerException.ToString();
                    //return ex.Message.ToString() + erro;
                }

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
