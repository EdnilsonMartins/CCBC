
var FormModel, FormModelOld;
var fluxo;

function PreencherCadastro(data) {

    $('[checked="checked"]').parent().removeClass("checked");

    $("#UsuarioId").val(data.Usuario.UsuarioId);
    $("#Nome").val(data.Usuario.Nome);
    $("#Email").val(data.Usuario.Email);
    $("#Login").val(data.Usuario.Login);
    $("#TedescoUsuario").val(data.Usuario.TedescoUsuario);
    $("#TedescoEmail").val(data.Usuario.TedescoEmail);

    var values = data.Usuario.ListaUsuarioGrupo;
    ListarUsuarioGrupo(values);

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

    var optFunc_295 = $("#optFunc_295").val();
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

            var status = "";
            if (item.Ativo == true) status = "Ativo";
            else if (item.Ativo == false) status = "Inativo";
            reg.push(status); //Status: Ativo | Inativo.

            var dTedescoUltimaNotificacao = FormatarDataJson(item.TedescoUltimaNotificacao);
            if (dTedescoUltimaNotificacao == null) {
                reg.push(null);
            } else {

                reg.push(moment(dTedescoUltimaNotificacao).format('DD/MM/YYYY HH:mm'));
            }

            var dTedescoDataConfirmacao = FormatarDataJson(item.TedescoDataConfirmacao);
            if (dTedescoDataConfirmacao == null) {
                reg.push(null);
            } else {
                reg.push(moment(dTedescoDataConfirmacao).format('DD/MM/YYYY HH:mm'));
            }

            reg.push(item.Complemento.TedescoStatus);

            var editar = "";
            var excluir = "";
            if (optFunc_300 != null) {
                editar = "<span title='Editar' class=\"icon-editar glyphicon glyphicon-wrench\" style=\"margin-right: 4px;\" onclick=\"javascript:EditarUsuario(" + item.UsuarioId + ")\"></span>";
            }
            if (optFunc_310 != null) {
                excluir = "<span title='Excluir' class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 4px;\" onclick=\"javascript:ExcluirUsuario(" + item.UsuarioId + ")\"></span>";
            }

            var notificar = "";
            if (optFunc_295 != null && (item.TedescoStatusId == null || item.TedescoStatusId != 2)) {
                notificar = "<span title='Enviar Notificação' class=\"glyphicon glyphicon-envelope icon-excluir\" style=\"margin-right: 0px;\" onclick=\"javascript:NotificarUsuario(" + item.UsuarioId + ")\"></span>";
            }

            reg.push(editar + " " + excluir + " " + notificar);
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
                { "sTitle": "ID", "width": "40px" },
                { "sTitle": "Nome" },
                { "sTitle": "Login" },
                { "sTitle": "Ativo", "width": "40px" },
                { "sTitle": "Notificação", "width": "94px" },
                { "sTitle": "Cadastro", "width": "94px" },
                { "sTitle": "Situação", "width": "94px" },
                { "sTitle": "", "width": "60px", "orderable": false }
            ]
        });
    });
}

function EditarUsuario(_UsuarioId) {
    window.location.href = AppPath + "Usuario/MinhaConta?UsuarioId=" + _UsuarioId;
}

function GravarUsuario() {
    ShowModal(true);
    var listaUsuarioGrupo = "";
    //if (fluxo != 1) {
        listaUsuarioGrupo = $('#tvUsuarioGrupo').jstree(true).get_selected();//$('#UsuarioGrupo').val();
    //}
    var indexGrupo = 0;
    var saidaGrupo = "";
    if (listaUsuarioGrupo != null) {
        while (indexGrupo < listaUsuarioGrupo.length) {
            if (indexGrupo > 0) saidaGrupo += ",";
            saidaGrupo += listaUsuarioGrupo[indexGrupo];
            indexGrupo++;
        }
    }
    console.log(saidaGrupo);

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

function ListarUsuarioGrupo(values) {
    var registros = [];
    $('#tvUsuarioGrupo').jstree({
        "core": { "check_callback": true, "cache": false },
        "plugins": ["search", "state", "checkbox"],
        "checkbox": {
            "keep_selected_style" : false
        },
        "types": {
            "default": {
                "check_node": false,
                "uncheck_node": true
            }
        }
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

        $('#tvUsuarioGrupo').jstree().deselect_all(true);
        $('#tvUsuarioGrupo').jstree(true).settings.core.data = registros;
        $('#tvUsuarioGrupo').jstree(true).refresh();

        SelecionarUsuarioGrupo(values);
    });

}

function SelecionarUsuarioGrupo(values) {
    var nodes = [];
    if (values != null) {
        $.each(values.split(","), function (i, e) {
            nodes.push(e);
        });
        console.log("Nodes selecionados: " + nodes);
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

function NotificarUsuario(_UsuarioId) {
    bootbox.dialog({
        message: "Deseja enviar uma notificação para o usuário realizar a atualização/conclusão do cadastro?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Usuario/NotificarUsuario", { UsuarioId: _UsuarioId }, function (data) {
                        if (data.Erro == false) {
                            MensagemSucesso("E-mail encaminhado para o usuário!");
                            ListarUsuario();
                        } else if (data.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao tentar enviar o e-mail para o usuário.");
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

    $("#Email").change(function () {
        if (fluxo == 1) {
            var tedescoEmail = $("#TedescoEmail").val();
            if (tedescoEmail == null || tedescoEmail == "") {
                $("#TedescoEmail").val($("#Email").val());
            }
        }
    });

    $("#btnNovoUsuario").attr("style", "display: block;");
    $("#btnNovoUsuarioWebFull").attr("style", "display: block;");

    $("#mnuAcoes").attr("style", "display: block;");
    $("#btnNovoUsuario").click(function () {
        window.location.href = "../Usuario/MinhaConta?UsuarioId=0";
    });
    $("#btnNovoUsuarioWebFull").click(function () {
        window.location.href = "../Usuario/MinhaConta?UsuarioId=0&Fluxo=1";
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
        $.get("../Usuario/CarregarUsuario", { UsuarioId: usuarioId }, function (data) {
            if (data.Usuario.UsuarioId != null) {
                $('[checked="checked"]').parent().addClass("checked");
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


    fluxo = $("#Fluxo").val();
    if (fluxo != null && fluxo == 1) {
        Mensagem("Um e-mail será enviado ao usuário após o preenchimento deste formulário.<br /><br />Os campos obrigatórios são:<br /><br />1) Aba <b>Geral</b><br />- Nome<br />- Email<br /><br />2) Aba <b>Externo</b><br/>- Login (WebFull)<br />- Email (WebFull)<br /><br />As instruções serão encaminhadas para o e-mail do futuro usuário com base no endereço registrado no campo <b>E-mail (WebFull)</b>.", "Intruções para Pré-Cadastro de Usuários");
    }

    

});