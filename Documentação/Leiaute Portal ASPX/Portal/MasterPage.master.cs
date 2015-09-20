using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    string url = HttpContext.Current.Request.Url.AbsoluteUri;

    protected void Page_Load(object sender, EventArgs e)
    {
        Panel_off.Visible = Convert.ToBoolean(Session["Panel_off"].ToString());
        Panel_on.Visible = Convert.ToBoolean(Session["Panel_on"].ToString()); ;

    }
    public void setMenu(string idclass)
    {

        string menu_set = idclass;

        if (idclass == "menu_home")
        {
            menu_home.Attributes["class"] = "topo-menu-on";
        }
        if (idclass == "menu_cam")
        {
            menu_cam.Attributes["class"] = "topo-menu-on";
        }
        if (idclass == "menu_servicos")
        {
            menu_servicos.Attributes["class"] = "topo-menu-on";
        }
        if (idclass == "menu_contato")
        {
            menu_contato.Attributes["class"] = "topo-menu-on";
        }

    }
    protected void ImageButton_Enviar_Click(object sender, ImageClickEventArgs e)
    {
        string usuario = "tendenza";
        string senha = "123";

        if (TextBox_Usuario.Text == usuario && TextBox_Senha.Text == senha)
        {

            Session["Panel_off"] = false;
            Session["Panel_on"] = true;
            Response.Redirect(url);

        }
        else
        {
            Response.Redirect("~/Erro.aspx?Codigo=1-002&Descricao=Erro ao tentar conectar ao serviço, verifique o nome de usuario ou senha!");

        }

    }
    protected void LinkButtonSair_Click(object sender, EventArgs e)
    {
        Session["Panel_off"] = true;
        Session["Panel_on"] = false;
        Response.Redirect(url);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Busca.aspx");
    }
}
