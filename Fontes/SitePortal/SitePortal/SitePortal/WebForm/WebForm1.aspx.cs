using DAL;
using DTO.Agenda;
using DTO.Publicacao;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitePortal.WebForm
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        List<Publicacao> agenda = new List<Publicacao>();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                string CultureName = "";
                var languageCookie = HttpContext.Current.Request.Cookies["langDataSelecionada"];
                if (languageCookie == null)
                {
                    var langCookie = new HttpCookie("langDataSelecionada", "pt-BR") { HttpOnly = true };
                    Response.AppendCookie(langCookie);
                }
                CultureName = HttpContext.Current.Request.Cookies["langDataSelecionada"].Value;
                CultureInfo culture = new CultureInfo(CultureName);


                int inc = Convert.ToInt32(Request.QueryString["inc"]);
                var currentDate = HttpContext.Current.Request.Cookies["DataSelecionada"] != null ? Convert.ToDateTime(HttpContext.Current.Request.Cookies["DataSelecionada"].Value, culture) : DateTime.Now;


                currentDate = currentDate.AddMonths(inc);
                var NovocurrentDate = new HttpCookie("DataSelecionada", currentDate.ToString()) { HttpOnly = true };
                Response.AppendCookie(NovocurrentDate);

                var languageCookie_Selecionado = HttpContext.Current.Request.Cookies["lang"];

                if (languageCookie_Selecionado == null)
                {
                    var langCookie = new HttpCookie("lang", "pt-BR") { HttpOnly = true };
                    Response.AppendCookie(langCookie);
                    languageCookie_Selecionado = HttpContext.Current.Request.Cookies["lang"];
                }
                var langDataSelecionada = new HttpCookie("langDataSelecionada", languageCookie_Selecionado.Value) { HttpOnly = true };
                Response.AppendCookie(langDataSelecionada);


                Calendar1.VisibleDate = currentDate;
            }
            catch (Exception ex)
            {

            }

            CarregarEventos();

            //Calendar1.VisibleDate = new DateTime(2015, 8, 1);
        }

        public void CarregarEventos()
        {
            var currentCulture = HttpContext.Current.Request.Cookies["lang"] != null ? HttpContext.Current.Request.Cookies["lang"].Value : "pt-BR";
            if (string.IsNullOrEmpty(currentCulture)) currentCulture = "pt-BR";
            int IdiomaId = Util.GetIdiomaId(currentCulture);

            var currentSite = HttpContext.Current.Request.Cookies["site"] != null ? HttpContext.Current.Request.Cookies["site"].Value : "2";
            if (string.IsNullOrEmpty(currentSite)) currentSite = "0";
            int SiteId = Convert.ToInt32(currentSite);

            var UsuarioId = HttpContext.Current.Request.Cookies["UsuarioId"] != null ? HttpContext.Current.Request.Cookies["UsuarioId"].Value : "0";
            var UsuarioNome = HttpContext.Current.Request.Cookies["UsuarioNome"] != null ? HttpContext.Current.Request.Cookies["UsuarioNome"].Value : "";

            PublicacaoDAL publicacaoDAL = new PublicacaoDAL();
            if (UsuarioId == "") UsuarioId = "0";
            agenda = publicacaoDAL.ListarPublicacao(SiteId, null, (int)Util.TIPOPUBLICACAO.EVENTO, null, null, Convert.ToInt32(UsuarioId), IdiomaId); 
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            

            bool possuiEvento;
            string cor;
            string rotulo = "";
            string cursor = " cursor: pointer;";

            DateTime theDate = e.Day.Date;
            possuiEvento = agenda.Any(x => x.Data == theDate);

            Publicacao a = new Publicacao();
            string linkEvento = "";
            if (possuiEvento)
            {
                //Funciona | Produção:
                //a = agenda.Find(x => x.Data == theDate);
                //if (a.Complemento.Privado != null && (bool)a.Complemento.Privado)
                //{
                //    e.Cell.Attributes.Add("style", "background-image: url('../Images/Img_cadeado.png'); background-repeat: no-repeat;background-position-x: 20px;");
                //}
                //rotulo = a.Detalhe.Titulo;

                //linkEvento = "/" + a.PublicacaoId + "/" + DAL.Util.GerarURLAmigavel(a.Detalhe.Titulo);


                //Novo | Desenvolvimento:
                var eventos = agenda.FindAll(x => x.Data == theDate);
                rotulo = "";
                bool possuiPrivado = false;

                
                //foreach (var item in eventos)
                //{
                //    string cadeadoItem = "";
                //    if (item.Complemento.Privado != null && (bool)item.Complemento.Privado)
                //    {
                //        possuiPrivado = true;
                //        //cadeadoItem = @"<div style=""background-image: url('/Images/Img_cadeado.png'); background-repeat: no-repeat;background-position-x: 20px;""></div>";
                //        cadeadoItem = "<input type='image' src='/Images/Img_cadeado.png' style='margin-left: 6px;'></input>";
                //    }

                //    //if (rotulo.Length > 0) rotulo += "<br />";
                //    linkEvento = String.Format("/Eventos/{0}/{1}", item.PublicacaoId, DAL.Util.GerarURLAmigavel(item.Detalhe.Titulo));
                //    rotulo += String.Format("<p><a href='{0}' title=''>- {1}</a>{2}</p>", linkEvento, item.Detalhe.Titulo, cadeadoItem);
                    
                //}


                foreach (var item in eventos)
                {
                    string cadeadoItem = "";
                    if (item.Complemento.Privado != null && (bool)item.Complemento.Privado)
                    {
                        possuiPrivado = true;
                        //cadeadoItem = @"<div style=""background-image: url('/Images/Img_cadeado.png'); background-repeat: no-repeat;background-position-x: 20px;""></div>";
                        cadeadoItem = "<input type='image' src='/Images/Img_cadeado.png' style='margin-left: 6px;'></input>";
                    }

                    //if (rotulo.Length > 0) rotulo += "<br />";
                    linkEvento = String.Format("/Eventos/{0}/{1}", item.PublicacaoId, DAL.Util.GerarURLAmigavel(item.Detalhe.Titulo));
                    rotulo += String.Format("<li><a href='{0}' title=' '>{1}{2}</a></li>", linkEvento, item.Detalhe.Titulo, cadeadoItem);

                }

                rotulo = string.Format("<ul>{0}</ul>", rotulo);
                rotulo = String.Format("<div class='menu-internas' style='width: 222px;' title=' '>{0}</div>", rotulo);

                if (possuiPrivado)
                {
                    e.Cell.Attributes.Add("style", "background-image: url('/Images/Img_cadeado.png'); background-repeat: no-repeat;background-position-x: 20px;");
                }
            }


            
            if (e.Day.IsOtherMonth)
            {
                cor = "color: lightgray !important;";
                if (possuiEvento) cor = "color: lightpink !important;";
            }
            else
            {
                cor = "color: gray;";
                if (possuiEvento) cor = "color: red !important;";
            }

            string border = "";
            if (theDate == DateTime.Today.Date)
            {
                border = "border-radius: 11px; border: 1px solid red; padding: 3px;";
                rotulo = "Hoje";
            }

            //e.Cell.Text = "<a href=" + e.SelectUrl + " style=\"" + cor + border + " font-weight: normal;\" title=\"" + rotulo + "\">" + e.Day.DayNumberText + "</a>";
            
            //Funciona | Em produção:
            //e.Cell.Text = string.Format("<a href=\"../Eventos{0}\" target=\"_parent\" style=\"{1} font-weight: normal;\" title=\"{2}\"  >" + e.Day.DayNumberText + "</a>", linkEvento, cor + border, rotulo);
            if (possuiEvento)
            {
                e.Cell.Text = string.Format("<a tabindex=\"0\" style=\"{0} font-weight: normal;\" data-toggle=\"popover\" data-trigger=\"focus\" title=\"" + Resources.Portal.EventosDoDia + "\" data-html=\"true\" data-content=\"{1}\" title=' '>" + e.Day.DayNumberText + "</a>", cor + border + cursor, rotulo);
                e.Cell.BackColor = System.Drawing.Color.AliceBlue;
            }
            else
            {
                e.Cell.Text = string.Format("<a tabindex=\"0\" style=\"{0} font-weight: normal;\" title=' '>" + e.Day.DayNumberText + "</a>", cor + border);
            }
                
        }
    }
}