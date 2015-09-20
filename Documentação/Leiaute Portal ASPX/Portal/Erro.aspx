<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Erro.aspx.cs" Inherits="Erro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <title>CCBC - Erro!</title>
</head>
<body>
    <form id="form1" runat="server">
        <!--linha topo-->
        <div class="topo-linha cor-padrao"></div>
        <!--linha topo-->

        <div class="erro">

            <div class="erro-logo">
                <a href="Default2.aspx">
                    <img src="imgs/Logo_cam.jpg" alt="ccbc" /></a>
            </div>

            <div class="erro-erro">
                <h1>
                    <asp:Label ID="Label_Titulo" runat="server" Text="Label"></asp:Label>
                </h1>
                <asp:Label ID="Label_Descricao" runat="server" Text="Label"></asp:Label>
                <div>
                    <br />
                    <br />
                    <br />

                    <asp:Button ID="Button1" runat="server" Text="<< VOLTAR" class="cor-padrao" ForeColor="White" OnClick="Button1_Click" />

                </div>
            </div>


        </div>
    </form>
</body>
</html>
