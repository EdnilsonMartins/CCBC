
var FormModel, FormModelOld;
var emQ = false;

function VerificaExibicao() {
    var privado = $('[name=Privado]:checked').val();
    console.log(privado);
    if (privado == "1") {
        console.log("privado");
        $("#dvRestricao").slideDown();
    } else {
        console.log("público");
        ListarUsuarioGrupo();
        ListarUsuario();
        $("#dvRestricao").slideUp();
    }
    $('#UsuarioGrupo').multiSelect("refresh");
}

function ListarMenuSuperior() {

    var registros = [];

    $('#tvMenuSuperior').jstree({
        "core": { "check_callback": true },
        "plugins": ["dnd", "search", "state"]
    });

    $('#tvMenuSuperior').on("move_node.jstree", function (e, data) {
        if (emQ) return true;
        emQ = true;
        var _Posicao = parseInt(data.position);
        var _MenuId = parseInt(data.node.id);
        var MenuPaiId = data.parent;
        if (MenuPaiId == "#") MenuPaiId = 0;
        var _MenuPaiId = parseInt(MenuPaiId);

        bootbox.dialog({
            message: "Deseja realmente reposicionar este Menu?",
            title: "Confirmação",
            buttons: {
                sim: {
                    label: "Sim",
                    className: "btn-success",
                    callback: function () {
                        $.get("../Menu/ReposicionarMenu", { MenuId: _MenuId, MenuPaiId: _MenuPaiId, Posicao: _Posicao }, function (data) {
                            emQ = false;
                            if (data.Resposta.Erro == false) {
                                MensagemSucesso("Menu atualizado com sucesso!");
                                ListarMenuSuperior();
                            } else if (data.Resposta.Mensagem != "") {
                                MensagemErro(data.Resposta.Mensagem);
                            }
                            else {
                                MensagemErro("Erro ao atualizar o menu.");
                            }
                        });
                    }
                },
                nao: {
                    label: "Não",
                    className: "btn-danger",
                    callback: function () {
                        emQ = false;
                        ListarMenuSuperior();
                    }
                }
            }
        });
    });

    $.get("../Menu/ListarMenu", { MenuTipoId: 1 }, function (data) {
        $.each(data, function (i, item) {
            var _parent = item.MenuPaiId;
            if (_parent == null) _parent = "#";
            var regi = {
                id: item.MenuId,
                parent: _parent,
                text: item.Rotulo.replace(' />', '/>').replace('<br/>', '').replace('<BR/>', '').replace('<Br/>', '').replace('<bR/>', ''),
                icon: "fa fa-file icon-state-info jstree-drop",
                class: "jstree-drop"
            };
            registros.push(regi);
        });

        $('#tvMenuSuperior').jstree(true).settings.core.data = registros;
        $('#tvMenuSuperior').jstree(true).refresh();
    });

}

function ListarMenuQuick() {

    var registros = [];

    $('#tvMenuQuick').jstree({
        "core": { "check_callback": true },
        "plugins": ["dnd", "search", "state"]
    });

    $('#tvMenuQuick').on("move_node.jstree", function (e, data) {
        if (emQ) return true;
        emQ = true;
        var _Posicao = parseInt(data.position);
        var _MenuId = parseInt(data.node.id);
        var MenuPaiId = data.parent;
        if (MenuPaiId == "#") MenuPaiId = 0;
        //MenuPaiId = 0;
        var _MenuPaiId = parseInt(MenuPaiId);

        bootbox.dialog({
            message: "Deseja realmente reposicionar este Menu?",
            title: "Confirmação",
            buttons: {
                sim: {
                    label: "Sim",
                    className: "btn-success",
                    callback: function () {
                        $.get("../Menu/ReposicionarMenu", { MenuId: _MenuId, MenuPaiId: _MenuPaiId, Posicao: _Posicao }, function (data) {
                            emQ = false;
                            if (data.Resposta.Erro == false) {
                                MensagemSucesso("Menu atualizado com sucesso!");
                                ListarMenuQuick();
                            } else if (data.Resposta.Mensagem != "") {
                                MensagemErro(data.Resposta.Mensagem);
                            }
                            else {
                                MensagemErro("Erro ao atualizar o menu.");
                            }
                        });
                    }
                },
                nao: {
                    label: "Não",
                    className: "btn-danger",
                    callback: function () {
                        emQ = false;
                        ListarMenuQuick();
                    }
                }
            }
        });
    });

    $.get("../Menu/ListarMenu", { MenuTipoId: 2 }, function (data) {
        $.each(data, function (i, item) {
            var _parent = item.MenuPaiId;
            if (_parent == null) _parent = "#";
            var regi = {
                id: item.MenuId,
                parent: _parent,
                text: item.Rotulo.replace(' />', '/>').replace('<br/>', '').replace('<BR/>', '').replace('<Br/>', '').replace('<bR/>', ''),
                icon: "fa fa-file icon-state-info jstree-drop",
                class: "jstree-drop"
            };
            registros.push(regi);
        });

        $('#tvMenuQuick').jstree(true).settings.core.data = registros;
        $('#tvMenuQuick').jstree(true).refresh();
    });

}

