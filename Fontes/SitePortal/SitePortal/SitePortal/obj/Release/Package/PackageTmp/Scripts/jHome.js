$(function () {

    $("#txtMinhaContaSenha").keyup(function (event) {
        var id = $("#txtMinhaContaID").val();
        var _senha = $("#txtMinhaContaSenha").val();
        console.log(_senha);
        $.post(AppPath + "MinhaConta/ValidarSenha", { ID: id, senha: _senha }, function (data) {
            console.log(data.Resposta);
            $("#dvMinhaSenhaValidacaoDefault").attr("style", "display: none;");
            if (data.Resposta.Erro == true) {
                $("#dvMinhaSenhaValidacao").attr("style", "display: inline-block; border: 2px solid red; border-radius: 2px; padding: 12px 10px 0 10px; margin: 10px 0 0 10px; background-color: lightsalmon; color: black; height: 28px;");
                $("#msgMinhaContaSenha").html(data.Resposta.Mensagem);
            } else {
                var codeValid = data.Resposta.Mensagem;
                console.log(codeValid);
                if (codeValid == "1") {
                    $("#dvMinhaSenhaValidacao").attr("style", "display: inline-block; border: 2px solid orange; border-radius: 2px; padding: 12px 10px 0 10px; margin: 10px 0 0 10px; background-color: #FFE17A; color: darkorange; height: 28px;");
                    $("#msgMinhaContaSenha").html("Nível de Segurança da Senha: <b>FRACO</b>");
                } else if (codeValid == "2") {
                    $("#dvMinhaSenhaValidacao").attr("style", "display: inline-block; border: 2px solid green; border-radius: 2px; padding: 12px 10px 0 10px; margin: 10px 0 0 10px; background-color: lightgreen; color: green; height: 28px;");
                    $("#msgMinhaContaSenha").html("Nível de Segurança da Senha: <b>MÉDIO</b>");
                } else if (codeValid == "3") {
                    $("#dvMinhaSenhaValidacao").attr("style", "display: inline-block; border: 2px solid blue; border-radius: 2px; padding: 12px 10px 0 10px; margin: 10px 0 0 10px; background-color: lightblue; color: blue; height: 28px;");
                    $("#msgMinhaContaSenha").html("Nível de Segurança da Senha: <b>ALTO</b>");
                }
            }
        });
    });

    $('#btnEntrar').click(function () {
        EfetuarLogin();
    });

    $('#btnSair').click(function () {
        //$("#Panel_on").attr("style", "display:none");
        //$("#Panel_off").attr("style", "display:block");
        EfetuarLogoff();
    });

    $("#txtUsuario").change(function () {
        $("#dvLoginFail").attr("style", "display: none");
    });

    $("#txtSenha").change(function () {
        $("#dvLoginFail").attr("style", "display: none");
    });

    $("#txtUsuario").keypress(function (event) {
        if (event.which == 13) {
            $("#txtSenha").focus();
        }
    });

    $("#txtSenha").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            EfetuarLogin();
        }
    });

    $("#btnMailing").click(function () {
        RegistrarMailing();
    });

    $(".itemCidade").click(function () {
        AtualizarTemperatura($(this));
    });

    $("#spInscrever").click(function () {
        var chkInscrever = $("#chkInscrever").attr("checked");
        if (chkInscrever) {
            $("#chkInscrever").attr("checked", false);
        } else {
            $("#chkDescadastrar").attr("checked", false);
            $("#chkInscrever").attr("checked", true);

        }
    });
    $("#chkInscrever").change(function () {
        var chkInscrever = $("#chkInscrever").attr("checked");
        if (chkInscrever) {
            //$("#chkInscrever").attr("checked", false);
        } else {
            $("#chkDescadastrar").attr("checked", false);
            //$("#chkInscrever").attr("checked", true);

        }
    });

    $("#spDescadastrar").click(function () {
        var chkDescadastrar = $("#chkDescadastrar").attr("checked");
        if (chkDescadastrar) {
            $("#chkDescadastrar").attr("checked", false);
        } else {
            $("#chkInscrever").attr("checked", false);
            $("#chkDescadastrar").attr("checked", true);

        }
    });
    $("#chkDescadastrar").change(function () {
        var chkDescadastrar = $("#chkDescadastrar").attr("checked");
        if (chkDescadastrar) {
            //$("#chkDescadastrar").attr("checked", false);
        } else {
            $("#chkInscrever").attr("checked", false);
            //$("#chkDescadastrar").attr("checked", true);

        }
    });

    CarregarTemperaturaTaxaCambio();



    $(document).ready(
        function () {
            $("#ControlePaiRotator").wtListRotator({
                screen_width:330,
                screen_height:270,
                item_width:410,
                item_height:90,
                item_display:3,
                list_align:"right",
                scroll_type:"mouse_move",
                auto_start:true,
                delay:4000,
                transition:"v.slide",
                transition_speed:800,
                display_playbutton:false,
                display_number:false,
                display_timer:true,
                display_arrow:false,
                display_thumbs:true,
                display_scrollbar:true,
                pause_mouseover:false,
                cpanel_mouseover:false,
                text_mouseover:false,
                text_effect:"fade",
                text_sync:true,
                cpanel_align:"TR",
                timer_align:"bottom",
                move_one:false,
                auto_adjust:true,
                shuffle:false,
                block_size:75,
                vert_size:50,
                horz_size:50,
                block_delay:35,
                vstripe_delay:90,
                hstripe_delay:180
            });
    });

});

