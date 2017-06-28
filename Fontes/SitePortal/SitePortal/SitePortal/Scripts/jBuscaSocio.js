



$(function () {
    ListarAssociado("");

});


$(document).ready(function () {

    

    $.each($('[class="busca-associados-filtro-linha-letra"]'), function (i, item) {
        $(item).click(function () {
            ListarAssociado($(item).attr('Letra'));
            LimparSelecionados();
            $(item).addClass("selecionado");
        });
    });

});

function LimparSelecionados() {
    
    $.each($('[class="busca-associados-filtro-linha-letra selecionado"]'), function (i, item) {
        $(item).removeClass("selecionado");
    });
}

function ToggleResumo(id) {
    $("#Resumo_" + id).slideToggle();
}

function ListarAssociado(letra) {

    $("#ListaEsquerda").html("");
    $("#ListaDireita").html("");

    var siteId = $("#SiteId").val();
    var associadoCategoriaId = $("#AssociadoCategoriaId").val();
    var ExibeEsquerda = 0;
    var ExibeDireita = 0;

    //Somente o 1-Associados possuem divisão PF / PJ
    if (associadoCategoriaId != 1){
        $("#dvDireita").removeClass("borda-esquerda-cinza");
        $("#TituloEsquerda").html("");
        $("#TituloDireita").html("");

    } else {
        $("#dvDireita").addClass("borda-esquerda-cinza");
        $("#TituloEsquerda").html("Pessoa Jurídica");
        $("#TituloDireita").html("Pessoa Física");
    }

    $.get(AppPath + "BuscaSocio/ListarAssociado", { SiteId: siteId, AssociadoCategoriaId: associadoCategoriaId, LetraInicial: letra }, function (data) {
        if (data != null) {

            var dataAtualizacao;
            $.each(data, function (
                i, item) {
                // 1 = Pessoa Física
                // 2 = Pessoa Jurídica
                var bandeira = "";
                var btnResumo = '<span class="resumo" id="btnResumo_' + item.AssociadoId + '" resumoid="' + item.AssociadoId + '"><span class="resumo-mais">+</span></span>';
                if (item.Resumo == null || item.Resumo == '') btnResumo = '';
                if (item.PaisId == 1) bandeira = " bandeira-brasil";
                if (item.PaisId == 2) bandeira = " bandeira-canada";

                var pessoa = '<div class="busca-associados-resultados-item"><div class="busca-associados-resultados-item-linha"><span class="' + bandeira + '"></span><span class="itemLetra">' + item.Nome + '</span>' + btnResumo + '</div><div class="busca-associados-resultados-item-linha-resumo" id="Resumo_' + item.AssociadoId + '"><span class="itemResumo">' + item.Resumo + '</span></div></div>';

                if (item.TipoPessoaId == 2) {
                    ExibeEsquerda = 1;
                    $("#ListaEsquerda").append(pessoa);
                } else {
                    ExibeDireita = 1;
                    $("#ListaDireita").append(pessoa);
                }

                
                if (item.DataAtualizacao != null) {
                    if (dataAtualizacao == null || (item.Detalhe.DataAtualizacao!= null && item.DataAtualizacao > data)) {
                        dataAtualizacao = item.DataAtualizacao;
                        document.getElementById("lblUltimaAtualizacao").innerHTML = item.Detalhe.DataAtualizacao;
                        console.log("Ultima Atualização (Formatado): " + item.Detalhe.DataAtualizacao);
                    }
                }



            });

            console.log("Ultima Atualização: " + dataAtualizacao);


            $.each($('[id^="Resumo_"]'), function (i, item) {
                $(item).hide();
            });

            $.each($('[id*="btnResumo_"]'), function (i, item) {
                $(item).click(function () {
                    //console.log($(item).attr('ResumoId'));
                    ToggleResumo($(item).attr('ResumoId'));
                });
            });


            if (associadoCategoriaId != 1) {
                if (ExibeEsquerda == 0) {
                    $("#dvEsquerda").attr("style", "display: none;");
                } else {
                    $("#dvEsquerda").attr("style", "display: block;");

                }
                if (ExibeDireita == 0) {
                    $("#dvDireita").attr("style", "display: none;");
                } else {
                    $("#dvDireita").attr("style", "display: block;");
                }
            }

            if (ExibeEsquerda == 0 && associadoCategoriaId != 1) {
                $("#dvEsquerda").attr("style", "display: none;");
            } else {
                $("#dvEsquerda").attr("style", "display: block;");

            }
            if (ExibeDireita == 0 && associadoCategoriaId != 1) {
                $("#dvDireita").attr("style", "display: none;");
            } else {
                $("#dvDireita").attr("style", "display: block;");
            }

            if (ExibeEsquerda == 0 && ExibeDireita == 0) {
                $(".resultados").attr("style", "display: block;");
                $("#dvEsquerda").attr("style", "display: none;");
                $("#dvDireita").attr("style", "display: none;");

            } else {
                $(".resultados").attr("style", "display: none;");
            }

                
        }
    });
}