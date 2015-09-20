<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contato.aspx.cs" Inherits="Contato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="geral-1050">
        <div class="conteudo">

            <!-- nav internas -->
            <div class="internas-navegacao">
                <a href="Default2.aspx">Home</a>
                <a href="#">-</a>
                <a href="Contato.aspx">Contato</a>
            </div>
            <!-- nav internas -->
            <!-- contato -->
            <div class="contato-titulo">
                <h1>Entre em <span class="cor-padrao-texto">Contato</span></h1>
            </div>
            <div class="contato-form">
                <input type="text" name="nome" value="Nome" />
                <input type="text" name="email" value="Email" />
                <input type="text" name="telefone" value="Telefone" />
                <select>
                    <option>Departamento</option>
                    <option>Financeiro</option>
                    <option>Administrativo</option>
                    <option>Desenvolvimento</option>
                </select>
                <input type="text" name="assunto" value="Assunto" />
                <textarea rows="auto" cols="auto">Mensagem</textarea>
                <input class="cor-padrao opacidade" type="submit" value="ENVIAR" />
            </div>
            <div class="contato-mapa">
                <div class="contato-mapa-mapa">
                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d3656.2890607735126!2d-46.68602249999999!3d-23.593964!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x94ce5746176c79e3%3A0x63c0c9a13f38972c!2sR.+do+Rocio%2C+220+-+Vila+Ol%C3%ADmpia%2C+S%C3%A3o+Paulo+-+SP!5e0!3m2!1spt-BR!2sbr!4v1416315817365" width="465" height="400" frameborder="0" style="border: 0"></iframe>
                </div>
                <div class="contato-mapa-texto">
                    <p>
                        Rua do Rócio, 220 12º andar - cj.121<br />
                        CEP. 04552-000 - Vila Olímpia São Paulo - SP - Brasil<br />
                        +55-11-4058-0400  |  ccbc@ccbc.org.br
                    </p>
                </div>
            </div>
            <!-- contato -->


        </div>
    </div>

</asp:Content>

