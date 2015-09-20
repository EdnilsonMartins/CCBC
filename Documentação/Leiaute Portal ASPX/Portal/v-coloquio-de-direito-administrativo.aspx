<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="v-coloquio-de-direito-administrativo.aspx.cs" Inherits="v_coloquio_de_direito_administrativo" %>

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
                <a href="v-coloquio-de-direito-administrativo.aspx">V Colóquio de Direito Administrativo</a>
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
                <h1>V Colóquio de Direito Administrativo </h1>
                <p>A utilização por parte da Administração Pública de métodos alternativos de controvérsias atinge, no final de 2014, um momento de muita expectativa: tramitam na Câmara dos Deputados os Projetos de Lei nº 7108/2014, para a reforma da Lei de Arbitragem, e nº 7.169/2014, que disciplina a aplicação da mediação à Administração Pública. O evento se propõe a discutir o estado da arte e as novas tendências relativas ao uso da arbitragem e da mediação em controvérsias que envolvem a Administração Pública.</p>
                <p>
                    Realização:<br />
                    Núcleo de Estudos e Pesquisas em Direito Administrativo Democrático da USP – NEPAD/USP
                    <br />
                    <br />
                    Coordenação:<br />
                    Prof. Dr. Gustavo Justino de Oliveira
                </p>
                <p>
                    Data: 2 de dezembro de 2014 (terça-feira)<br />
                    Horário: 9h30 às 12h<br />
                    Local: Faculdade de Direito da USP (Auditório Arcadas - Prédio Anexo, 4º Intermediário, com entrada pela Rua Riachuelo, nº 185) – Largo de São Francisco, 95 – Centro – São Paulo/SP
                    <br />
                    Evento gratuito. Não é necessário inscrição.
                    <br />
                    Informações: escritorio@justinodeoliveira.com.br
                </p>

            </div>

            <!--lateral direita-->



            <uc1:UC_quickmenu runat="server" ID="UC_quickmenu" />
        </div>
        <!--fehca direita-->

    </div>

</asp:Content>

