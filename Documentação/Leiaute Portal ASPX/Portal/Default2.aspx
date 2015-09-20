<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<%@ Register Src="~/UC_quickmenu.ascx" TagPrefix="uc1" TagName="UC_quickmenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="geral-1050">
        <div class="conteudo">

            <div class="conteudo-home-banner">

                <!--banner roativo-->
                <div id='coin-slider'>

                    <a href="terceiro-andar.aspx">
                        <img src="imgs/Img_banner_home2.jpg" alt="Banner Home CCBC" />
                    </a>

                    <a href="scotiabank.aspx">
                        <img src="imgs/Img_banner_home3.jpg" alt="Banner Home CCBC" />
                    </a>



                </div>
                <!--banner roativo-->

            </div>

            <uc1:UC_quickmenu runat="server" ID="UC_quickmenu" />

            <!--lateral esquerda-->
            <div class="lateral-home">
                <div class="lateral-conteudo">

                    <!--agenda-->
                    <div class="lateral-conteudo-agenda">
                        <div class="ico">
                            <img src="imgs/Icon_agenda.png" alt="Icone CCBC" />
                        </div>
                        <div class="titulo">
                            <h1>Agenda</h1>
                        </div>
                        <div class="subtitulo">
                            <h2>Eventos e Programações</h2>
                        </div>
                        <div class="lateral-conteudo-agenda-conteudo">
                            <!--<img src="imgs/Img_agenda.png" alt="Icone CCBC" />-->
                            <asp:Calendar ID="Calendar1" runat="server" Height="30px" OnSelectionChanged="Calendar1_SelectionChanged" Width="220px" BorderColor="#EEEEEE" CssClass="agenda-topo" ForeColor="White" ShowGridLines="True" TodayDayStyle-BackColor="#e8999d" Font-Size="Small" DayHeaderStyle-Height="20px" DayNameFormat="Shortest">
                                <DayHeaderStyle BackColor="#F7F7F7" ForeColor="Black" BorderColor="#EEEEEE" Height="20px" />
                                <DayStyle BorderColor="#EEEEEE" ForeColor="#999999" Height="28px" />
                                <SelectedDayStyle ForeColor="#FFFFFF" BackColor="#3333FF" />
                                <TitleStyle Height="45px" BackColor="#CC1F26" ForeColor="White" />

                                <TodayDayStyle BackColor="Red" ForeColor="White" Font-Bold="False"></TodayDayStyle>
                            </asp:Calendar>
                        </div>
                    </div>
                    <!--agenda-->


                    <!--newsletter-->
                    <div class="lateral-conteudo-newsletter">
                        <div class="ico">
                            <img src="imgs/Icon_newsletter.png" alt="Icone CCBC" />
                        </div>
                        <div class="titulo">
                            <h1>Newsletterer</h1>
                        </div>
                        <div class="subtitulo">
                            <h2>Receba nossas news</h2>
                        </div>
                        <div class="lateral-conteudo-newsletter-conteudo">
                            <input type="text" name="nome" value="nome" />
                            <input type="text" name="email" value="email" />
                            <div class="lateral-conteudo-newsletter-conteudo-texto">*Campos de preenchimento obrigatório</div>
                            <input type="submit" value="Inscrever-se" class="cor-padrao" />
                        </div>
                    </div>
                    <!--newsletter-->


                    <!--publicidade 01-->
                    <div class="lateral-conteudo-publicidade opacidade">
                        <a href="terceiro-andar.aspx">
                            <img src="imgs/Banner_reserve.png" alt="Publicidade CCBC" /></a>
                    </div>
                    <!--publicidade 01-->


                    <!--publicidade 01-->
                    <div class="lateral-conteudo-publicidade opacidade">
                        <a href="http://www.vale.com/PT/Paginas/Landing.aspx" target="_blank">
                            <img src="imgs/Banner_vale.png" alt="Publicidade CCBC" /></a>
                    </div>
                    <!--publicidade 01-->

                </div>
            </div>
            <!--lateral esquerda-->


            <!--conteudo / direta-->
            <div class="conteudo-home-direita">

                <!--eventos-->
                <div class="conteudo-home-direita-eventos">
                    <div class="ico">
                        <img src="imgs/Icon_eventos.png" alt="Icone CCBC" />
                    </div>
                    <div class="titulo">
                        <h1>Próximos Eventos</h1>
                    </div>
                    <div class="subtitulo">
                        <h2>Nossos eventos mais próximos</h2>
                    </div>
                    <div class="conteudo-home-direita-eventos-conteudo">
                        <div class="container">
                            <div class="l-rotator">
                                <div class="screen">
                                    <noscript>

