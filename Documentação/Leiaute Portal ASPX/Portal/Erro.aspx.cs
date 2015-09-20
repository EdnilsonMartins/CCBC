using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Erro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string codigo = Request.QueryString["Codigo"];
        string descricao = Request.QueryString["Descricao"];

        Label_Titulo.Text = codigo;
        Label_Descricao.Text = descricao;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default2.aspx");
    }
}