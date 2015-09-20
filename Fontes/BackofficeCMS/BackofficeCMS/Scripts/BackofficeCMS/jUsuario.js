
var FormModel, FormModelOld;

function PreencherCadastro(data) {

    $('[checked="checked"]').parent().removeClass("checked");

    $("#UsuarioId").val(data.Usuario.UsuarioId);
    $("#Nome").val(data.Usuario.Nome);
    $("#Email").val(data.Usuario.Email);
    $("#Login").val(data.Usuario.Login);
    $("#TedescoUsuario").val(data.Usuario.TedescoUsuario);
    $("#TedescoEmail").val(data.Usuario.TedescoEmail);

    var values = data.Usuario.ListaUsuarioGrupo;
    SelecionarUsuarioGrupo(values);

    $("[name='Ativo']").filter("[value='1']").attr("checked", false);
    $("[name='Ativo']").filter("[value='0']").attr("checked", false);
    if (data.Usuario.Ativo != null && data.Usuario.Ativo) {
        $("[name='Ativo']").filter("[value='1']").attr("checked", true);
    } else if (data.Usuario.Ativo != null) {
        $("[name='Ativo']").filter("[value='0']").attr("checked", true);
    }

    $.each(data.Usuario.Funcionalidades, function (i, e) {
        var FuncId = e.FuncionalidadeId;
        $("[name='optFunc_" + FuncId + "']").attr("checked", true);
    });

    



    
    $('[checked="checked"]').parent().addClass("checked");
}

function ListarUsuario() {

    var optFunc_300 = $("#optFunc_300").val();
    var optFunc_310 = $("#optFunc_310").val();

    var regs = [];
    var reg = [];
    $.get("../Usuario/ListarUsuario", {}, function (data) {
        $.each(data, function (i, item) {
            reg = new Array();
            reg.push(item.UsuarioId);
            reg.push(item.Nome);
            reg.push(item.Login);

            var editar = "";
            var excluir = "";
            if (optFunc_300 != null) {
                editar = "<span class=\"icon-editar glyphicon glyphicon-wrench\" onclick=\"javascript:EditarUsuario(" + item.UsuarioId + ")\"></span>";
            }
            if (optFunc_310 != null) {
                excluir = "<span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" onclick=\"javascript:ExcluirUsuario(" + item.UsuarioId + ")\"></span>";
            }

            reg.push(editar + " " + excluir);
            regs.push(reg);
        });


        //Nova Table
        var oTable = $('#todos').dataTable({
            //"bJQueryUI": true,
            "bDestroy": true,
            "sPaginationType": "full_numbers",
            "oLanguage": {
                //"sUrl": "../Content/support/pt_BR.txt"
                "sUrl": "../Content/support/pt_BR.txt"
            },
            "aaData": regs
                ,
            "aoColumns": [
                { "sTitle": "ID" },
                { "sTitle": "Nome" },
                { "sTitle": "Login" },
                { "sTitle": "" }
            ]
        });
    });
}

function EditarUsuario(_UsuarioId) {
    window.location.href = AppPath + "Usuario/MinhaConta?UsuarioId=" + _UsuarioId;
}

function GravarUsuario() {
    ShowModal(true);
    var listaUsuarioGrupo = $('#tvUsuarioGrupo').jstree(true).get_selected();//$('#UsuarioGrupo').val();
    var indexGrupo = 0;
    var saidaGrupo = "";
    if (listaUsuarioGrupo != null) {
        while (indexGrupo < listaUsuarioGrupo.length) {
            if (indexGrupo > 0) saidaGrupo += ",";
            saidaGrupo += listaUsuarioGrupo[indexGrupo];
            indexGrupo++;
        }
    }

    FormModel = SerializaForm("frmCadUsuario");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.post("../Usuario/GravarUsuario", { Usuario: formdata, UsuarioOld: formDataOld, ListaUsuarioGrupo: saidaGrupo }, function (data) {
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
                            window.location.href = "../Usuario/ListaUsuario";
                        }
                    }
                }
            });
        } else {
            ShowModal(false);
            MensagemErro("Não foi possível gravar os dados do usuário.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}

function ListarUsuarioGrupo() {

    var registros = [];

    $('#tvUsuarioGrupo').jstree({
        "core": { "check_callback": true },
        "plugins": ["search", "state", "checkbox"]
    });

    $.get("../Usuario/ListarUsuarioGrupo", {}, function (data) {
        $.each(data, function (i, item) {
            var _parent = item.UsuarioGrupoPaiId;
            if (_parent == null) _parent = "#";
            var regi = {
                id: item.UsuarioGrupoId,
                parent: _parent,
                text: item.Nome,
                icon: "fa fa-group icon-state-info jstree-drop",
                class: "jstree-drop"
            };
            registros.push(regi);
        });

        $('#tvUsuarioGrupo').jstree(true).settings.core.data = registros;
        $('#tvUsuarioGrupo').jstree(true).refresh();
    });
}

function SelecionarUsuarioGrupo(values) {
    var nodes = [];
    if (values != null) {
        $.each(values.split(","), function (i, e) {
            nodes.push(e);
        });
        $('#tvUsuarioGrupo').jstree(true).select_node(nodes);
    }
}


function ExcluirUsuario(_UsuarioId) {
    bootbox.dialog({
        message: "Deseja realmente excluir este usuário?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Usuario/ExcluirUsuario", { UsuarioId: _UsuarioId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Usuário excluído com sucesso!");
                            ListarUsuario();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir o usuário.");
                        }
                    });
                }
            },
            nao: {
                label: "Não",
                className: "btn-danger",
                callback: function () {
                    //Example.show("uh oh, look out!");
                }
            }
        }
    });
}

$(function () {

    $("#btnNovoUsuario").attr("style", "display: block;");
    $("#mnuAcoes").attr("style", "display: block;");
    $("#btnNovoUsuario").click(function () {
        window.location.href = "../Usuario/MinhaConta?UsuarioId=0";
    });

    $("#btnSalvarUsuario").click(function () {
        GravarUsuario();
    });

    $("#btnAtualizarUsuario").click(function () {
        ListarUsuario();
    });

    ListarUsuario();

    var usuarioId = $("#UsuarioId").val();

    if (usuarioId != null) {
        ShowModal(true);
        ListarUsuarioGrupo();
        $.get("../Usuario/CarregarUsuario", { UsuarioId: usuarioId }, function (data) {
            if (data.Usuario.UsuarioId != null) {
                if (data.Usuario.UsuarioId != 0) {
                    $("#Password").attr("disabled", true);
                } else {
                    $("[name='Ativo']").filter("[value='1']").attr("checked", true);
                    $('[checked="checked"]').parent().addClass("checked");
                }
                PreencherCadastro(data);
                ShowModal(false);
            } else {
                ShowModal(false);
                MensagemErro("Erro ao carregar dados do usuário.");
            }
        });
    } 


});