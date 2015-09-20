$(function () {

    //$("btnAprovar").click(function () {
    //    GravarPublicacao();
    //});

    $(".btnHomeAprovar").each(function () {
        $(this).click(function () {
            LiberarPublicacao($(this).prop("id"), true);
        });
    });

    $(".btnHomeReprovar").each(function () {
        $(this).click(function () {
            LiberarPublicacao($(this).prop("id"), false);
        });
    });

});

function LiberarPublicacao(_PublicacaoId, _Liberado) {

    var msg = "";
    if (_Liberado == true) {
        msg = "Deseja realmente APROVAR esta Publicação?";
    } else {
        msg = "Deseja realmente REPROVAR esta Publicação?";
    }

    bootbox.dialog({
        message: msg,
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Publicacao/LiberarPublicacao", { PublicacaoId: _PublicacaoId, Liberado: _Liberado }, function (data) {
                        if (data.Resposta.Erro == false) {
                            //CarregarPublicacao(_PublicacaoId);
                            //MensagemSucesso("Sua avaliação para liberação da publicação foi submetida com sucesso!");
                            window.location.href = AppPath;
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao liberar a publicação.");
                        }
                    });
                }
            },
            nao: {
                label: "Não",
                className: "btn-danger",
                callback: function () {

                }
            }
        }
    });
}