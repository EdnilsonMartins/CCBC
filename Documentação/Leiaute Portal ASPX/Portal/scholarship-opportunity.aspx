<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="scholarship-opportunity.aspx.cs" Inherits="scholarship_opportunity" %>

<%@ Register Src="~/UC_quickmenu.ascx" TagPrefix="uc1" TagName="UC_quickmenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="geral-1050">
        <div class="conteudo">
            <div class="internas-navegacao">
                <a href="Default2.aspx">Home</a>
                <a href="#">-</a>
                <a href="Eventos.aspx">Eventos</a>
                <a href="#">-</a>
                <a href="scholarship-opportunity.aspx">Scholarship Opportunity</a>
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
                <h1>Scholarship Opportunity</h1>
                <img src="imgs/Img_banner_interna_evento.jpg" alt="cccbc" />
                <p><b>The Fundamentals of International Arbitration’</b></p>
                <p>7 to 11 January 2015, Miami, FL</p>
                <p>The Office of International Graduate Law Programs of the University of Miami, in cooperation with the Pontifícia Universidade Católica de São Paulo will host a one week intensive non-credit course for lawyers and law students in 2015.</p>
                <p>Please find here the rules for applying.</p>
                <p>Stay tuned for the scholarship grant program.</p>

            </div>

            <!--lateral direita-->


            <uc1:UC_quickmenu runat="server" ID="UC_quickmenu" />

        </div>
        <!--fehca direita-->

    </div>

</asp:Content>

