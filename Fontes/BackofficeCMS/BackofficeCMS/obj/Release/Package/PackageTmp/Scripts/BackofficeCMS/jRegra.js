
var FormModel, FormModelOld;
var FormModelRegraPasso, FormModelRegraPassoOld;

function LimparCamposRegraPasso() {
    $("#Sequencia", "#frmRegraPasso").val("");
    $("#Descricao", "#frmRegraPasso").val("");
    $("#QtdAprovacao", "#frmRegraPasso").val("");
}

function LimparCamposRegraPassoCondicao() {
    $("#UsuarioGrupo", "#frmRegraPassoCondicao").selectpicker('val', "");
    $('#UsuarioGrupo', "#frmRegraPassoCondicao").selectpicker('refresh');

    $("#Usuario", "#frmRegraPassoCondicao").selectpicker('val', "");
    $('#Usuario', "#frmRegraPassoCondicao").selectpicker('refresh');

    $("#QtdAprovacao", "#frmRegraPassoCondicao").val("");

}

function PreencherCadastro(data, IdiomaId) {
    $("#RegraId").val(data.Regra.RegraId);
    $("#Descricao").val(data.Regra.Descricao);


    $('select[name=RegraTipo]').selectpicker('val', data.Regra.RegraTipoId);


    ListarRegraPasso();
    ListarRegraPassoCondicao();
}

function PreencherCadastroPublicacaoTipoRegra(data) {
    

    $('select[name=PublicacaoTipoRegra]').selectpicker('val', data.PublicacaoTipoRegra.PublicacaoTipoRegraId);
    //$('select[name=Regra]').selectpicker('val', data.PublicacaoTipoRegra.RegraId);
    console.log("carregar..." + data.PublicacaoTipoRegra.RegraId);

    ListarRegraCombo(data.PublicacaoTipoRegra.RegraId);
   

    if (data.PublicacaoTipoRegra.Privado != null && data.PublicacaoTipoRegra.Privado) {
        $("[name='Privado']").filter("[value='1']").attr("checked", true);
    } else if (data.PublicacaoTipoRegra.Privado != null) {
        $("[name='Privado']").filter("[value='0']").attr("checked", true);
    } else {
        $("[name='Privado']").filter("[value='1']").attr("checked", false);
        $("[name='Privado']").filter("[value='0']").attr("checked", false);
    }

    $('[checked="checked"]').parent().addClass("checked");

}



function ListarRegra() {

    var regs = [];
    var reg = [];

    $.get("../Config/ListarRegra", {}, function (data) {
        $.each(data, function (i, item) {
            
            reg = new Array();
            
            reg.push(item.RegraId);
            reg.push(item.Descricao);
            reg.push(item.RegraTipo.Descricao);
            
            var d = FormatarDataJson(item.DataCadastro);

            if (d == null) {
                reg.push(null);
            } else {
                reg.push(moment(d).format('DD/MM/YYYY'));
            }

            reg.push("<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarRegra(" + item.RegraId + ")\"></span> <span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" title=\"Excluir\" onclick=\"javascript:ExcluirRegra(" + item.RegraId + ")\"></span> ");
            regs.push(reg);

        });

        $.fn.dataTable.moment('DD/MM/YYYY');

        $('#tblRegra').dataTable({
            //"bJQueryUI": true,
            "bDestroy": true,
            "sPaginationType": "full_numbers",
            "oLanguage": {
                "sUrl": "../Content/support/pt_BR.txt"
            },
            "aaData": regs
                ,
            "aoColumns": [
                { "sTitle": "ID" },
                { "sTitle": "Regra" },
                { "sTitle": "Tipo" },
                { "sTitle": "Data" },
                { "sTitle": "", "orderable": false }
            ]

        });

        
    });
}

