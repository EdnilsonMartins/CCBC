
var FormModel_Editoria, FormModelOld_Editoria;

function GravarEditoria() {

    FormModel_Editoria = SerializaForm("frmCadEditoria");
    var formdata = JSON.stringify(FormModel_Editoria);
    var formDataOld = JSON.stringify(FormModelOld_Editoria);

    $.post("../Editoria/GravarEditoria", { Editoria: formdata, EditoriaOld: formDataOld }, function (data) {
        if (data.Resposta.Erro == false) {
            $("#CadEditoria").modal("hide");
            var editoriaId = data.Editoria.EditoriaId;
            ListarEditoria(editoriaId);
            bootbox.dialog({
                message: "Dados gravados com sucesso!",
                title: "Sucesso",
                buttons: {
                    ok: {
                        label: "OK",
                        className: "btn-success",
                        callback: function () {
                            
                            
                            //window.location.href = "../Publicacao/ListaPublicacao";
                        }
                    }
                }
            });
        } else {
            MensagemErro("Não foi possível gravar os dados da Editoria.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}

function NovoEditoria() {
    $("#EditoriaId", "#frmCadEditoria").val("0");
    $("#Descricao", "#frmCadEditoria").val("");
    $("#DescricaoEN", "#frmCadEditoria").val("");
    $("#DescricaoES", "#frmCadEditoria").val("");
    $("#DescricaoFR", "#frmCadEditoria").val("");
    $("#Descricao", "#frmCadEditoria").focus();
    $("#myModalTitle", "#CadEditoria").html("Nova Editoria");
    $("#btnNovoEditoria", "#CadEditoria").attr("style", "display: none;");
}


function PreencherCadastroEditoria(data, IdiomaId) {
    $("#EditoriaId", "#frmCadEditoria").val(data.Editoria.EditoriaId);
    if (IdiomaId == 1) {
        //PT
        $("#Descricao", "#frmCadEditoria").val(data.Editoria.Detalhe.Descricao);
    } else if (IdiomaId == 2) {
        //ES
        $("#DescricaoES", "#frmCadEditoria").val(data.Editoria.Detalhe.Descricao);
    } else if (IdiomaId == 3) {
        //EN
        $("#DescricaoEN", "#frmCadEditoria").val(data.Editoria.Detalhe.Descricao);
    } else if (IdiomaId == 4) {
        //FR
        $("#DescricaoFR", "#frmCadEditoria").val(data.Editoria.Detalhe.Descricao);
    }
}

function CarregarEditoria(editoriaId) {
    if (editoriaId != null && editoriaId != "") {
        $("#myModalTitle", "#CadEditoria").html("Manutenção de Editoria");
        //--xx
        //$.get("../Editoria/CarregarEditoria", { EditoriaId: editoriaId, IdiomaId: 1 }, function (data) {
        //    if (data != null) {
        //        $("#EditoriaId", "#frmCadEditoria").val(data.Editoria.EditoriaId);
        //        $("#Descricao", "#frmCadEditoria").val(data.Editoria.Descricao);
        //    }
        //});
        //--XX
        var idiomaId = 1;
        $.get("../Editoria/CarregarEditoria", { EditoriaId: editoriaId, IdiomaId: idiomaId }, function (data) {
            if (data.Editoria.EditoriaId != null) {
                PreencherCadastroEditoria(data, idiomaId);
                //ES
                idiomaId = 2;
                $.get("../Editoria/CarregarEditoria", { EditoriaId: editoriaId, IdiomaId: idiomaId }, function (data) {
                    if (data.Editoria.EditoriaId != null) {
                        PreencherCadastroEditoria(data, idiomaId);
                        //EN
                        idiomaId = 3;
                        $.get("../Editoria/CarregarEditoria", { EditoriaId: editoriaId, IdiomaId: idiomaId }, function (data) {
                            if (data.Editoria.EditoriaId != null) {
                                PreencherCadastroEditoria(data, idiomaId);
                                //FR
                                idiomaId = 4;
                                $.get("../Editoria/CarregarEditoria", { EditoriaId: editoriaId, IdiomaId: idiomaId }, function (data) {
                                    if (data.Editoria.EditoriaId != null) {
                                        PreencherCadastroEditoria(data, idiomaId);
                                        FormModelOld_Editoria = SerializaForm("frmCadEditoria");
                                    } else {
                                        MensagemErro("Erro ao carregar dados da editoria.4");
                                    }
                                });
                            } else {
                                MensagemErro("Erro ao carregar dados da editoria.3");
                            }
                        });
                    } else {
                        MensagemErro("Erro ao carregar dados da editoria.2");
                    }
                });

            } else {
                MensagemErro("Erro ao carregar dados da editoria.1");
            }
        });
        $("#btnNovoEditoria", "#CadEditoria").attr("style", "float: left; display: inline;");
    } else {
        NovoEditoria();
    }
}

function ListarEditoria(editoriaId) {

    $("#Editoria", "#frmCadPublicacao").html("");
    
    $.get("../Editoria/ListarEditoria", {}, function (data) {
        if (data != null) {

            $("#Editoria", "#frmCadPublicacao").html("");

            $("#Editoria", "#frmCadPublicacao").append("<option value=\"\"></option>");
            $.each(data, function (i, item) {
                $("#Editoria", "#frmCadPublicacao").append("<option value=\"" + item.EditoriaId + "\">" + item.Detalhe.Descricao + "</option>");
            });


            $("#Editoria", "#frmCadPublicacao").selectpicker();
            $("#Editoria", "#frmCadPublicacao").selectpicker('val', "");
            $("#Editoria", "#frmCadPublicacao").selectpicker('refresh');
            
            if (editoriaId != null && editoriaId != "") {
                $('select[name=Editoria]', "#frmCadPublicacao").selectpicker('val', editoriaId);
            }
        }
    });

}

function VisualizarDialogEditoria() {
    $("#CadEditoria").modal("show");
}

$(function () {

    $("#btnEditoria").click(function () {
        VisualizarDialogEditoria();
        var editoriaId = $("#Editoria").val();
        CarregarEditoria(editoriaId);
    });

    $("#btnNovoEditoria").click(function () {
        NovoEditoria();
    })

    $("#btnSalvarEditoria").click(function () {
        GravarEditoria();
    })
});

