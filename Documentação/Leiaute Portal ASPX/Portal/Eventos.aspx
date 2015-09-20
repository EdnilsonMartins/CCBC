<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Eventos.aspx.cs" Inherits="Eventos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="geral-1050">
        <div class="conteudo">

            <!-- nav internas -->
            <div class="internas-navegacao">
                <a href="Default2.aspx">Home</a>
                <a href="#">-</a>
                <a href="Eventos.aspx">Eventos</a>

            </div>
            <!-- nav internas -->
            <!-- contato -->
            <div class="resultados-titulo">
                <h1><span class="cor-padrao-texto">EVENTOS</span></h1>
            </div>
            <!--resultado-->
            <div class="resultados">
                <h3>02/12/2014</h3>
                <h1><a href="v-coloquio-de-direito-administrativo.aspx" class="opacidade">V COLÓQUIO DE DIREITO ADMINISTRATIVO </a></h1>
                <h2>Novas tendências em arbitragem e mediação envolvendo a Administração Pública </h2>
            </div>
            <!--resultado-->
            <!--resultado-->
            <div class="resultados">
                <h3>05/12/2014</h3>
                <h1><a href="v-coloquio-de-direito-administrativo2.aspx" class="opacidade">V COLÓQUIO DE DIREITO ADMINISTRATIVO2</a></h1>
                <h2>Novas tendências em arbitragem e mediação envolvendo a Administração Pública</h2>
            </div>
            <!--resultado-->
            <!--resultado-->
            <div class="resultados">
                <h3>28/12/2014</h3>
                <h1><a href="ciclo-nacional-de-arbitragem-e-mediacao.aspx" class="opacidade">Ciclo Nacional de Arbitragem e Mediação</a></h1>
                <h2>LOCAL : Escola Superior da Magistratura da Paraíba / Rua Abelardo S. G. Barreto, s/n / Altiplano - João Pessoa - PB</h2>
            </div>
            <!--resultado-->
            <asp:Panel ID="PanelEventos" Visible="false" runat="server">
                <!--resultado-->
                <div class="resultados">
                    <h3>07/01/2015&nbsp;&nbsp;
                        <img src="imgs/Img_cadeado.png" alt="cadeado ccbc" /></h3>
                    <h1><a href="scholarship-opportunity.aspx" class="opacidade">Scholarship Opportunity</a></h1>
                    <h2>‘The Fundamentals of International Arbitration’
under the coordination of Prof. Jan Paulsson.</h2>
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

