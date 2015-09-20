<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="reserva-de-salas.aspx.cs" Inherits="reserva_de_salas" %>

<%@ Register Src="~/UC_quickmenu.ascx" TagPrefix="uc1" TagName="UC_quickmenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!--fancybox-->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
    <script type="text/javascript" src="./fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="./fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <link rel="stylesheet" type="text/css" href="./fancybox/jquery.fancybox-1.3.4.css" media="screen" />
    <script type="text/javascript">
        $(document).ready(function () {
            /*
            *   Examples - images
            */

            $("a#example1").fancybox();

            $("a[rel=example_group]").fancybox({
                'transitionIn': 'none',
                'transitionOut': 'none',
                'titlePosition': 'over',
                'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
                    return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
                }
            });

            /*
            *   Examples - various
            */

            $("#various1").fancybox({
                'titlePosition': 'inside',
                'transitionIn': 'none',
                'transitionOut': 'none'
            });

            $("#various2").fancybox();

            $("#various3").fancybox({
                'width': '75%',
                'height': '75%',
                'autoScale': false,
                'transitionIn': 'none',
                'transitionOut': 'none',
                'type': 'iframe'
            });

            $("#various4").fancybox({
                'padding': 0,
                'autoScale': false,
                'transitionIn': 'none',
                'transitionOut': 'none'
            });
        });
    </script>
    <!--fancybox-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="geral-1050">
        <div class="conteudo">
            <div class="internas-banner opacidade">
                <img src="imgs/Img_superbanner_interna.jpg" alt="banner ccbc" />
            </div>
            <div class="internas-navegacao">
                <a href="Default2.aspx">Home</a>
                <a href="reserva-de-salas.aspx">Reserva de Salas</a>
                <a href="terceiro-andar.aspx">Terceiro Andar</a>
            </div>
            <!--lateral esquerda-->
            <div class="lateral">
                <div class="lateral-conteudo">

                    <!--menu internas-->
                    <div class="menu-internas">
                        <div class="menu-internas-titulo cor-padrao">
                            <h1>RESERVA DE SALAS</h1>
                        </div>
                        <ul>
                            <li><a href="terceiro-andar.aspx">Terceiro Andar</a></li>

                        </ul>
                    </div>

                    <!--menu internas-->
                    <!--publicidade 01-->
                    <div class="lateral-conteudo-publicidade opacidade">
                        <a href="#">
                            <img src="imgs/Img_banner_scot.jpg" alt="Publicidade CCBC" /></a>
                    </div>
                    <!--publicidade 01-->

                </div>
            </div>
            <!--lateral esquerda-->
            <!--conteudo / direta-->
            <div class="internas-conteudo">
                <h1>Reserva de Salas</h1>
                <p>O Centro de Arbitragem e Mediação foi fundado em 1979 por um grupo de advogados e professores da Faculdade de Direito do Largo São Francisco com o apoio da Câmara de Comércio Brasil-Canadá.</p>
                <p>Reserva de Salas</p>
                &nbsp;

            </div>

            <!--lateral direita-->

            <uc1:UC_quickmenu runat="server" ID="UC_quickmenu" />
            <!--fehca direita-->

        </div>
        <!--conteudo-->
    </div>

</asp:Content>

