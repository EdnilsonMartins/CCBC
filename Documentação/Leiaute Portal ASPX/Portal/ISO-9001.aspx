<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ISO-9001.aspx.cs" Inherits="ISO_9001" %>

<%@ Register Src="~/UC_quickmenu.ascx" TagPrefix="uc1" TagName="UC_quickmenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="geral-1050">
        <div class="conteudo">

            <div class="internas-navegacao">
                <a href="Default2.aspx">Home</a>
                <a href="#">-</a>
                <a href="CAM-CCBC.aspx">CAM</a>
                <a href="#">-</a>
                <a href="ISO-9001.aspx">ISO 9001</a>
            </div>

            <!--lateral esquerda-->
            <div class="lateral">
                <div class="lateral-conteudo">


                    <!--menu internas-->

                    <div class="menu-internas">
                        <div class="menu-internas-titulo cor-padrao">
                            <h1>CAM</h1>
                        </div>

                        <ul>
                            <li><a href="Historico.aspx">Histórico</a></li>
                            <li><a href="Gestao.aspx">Gestão</a></li>

                            <li><a href="ISO-9001.aspx" class="menu-internas-on">ISO 9001</a></li>


                        </ul>
                    </div>


                    <!--menu internas-->



                </div>
            </div>
            <!--lateral esquerda-->


            <!--conteudo / direta-->

            <div class="internas-conteudo">
                <h1>ISo 9001</h1>

                <p>
                    O CAM/CCBC teve seu certificado ISO renovado em 1º de dezembro de 2011, mantendo-se constantemente atento e em conformidade com os requisitos da norma NBR ISO 9001 desde sua primeira auditoria em dezembro de 2004.
                </p>
                <p style="text-align:center">
                    <img src="imgs/logo_sgs.gif" alt="ISO 9001" />
                </p>

                <div class="pw-widget pw-size-small" style="float: left; margin: 25px 0 0 0;">
                    <a class="pw-button-facebook"></a>
                    <a class="pw-button-twitter"></a>
                    <a class="pw-button-linkedin"></a>
                    <a class="pw-button-email"></a>
                </div>
                <div style="float: left;">
                    <a href="Print/ISO-9001.aspx" target="_blank">
                        <img src="imgs/Bt_print.png" width="16" /></a>
                </div>
                <script src="http://i.po.st/static/v3/post-widget.js#publisherKey=mtnnocp28jgch1fbpjfs&retina=true" type="text/javascript"></script>

            </div>





            <!--lateral direita-->
            <div class="internas-direita">


                <uc1:UC_quickmenu runat="server" ID="UC_quickmenu1" />



            </div>
            <!--fehca direita-->


        </div>
        <!--conteudo-->

    </div>

</asp:Content>

