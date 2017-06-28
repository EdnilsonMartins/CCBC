function Calcular() {
    var valor = $("#txtValor").maskMoney('unmasked')[0];
    console.log(valor);
    $.post(AppPath + "Conteudo/Calcular2015", { Valor: valor }, function (data) {
        if (data.Resposta.Erro == false) {
            
            console.log(data.Calculadora.ArbitroUnico);
            console.log(data.Calculadora.Presidente);
            console.log(data.Calculadora.CoArbitros);
            console.log(data.Calculadora.Total);
            console.log(data.Calculadora.DespesasAdministrativas);

            $("#lblArbitroUnico").html(data.Calculadora.ArbitroUnicoStr);
            $("#lblPresidente").html(data.Calculadora.PresidenteStr);
            $("#lblCoArbitros").html(data.Calculadora.CoArbitrosStr);
            $("#lblTotal").html(data.Calculadora.TotalStr);
            $("#lblDespesasAdministrativas").html(data.Calculadora.DespesasAdministrativasStr);

            //$("#newsletter_nome").val("");
            //$("#newsletter_email").val("");
            //$("#chkInscrever").attr("checked", false);
            //$("#chkDescadastrar").attr("checked", false);

        }
    });
}

function Calcular2017() {
    var valor = $("#txtValor2017").maskMoney('unmasked')[0];
    $.post(AppPath + "Conteudo/Calcular2017", { Valor: valor }, function (data) {
        if (data.Resposta.Erro == false) {
            console.log(data.Calculadora.ArbitroUnico);
            console.log(data.Calculadora.Presidente);
            console.log(data.Calculadora.CoArbitros);
            console.log(data.Calculadora.Total);
            console.log(data.Calculadora.DespesasAdministrativas);

            console.log(data.Calculadora.TaxaRegistro);
            console.log(data.Calculadora.HonorarioArbitroUnico);
            console.log(data.Calculadora.TotalRequerenteArbitroUnico);
            console.log(data.Calculadora.TotalRequeridoArbitroUnico);
            console.log(data.Calculadora.Honorario3Arbitros);
            console.log(data.Calculadora.TotalRequerente3Arbitros);
            console.log(data.Calculadora.TotalRequerido3Arbitros);

            $("#lblArbitroUnico").val(data.Calculadora.ArbitroUnicoStr);
            $("#lblPresidente").val(data.Calculadora.PresidenteStr);
            $("#lblCoArbitros").val(data.Calculadora.CoArbitrosStr);
            $("#lblCoArbitrosX").val(data.Calculadora.CoArbitrosStr2);

            $("#lblTotal").val(data.Calculadora.TotalStr);
            $("#lblRequerenteX").val(data.Calculadora.Honorario3ArbitrosStr);
            $("#lblRequeridoX").val(data.Calculadora.Honorario3ArbitrosStr);
            $("#lblDespesasAdministrativas").html(data.Calculadora.DespesasAdministrativasStr);

            //Tribunal Arbitral (3 Árbitros)
                //Requerente
                $("#lblRequerenteTaxaRegistro").html(data.Calculadora.TaxaRegistroStr);
                $("#lblRequerenteTaxaAdministracao").html(data.Calculadora.DespesasAdministrativasStr);
                $("#lblRequerenteHonorarioArbitros").html(data.Calculadora.Honorario3ArbitrosStr);
                $("#lblRequerenteTotal").html(data.Calculadora.TotalRequerente3ArbitrosStr);
                //Requerido
                $("#lblRequeridoTaxaAdministracao").html(data.Calculadora.DespesasAdministrativasStr);
                $("#lblRequeridoHonorarioArbitros").html(data.Calculadora.Honorario3ArbitrosStr);
                $("#lblRequeridoTotal").html(data.Calculadora.TotalRequerido3ArbitrosStr);

            //Árbitro Único
                //Requerente
                $("#lblRequerenteTaxaRegistro2").html(data.Calculadora.TaxaRegistroStr);
                $("#lblRequerenteTaxaAdministracao2").html(data.Calculadora.DespesasAdministrativasStr);
                $("#lblRequerenteHonorarioArbitros2").html(data.Calculadora.HonorarioArbitroUnicoStr);
                $("#lblRequerenteTotal2").html(data.Calculadora.TotalRequerenteArbitroUnicoStr);
                //Requerido
                $("#lblRequeridoTaxaAdministracao2").html(data.Calculadora.DespesasAdministrativasStr);
                $("#lblRequeridoHonorarioArbitros2").html(data.Calculadora.HonorarioArbitroUnicoStr);
                $("#lblRequeridoTotal2").html(data.Calculadora.TotalRequeridoArbitroUnicoStr);


            //Impressão
                $("#lblRequerenteTaxaRegistroX").val(data.Calculadora.TaxaRegistroStr);
                $("#lblRequerenteTaxaAdministracaoX").val(data.Calculadora.DespesasAdministrativasStr);
                $("#lblRequeridoTaxaAdministracaoX").val(data.Calculadora.DespesasAdministrativasStr);
                $("#lblRequerenteHonorarioArbitrosX").val(data.Calculadora.Honorario3ArbitrosStr);
                $("#lblRequeridoHonorarioArbitrosX").val(data.Calculadora.Honorario3ArbitrosStr);
                $("#lblRequerenteTotalX").val(data.Calculadora.TotalRequerente3ArbitrosStr);
                $("#lblRequeridoTotalX").val(data.Calculadora.TotalRequerido3ArbitrosStr);


                $("#lblRequerenteTaxaRegistro2X").val(data.Calculadora.TaxaRegistroStr);
                $("#lblRequerenteTaxaAdministracao2X").val(data.Calculadora.DespesasAdministrativasStr);
                $("#lblRequeridoTaxaAdministracao2X").val(data.Calculadora.DespesasAdministrativasStr);
                $("#lblRequerenteHonorarioArbitros2X").val(data.Calculadora.HonorarioArbitroUnicoStr);
                $("#lblRequeridoHonorarioArbitros2X").val(data.Calculadora.HonorarioArbitroUnicoStr);
                $("#lblRequerenteTotal2X").val(data.Calculadora.TotalRequerenteArbitroUnicoStr);
                $("#lblRequeridoTotal2X").val(data.Calculadora.TotalRequeridoArbitroUnicoStr);
        }
    });
}

function Imprimir2017() {
    window.open(AppPath + 'Conteudo/Composicao2017Imprimir', 'Calculadora 2017', 'STATUS=NO, TOOLBAR=NO, LOCATION=NO, DIRECTORIES=NO, RESISABLE=NO, SCROLLBARS=YES, TOP=10, LEFT=10, WIDTH=930, HEIGHT=600');
}

$(function () {

    $("#txtValor").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            Calcular();
        }
    });

    $("#txtValor2017").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            Calcular2017();
        }
    });

    $('#cmdCalcular').click(function () {
        Calcular();
    });

    $('#cmdCalcular2017').click(function () {
        Calcular2017();
        $("#cmdImprimir2017").attr("style", "display: block; float: right; margin: 5px 0px 0 100px;");
        
    });

    $('#cmdImprimir2017').click(function () {
        Imprimir2017();
    });

    $('#txtValor').maskMoney();
    $('#txtValor2017').maskMoney();
});