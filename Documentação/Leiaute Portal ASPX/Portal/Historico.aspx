<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Historico.aspx.cs" Inherits="Historico" %>

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
                <a href="Historico.aspx">Histórico</a>
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
                            <li><a href="Historico.aspx" class="menu-internas-on">Histórico</a></li>
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
                <h1>HISTÓRICO</h1>

                <p>
                    A Câmara de Comércio Brasil-Canadá foi pioneira no Brasil em matéria de arbitragem, pois já em 26 de julho de 1979 criara a sua Comissão de Arbitragem, hoje Centro de Arbitragem, com vistas a proporcionar meios fáceis e ágeis para a solução de litígios, quer entre pessoas físicas, quer, sobretudo, entre pessoas jurídicas em matéria contratual.

                </p>
                <p>
                    Devido à falta de legislação adequada e também de uma cultura propícia a esse tipo de solução de controvérsias, não foram muitos os casos levados à então Comissão de Arbitragem.

                </p>
                <p>
                    O quadro, porém, está se alterando rapidamente com a superação daqueles dois fatores negativos. Em primeiro lugar, o Brasil conta, desde 1996, com uma lei moderna e bastante funcional: a Lei 9.307, que oferece o respaldo jurídico necessário tanto para as partes quanto para os árbitros. Por outro lado, a mentalidade refratária à adoção de soluções arbitrais vem mudando em função dos requisitos de celeridade, tecnicidade e segurança exigidos pela vida econômica moderna. Tudo isso tem levado a uma crescente adoção de cláusulas arbitrais nos mais variados tipos de contratos celebrados por empresas e mesmo por particulares.

                </p>
                <p>
                    Dentro desse quadro, a CCBC promoveu a completa reforma do seu regulamento de arbitragem ao qual foi introduzido igualmente um roteiro para procedimentos de mediação. Ambos estão perfeitamente adaptados à nova legislação e seguem os padrões vigentes nos modernos centros de arbitragem do mundo desenvolvido.

                </p>
                <p>
                    Não é necessário repisar as incontáveis vantagens trazidas pelo sistema arbitral de solução ou composição de conflitos. Os três requisitos acima mencionados - celeridade, tecnicidade e segurança falam por si. Com efeito, as controvérsias no mundo dos negócios envolvem cada vez mais aspectos técnicos de alta complexidade cujo equacionamento nem sempre se ajusta aos formalismos puramente processuais. Exigem não apenas proficiência jurídica, mas conhecimentos especializados dos mercados relevantes e de suas formas de operação. Daí poderem integrar os tribunais arbitrais não apenas juristas mas especialistas de outros ramos do conhecimento.

                </p>
                <p>
                    Um ponto importante a ser realçado é o fato de em uma arbitragem reduzir-se em muito o caráter de animosidade que sói marcar as partes nos litígios forenses. Muitas vezes essas partes, que num dado momento levam as suas diferenças de avaliação a um árbitro ou árbitros, estarão dentro em breve juntas, lado a lado, em outro importante negócio como, por exemplo, a formação de um consórcio para o suprimento de equipamentos. Interessa-lhes, portanto, evitar o atrito e as seqüelas próprias das intermináveis batalhas judiciais. Acrescente-se a tudo isto o sigilo e a confiança pessoal de cada parte no árbitro por ela escolhido, deixando-a tranqüila quanto à escolha do terceiro árbitro, isto quando as duas partes não convierem na indicação de um único julgador.

                </p>
                <p>
                    A essas e tantas outras vantagens do instituto, deve-se somar uma outra de caráter eminentemente social: a sua contribuição para o descongestionamento do Judiciário, mal que aflige a grande maioria dos países, mas que é indubitavelmente gravíssimo, sendo mesmo objeto de preocupação junto a órgãos internacionais que acompanham a evolução da sociedade brasileira.
                </p>
                <div class="pw-widget pw-size-small" style="float: left; margin: 25px 0 0 0;">
                    <a class="pw-button-facebook"></a>
                    <a class="pw-button-twitter"></a>
                    <a class="pw-button-linkedin"></a>
                    <a class="pw-button-email"></a>
                </div>
                <div style="float: left;">
                    <a href="Print/Historico.aspx" target="_blank">
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

