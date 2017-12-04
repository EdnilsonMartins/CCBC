
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

    if (IdiomaId == 1) {

        $("#PodcastId").val(data.Podcast.PodcastId);
        $("#PodcastPaiId").val(data.Podcast.PodcastPaiId);


        $('select[name=PodcastGrupo]').selectpicker('val', data.Podcast.PodcastGrupoId);
    
        //var status = "";
        //if (data.Banner.Ativo == true) status = "1";
        //if (data.Banner.Ativo == false) status = "0";
        //$('select[name=Status]').selectpicker('val', status);

        //var valuesUser = data.Banner.Complemento.ListaUsuario;
        //SelecionarUsuario(valuesUser);

        //var values = data.Banner.Complemento.ListaUsuarioGrupo;
        //SelecionarUsuarioGrupo(values);

        $("#Autor").val(data.Podcast.Autor);
        $("#DireitosAutorais").val(data.Podcast.DireitosAutorais);
        $("#ProprietarioNome").val(data.Podcast.ProprietarioNome);
        $("#ProprietarioEmail").val(data.Podcast.ProprietarioEmail);
        $("#Duracao").val(data.Podcast.Duracao);



    
        if (data.Podcast.DataPublicacao != null) {
            var data1 = FormatarDataJson(data.Podcast.DataPublicacao);
            $('#dtDataPublicacao').datepicker('setDate', data1);
        } else {
            $('#DataPublicacao').val("");
        }

        $("#LinkURL").val(data.Podcast.LinkURL);
        $("#LinkURLImagem").val(data.Podcast.LinkURLImagem);

    }
    //if (data.Banner.Complemento.Privado != null && data.Banner.Complemento.Privado) {
    //    $("[name='Privado']").filter("[value='1']").attr("checked", true);
    //} else if (data.Banner.Complemento.Privado != null) {
    //    $("[name='Privado']").filter("[value='0']").attr("checked", true);
    //} else {
    //    $("[name='Privado']").filter("[value='1']").attr("checked", false);
    //    $("[name='Privado']").filter("[value='0']").attr("checked", false);
    //}
    //VerificaExibicao();


    //$('[checked="checked"]').parent().addClass("checked");

    if (IdiomaId == 1) {
        //PT
        $("#Titulo").val(data.Podcast.Detalhe.Titulo);
        $("#SubTitulo").val(data.Podcast.Detalhe.SubTitulo);
        $("#TituloEpisodio").val(data.Podcast.Detalhe.TituloEpisodio);
        $("#Descricao").val(data.Podcast.Detalhe.Descricao);
    } else if (IdiomaId == 2) {
        //ES
        $("#TituloES").val(data.Podcast.Detalhe.Titulo);
        $("#SubTituloES").val(data.Podcast.Detalhe.SubTitulo);
        $("#TituloEpisodioES").val(data.Podcast.Detalhe.TituloEpisodio);
        $("#DescricaoES").val(data.Podcast.Detalhe.Descricao);
    } else if (IdiomaId == 3) {
        //EN
        $("#TituloEN").val(data.Podcast.Detalhe.Titulo);
        $("#SubTituloEN").val(data.Podcast.Detalhe.SubTitulo);
        $("#TituloEpisodioEN").val(data.Podcast.Detalhe.TituloEpisodio);
        $("#DescricaoEN").val(data.Podcast.Detalhe.Descricao);
    } else if (IdiomaId == 4) {
        //FR
        $("#TituloFR").val(data.Podcast.Detalhe.Titulo);
        $("#SubTituloFR").val(data.Podcast.Detalhe.SubTitulo);
        $("#TituloEpisodioFR").val(data.Podcast.Detalhe.TituloEpisodio);
        $("#DescricaoFR").val(data.Podcast.Detalhe.Descricao);
    }

    if (IdiomaId == 1) {
        $("#gridArquivos").attr("ownerid", data.Podcast.PodcastId);
        $("#gridArquivos").attr("arquivocategoriatipoid", 5); //5 = Podcast!
        CarregarArquivos();
    }

    GerarURLAmigavel();
}

function ListarPodcast() {

    var optFunc_120 = $("#optFunc_120").val();
    var optFunc_130 = $("#optFunc_130").val();

    var regs = [];
    var reg = [];

    var _tipo = $("#Tipo").val();

    $.get("../Podcast/ListarPodcast", { Tipo: _tipo}, function (data) {
        $.each(data, function (i, item) {
            reg = new Array();
            reg.push(item.PodcastId);
            reg.push(item.Complemento.PodcastGrupo);
            reg.push(item.Detalhe.Titulo);

            //reg.push(item.Detalhe.SubTitulo);
            //reg.push(item.Detalhe.TituloEpisodio);
            reg.push(item.Detalhe.Descricao);

            //var status = "";
            //if (item.Ativo == true) status = "Ativo";
            //else if (item.Ativo == false) status = "Inativo";
            //reg.push(status); //Status: Ativo | Inativo.

            var d = FormatarDataJson(item.DataPublicacao);

            if (d == null) {
                reg.push(null);
            } else {
                reg.push(moment(d).format('DD/MM/YYYY'));
            }


            var editar = "";
            var excluir = "";
            if (optFunc_120 != null) {
                editar = "<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarPodcast(" + item.PodcastId + ")\"></span>";
            }
            if (optFunc_130 != null) {
                excluir = "<span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" title=\"Excluir\" onclick=\"javascript:ExcluirPodcast(" + item.PodcastId + ")\"></span>";
            }

            reg.push(editar + " " + excluir);
            regs.push(reg);

        });

        $.fn.dataTable.moment('DD/MM/YYYY');

        var titulo = "Título";
        if (_tipo == 2) {
            titulo = "Episódio";
        }

        $('#tblPodcast').dataTable({
            //"bJQueryUI": true,
            "bDestroy": true,
            "sPaginationType": "full_numbers",
            "oLanguage": {
                "sUrl": "../Content/support/pt_BR.txt"
            },
            "aaData": regs
                ,
            "aoColumns": [
                { "sTitle": "ID", "width": "50px" },
                { "sTitle": "Categoria", "width": "130px" },
                { "sTitle": titulo, "width": "130px" },
                //{ "sTitle": "Subtítulo" },
                { "sTitle": "Descrição" },
                { "sTitle": "Data", "width": "80px" },
                { "sTitle": "", "orderable": false }
            ]

        });

    });
}

