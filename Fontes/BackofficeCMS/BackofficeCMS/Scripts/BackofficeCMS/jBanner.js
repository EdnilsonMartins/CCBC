
var FormModel, FormModelOld;

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

function PreencherCadastro(data, IdiomaId) {

    $("#BannerId").val(data.Banner.BannerId);

    $('select[name=BannerTipo]').selectpicker('val', data.Banner.BannerTipoId);
    $('select[name=Target]').selectpicker('val', data.Banner.TargetId);

    var status = "";
    if (data.Banner.Ativo == true) status = "1";
    if (data.Banner.Ativo == false) status = "0";
    $('select[name=Status]').selectpicker('val', status);

    var valuesUser = data.Banner.Complemento.ListaUsuario;
    SelecionarUsuario(valuesUser);

    var values = data.Banner.Complemento.ListaUsuarioGrupo;
    SelecionarUsuarioGrupo(values);
    
    if (data.Banner.Data != null) {
        var data1 = FormatarDataJson(data.Banner.Data);
        $('#dtData').datepicker('setDate', data1);
    } else {
        $('#Data').val("");
    }
    if (data.Banner.DataValidade != null) {
        var data2 = FormatarDataJson(data.Banner.DataValidade);
        $('#dtDataValidade').datepicker('setDate', data2);
    } else {
        $('#DataValidade').val("");
    }

    $("#Referencia").val(data.Banner.Referencia);
    $("#Posicao").val(data.Banner.Posicao);
    $("#LinkURL").val(data.Banner.LinkURL);

    if (data.Banner.Complemento.Privado != null && data.Banner.Complemento.Privado) {
        $("[name='Privado']").filter("[value='1']").attr("checked", true);
    } else if (data.Banner.Complemento.Privado != null) {
        $("[name='Privado']").filter("[value='0']").attr("checked", true);
    } else {
        $("[name='Privado']").filter("[value='1']").attr("checked", false);
        $("[name='Privado']").filter("[value='0']").attr("checked", false);
    }
    VerificaExibicao();


    $('[checked="checked"]').parent().addClass("checked");

    if (IdiomaId == 1) {
        //PT
        $("#Titulo").val(data.Banner.Detalhe.Titulo);
        $("#Resumo").val(data.Banner.Detalhe.Resumo);
        $("#Descricao").val(data.Banner.Detalhe.Descricao);
    } else if (IdiomaId == 2) {
        //ES
        $("#TituloES").val(data.Banner.Detalhe.Titulo);
        $("#ResumoES").val(data.Banner.Detalhe.Resumo);
        $("#DescricaoES").val(data.Banner.Detalhe.Descricao);
    } else if (IdiomaId == 3) {
        //EN
        $("#TituloEN").val(data.Banner.Detalhe.Titulo);
        $("#ResumoEN").val(data.Banner.Detalhe.Resumo);
        $("#DescricaoEN").val(data.Banner.Detalhe.Descricao);
    } else if (IdiomaId == 4) {
        //FR
        $("#TituloFR").val(data.Banner.Detalhe.Titulo);
        $("#ResumoFR").val(data.Banner.Detalhe.Resumo);
        $("#DescricaoFR").val(data.Banner.Detalhe.Descricao);
    }

    $("#gridArquivos").attr("ownerid", data.Banner.BannerId);
    $("#gridArquivos").attr("arquivocategoriatipoid", 3); //3 = Banner!
    CarregarArquivos();
}

function ListarBanner() {

    var optFunc_120 = $("#optFunc_120").val();
    var optFunc_130 = $("#optFunc_130").val();

    var regs = [];
    var reg = [];

    $.get("../Banner/ListarBanner", {}, function (data) {
        $.each(data, function (i, item) {
            reg = new Array();
            reg.push(item.BannerId);
            reg.push(item.Complemento.BannerTipo);
            reg.push(item.Detalhe.Titulo);

            var d = FormatarDataJson(item.Data);

            if (d == null) {
                reg.push(null);
            } else {
                reg.push(moment(d).format('DD/MM/YYYY'));
            }

            reg.push(item.TotalVisualizacao);
            reg.push(item.TotalClique);

            var status = "";
            if (item.Ativo == true) status = "Ativo";
            else if (item.Ativo == false) status = "Inativo";
            reg.push(status); //Status: Ativo | Inativo.

            var editar = "";
            var excluir = "";
            if (optFunc_120 != null) {
                editar = "<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarBanner(" + item.BannerId + ")\"></span>";
            }
            if (optFunc_130 != null) {
                excluir = "<span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" title=\"Excluir\" onclick=\"javascript:ExcluirBanner(" + item.BannerId + ")\"></span>";
            }

            reg.push(editar + " " + excluir);
            regs.push(reg);

        });

        $.fn.dataTable.moment('DD/MM/YYYY');

        $('#tblBanner').dataTable({
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
                { "sTitle": "Tipo" },
                { "sTitle": "Título" },
                { "sTitle": "Data" },
                { "sTitle": "Visualizações" },
                { "sTitle": "Cliques" },
                { "sTitle": "Status", "width": "60px" },
                { "sTitle": "", "orderable": false }
            ]

        });

    });
}

