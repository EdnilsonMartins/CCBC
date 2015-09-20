<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="servicos.aspx.cs" Inherits="servicos" %>

<%@ Register Src="~/UC_quickmenu.ascx" TagPrefix="uc1" TagName="UC_quickmenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="geral-1050">
        <div class="conteudo">
            <div class="internas-navegacao">
                <a href="Default2.aspx">Home</a>
                <a href="#">-</a>

                <a href="#">Serviços</a>
            </div>
            <!--lateral esquerda-->
            <div class="lateral">
                <div class="lateral-conteudo">

                    <!--publicidade 01-->
                    <a href="#" class="opacidade">
                        <img src="imgs/Img_banner_scot.jpg" alt="Publicidade CCBC" /></a>
                    <!--publicidade 01-->

                </div>
            </div>
            <!--lateral esquerda-->
            <!--conteudo / direta-->
            <div class="internas-conteudo">
                <h1>SERVIÇOS </h1>

                <p>
                    Aguardando publicação de conteudo.
                </p>


            </div>

            <!--lateral direita-->

            <uc1:UC_quickmenu runat="server" ID="UC_quickmenu" />


        </div>
        <!--fehca direita-->

    </div>

</asp:Content>