function ListarMenuInferior() {

    var registros = [];

    $('#tvMenuInferior').jstree({
        "core": { "check_callback": true },
        "plugins": ["dnd", "search", "state"]
    });

    $('#tvMenuInferior').on("move_node.jstree", function (e, data) {
        if (emQ) return true;
        emQ = true;
        var _Posicao = parseInt(data.position);
        var _MenuId = parseInt(data.node.id);
        var MenuPaiId = data.parent;
        if (MenuPaiId == "#") MenuPaiId = 0;
        var _MenuPaiId = parseInt(MenuPaiId);

        bootbox.dialog({
            message: "Deseja realmente reposicionar este Menu?",
            title: "Confirmação",
            buttons: {
                sim: {
                    label: "Sim",
                    className: "btn-success",
                    callback: function () {
                        $.get("../Menu/ReposicionarMenu", { MenuId: _MenuId, MenuPaiId: _MenuPaiId, Posicao: _Posicao }, function (data) {
                            emQ = false;
                            if (data.Resposta.Erro == false) {
                                MensagemSucesso("Menu atualizado com sucesso!");
                                ListarMenuInferior();
                            } else if (data.Resposta.Mensagem != "") {
                                MensagemErro(data.Resposta.Mensagem);
                            }
                            else {
                                MensagemErro("Erro ao atualizar o menu.");
                            }
                        });
                    }
                },
                nao: {
                    label: "Não",
                    className: "btn-danger",
                    callback: function () {
                        emQ = false;
                        ListarMenuInferior();
                    }
                }
            }
        });
    });

    $.get("../Menu/ListarMenu", { MenuTipoId: 3 }, function (data) {
        $.each(data, function (i, item) {
            var _parent = item.MenuPaiId;
            if (_parent == null) _parent = "#";
            var regi = {
                id: item.MenuId,
                parent: _parent,
                text: item.Rotulo.replace(' />', '/>').replace('<br/>', '').replace('<BR/>', '').replace('<Br/>', '').replace('<bR/>', ''),
                icon: "fa fa-file icon-state-info jstree-drop",
                class: "jstree-drop"
            };
            registros.push(regi);
        });

        $('#tvMenuInferior').jstree(true).settings.core.data = registros;
        $('#tvMenuInferior').jstree(true).refresh();
    });

}

function PreencherCadastro(data, IdiomaId) {

    $("#MenuId").val(data.Menu.MenuId);
    $("#MenuPaiId").val(data.Menu.MenuPaiId);
    $('select[name=MenuTipo]').selectpicker('val', data.Menu.MenuTipoId);
    $('#MenuTipo').prop('disabled', true);

    $('select[name=Target]').selectpicker('val', data.Menu.TargetId);
    $('select[name=MenuTipoAcao]').selectpicker('val', data.Menu.MenuTipoAcaoId);

    var valuesUser = data.Menu.Complemento.ListaUsuario;
    SelecionarUsuario(valuesUser);

    var values = data.Menu.Complemento.ListaUsuarioGrupo;
    SelecionarUsuarioGrupo(values);

    $("#LinkURL").val(data.Menu.LinkURL);

    if (data.Menu.Complemento.Privado != null && data.Menu.Complemento.Privado) {
        $("[name='Privado']").filter("[value='1']").attr("checked", true);
    } else if (data.Menu.Complemento.Privado != null) {
        $("[name='Privado']").filter("[value='0']").attr("checked", true);
    } else {
        $("[name='Privado']").filter("[value='1']").attr("checked", false);
        $("[name='Privado']").filter("[value='0']").attr("checked", false);
    }
    VerificaExibicao();


    $('[checked="checked"]').parent().addClass("checked");

    if (IdiomaId == 1) {
        //PT
        $("#Rotulo").val(data.Menu.Detalhe.Rotulo);

    } else if (IdiomaId == 2) {
        //ES
        $("#RotuloES").val(data.Menu.Detalhe.Rotulo);

    } else if (IdiomaId == 3) {
        //EN
        $("#RotuloEN").val(data.Menu.Detalhe.Rotulo);

    } else if (IdiomaId == 4) {
        //FR
        $("#RotuloFR").val(data.Menu.Detalhe.Rotulo);

    }

    $("#gridArquivos").attr("ownerid", data.Menu.MenuId);
    $("#gridArquivos").attr("arquivocategoriatipoid", 2); //2 = Menu!
    CarregarArquivos();
}