</noscript>
                                </div>
                                <div class="thumbnails">
                                    <ul>


                                        <!--evento-->
                                        <li>
                                            <div class="thumb cadeado-ativo">
                                                <p style="width: 80%; float: left;">
                                                    <!--TITULO-->
                                                    <span class="title">V Colóquio de Direito Administrativo </span>
                                                    <br />
                                                    <!--DESCRICAO-->
                                                    Novas tendências em arbitragem e mediação envolvendo a Administração Pública
                                                </p>
                                                <!--DIA / MES-->
                                                <span class="data">
                                                    <h1>02</h1>
                                                    <h3>dezembro</h3>
                                                </span>
                                            </div>
                                            <!--IMAGEM EM DESTAQUE-->
                                            <a href="images/scottwills_building2.jpg"></a>
                                            <!--DIV DA IMG DESTACADA-->
                                            <div style="bottom: 0px; left: 0px; width: 600px; height: 50px;"></div>
                                        </li>
                                        <!--evento-->

                                        <!--evento-->
                                        <li>
                                            <div class="thumb cadeado-ativo">
                                                <p style="width: 80%; float: left;">
                                                    <!--TITULO-->
                                                    <span class="title">V Colóquio de Direito Administrativo2 </span>
                                                    <br />
                                                    <!--DESCRICAO-->
                                                    Novas tendências em arbitragem e mediação envolvendo a Administração Pública
                                                </p>
                                                <!--DIA / MES-->
                                                <span class="data">
                                                    <h1>05</h1>
                                                    <h3>dezembro</h3>
                                                </span>
                                            </div>
                                            <!--IMAGEM EM DESTAQUE-->
                                            <a href="images/scottwills_building2.jpg"></a>
                                            <!--DIV DA IMG DESTACADA-->
                                            <div style="bottom: 0px; left: 0px; width: 600px; height: 50px;"></div>
                                        </li>
                                        <!--evento-->

                                        <!--evento-->
                                        <li>
                                            <div class="thumb">
                                                <p style="width: 80%; float: left;">
                                                    <!--TITULO-->
                                                    <span class="title">Ciclo Nacional de Arbitragem e Mediação</span><br />
                                                    <!--DESCRICAO-->
                                                    LOCAL : Escola Superior da Magistratura da Paraíba / Rua Abelardo S. G. Barreto, s/n / Altiplano - João Pessoa - PB
                                                </p>
                                                <!--DIA / MES-->
                                                <span class="data">
                                                    <h1>28</h1>
                                                    <h3>dezembro</h3>
                                                </span>
                                            </div>
                                            <!--IMAGEM EM DESTAQUE-->
                                            <a href="imgs/Evento02.jpg"></a>
                                            <!--DIV DA IMG DESTACADA-->
                                            <div style="bottom: 0px; left: 0px; width: 600px; height: 50px;"></div>
                                        </li>
                                        <!--evento-->

                                        <asp:Panel ID="PanelEventos" Visible="false" runat="server">
                                            <!--evento-->
                                            <li>
                                                <div class="thumb">
                                                    <p style="width: 80%; float: left;">
                                                        <!--TITULO-->
                                                        <span class="title">Scholarship Opportunity</span><br />
                                                        <!--DESCRICAO-->
                                                        ‘The Fundamentals of International Arbitration’
