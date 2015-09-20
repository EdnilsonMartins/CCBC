
var RegistrandoClique = false;

$(function () {

    $(document).ready(function () {

        $.each($('a[class*="cms-counter"]'), function (i, item) {
            $(item).click(function () {

                var arquivoId = $(item).attr('arquivoId');

                CounterXX($(item).attr('referenciaId'), arquivoId);
            });
            
        });

    });

});



function CounterXX(item, arquivoId) {

    //alert('Lets go insert a click for this banner, thats cool!! ;)   ' + item);
    if (RegistrandoClique == false) {
        console.log("Clique: BannerId=" + item + " ArquivoId=" + arquivoId);
        RegistrandoClique = true;
        var _bannerId = item;
        $.post(AppPath + "Conteudo/RegistrarClick", { BannerId: _bannerId, ArquivoId: arquivoId }, function (data) {
            RegistrandoClique = false;
            if (data.length == 0) {

            }
        });
    }
}