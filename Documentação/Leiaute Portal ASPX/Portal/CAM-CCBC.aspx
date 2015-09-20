<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CAM-CCBC.aspx.cs" Inherits="CAM_CCBC" %>

<%@ Register Src="~/UC_quickmenu.ascx" TagPrefix="uc1" TagName="UC_quickmenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!--fancybox-->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
    <script type="text/javascript" src="fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <link rel="stylesheet" type="text/css" href="fancybox/jquery.fancybox-1.3.4.css" media="screen" />
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

            <div class="internas-navegacao">
                <a href="Default2.aspx">Home</a>
                <a href="#">-</a>
                <a href="CAM-CCBC.aspx">CAM</a>
                <a href="#">-</a>
                <a href="CAM-CCBC.aspx">Centro de arbitragem e mediação</a>
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

                            <li><a href="ISO-9001.aspx">ISO 9001</a></li>


                        </ul>
                    </div>


                    <!--menu internas-->



                </div>
            </div>
            <!--lateral esquerda-->


            <!--conteudo / direta-->

            <div class="internas-conteudo">
                <h1>Centro de Arbitragem e Mediação</h1>

                <p>O Centro de Arbitragem e Mediação foi fundado em 1979 por um grupo de advogados e professores da Faculdade de Direito do Largo São Francisco com o apoio da Câmara de Comércio Brasil-Canadá.</p>
                <p>Muito mudou desde aquela época uma vez que, no início de suas atividades, poucos foram os casos solucionados pelo instituto, que não tinha o suporte de uma lei específica que regesse a matéria no país.</p>
                <p>No entanto, com a promulgação da Lei de Arbitragem, lei nº 9.307 em 1996, o método alternativo ganhou fôlego com a recém conquistada segurança jurídica que beneficiou partes, advogados e árbitros, e permitiu a adoção de cláusulas compromissórias eficazes que garantissem os requisitos de celeridade, tecnicidade e segurança exigidos pela prática jurídica internacional mais moderna.</p>
                <p>Ao longo dos últimos anos, o CAM/CCBC vem participando ativamente da mudança de panorama jurídico, contribuindo para a crescente adoção de cláusulas arbitrais nos mais variados tipos de contrato através de um eficiente acompanhamento administrativo dos procedimentos.</p>
                <p>O CAM/CCBC acompanhou a evolução do instituto de arbitragem e recentemente promoveu uma completa reforma em seu Regulamento, adaptando-o às recentes modificações e avanços doutrinários advindo das complexidades dos próprios procedimentos instaurados e administrados pelo Centro.</p>
                <p>Hoje o CAM segue os padrões dos mais modernos Centros de Arbitragens do mundo e superou nos últimos 10 anos sua própria previsão de crescimento. O Centro processou até 2012 mais de 319 processos, tendo sido instaurados somente em 2011 63 novos procedimentos. Os valores das disputas, em dezembro de 2011, superavam 9 milhões de reais. </p>
                <p>Passados mais de 30 anos desde sua fundação, o mais antigo Centro de Arbitragem do país e único certificado pela pelas normas de qualidade do programa ISO 9001, consolidou sua importância para solução de conflitos e prepara-se para se tornar receber um número cada vez maior de arbitragens internacionais.</p>
                <p>Para tanto, revisou seu Regulamento adaptando-o para as complexas disputas de mercado, ampliou sua lista de árbitros internacionalizando-a e reformou suas instalações que contam agora com duas modernas salas de audiência com infraestrutura para tele e videoconferência e salas privativas para acomodar advogados, testemunhas, partes e árbitros.</p>
                <p>O CAM/CCBC firmou ainda acordos de cooperação e estudos com várias entidades internacionais do mesmo gênero, que atuam no Chile, Portugal, Itália, Alemanha e Bélgica.</p>
                <p>Infraestrutura, árbitros renomados e com conhecimento altamente especializado às práticas do mercado e suas formas de operação; </p>
                <p>Rigoroso e eficiente serviço de administração de procedimentos arbitrais; e </p>
                <p>Foco no custo benefício de cada processo, seja ele de Mediação, Arbitragem, Dispute Board ou solução de conflitos sobre nomes de domínios da internet.</p>
                <p>Tradição e modernidade aliados à eficiência são o lema do CAM/CCBC que corroboram para que os institutos da Arbitragem e Mediação se fortaleçam e continuem sendo, cada vez mais, métodos eficazes e céleres para solucionar conflitos de natureza patrimonial.</p>
                <p>CAM/CCBC, construindo a história da Arbitragem no Brasil.</p>
                <div class="pw-widget pw-size-small" style="float: left; margin: 25px 0 0 0;">
                    <a class="pw-button-facebook"></a>
                    <a class="pw-button-twitter"></a>
                    <a class="pw-button-linkedin"></a>
                    <a class="pw-button-email"></a>
                </div>
                <div style="float: left;">
                    <a href="Print/CAM-CCBC.aspx" target="_blank">
                        <img src="imgs/Bt_print.png" width="16" /></a>
                </div>
                <script src="http://i.po.st/static/v3/post-widget.js#publisherKey=mtnnocp28jgch1fbpjfs&retina=true" type="text/javascript"></script>
                <div class="internas-conteudo-galeria">
                    <h3>GALERIA DE IMAGENS</h3>
                    <a id="example1" href="imgs/Foto05.jpg" title="CAM-CCBC / Recepção" rel="example_group">
                        <img src="imgs/Foto05.jpg" alt="ccbc" class="last" /></a>
                    <a id="example1" href="imgs/Foto06.jpg" title="CAM-CCBC / Sala" rel="example_group">
                        <img src="imgs/Foto06.jpg" alt="ccbc" class="last" /></a>
                    <a id="example1" href="imgs/Foto04.jpg" title="CAM-CCBC / Sala" rel="example_group">
                        <img src="imgs/Foto04.jpg" alt="ccbc" class="last" /></a>
                    <a id="example1" href="imgs/Foto03.jpg" title="CAM-CCBC / Sala" rel="example_group">
                        <img src="imgs/Foto03.jpg" alt="ccbc" class="last" /></a>
                    <a id="example1" href="imgs/Foto02.jpg" title="CAM-CCBC / Sala" rel="example_group">
                        <img src="imgs/Foto02.jpg" alt="ccbc" class="last" /></a>
                    <a id="example1" href="imgs/Foto01.jpg" title="CAM-CCBC / Sala" rel="example_group">
                        <img src="imgs/Foto01.jpg" alt="ccbc" class="last" /></a>
                </div>
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