under the coordination of Prof. Jan Paulsson.
                                                    </p>
                                                    <!--DIA / MES-->
                                                    <span class="data">
                                                        <h1>07</h1>
                                                        <h3>janeiro</h3>
                                                    </span>
                                                </div>
                                                <!--IMAGEM EM DESTAQUE-->
                                                <a href="imgs/Evento03.jpg"></a>
                                                <!--DIV DA IMG DESTACADA-->
                                                <div style="bottom: 0px; left: 0px; width: 600px; height: 50px;"></div>
                                            </li>
                                            <!--evento-->
                                        </asp:Panel>

                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="vertodos"><a href="Eventos.aspx">+ ver todos</a></div>
                <!--eventos-->


                <!--noticias-->
                <div class="conteudo-home-direita-noticias">
                    <div class="ico">
                        <img src="imgs/Icon_noticias.png" alt="Icone CCBC" />
                    </div>
                    <div class="titulo">
                        <h1>Notícias</h1>
                    </div>
                    <div class="subtitulo">
                        <h2>Atualize-se com nossas news</h2>
                    </div>
                    <div class="conteudo-home-direita-noticias-conteudo">
                        <!--esquerda-->
                        <div class="conteudo-home-direita-noticias-conteudo-esquerda">
                            <!--noticia destaque-->
                            <div class="conteudo-home-direita-noticias-conteudo-esquerda-destaque">
                                <div class="conteudo-home-direita-noticias-conteudo-esquerda-destaque-img opacidade">
                                    <a href="canada-pode-ser-parceiro-de-londrina-em-investimentos.aspx">
                                        <img src="imgs/Img_noticias.jpg" alt="Noticias CCBC" /></a>
                                </div>
                                <div class="conteudo-home-direita-noticias-conteudo-esquerda-destaque-texto">
                                    <h2 class="opacidade"><a href="canada-pode-ser-parceiro-de-londrina-em-investimentos.aspx">CANADÁ PODE SER PARCEIRO DE LONDRINA EM INVESTIMENTOS</a></h2>
                                    <p>Londrina dará continuidade às atividades de fomento de negócios e parcerias com o Canadá.</p>
                                    <p class="data-noticias">
                                        02 de abril de 2014
                                           <!-- <img src="imgs/Img_cadeado_fechado.png" alt="cadeado ccbc" />-->
                                    </p>
                                </div>
                                <!--noticia destaque-->
                                <div class="margem-top">
                                    <hr size="1" width="100%" />
                                </div>
                                <div class="conteudo-home-direita-noticias-conteudo-texto">
                                    <h3 class="opacidade"><a href="camara-de-comercio-brasil-canada-realiza-evento-no-norte-do-parana.aspx">CÂMARA DE COMÉRCIO BRASIL-CANADÁ REALIZA EVENTO NO NORTE DO PARANÁ</a></h3>
                                    <p class="data-noticias">
                                        02 de abril de 2014
                                          <!--   <img src="imgs/Img_cadeado.png" alt="cadeado ccbc" /> -->
                                    </p>
                                </div>
                            </div>
                            <!--esquerda-->


                        </div>

                        <div class="conteudo-home-direita-noticias-conteudo-direita">
                            <div class="conteudo-home-direita-noticias-conteudo-texto">
                                <h3 class="opacidade"><a href="brasil-sobe-para-a-38-posicao-entre-melhores-paises-para-fazer-negocios.aspx">BRASIL SOBE PARA A 38ª POSIÇÃO ENTRE MELHORES PAÍSES PARA FAZER NEGÓCIOS</a></h3>
                                <p class="data-noticias">
                                    02 de abril de 2014
                                        <!-- <img src="imgs/Img_cadeado_fechado.png" alt="cadeado ccbc" /> -->
                                </p>
                            </div>
                        </div>

                        <div class="margem-top margem-left">
                            <hr size="1" width="100%" />
                        </div>

                        <div class="conteudo-home-direita-noticias-conteudo-direita">
                            <div class="conteudo-home-direita-noticias-conteudo-texto">
                                <h3 class="opacidade"><a href="numero-de-brasileiros-cresce-8-no-canada-em-2012.aspx">NÚMERO DE BRASILEIROS CRESCE 8% NO CANADÁ EM 2012</a></h3>
                                <p class="data-noticias">
                                    02 de abril de 2014
                                       <!-- <img src="imgs/Img_cadeado.png" alt="cadeado ccbc" />-->
                                </p>
                            </div>
                        </div>

                        <asp:Panel ID="PanelNoticias" Visible="false" runat="server">
                            <div class="margem-top margem-left">
                                <hr size="1" width="100%" />
                            </div>
                            <div class="conteudo-home-direita-noticias-conteudo-direita">
                                <div class="conteudo-home-direita-noticias-conteudo-texto">
                                    <h3 class="opacidade"><a href="air-canada-recebe-ranking-quatro-estrelas.aspx">AIR CANADA RECEBE RANKING QUATRO ESTRELAS</a></h3>
                                    <p class="data-noticias">
                                        02 de abril de 2014
                                        <img src="imgs/Img_cadeado.png" alt="cadeado ccbc" />
                                    </p>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>
                </div>

                <div class="vertodos"><a href="Noticias.aspx">+ ver todos</a></div>
                <!--noticias-->


                <div class="conteudo-home-direita-assossiados">
                    <h1>Associados Mantenedores</h1>
                    <div class="conteudo-home-direita-assossiados-conteudo">
                        <img src="imgs/Assos_01.jpg" alt="Assossiados CCBC" class="opacidade" />
                        <img src="imgs/Assos_02.jpg" alt="Assossiados CCBC" class="opacidade" />
                        <img src="imgs/Assos_03.jpg" alt="Assossiados CCBC" class="opacidade" />
                        <img src="imgs/Assos_04.jpg" alt="Assossiados CCBC" class="opacidade" />
                        <img src="imgs/Assos_05.jpg" alt="Assossiados CCBC" class="opacidade" />
                        <img src="imgs/Assos_06.jpg" alt="Assossiados CCBC" class="opacidade" />
                    </div>
                </div>


                <div class="conteudo-home-direita-instituicoes">
                    <h1>Instituições Parceiras</h1>
                    <div class="conteudo-home-direita-instituicoes-conteudo">
                        <img src="imgs/Parceiros_01.jpg" alt="Assossiados CCBC" class="opacidade" />
                        <img src="imgs/Parceiros_02.jpg" alt="Assossiados CCBC" class="opacidade" />
                        <img src="imgs/Parceiros_03.jpg" alt="Assossiados CCBC" class="opacidade" />
                        <img src="imgs/Parceiros_04.jpg" alt="Assossiados CCBC" class="opacidade" />
                        <img src="imgs/Parceiros_05.jpg" alt="Assossiados CCBC" class="opacidade" />
                        <img src="imgs/Parceiros_06.jpg" alt="Assossiados CCBC" class="opacidade" />
                    </div>
                </div>

                <div class="conteudo-home-direita-publicidade opacidade">
                    <a href="scotiabank.aspx">
                        <img src="imgs/Img_publicidade.jpg" alt="Publicidade CCBC" /></a>
                </div>

            </div>
            <!--conteudo / direta-->
        </div>
    </div>

</asp:Content>

