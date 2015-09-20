<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css/demo.css" />
    <link rel="stylesheet" type="text/css" href="css/style1.css" />
    <script type="text/javascript" src="js/modernizr.custom.86080.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style>
        .opacidade:hover {
            opacity: 0.5;
        }

        .linha {
            width: 500px;
            height: auto;
            margin: 100px auto;
        }

        .box {
            float: left;
            width: 50%;
            height: auto;
            min-height: 110px;
        }

            .box:hover {
                opacity: 0.5;
            }
    </style>
    <title>CCBC - Câmara de Comércio Brasil Canadá</title>
</head>
<body>
    <form id="form1" runat="server">
        <ul class="bg-slideshow">
            <li><span>Image 01</span><div></div>
            </li>
            <li><span>Image 02</span><div></div>
            </li>
            <li><span>Image 03</span><div></div>
            </li>
            <li><span>Image 04</span><div></div>
            </li>
            <li><span>Image 05</span><div></div>
            </li>
            <li><span>Image 06</span><div></div>
            </li>
            <li><span>Image 07</span><div></div>
            </li>
            <li><span>Image 08</span><div></div>
            </li>
            <li><span>Image 09</span><div></div>
            </li>
            <li><span>Image 10</span><div></div>
            </li>
        </ul>
        <!-- BG SlideShow -->




        <div class="conteudo">


            <!-- Logo + Menu -->
            <div id="tdz_escolha_bg">

                <div class="linha">
                    <div class="box">
                        <a href="Erro.aspx?Codigo=1-001&Descricao=Erro ao tentar conectar ao serviço">
                            <img src="images/bt-01.png" alt="ccbc" /></a>
                    </div>
                    <div class="box">
                        <a href="Default2.aspx">
                            <img src="images/bt-02.png" alt="ccbc" /></a>
                    </div>
                    <div class="box">
                        <a href="Erro.aspx?Codigo=1-001&Descricao=Erro ao tentar conectar ao serviço">
                            <img src="images/bt-03.png" alt="ccbc" /></a>
                    </div>
                    <div class="box">
                        <a href="Erro.aspx?Codigo=1-001&Descricao=Erro ao tentar conectar ao serviço">
                            <img src="images/bt-04.png" alt="ccbc" /></a>
                    </div>
                </div>




            </div>
            <!-- Logo + Menu -->
        </div>
    </form>
</body>
</html>
