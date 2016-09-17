using BackofficeCMS.Models;
using DAL;
using DTO.Mailing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BackofficeCMS.Controllers
{
    public class MailingController : Controller
    {
        public ActionResult ListaMailing()
        {
            CMS model = new CMS().CarregarModel();

            model.MenuId = 200; // CRM
            model.NavegacaoBarra.ExibirNavegacao = true;
            model.NavegacaoBarra.Titulo = "Mailing";
            model.NavegacaoBarra.Resumo = "relação de assinantes do News Letter do site...";
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/", Rotulo = "Home" });
            model.NavegacaoBarra.ListaNavegacao.Add(new CMS.Navegacao.NavegacaoItem() { URL = "/Mailing/ListaMailing", Rotulo = "Mailing" });

            return View(model);
        }

        public ActionResult ListarMailing()
        {
            int? SiteId = GetCurrentSite();
            int UsuarioId = 1;
            int IdiomaId = 1;

            //Mailing
            MailingDAL mailingDAL = new MailingDAL();
            List<MailingDTO> listaMailing = mailingDAL.ListarMailing(null, SiteId);

            return Json(listaMailing, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportarExcel()
        {
            
            int? SiteId = GetCurrentSite();

            HttpContext.Response.Clear();
            HttpContext.Response.AddHeader("content-disposition", string.Format("attachment;filename=Mailing_{0}.xls", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));

            HttpContext.Response.ContentType = "application/ms-excel";
            HttpContext.Response.ContentEncoding = System.Text.Encoding.Default;

            //Mailing
            MailingDAL mailingDAL = new MailingDAL();
            List<MailingDTO> lista = mailingDAL.ListarMailing(null, SiteId);

            var tabela = new StringBuilder();

            CriarTagDeEstilo(tabela);

            CriarCabecalho(tabela);

            lista.ForEach(item => { CriarConteudo(tabela, item); });

            CriarTagDeRodape(tabela);

            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Write(tabela.ToString());
            System.Threading.Thread.Sleep(2000);
            HttpContext.Response.End();
            System.Threading.Thread.Sleep(2000);

            return null;
            
        }

        private static void CriarTagDeEstilo(StringBuilder sb)
        {
            sb.Append("<style type=\"text/css\">\r\n");
            sb.Append(".tabHead\r\n");
            sb.Append("{\r\n");
            sb.Append("   background-color: #cccccc;\r\n");
            sb.Append("   border: solid 1px black;\r\n");
            sb.Append("}\r\n");
            sb.Append(".tabRow\r\n");
            sb.Append("{\r\n");
            sb.Append("   border: solid 0.8em black;\r\n");
            sb.Append("}\r\n");
            sb.Append("</style>\r\n\r\n");
        }

        private static void CriarCabecalho(StringBuilder cabecalho)
        {
            cabecalho.AppendFormat("<table>\r\n");
            cabecalho.AppendFormat("<thead>\r\n");
            cabecalho.AppendFormat("<tr>\r\n");
            cabecalho.AppendFormat("\t<td class=\"tabHead\">{0}</td>\r\n", "ID");
            cabecalho.AppendFormat("\t<td class=\"tabHead\">{0}</td>\r\n", "Nome");
            cabecalho.AppendFormat("\t<td class=\"tabHead\">{0}</td>\r\n", "E-mail");
            cabecalho.AppendFormat("\t<td class=\"tabHead\">{0}</td>\r\n", "Segmento");
            cabecalho.AppendFormat("\t<td class=\"tabHead\">{0}</td>\r\n", "Data Registro");
            cabecalho.AppendFormat("\t<td class=\"tabHead\">{0}</td>\r\n", "Ativo");
            cabecalho.AppendFormat("</tr>\r\n");
            cabecalho.AppendFormat("</thead>\r\n");
            cabecalho.AppendFormat("<tbody>\r\n");
        }

        private static void CriarConteudo(StringBuilder conteudo, MailingDTO item)
        {
            conteudo.AppendFormat("<tr>\r\n");
            conteudo.AppendFormat(String.Format("\t<td class=\"tabRow\">{0}</td>\r\n", item.MailingId));
            conteudo.AppendFormat(String.Format("\t<td class=\"tabRow\">{0}</td>\r\n", item.Nome));
            conteudo.AppendFormat(String.Format("\t<td class=\"tabRow\">{0}</td>\r\n", item.Email));
            conteudo.AppendFormat(String.Format("\t<td class=\"tabRow\">{0}</td>\r\n", item.Segmento));
            conteudo.AppendFormat(String.Format("\t<td class=\"tabRow\">{0}</td>\r\n", item.DataInclusao.ToString("dd/MM/yyyy")));
            conteudo.AppendFormat(String.Format("\t<td class=\"tabRow\">{0}</td>\r\n", item.Ativo));
            conteudo.AppendFormat("</tr>\r\n");
        }

        private static void CriarTagDeRodape(StringBuilder rodape)
        {
            rodape.AppendFormat("</tbody>\r\n");
            rodape.AppendFormat("</table>\r\n");
        }

        private int GetCurrentSite()
        {
            return Convert.ToInt32(HttpContext.Request.Cookies["CMS_SiteId"] != null ? HttpContext.Request.Cookies["CMS_SiteId"].Value : "0");
        }
    }
}
