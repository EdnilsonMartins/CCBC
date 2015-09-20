using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Noticias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Session["Panel_off"].ToString()) == false)
        {

            PanelNoticias.Visible = true;

        }
        else
        {
            //Response.Redirect("~/Erro.aspx?Codigo=1-003&Descricao=Erro ao tentar conectar ao serviço, tecle F5 para atualização do browser!");

        }
    }
}