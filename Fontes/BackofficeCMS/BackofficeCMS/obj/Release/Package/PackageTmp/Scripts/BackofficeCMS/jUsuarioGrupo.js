
var FormModel, FormModelOld;

function LimparUsuario() {
    LimparForm();
}

function PreencherCadastro(data) {
    $("#UsuarioGrupoId").val(data.UsuarioGrupo.UsuarioGrupoId);
    $("#Nome").val(data.UsuarioGrupo.Nome);
}

function ListarUsuarioGrupo() {

    var registros = [];

    $('#tvUsuarioGrupoTodos').jstree({
        "core": { "check_callback": true },
        "plugins": ["search", "state"]
    });

    $.get("../Usuario/ListarUsuarioGrupo", { }, function (data) {
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

        $('#tvUsuarioGrupoTodos').jstree(true).settings.core.data = registros;
        $('#tvUsuarioGrupoTodos').jstree(true).refresh();
    });

}

function EditarUsuarioGrupo(_UsuarioGrupoId) {
    window.location.href = AppPath + "Usuario/CadUsuarioGrupo?UsuarioGrupoId=" + _UsuarioGrupoId;
}

function GravarUsuarioGrupo() {
    ShowModal(true);

    FormModel = SerializaForm("frmCadUsuarioGrupo");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.get("../Usuario/GravarUsuarioGrupo", { UsuarioGrupo: formdata, UsuarioGrupoOld: formDataOld }, function (data) {
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
                            window.location.href = "../Usuario/ListaGrupoUsuario";
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

function ExcluirUsuarioGrupo(_UsuarioGrupoId) {
    bootbox.dialog({
        message: "Deseja realmente excluir este grupo de usuários?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Usuario/ExcluirUsuarioGrupo", { UsuarioGrupoId: _UsuarioGrupoId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Grupo de Usuários excluído com sucesso!");
                            ListarUsuarioGrupo();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir o grupo de usuários.");
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

    $("#btnEditarUsuarioGrupo").click(function () {
        var x = $('#tvUsuarioGrupoTodos').jstree(true).get_selected();
        EditarUsuarioGrupo(parseInt(x));
    });

    $("#btnExcluirUsuarioGrupo").click(function () {
        var x = parseInt($('#tvUsuarioGrupoTodos').jstree(true).get_selected());
        ExcluirUsuarioGrupo(parseInt(x));
    });

    $("#btnNovoUsuarioGrupo").click(function () {
        var x = $('#tvUsuarioGrupoTodos').jstree(true).get_selected();
        window.location.href = "../Usuario/CadUsuarioGrupo?UsuarioGrupoId=0&UsuarioGrupoPaiId=" + x;
    });
    

    $("#btnSalvarUsuarioGrupo").click(function () {
        GravarUsuarioGrupo();
    });

    $("#btnAtualizarUsuarioGrupo").click(function () {
        ListarUsuarioGrupo();
    });

    var usuarioGrupoId = $("#UsuarioGrupoId").val();
    if (usuarioGrupoId != null) {
        ShowModal(true);
        $.get("../Usuario/CarregarUsuarioGrupo", { UsuarioGrupoId: usuarioGrupoId }, function (data) {
            if (data.UsuarioGrupo.UsuarioGrupoId != null) {
                PreencherCadastro(data);
                ShowModal(false);
            } else {
                ShowModal(false);
                MensagemErro("Erro ao carregar dados do usuário.");
            }
        });
    } else {
        ListarUsuarioGrupo();
    }

});