function EfetuarLogin() {

    var login = $("#txtUsuario").val();
    var senha = $("#txtSenha").val();
    
    $.post(AppPath + "Home/EfetuarLogin", { Login: login, Senha: senha}, function (data) {
        if (data.Resposta.Erro == true) {
            $("#dvLoginFail").attr("style", "display:block");
            $("#lblLoginFail").html(data.Resposta.Mensagem);
            $("#txtUsuario").focus();
        } else {
            $("#lblLoginNome").html(data.Nome);
            //$("#Panel_off").attr("style", "display:none");
            //$("#Panel_on").attr("style", "display:block");
            
            
            window.location.href = AppPath + "Home";
        }
    });
    
}

function EfetuarLogoff() {

    
    //alert(AppPath);

    $.post(AppPath + "Home/EfetuarLogoff", {  }, function (data) {
        
            window.location.href = AppPath + "Home";
    });

}

function RegistrarMailing() {

    var nome = $("#newsletter_nome").val();
    var email = $("#newsletter_email").val();
    var chkInscrever = $("#chkInscrever").attr("checked");
    var chkDescadastrar = $("#chkDescadastrar").attr("checked");
    var cadastrar = true;
    if (chkInscrever) cadastrar = true;
    if (chkDescadastrar) cadastrar = false;
    //console.log(cadastrar);
    if (chkInscrever || chkDescadastrar) {
        $.post(AppPath + "Home/RegistrarMailing", { Nome: nome, Email: email, Cadastrar: cadastrar }, function (data) {
            if (data.Resposta.Erro == false) {
                if (cadastrar) {
                    alert('Assinatura registrada, obrigado!');
                } else {
                    alert('Assinatura descadastrada, obrigado!');

                }
                $("#newsletter_nome").val("");
                $("#newsletter_email").val("");
                $("#chkInscrever").attr("checked", false);
                $("#chkDescadastrar").attr("checked", false);

            }
        });
        $("#validaCamposNews").attr("style", "display: none;");
    } else {
        $("#validaCamposNews").attr("style", "float: left; margin-bottom: 15px; display: none;");
    }

}

function AtualizarTemperatura(item) {
    //console.log(item);
    var id = item.attr("id");
    var nome = item.html();
    $("#nomeCidade").html(nome);
    $("#ListaTemperatura").hide();
    $("#lblTemperatura").html("");
    $.post(AppPath + "Home/GetWeatherExchangeFee", { CidadeId: id}, function (data) {
        if (data != null) {
            $("#lblTemperatura").html(data.Temperatura);
        }
    });
}

function CarregarTemperaturaTaxaCambio() {

    /*
    $.post(AppPath + "Home/GetWeatherExchangeFee", { CidadeId: 1 }, function (data) {
        if (data != null) {
            $("#lblTemperatura").html(data.Temperatura);
        }
    });
    */


    var moedas = "%28%22USDBRL%22%2C%22CADBRL%22%2C%22AUDBRL%22%2C%22EURBRL%22%29";
    var url = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20" + moedas + "%20&format=json&diagnostics=false&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
    
    
    // Obtem e trata os dados em JSON
    $.getJSON(url, function (data) {
        //console.log(data);
        // Agrupa os dados em HTML
        try {
            $("#lblTaxaCAN").html("R$ " + data.query.results.rate[1].Rate);
            $("#lblTaxaUSA").html("R$ " + data.query.results.rate[0].Rate);
            $("#lblTaxaEURO").html("R$ " + data.query.results.rate[3].Rate);
        } catch (error) {
            var indices = error.message;
        }

        // Mostra a cotação do dolar na tag <div id="info"></div>
        //$('#lblTaxaCAN').html(indices);
    });


    

}