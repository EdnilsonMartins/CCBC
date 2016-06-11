
var FormModel, FormModelOld;

function PreencherCadastro(data) {

    $("#AssociadoId").val(data.Associado.AssociadoId);

    $('select[name=AssociadoCategoria]').selectpicker('val', data.Associado.AssociadoCategoriaId);
    $('select[name=TipoPessoa]').selectpicker('val', data.Associado.TipoPessoaId);
    $('select[name=Pais]').selectpicker('val', data.Associado.PaisId);

    $("#Nome").val(data.Associado.Nome);
    $("#Resumo").val(data.Associado.Resumo);

    $('[checked="checked"]').parent().addClass("checked");

    $("#AssociadoResumo").val(data.Associado.Resumo);
    //Instanciar caso ainda nao esteja.
    if (CKEDITOR.instances.Conteudo == null) {
        CKEDITOR.replace('Resumo', {
            extraPlugins: 'mediacenter',
            allowedContent: true,
            extraAllowedContent: 'link section article figure span ul li header nav aside[*]'
        });
    }
    CKEDITOR.instances.Resumo.setData(data.Associado.Resumo, function () {
        this.checkDirty();
    });
}

function ListarAssociado() {

    var optFunc_400 = $("#optFunc_400").val();
    var optFunc_410 = $("#optFunc_410").val();

    var regs = [];
    var reg = [];

    $.get("../Associado/ListarAssociado", {}, function (data) {
        $.each(data, function (i, item) {
            reg = new Array();
            reg.push(item.AssociadoId);
            reg.push(item.Nome);
            reg.push(item.Resumo);
            reg.push(item.Detalhe.TipoPessoa);
            reg.push(item.Detalhe.AssociadoCategoria);
            reg.push(item.Detalhe.Pais);

            var editar = "";
            var excluir = "";
            if (optFunc_400 != null) {
                editar = "<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarAssociado(" + item.AssociadoId + ")\"></span>";
            }
            if (optFunc_410 != null) {
                excluir = "<span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" title=\"Excluir\" onclick=\"javascript:ExcluirAssociado(" + item.AssociadoId + ")\"></span>";
            }

            reg.push(editar + " " + excluir);
            regs.push(reg);

        });

        $.fn.dataTable.moment('DD/MM/YYYY');

        $('#tblAssociado').dataTable({
            //"bJQueryUI": true,
            "bDestroy": true,
            "sPaginationType": "full_numbers",
            "oLanguage": {
                "sUrl": "../Content/support/pt_BR.txt"
            },
            "aaData": regs
                ,
            "aoColumns": [
                { "sTitle": "ID", "width": "80px" },
                { "sTitle": "Nome" },
                { "sTitle": "Resumo" },
                { "sTitle": "Tipo" },
                { "sTitle": "Categoria" },
                { "sTitle": "País" },
                { "sTitle": "", "orderable": false }
            ]

        });

    });
}

function EditarAssociado(_AssociadoId) {
    window.location.href = AppPath + "Associado/CadAssociado?AssociadoId=" + _AssociadoId;
}

function GravarAssociado() {
    ShowModal(true);
    
    var x = CKEDITOR.instances.Resumo.getData();
    $("#AssociadoResumo").val(x);

    FormModel = SerializaForm("frmCadAssociado");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.post("../Associado/GravarAssociado", { Associado: formdata, AssociadoOld: formDataOld }, function (data) {
        if (data.Resposta.Erro == false) {
            ShowModal(false);
            bootbox.dialog({
                message: "Dados gravados com sucesso!",
                title: "Sucesso",
                buttons: {
                    ok: {
                        label: "OK",
                        className: "btn-success",
                        callback: function () {
                            window.location.href = "../Associado/ListaAssociado";
                        }
                    }
                }
            });


        } else {
            ShowModal(false);
            MensagemErro("Não foi possível gravar os dados da pessoa.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}

function ExcluirAssociado(_AssociadoId) {
    bootbox.dialog({
        message: "Deseja realmente excluir esta Pessoa?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Associado/ExcluirAssociado", { AssociadoId: _AssociadoId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Passoa excluída com sucesso!");
                            ListarAssociado();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir a Pessoa.");
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

$(function () {

    $("#btnNovoAssociado").attr("style", "display: block;");
    $("#mnuAcoes").attr("style", "display: block;");
    $("#btnNovoAssociado").click(function () {
        window.location.href = "../Associado/CadAssociado?AssociadoId=0";
    });

    $("#btnSalvarAssociado").click(function () {
        GravarAssociado();
    });

    $("#btnAtualizarAssociado").click(function () {
        ListarAssociado();
    });

    ListarAssociado();
    
    var associadoId = $("#AssociadoId").val();
    if (associadoId != null) {
        ShowModal(true);
        //PT
        var idiomaId = 1;
        $.get("../Associado/CarregarAssociado", { AssociadoId: associadoId }, function (data) {
            if (data.Associado.AssociadoId != null) {
                PreencherCadastro(data);
                ShowModal(false);
            } else {
                ShowModal(false);
                MensagemErro("Erro ao carregar dados da pessoa.");
            }
        });
    }


});
