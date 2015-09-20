<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Busca.aspx.cs" Inherits="Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="geral-1050">
        <div class="conteudo">

            <!-- nav internas -->
            <div class="internas-navegacao">
                <a href="Default2.aspx">Home</a>
                <a href="#">-</a>
                <a href="#">Resultados da Busca</a>
            </div>
            <!-- nav internas -->
            <!-- contato -->
            <div class="resultados-titulo">
                <h1>RESULTADOS DA <span class="cor-padrao-texto">BUSCA</span></h1>
            </div>
            <!--resultado-->
            <div class="resultados">
                <h3>02/12/2014 - Eventos</h3>
                <h1><a href="v-coloquio-de-direito-administrativo.aspx" class="opacidade">V COLÓQUIO DE DIREITO ADMINISTRATIVO </a></h1>
                <h2>Novas tendências em arbitragem e mediação envolvendo a Administração Pública </h2>
            </div>
            <!--resultado-->
            <!--resultado-->
            <div class="resultados">
                <h3>05/12/2014 - Eventos </h3>
                <h1><a href="v-coloquio-de-direito-administrativo2.aspx" class="opacidade">V COLÓQUIO DE DIREITO ADMINISTRATIVO2</a></h1>
                <h2>Novas tendências em arbitragem e mediação envolvendo a Administração Pública</h2>
            </div>
            <!--resultado-->
            <!--resultado-->
            <div class="resultados">
                <h3>28/12/2014 - Eventos</h3>
                <h1><a href="ciclo-nacional-de-arbitragem-e-mediacao.aspx" class="opacidade">Ciclo Nacional de Arbitragem e Mediação</a></h1>
                <h2>LOCAL : Escola Superior da Magistratura da Paraíba / Rua Abelardo S. G. Barreto, s/n / Altiplano - João Pessoa - PB</h2>
            </div>
            <!--resultado-->
            <asp:Panel ID="PanelEventos" Visible="false" runat="server">
                <!--resultado-->
                <div class="resultados">
                    <h3>07/01/2015 - Eventos&nbsp;&nbsp;
                        <img src="imgs/Img_cadeado.png" alt="cadeado ccbc" /></h3>
                    <h1><a href="scholarship-opportunity.aspx" class="opacidade">Scholarship Opportunity</a></h1>
                    <h2>‘The Fundamentals of International Arbitration’
under the coordination of Prof. Jan Paulsson.</h2>
                </div>
            </asp:Panel>

            <!--resultado-->
            <!--resultado-->
            <div class="resultados">
                <h3>02/04/2014 - Notícias</h3>
                <h1><a href="canada-pode-ser-parceiro-de-londrina-em-investimentos.aspx" class="opacidade">CANADÁ PODE SER PARCEIRO DE LONDRINA EM INVESTIMENTOS</a></h1>
                <h2>Londrina dará continuidade às atividades de fomento de negócios e parcerias com o Canadá.</h2>
            </div>
            <!--resultado-->
            <!--resultado-->
            <div class="resultados">
                <h3>02/04/2014 - Notícias</h3>
                <h1><a href="camara-de-comercio-brasil-canada-realiza-evento-no-norte-do-parana.aspx" class="opacidade">CÂMARA DE COMÉRCIO BRASIL-CANADÁ REALIZA EVENTO NO NORTE DO PARANÁ</a></h1>
                <h2>A Câmara de Comércio Brasil-Canadá em parceria com o Consulado Geral do Canadá em São Paulo, Terra roxa Investimentos, ACIL e ACIM realizou um evento nas cidades de Londrina e Maringá para apresentar a potencial do mercado canadense para absorver produtos brasileiros.</h2>
            </div>
            <!--resultado-->
            <!--resultado-->
            <div class="resultados">
                <h3>02/04/2014 - Notícias</h3>
                <h1><a href="brasil-sobe-para-a-38-posicao-entre-melhores-paises-para-fazer-negocios.aspx" class="opacidade">BRASIL SOBE PARA A 38ª POSIÇÃO ENTRE MELHORES PAÍSES PARA FAZER NEGÓCIOS</a></h1>
                <h2>O Brasil ganhou posições no mundo entre os melhores países para fazer negócios. Segundo ranking da agência Bloomberg, o país passou do 61º lugar em 2012 para o 38º em 2013, deixando para trás Rússia (48º) e Índia (48º), seus “parceiros” nos Brics.</h2>
            </div>
            <!--resultado-->
            <!--resultado-->
            <div class="resultados">
                <h3>02/04/2014 - Notícias</h3>
                <h1><a href="numero-de-brasileiros-cresce-8-no-canada-em-2012.aspx" class="opacidade">NÚMERO DE BRASILEIROS CRESCE 8% NO CANADÁ EM 2012</a></h1>
                <h2>O movimento da economia no ano passado fez com que empresas e destinos apresentassem balanços com queda ou abaixo da expectativa. Assim ocorreu com o Canadá, que esperava uma taxa de crescimento acima dos 8% conquistados, em relação aos visitantes brasileiros.</h2>
            </div>
            <!--resultado-->

            <asp:Panel ID="PanelNoticias" Visible="false" runat="server">
                <!--resultado-->
                <div class="resultados">
                    <h3>02/04/2014 - Notícias&nbsp;&nbsp;
                        <img src="imgs/Img_cadeado.png" alt="cadeado ccbc" /></h3>
                    <h1><a href="air-canada-recebe-ranking-quatro-estrelas.aspx" class="opacidade">AIR CANADA RECEBE RANKING QUATRO ESTRELAS</a></h1>
                    <h2>A Air Canada recebeu o ranking Quatro Estrelas de acordo com a empresa britânica de pesquisa Skytrax e se tornou a única transportadora aérea de rotas internacionais na América do Norte com essa referência.</h2>
                </div>
                <!--resultado-->

            </asp:Panel>


            <div class="resultados-paginacao">
                <a href="#" class="cor-padrao opacidade">1</a>
                <a href="#" class="cor-padrao opacidade">2</a>
                <a href="#" class="cor-padrao opacidade">3</a>
                <a href="#" class="cor-padrao opacidade">4</a>
            </div>

        </div>
    </div>

</asp:Content>

