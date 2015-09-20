<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="scotiabank.aspx.cs" Inherits="scotiabank" %>

<%@ Register Src="~/UC_quickmenu.ascx" TagPrefix="uc1" TagName="UC_quickmenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="geral-1050">
        <div class="conteudo">

            <div class="internas-navegacao">
                <a href="Default2.aspx">Home</a>
                <a href="scotiabank.aspx">Scotiabank</a>
            </div>

            <!--lateral esquerda-->
            <div class="lateral">
                <div class="lateral-conteudo">


                    <!--menu internas-->

                    <div class="menu-internas">
                        <div class="menu-internas-titulo cor-padrao">
                            <h1>SCOTIABANK</h1>
                        </div>

                        <ul>
                            <li><a href="Conteudo/Documentos/BNS_2013_Annual_Report.pdf" target="_blank">Relatório Anual Scotiabank 2013 (PDF)</a></li>

                        </ul>
                    </div>


                    <!--menu internas-->



                </div>
            </div>
            <!--lateral esquerda-->


            <!--conteudo / direta-->

            <div class="internas-conteudo">
                <h1>Scotiabank</h1>
                <img src="imgs/Img_banner_interna_scotiabank.jpg" alt="cccbc" />
                <p>
                    O Scotiabank, uma das principais instituições financeiras das Américas, fornece aos seus clientes uma ampla gama de produtos e serviços de capital markets, 
                    e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de negócios, 
                    agregando valores, e atingindo metas financeiras.
                </p>
                <p>
                    <strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em operações de crédito para grandes empresas (Corporate Banking), financiamento ao comércio exterior (Trade Finance), e em operações de mercado de capitais local e internacional, bem como operações de câmbio e de tesouraria. Nossos clientes são empresas e instituições financeiras de origem doméstica e internacional de médio e grande portes.

                </p>
                <p>Não oferecemos produtos ou serviços para pessoas físicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou serviços.</p>

                <div class="pw-widget pw-size-small" style="float: left; margin: 25px 0 0 0;">
                    <a class="pw-button-facebook"></a>
                    <a class="pw-button-twitter"></a>
                    <a class="pw-button-linkedin"></a>
                    <a class="pw-button-email"></a>
                </div>
                <div style="float: left;">
                    <a href="#">
                        <img src="imgs/Bt_print.png" width="16" /></a>
                </div>
                <script src="http://i.po.st/static/v3/post-widget.js#publisherKey=mtnnocp28jgch1fbpjfs&retina=true" type="text/javascript"></script>

            </div>





            <!--lateral direita-->
            <div class="internas-direita">

                <uc1:UC_quickmenu runat="server" ID="UC_quickmenu" />




            </div>
            <!--fehca direita-->


        </div>
        <!--conteudo-->

    </div>

</asp:Content>

