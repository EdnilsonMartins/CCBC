function Calcular() {
    var valor = $("#txtValor").maskMoney('unmasked')[0]
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


$(function () {

    $("#txtValor").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            Calcular();
        }
    });

    $('#cmdCalcular').click(function () {
        Calcular();
    });

    $('#txtValor').maskMoney();

});