function ListarRegraPasso() {

    var regs = [];
    var reg = [];

    var regraId = $("#RegraId").val();

    $.get("../Config/ListarRegraPasso", { RegraId: regraId }, function (data) {
        $.each(data, function (i, item) {

            reg = new Array();

            reg.push(item.RegraPassoId);
            reg.push(item.Sequencia);
            reg.push(item.Descricao);
            reg.push(item.QuantidadeMinimaUsuariosDoGrupo);
            reg.push("<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarRegraPasso(" + item.RegraPassoId + ")\"></span> <span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" title=\"Excluir\" onclick=\"javascript:ExcluirRegraPasso(" + item.RegraPassoId + ")\"></span> ");

            regs.push(reg);

        });

        //$.fn.dataTable.moment('DD/MM/YYYY');

        oTable = $('#tblRegraPasso').dataTable({
            //"bJQueryUI": true,
            paging: false,
            searching: false,
            "bDestroy": true,
            "sPaginationType": "full_numbers",
            "oLanguage": {
                "sUrl": "../Content/support/pt_BR_Interna.txt"
            },
            "aaData": regs
                ,
            "aoColumns": [
                { "sTitle": "ID" },
                { "sTitle": "Sequência" },
                { "sTitle": "Descrição" },
                { "sTitle": "Qtd Mínima de Aprovações" },
                { "sTitle": "", "orderable": false }
            ]

        });

        oTable.$('tr').attr("style", "cursor: pointer;");
        oTable.$('tr').click(function () {
            var regraPassoId = oTable.fnGetData(this)[0];
            $("#RegraPassoId").val(regraPassoId);
            ListarRegraPassoCondicao();
        });

    });
}

function ListarRegraPassoCondicao() {

    var regs = [];
    var reg = [];

    var regraPassoId = $("#RegraPassoId").val();

    $.get("../Config/ListarRegraPassoCondicao", { RegraPassoId: regraPassoId }, function (data) {
        $.each(data, function (i, item) {

            reg = new Array();

            reg.push(item.RegraPassoCondicaoId);
            reg.push(item.Detalhes.UsuarioGrupo);
            reg.push(item.Detalhes.Usuario);
            reg.push(item.QuantidadeMinimaUsuarios);

            reg.push("<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarRegraPassoCondicao(" + item.RegraPassoCondicaoId + ")\"></span> <span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" title=\"Excluir\" onclick=\"javascript:ExcluirRegraPassoCondicao(" + item.RegraPassoCondicaoId + ")\"></span> ");
            regs.push(reg);

        });

        $.fn.dataTable.moment('DD/MM/YYYY');

        $('#tblRegraPassoCondicao').dataTable({
            //"bJQueryUI": true,
            paging: false,
            searching: false,
            "bDestroy": true,
            "sPaginationType": "full_numbers",
            "oLanguage": {
                "sUrl": "../Content/support/pt_BR_Interna.txt"
            },
            "aaData": regs
                ,
            "aoColumns": [
                { "sTitle": "ID" },
                { "sTitle": "Grupo" },
                { "sTitle": "Usuário" },
                { "sTitle": "Qtd Mínima de Aprovações" },
                { "sTitle": "", "orderable": false }
            ]

        });

    });
}

function ListarPublicacaoTipoRegra() {

    var regs = [];
    var reg = [];

    $.get("../Config/ListarPublicacaoTipoRegra", {}, function (data) {
        $.each(data, function (i, item) {

            reg = new Array();

            reg.push(item.PublicacaoTipoRegraId);
            reg.push(item.Detalhes.PublicacaoTipo);
            reg.push(item.Detalhes.Regra);


            var privado = "Público";
            if (item.Privado == true) {
                privado = "Privado";
            }


            reg.push(privado);

            reg.push("<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarPublicacaoTipoRegra(" + item.PublicacaoTipoRegraId + ")\"></span> <span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" title=\"Excluir\" onclick=\"javascript:ExcluirPublicacaoTipoRegra(" + item.PublicacaoTipoRegraId + ")\"></span> ");
            regs.push(reg);

        });

        $('#tblPublicacaoTipoRegra').dataTable({
            //"bJQueryUI": true,
            "bDestroy": true,
            "sPaginationType": "full_numbers",
            "oLanguage": {
                "sUrl": "../Content/support/pt_BR.txt"
            },
            "aaData": regs
                ,
            "aoColumns": [
                { "sTitle": "ID" },
                { "sTitle": "Tipo de Publicação", "width": "140px" },
                { "sTitle": "Regra" },
                { "sTitle": "Privado/Público" },
                { "sTitle": "", "orderable": false }
            ]

        });


    });
}