function EditarBanner(_BannerId) {
    window.location.href = AppPath + "Banner/CadBanner?BannerId=" + _BannerId;
}

function GravarBanner() {
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

    FormModel = SerializaForm("frmCadBanner");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.post("../Banner/GravarBanner", { Banner: formdata, BannerOld: formDataOld, ListaUsuarioGrupo: saidaGrupo, ListaUsuario: saidaUsuario }, function (data) {
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
                            window.location.href = "../Banner/ListaBanner";
                        }
                    }
                }
            });


        } else {
            ShowModal(false);
            MensagemErro("Não foi possível gravar os dados do banner.<br /><br />" + data.Resposta.Mensagem);
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

function ExcluirBanner(_BannerId) {
    bootbox.dialog({
        message: "Deseja realmente excluir este Banner?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Banner/ExcluirBanner", { BannerId: _BannerId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Banner excluído com sucesso!");
                            ListarBanner();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir o Banner.");
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

    $("#btnNovoBanner").attr("style", "display: block;");
    $("#mnuAcoes").attr("style", "display: block;");
    $("#btnNovoBanner").click(function () {
        window.location.href = "../Banner/CadBanner?BannerId=0";
    });

    $("#btnSalvarBanner").click(function () {
        GravarBanner();
    });

    $("#btnAtualizarBanner").click(function () {
        ListarBanner();
    });

    ListarBanner();


    $('#dtData').datepicker({
        format: "dd/mm/yyyy",
        clearBtn: true,
        language: "pt-BR",
        autoclose: true
    });

    $('#dtDataValidade').datepicker({
        format: "dd/mm/yyyy",
        clearBtn: true,
        language: "pt-BR",
        autoclose: true
    });

    $('#tags_1').tagsInput({
        width: 'auto',
        'onAddTag': function () {

        },
    });

    $("[name=Privado]").click(function () {
        VerificaExibicao();
    });

    var bannerId = $("#BannerId").val();
    if (bannerId != null) {
        ShowModal(true);
        //PT
        var idiomaId = 1;
        $.get("../Banner/CarregarBanner", { BannerId: bannerId, IdiomaId: idiomaId }, function (data) {
            if (data.Banner.BannerId != null) {
                PreencherCadastro(data, idiomaId);
                //ES
                idiomaId = 2;
                $.get("../Banner/CarregarBanner", { BannerId: bannerId, IdiomaId: idiomaId }, function (data) {
                    if (data.Banner.BannerId != null) {
                        PreencherCadastro(data, idiomaId);
                        //EN
                        idiomaId = 3;
                        $.get("../Banner/CarregarBanner", { BannerId: bannerId, IdiomaId: idiomaId }, function (data) {
                            if (data.Banner.BannerId != null) {
                                PreencherCadastro(data, idiomaId);
                                //FR
                                idiomaId = 4;
                                $.get("../Banner/CarregarBanner", { BannerId: bannerId, IdiomaId: idiomaId }, function (data) {
                                    if (data.Banner.BannerId != null) {
                                        PreencherCadastro(data, idiomaId);
                                        FormModelOld = SerializaForm("frmCadBanner");
                                        ShowModal(false);
                                    } else {
                                        ShowModal(false);
                                        MensagemErro("Erro ao carregar dados do banner.");
                                    }
                                });
                            } else {
                                ShowModal(false);
                                MensagemErro("Erro ao carregar dados do banner.1");
                            }
                        });
                    } else {
                        ShowModal(false);
                        MensagemErro("Erro ao carregar dados do banner.2");
                    }
                });

            } else {
                ShowModal(false);
                MensagemErro("Erro ao carregar dados do banner.3");
            }
        });


        if (bannerId == "0") {
            $("[name='Privado']").filter("[value='0']").attr("checked", true);
            $('[checked="checked"]').parent().addClass("checked");
            VerificaExibicao();
        }


        ListarUsuarioGrupo();
        ListarUsuario();
    }
    

});