function EditarMenu(_MenuId) {
    window.location.href = AppPath + "Menu/CadMenu?MenuId=" + _MenuId;
}

function GravarMenu() {
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

    var listaUsuario = $('#tvUsuario').jstree(true).get_selected();
    var indexUsuario = 0;
    var saidaUsuario = "";
    if (listaUsuario != null) {
        while (indexUsuario < listaUsuario.length) {
            if (indexUsuario > 0) saidaUsuario += ",";
            saidaUsuario += listaUsuario[indexUsuario];
            indexUsuario++;
        }
    }

    FormModel = SerializaForm("frmCadMenu");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.post("../Menu/GravarMenu", { Menu: formdata, MenuOld: formDataOld, ListaUsuarioGrupo: saidaGrupo, ListaUsuario: saidaUsuario }, function (data) {
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
                            window.location.href = "../Menu/ListaMenu";
                        }
                    }
                }
            });


        } else {
            ShowModal(false);
            MensagemErro("Não foi possível gravar os dados do menu.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}

function ListarUsuario() {
    var registros = [];

    $('#tvUsuario').jstree({
        "core": { "check_callback": true },
        "plugins": ["search", "state", "checkbox"]
    });

    $.get("../Usuario/ListarUsuario", {}, function (data) {
        $.each(data, function (i, item) {
            var _parent = null;
            if (_parent == null) _parent = "#";
            var regi = {
                id: item.UsuarioId,
                parent: _parent,
                text: item.Nome,
                icon: "fa fa-user icon-state-info jstree-drop",
                class: "jstree-drop"
            };
            registros.push(regi);
        });

        $('#tvUsuario').jstree(true).settings.core.data = registros;
        $('#tvUsuario').jstree(true).refresh();
    });
}

function SelecionarUsuario(values) {
    var nodes = [];
    if (values != null) {
        $.each(values.split(","), function (i, e) {
            nodes.push(e);
        });
        $('#tvUsuario').jstree(true).select_node(nodes);
    }
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


function ExcluirMenu(_MenuId) {
    bootbox.dialog({
        message: "Deseja realmente excluir este Menu?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Menu/ExcluirMenu", { MenuId: _MenuId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Menu excluído com sucesso!");
                            ListarMenuSuperior();
                            ListarMenuQuick();
                            ListarMenuInferior();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir o menu.");
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

function MoverMenu(_MenuId, _MenuPaiId, _Posicao) {
    bootbox.dialog({
        message: "Deseja realmente reposicionar este Menu?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Menu/ReposicionarMenu", { MenuId: _MenuId, MenuPaiId: _MenuPaiId, Posicao: _Posicao }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Menu atualizado com sucesso!");
                            ListarMenuSuperior();
                            ListarMenuQuick();
                            ListarMenuInferior();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao atualizar o menu.");
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

    $("#MenuTipoAcao").change(function () {

        var _menuTipoAcao = $("#MenuTipoAcao").val();

        if (_menuTipoAcao == 1) {
            //Página Fixa.
            $("#dvPaginaFixa").attr("style", "display: block;");
            $("#dvLink").attr("style", "display: none;");
            $("#LinkURL").attr("disabled", true);
        } else if (_menuTipoAcao == 2) {
            //Link.
            $("#dvPaginaFixa").attr("style", "display: none;");
            $("#dvLink").attr("style", "display: block;");
            $("#LinkURL").attr("disabled", false);
        }
    });

    $("#PaginaFixa").change(function () {
        var _paginaFixa = $("#PaginaFixa").val();

        if (_paginaFixa == 1) {
            $("#LinkURL").val("Eventos");
        } else  if (_paginaFixa == 2) {
            $("#LinkURL").val("Noticias");
        } else if (_paginaFixa == 3) {
            $("#LinkURL").val("Contato");
        } else if (_paginaFixa == 4) {
            $("#LinkURL").val("BuscaSocio");
        }
    });

    // Superior
    $("#btnNovoMenuSuperior").click(function () {
        var x = $('#tvMenuSuperior').jstree(true).get_selected();
        window.location.href = "../Menu/CadMenu?MenuId=0&MenuPaiId=" + x + "&MenuTipoId=1";
    });

    $("#btnEditarMenuSuperior").click(function () {
        var x = $('#tvMenuSuperior').jstree(true).get_selected();
        EditarMenu(parseInt(x));
    });

    $("#btnExcluirMenuSuperior").click(function () {
        var x = parseInt($('#tvMenuSuperior').jstree(true).get_selected());
        ExcluirMenu(parseInt(x));
    });

    $("#btnAtualizarMenuSuperior").click(function () {
        ListarMenuSuperior();
    });

    // Quick
    $("#btnNovoMenuQuick").click(function () {
        var x = $('#tvMenuQuick').jstree(true).get_selected();
        window.location.href = "../Menu/CadMenu?MenuId=0&MenuPaiId=" + x + "&MenuTipoId=2";
    });

    $("#btnEditarMenuQuick").click(function () {
        var x = $('#tvMenuQuick').jstree(true).get_selected();
        EditarMenu(parseInt(x));
    });

    $("#btnExcluirMenuQuick").click(function () {
        var x = parseInt($('#tvMenuQuick').jstree(true).get_selected());
        ExcluirMenu(parseInt(x));
    });

    $("#btnAtualizarMenuQuick").click(function () {
        ListarMenuQuick();
    });

    //Inferior
    $("#btnNovoMenuInferior").click(function () {
        var x = $('#tvMenuInferior').jstree(true).get_selected();
        window.location.href = "../Menu/CadMenu?MenuId=0&MenuPaiId=" + x + "&MenuTipoId=3";
    });

    $("#btnEditarMenuInferior").click(function () {
        var x = $('#tvMenuInferior').jstree(true).get_selected();
        EditarMenu(parseInt(x));
    });

    $("#btnExcluirMenuInferior").click(function () {
        var x = parseInt($('#tvMenuInferior').jstree(true).get_selected());
        ExcluirMenu(parseInt(x));
    });

    $("#btnAtualizarMenuInferior").click(function () {
        ListarMenuInferior();
    });



    $("#btnSalvarMenu").click(function () {
        GravarMenu();
    });

    $("[name=Privado]").click(function () {
        VerificaExibicao();
    });

    var menuId = $("#MenuId").val();
    var menuPaiId = $("#MenuPaiId").val();
    var menuTipoId = $("#MenuTipoId").val();
    if (menuTipoId != 0 ) {
        $("#MenuTipo").val(menuTipoId);
        $('#MenuTipo').prop('disabled', true);
    }

    if (menuId != null && menuId != "0" && menuId != 0) {
        ShowModal(true);
        //PT
        var idiomaId = 1;
        $.get("../Menu/CarregarMenu", { MenuId: menuId, IdiomaId: idiomaId }, function (data) {
            if (data.Menu.MenuId != null) {
                PreencherCadastro(data, idiomaId);
                
                //ES
                idiomaId = 2;
                $.get("../Menu/CarregarMenu", { MenuId: menuId, IdiomaId: idiomaId }, function (data) {
                    if (data.Menu.MenuId != null) {
                        PreencherCadastro(data, idiomaId);
                
                        //EN
                        idiomaId = 3;
                        $.get("../Menu/CarregarMenu", { MenuId: menuId, IdiomaId: idiomaId }, function (data) {
                            if (data.Menu.MenuId != null) {
                                PreencherCadastro(data, idiomaId);
                                
                                //FR
                                idiomaId = 4;
                                $.get("../Menu/CarregarMenu", { MenuId: menuId, IdiomaId: idiomaId }, function (data) {
                                    if (data.Menu.MenuId != null) {
                                        PreencherCadastro(data, idiomaId);
                                        FormModelOld = SerializaForm("frmCadMenu");
                                        ShowModal(false);
                                    } else {
                                        ShowModal(false);
                                        MensagemErro("Erro ao carregar dados do menu.");
                                    }
                                });
                            } else {
                                ShowModal(false);
                                MensagemErro("Erro ao carregar dados do menu.");
                            }
                        });
                    } else {
                        ShowModal(false);
                        MensagemErro("Erro ao carregar dados do menu.");
                    }
                });

            } else {
                ShowModal(false);
                MensagemErro("Erro ao carregar dados do menu.");
            }
        });

        ListarUsuarioGrupo();
        ListarUsuario();

    } else {

        if (menuId == null) {
            ListarMenuSuperior();
            ListarMenuQuick();
            ListarMenuInferior();
        }
        
        $("[name='Privado']").filter("[value='0']").attr("checked", true);
        $('[checked="checked"]').parent().addClass("checked");
        VerificaExibicao();
        
    }

});