function ListarUsuarioGrupo() {

    //$('#UsuarioGrupo').html("");
    $.get("../Usuario/ListarUsuarioGrupo", {}, function (data) {
        if (data != null) {
            $("#UsuarioGrupo", "#frmRegraPassoCondicao").append("<option value=\"\"></option>");
            $.each(data, function (i, item) {
                $("#UsuarioGrupo", "#frmRegraPassoCondicao").append("<option value=\"" + item.UsuarioGrupoId + "\">" + item.Nome + "</option>");
            });

           //$('#Regra', "#frmCadPublicacaoTipoRegra").selectpicker();
           //$("#Regra", "#frmCadPublicacaoTipoRegra").selectpicker('val', "");
           //$('#Regra', "#frmCadPublicacaoTipoRegra").selectpicker('refresh');
        }
    });

    
}

function ListarUsuario() {

    //$('#UsuarioGrupo').html("");
    $.get("../Usuario/ListarUsuario", {}, function (data) {
        if (data != null) {
            $("#Usuario", "#frmRegraPassoCondicao").append("<option value=\"\"></option>");
            $.each(data, function (i, item) {
                $("#Usuario", "#frmRegraPassoCondicao").append("<option value=\"" + item.UsuarioId + "\">" + item.Nome + "</option>");
            });
        }
    });
}

function ListarRegraCombo(valor) {
    
    
    
    
    $.get("../Config/ListarRegra", {}, function (data) {
        if (data != null) {
            $('#Regra', "#frmCadPublicacaoTipoRegra").html("");
            $("#Regra", "#frmCadPublicacaoTipoRegra").append("<option value=\"\"></option>");
            $.each(data, function (i, item) {
                $("#Regra", "#frmCadPublicacaoTipoRegra").append("<option value=\"" + item.RegraId + "\">" + item.Descricao + "</option>");
            });
            if (valor != null)
                $("#Regra", "#frmCadPublicacaoTipoRegra").val(valor);
        }
    });

    //$("#Regra", "#frmCadPublicacaoTipoRegra").selectpicker();
    //$('select').selectpicker();
    
}


function EditarRegra(_RegraId) {
    window.location.href = AppPath + "Config/CadRegra?RegraId=" + _RegraId;
}

function EditarRegraPasso(_RegraPassoId) {
    $("#RegraPassoId").val(_RegraPassoId);
    var regraId = $("#RegraId").val();

    $.get("../Config/CarregarRegraPasso", { RegraPassoId: _RegraPassoId, RegraId: regraId }, function (data) {
        console.log(data);
        if (data.RegraPasso.RegraPassoId != null) {
            $("#Sequencia", "#frmRegraPasso").val(data.RegraPasso.Sequencia);
            $("#Descricao", "#frmRegraPasso").val(data.RegraPasso.Descricao);
            $("#QtdAprovacao", "#frmRegraPasso").val(data.RegraPasso.QuantidadeMinimaUsuariosDoGrupo);

        } else {
            MensagemErro("Erro ao carregar dados do Passo da Regra.");
        }
    });

    $("#CadRegraPasso").modal("show");
}

function EditarRegraPassoCondicao(_RegraPassoCondicaoId) {
    $("#RegraPassoCondicaoId").val(_RegraPassoCondicaoId);
    var regraPassoId = $("#RegraPassoId").val();

    $.get("../Config/CarregarRegraPassoCondicao", { RegraPassoCondicaoId: _RegraPassoCondicaoId, RegraPassoId: regraPassoId }, function (data) {
        console.log(data);
        if (data.RegraPassoCondicao.RegraPassoCondicaoId != null) {
            $("#UsuarioGrupo", "#frmRegraPassoCondicao").selectpicker('val', data.RegraPassoCondicao.UsuarioGrupoId);
            $('#UsuarioGrupo', "#frmRegraPassoCondicao").selectpicker('refresh');
            
            $("#Usuario", "#frmRegraPassoCondicao").selectpicker('val', data.RegraPassoCondicao.UsuarioId);
            $('#Usuario', "#frmRegraPassoCondicao").selectpicker('refresh');

            $("#QtdAprovacao", "#frmRegraPassoCondicao").val(data.RegraPassoCondicao.QuantidadeMinimaUsuarios);

        } else {
            MensagemErro("Erro ao carregar dados da Condição do Passo da Regra.");
        }
    });

    $("#CadRegraPassoCondicao").modal("show");
}