function EditarPodcast(_PodcastId) {
    var _tipo = $("#Tipo").val();
    console.log("Tipo: " + _tipo);
    window.location.href = AppPath + "Podcast/CadPodcast?PodcastId=" + _PodcastId + "&Tipo=" + _tipo;
}

function GravarPodcast() {
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

    FormModel = SerializaForm("frmCadPodcast");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.post("../Podcast/GravarPodcast", { Podcast: formdata, PodcastOld: formDataOld, ListaUsuarioGrupo: saidaGrupo, ListaUsuario: saidaUsuario }, function (data) {
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
                            var _tipo = $("#Tipo").val();

                            window.location.href = "../Podcast/ListaPodcast?Tipo=" + _tipo;
                        }
                    }
                }
            });


        } else {
            ShowModal(false);
            MensagemErro("Não foi possível gravar os dados do Podcast.<br /><br />" + data.Resposta.Mensagem);
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

function ExcluirPodcast(_PodcastId) {
    bootbox.dialog({
        message: "Deseja realmente excluir este Podcast?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Podcast/ExcluirPodcast", { PodcastId: _PodcastId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Podcast excluído com sucesso!");
                            ListarPodcast();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir o Podcast.");
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

function GerarURLAmigavel() {
    var _podcastId = $("#PodcastId").val();
    var _titulo = $("#Titulo").val();
    var _publicacaoTipoId = $("#PublicacaoTipo").val();
    $.post("../Podcast/GerarURLAmigavel", { PodcastId: _podcastId }, function (data) {
        if (data != null) {
            $("#LinkURL").val(data);
            //$("#LinkURLEN").text(data + "/en-US");
            //$("#LinkURLES").text(data + "/es-ES");
            //$("#LinkURLFR").text(data + "/fr-CA");
        }
    });
}

$(function () {

    $("#btnNovoPodcast").attr("style", "display: block;");
    $("#btnNovoPodcastEpisodio").attr("style", "display: block;");

    $("#mnuAcoes").attr("style", "display: block;");
    $("#btnNovoPodcast").click(function () {
        window.location.href = "../Podcast/CadPodcast?PodcastId=0&Tipo=1";
    });
    $("#btnNovoPodcastEpisodio").click(function () {
        window.location.href = "../Podcast/CadPodcast?PodcastId=0&Tipo=2";
    });

    $("#btnSalvarPodcast").click(function () {
        GravarPodcast();
    });

    $("#btnAtualizarPodcast").click(function () {
        ListarPodcast();
    });

    ListarPodcast();


    $('#dtDataPublicacao').datepicker({
        format: "dd/mm/yyyy",
        clearBtn: true,
        language: "pt-BR",
        autoclose: true
    });

    //$("[name=Privado]").click(function () {
    //    VerificaExibicao();
    //});

    var podcastId = $("#PodcastId").val();

    if (podcastId == null || podcastId == 0) {
        $("#btnNovoArquivo").attr("disabled", true);
    }

    if (podcastId != null) {
        ShowModal(true);
        //PT
        var idiomaId = 1;
        $.get("../Podcast/CarregarPodcast", { PodcastId: podcastId, IdiomaId: idiomaId }, function (data) {
            if (data.Podcast.PodcastId != null) {
                PreencherCadastro(data, idiomaId);
                //ES
                idiomaId = 2;
                $.get("../Podcast/CarregarPodcast", { PodcastId: podcastId, IdiomaId: idiomaId }, function (data) {
                    if (data.Podcast.PodcastId != null) {
                        PreencherCadastro(data, idiomaId);
                        //EN
                        idiomaId = 3;
                        $.get("../Podcast/CarregarPodcast", { PodcastId: podcastId, IdiomaId: idiomaId }, function (data) {
                            if (data.Podcast.PodcastId != null) {
                                PreencherCadastro(data, idiomaId);
                                //FR
                                idiomaId = 4;
                                $.get("../Podcast/CarregarPodcast", { PodcastId: podcastId, IdiomaId: idiomaId }, function (data) {
                                    if (data.Podcast.PodcastId != null) {
                                        PreencherCadastro(data, idiomaId);
                                        FormModelOld = SerializaForm("frmCadPodcast");
                                        ShowModal(false);
                                    } else {
                                        ShowModal(false);
                                        MensagemErro("Erro ao carregar dados do podcast.");
                                    }
                                });
                            } else {
                                ShowModal(false);
                                MensagemErro("Erro ao carregar dados do podcast.1");
                            }
                        });
                    } else {
                        ShowModal(false);
                        MensagemErro("Erro ao carregar dados do podcast.2");
                    }
                });

            } else {
                ShowModal(false);
                MensagemErro("Erro ao carregar dados do podcast.3");
            }
        });


        if (podcastId == "0") {
            $("[name='Privado']").filter("[value='0']").attr("checked", true);
            $('[checked="checked"]').parent().addClass("checked");
            VerificaExibicao();
        }


        ListarUsuarioGrupo();
        ListarUsuario();
    }


});
