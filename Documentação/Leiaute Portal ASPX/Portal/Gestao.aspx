<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestao.aspx.cs" Inherits="Gestao" %>

<%@ Register Src="~/UC_quickmenu.ascx" TagPrefix="uc1" TagName="UC_quickmenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="geral-1050">
        <div class="conteudo">

            <div class="internas-navegacao">
                <a href="Default2.aspx">Home</a>
                <a href="#">-</a>
                <a href="CAM-CCBC.aspx">CAM</a>
                <a href="#">-</a>
                <a href="Gestao.aspx">Gestão</a>
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
                            <li><a href="Gestao.aspx" class="menu-internas-on">Gestão</a></li>

                            <li><a href="ISO-9001.aspx">ISO 9001</a></li>


                        </ul>
                    </div>


                    <!--menu internas-->



                </div>
            </div>
            <!--lateral esquerda-->


            <!--conteudo / direta-->

            <div class="internas-conteudo">
                <h1>GESTÃO</h1>

                <p>
                    <strong>Presidente</strong><br />
                    Frederico José Straube 
                </p>
                <p>
                    <strong>Vice-Presidentes</strong><br />
                    Antonio Luiz Sampaio Carvalho<br />
                    Eduardo Silva Romero<br />
                    Donald Donovan<br />
                    Gilberto Giusti<br />
                    Maristela Basso
	

                </p>
                <p>
                    <strong>Secretário Geral</strong>
                    <br />


                    Carlos Suplicy de Figueiredo Forbes

                </p>
                <p>
                    <strong>Secretária Geral Adjunta</strong><br />


                    Patrícia Shiguemi Kobayashi
	
	


                </p>
                <p>
                    <strong>Presidentes anteriores</strong><br />

                    Marcos Paulo de Almeida Salles (1999-2007)<br />


                    Fábio Nusdeo (1995-1999)<br />


                    Frederico José Straube (1993-1995)<br />


                    Guido Fernando Silva Soares (1992-1993)<br />


                    João Caio Goulart Penteado (1984-1992)<br />


                    José Carlos de Magalhães (1979-1984)
                </p>
                <p class="auto-style1">
                    <br />


                    <strong>Conselho Consultivo</strong>
                </p>
                <p>
                    <strong>Presidente</strong><br />


                    Prof. Dr. Fábio Nusdeo

                </p>
                <p>
                    <strong>Vice Presidente</strong><br />


                    Dr. Mário Sérgio Duarte Garcia

                </p>
                <p>
                    <strong>Secretários</strong><br />


                    Prof. Dr. Luiz Périssé Duarte Jr.<br />


                    Prof.ª Dra. Adriana Braghetta
                </p>
                <p>
                    <strong>Membros Permanentes</strong><br />


                    Prof. Dr. Fábio Nusdeo<br />


                    Prof. Dr. José Carlos de Magalhães<br />


                    Dr. Marcos Paulo de Almeida Salles<br />



                    Dr. Frederico Straube (Presidente do CAM/CCBC)
                </p>
                <p>
                    <strong>Membros Eleitos</strong><br />


                    Prof. Dr. Carlos Alberto Carmona<br />


                    Dr. Francisco Florence<br />


                    Prof. Dr. João Bosco Lee<br />


                    Prof. Dr. Luiz Olavo Baptista<br />


                    Prof. Dr. Luiz Périssé Duarte Jr.<br />


                    Dr. Mário Sérgio Duarte Garcia<br />


                    Prof. Dr. Pedro Antônio Batista Martins<br />


                    Profª. Dra. Selma Maria Ferreira Lemes
                </p>

                <div class="pw-widget pw-size-small" style="float: left; margin: 25px 0 0 0;">
                    <a class="pw-button-facebook"></a>
                    <a class="pw-button-twitter"></a>
                    <a class="pw-button-linkedin"></a>
                    <a class="pw-button-email"></a>
                </div>
                <div style="float: left;">
                    <a href="Print/Gestao.aspx" target="_blank">
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