function EditarPublicacaoTipoRegra(_PublicacaoTipoRegraId) {
    window.location.href = AppPath + "Config/CadPublicacaoTipoRegra?PublicacaoTipoRegraId=" + _PublicacaoTipoRegraId;
}



function GravarRegra() {
    ShowModal(true);

    FormModel = SerializaForm("frmCadRegra");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.post("../Config/GravarRegra", { Regra: formdata, RegraOld: formDataOld }, function (data) {
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
                            window.location.href = "../Config/ListaRegra";
                        }
                    }
                }
            });


        } else {
            ShowModal(false);

            MensagemErro("Não foi possível gravar os dados da regra.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}

function GravarRegraPasso() {
    ShowModal(true);

    FormModelRegraPasso = SerializaForm("frmRegraPasso");
    var formdata = JSON.stringify(FormModelRegraPasso);
    var formDataOld = JSON.stringify(FormModelRegraPassoOld);

    regraPassoId = $("#RegraPassoId").val();
    regraId = $("#RegraId").val();

    $.post("../Config/GravarRegraPasso", { RegraPassoId: regraPassoId, RegraId: regraId, Regra: formdata, RegraOld: formDataOld }, function (data) {
        if (data.Resposta.Erro == false) {
            $("#CadRegraPasso").modal("hide");
            $("#RegraPassoId").val("0");
            ListarRegraPasso();
            ListarRegraPassoCondicao();
            ShowModal(false);

            bootbox.dialog({
                message: "Dados gravados com sucesso!",
                title: "Sucesso",
                buttons: {
                    ok: {
                        label: "OK",
                        className: "btn-success",
                        callback: function () {
                            
                        }
                    }
                }
            });


        } else {
            ShowModal(false);

            MensagemErro("Não foi possível gravar os dados do Passo.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}

function GravarRegraPassoCondicao() {
    ShowModal(true);

    FormModel = SerializaForm("frmRegraPassoCondicao");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    var regraPassoCondicaoId = $("#RegraPassoCondicaoId").val();
    var regraPassoId = $("#RegraPassoId").val();
    var usuarioGrupoId = "" + $("#UsuarioGrupo").val() + "";
    var usuarioId = "" + $("#Usuario").val() + "";

    $.post("../Config/GravarRegraPassoCondicao", { RegraPassoCondicaoId: regraPassoCondicaoId, RegraPassoId: regraPassoId, UsuarioGrupoId: usuarioGrupoId, UsuarioId: usuarioId, Regra: formdata, RegraOld: formDataOld }, function (data) {
        if (data.Resposta.Erro == false) {
            $("#CadRegraPassoCondicao").modal("hide");
            $("#RegraPassoCondicaoId").val("0");
            ListarRegraPassoCondicao();
            ShowModal(false);

            bootbox.dialog({
                message: "Dados gravados com sucesso!",
                title: "Sucesso",
                buttons: {
                    ok: {
                        label: "OK",
                        className: "btn-success",
                        callback: function () {
                            
                        }
                    }
                }
            });


        } else {
            ShowModal(false);

            MensagemErro("Não foi possível gravar os dados da Condição.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}

function GravarPublicacaoTipoRegra() {
    ShowModal(true);

    FormModel = SerializaForm("frmCadPublicacaoTipoRegra");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.post("../Config/GravarPublicacaoTipoRegra", { Regra: formdata, RegraOld: formDataOld }, function (data) {
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
                            window.location.href = "../Config/ListaPublicacaoTipoRegra";
                        }
                    }
                }
            });


        } else {
            ShowModal(false);

            MensagemErro("Não foi possível gravar a associção de regra para este tipo de publicação.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}



function ExcluirRegra(_RegraId) {
    bootbox.dialog({
        message: "Deseja realmente excluir esta Regra?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Config/ExcluirRegra", { RegraId: _RegraId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Regra excluída com sucesso!");
                            ListarRegra();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir a Regra.");
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

function ExcluirRegraPasso(_RegraPassoId) {
    bootbox.dialog({
        message: "Deseja realmente excluir este Passo?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Config/ExcluirRegraPasso", { RegraPassoId: _RegraPassoId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Passo excluído com sucesso!");
                            $("#RegraPassoId").val("0");
                            ListarRegraPasso();
                            ListarRegraPassoCondicao();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir este Passo.");
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

function ExcluirRegraPassoCondicao(_RegraPassoCondicaoId) {
    bootbox.dialog({
        message: "Deseja realmente excluir esta Condição?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Config/ExcluirRegraPassoCondicao", { RegraPassoCondicaoId: _RegraPassoCondicaoId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Condição excluída com sucesso!");
                            ListarRegraPassoCondicao();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir esta Condição.");
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

function ExcluirPublicacaoTipoRegra(_PublicacaoTipoRegraId) {
    bootbox.dialog({
        message: "Deseja realmente excluir esta Associação de Regra?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Config/ExcluirPublicacaoTipoRegra", { PublicacaoTipoRegraId: _PublicacaoTipoRegraId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Associção removida com sucesso!");
                            ListarPublicacaoTipoRegra();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao remover a Associção.");
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
    
    $("#btnNovoRegra").attr("style", "display: block;");
    $("#btnNovoPublicacaoTipoRegra").attr("style", "display: block;");
    
    $("#mnuAcoes").attr("style", "display: block;");
    $("#btnNovoRegra").click(function () {
        window.location.href = "../Config/CadRegra?RegraId=0";
    });

    $("#btnSalvarRegra").click(function () {
        GravarRegra();
    });

    $("#btnAtualizarRegra").click(function () {
        ListarRegra();
    });

    $("#btnNovoRegraPasso").click(function () {
        $("#RegraPassoId").val("0");
        ListarRegraPasso();
        ListarRegraPassoCondicao();

        LimparCamposRegraPasso();
        $("#CadRegraPasso").modal("show");
    });

    $("#btnNovoRegraPassoCondicao").click(function () {
        $("#RegraPassoCondicaoId").val("0");
        ListarRegraPassoCondicao();

        LimparCamposRegraPassoCondicao();
        $("#CadRegraPassoCondicao").modal("show");
    });

    $("#btnNovoPublicacaoTipoRegra").click(function () {
        window.location.href = "../Config/CadPublicacaoTipoRegra?PublicacaoTipoRegraId=0";
    });

    $("#btnSalvarRegraPasso").click(function () {
        GravarRegraPasso();
    });
    
    $("#btnSalvarRegraPassoCondicao").click(function () {
        GravarRegraPassoCondicao();
    });

    $("#btnSalvarPublicacaoTipoRegra").click(function () {
        GravarPublicacaoTipoRegra();
    });

    ListarRegra();
    ListarPublicacaoTipoRegra();

    $('#tags_1').tagsInput({
        width: 'auto',
        'onAddTag': function () {

        },
    });

    var regraId = $("#RegraId").val();
    if (regraId != null) {
        ShowModal(true);

        var idiomaId = 1;
        $.get("../Config/CarregarRegra", { RegraId: regraId }, function (data) {
            console.log(data);
            if (data.Regra.RegraId != null) {
                PreencherCadastro(data, idiomaId);
                ShowModal(false);

            } else {
                ShowModal(false);

                MensagemErro("Erro ao carregar dados da regra.");
            }
        });

        ListarUsuarioGrupo();
        ListarUsuario();
    }

    var publicacaoTipoRegraId = $("#PublicacaoTipoRegraId").val();
    if (publicacaoTipoRegraId != null) {
        ShowModal(true);

        $.get("../Config/CarregarPublicacaoTipoRegra", { PublicacaoTipoRegraId: publicacaoTipoRegraId }, function (data) {
            console.log(data);
            if (data.PublicacaoTipoRegra.RegraId != null) {
                PreencherCadastroPublicacaoTipoRegra(data);
                ShowModal(false);
            } else {
                ShowModal(false);

                MensagemErro("Erro ao carregar dados da associação.");
            }
        });

        ListarRegraCombo();
    }


    

    $(document).ready(function () {
        
        //$("#Regra", "#frmCadPublicacaoTipoRegra").selectpicker('val', "");
        //$('#Regra', "#frmCadPublicacaoTipoRegra").selectpicker('refresh');

    });
});
