$(function () {

    $("#txtBusca").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            //var cod = $("#CodCliente").val();
            //if (cod.length > 0)
            //    validaCodigo(cod);
            //window.location.href = AppPath + "Busca";
            document.forms[0].submit();
        }
    });

    $('#cmdBusca').click(function () {
        //window.location.href = AppPath + "Busca";
        document.forms[0].submit();
    });

});