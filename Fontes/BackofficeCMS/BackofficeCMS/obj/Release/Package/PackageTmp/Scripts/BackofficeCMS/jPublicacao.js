
var FormModel, FormModelOld;

function VerificaExibicao() {
    var privado = $('[name=Privado]:checked').val();
    console.log(privado);
    if (privado == "1") {
        console.log("privado");
        $("#dvRestricao").slideDown();
        //$("#tvUsuarioGrupo").removeAttr("disabled");
        //$('#tvUsuarioGrupo').multiSelect();
    } else {
        console.log("público");
        ListarUsuarioGrupo();
        ListarUsuario();
        $("#dvRestricao").slideUp();
        //$("#tvUsuarioGrupo").attr("disabled", "true");
        //$('#tvUsuarioGrupo').multiSelect();
    }
    //$('#tvUsuarioGrupo').multiSelect("refresh");
}

function VerificaExibicaoPublicacaoTipo() {
    var publicacaoTipo = $('[name=PublicacaoTipo]').val();
    console.log(publicacaoTipo);
    if (publicacaoTipo == "7" || publicacaoTipo == "8") {
        $("#dvLinkTarget").slideDown();

        $("#dvFonteEditoria").slideUp();
        $("#dvLinkAuto").slideUp();
        $("#dvResumo").slideUp();
        $("#dvConteudo").slideUp();
        $("#hConteudo").slideUp();
        $("#hDestaque").slideUp();
        $("#dvDestaque").slideUp();
    } else {
        $("#dvLinkTarget").slideUp();

        $("#dvFonteEditoria").slideDown();
        $("#dvLinkAuto").slideDown();
        $("#dvResumo").slideDown();
        $("#dvConteudo").slideDown();
        $("#hConteudo").slideDown();
        $("#hDestaque").slideDown();
        $("#dvDestaque").slideDown();
    }
}

function LimparCadastro() {
    LimparForm();
}

function MostrarCadastro() {
    $("#CadPublicacao").modal("show");
}

function PreencherCadastro(data, IdiomaId) {
    
    $("#PublicacaoId").val(data.Publicacao.PublicacaoId);

    $('select[name=PublicacaoTipo]').selectpicker('val', data.Publicacao.PublicacaoTipoId);
    $('select[name=Target]').selectpicker('val', data.Publicacao.TargetId);

    var status = "";
    if (data.Publicacao.Ativo == true) status = "1";
    if (data.Publicacao.Ativo == false) status = "0";
    $('select[name=Status]').selectpicker('val', status);

    var valuesUser = data.Publicacao.Complemento.ListaUsuario;
    SelecionarUsuario(valuesUser);

    var values = data.Publicacao.Complemento.ListaUsuarioGrupo;
    SelecionarUsuarioGrupo(values);
    //if (values != null) {
    //    $.each(values.split(","), function (i, e) {
    //        $("#UsuarioGrupo option[value='" + e + "']").prop("selected", true);
    //    });
    //    $('#UsuarioGrupo').multiSelect();
    //}

    if (data.Publicacao.Data != null) {
        var data1 = FormatarDataJson(data.Publicacao.Data);
        $('#dtData').datepicker('setDate', data1);
    } else {
        $('#Data').val("");
    }
    if (data.Publicacao.DataValidade != null) {
        var data2 = FormatarDataJson(data.Publicacao.DataValidade);
        $('#dtDataValidade').datepicker('setDate', data2);
    } else {
        $('#DataValidade').val("");
    }

    if (data.Publicacao.Tags != null && data.Publicacao.Tags != "") {
        $('#tags_1').importTags(data.Publicacao.Tags);
    }

    $("#Posicao").val(data.Publicacao.Posicao);
    $("#LinkURLRevista").val(data.Publicacao.LinkURL);

    if (data.Publicacao.Destaque != null && data.Publicacao.Destaque) {
        $("[name='ExibirHome']").filter("[value='1']").attr("checked", true);
    } else if (data.Publicacao.Destaque != null) {
        $("[name='ExibirHome']").filter("[value='0']").attr("checked", true);
    }

    if (data.Publicacao.Complemento.Privado != null && data.Publicacao.Complemento.Privado) {
        $("[name='Privado']").filter("[value='1']").attr("checked", true);
    } else if (data.Publicacao.Complemento.Privado != null) {
        $("[name='Privado']").filter("[value='0']").attr("checked", true);
    } else {
        $("[name='Privado']").filter("[value='1']").attr("checked", false);
        $("[name='Privado']").filter("[value='0']").attr("checked", false);
    }
    VerificaExibicao();
    VerificaExibicaoPublicacaoTipo();

    $('[checked="checked"]').parent().addClass("checked");

    

    if (IdiomaId == 1) {
        //PT
        $("#Titulo").val(data.Publicacao.Detalhe.Titulo);
        $("#Resumo").val(data.Publicacao.Detalhe.Resumo);
        $("#Editoria").val(data.Publicacao.Detalhe.Editora);
        $("#Fonte").val(data.Publicacao.Detalhe.Fonte);
        $("#PublicacaoConteudo").val(data.Publicacao.Detalhe.Conteudo);
        //Instanciar caso ainda nao esteja.
        if (CKEDITOR.instances.Conteudo == null) {
            CKEDITOR.replace('Conteudo', {
                extraPlugins: 'mediacenter',
                allowedContent: true,
                extraAllowedContent: 'link section article figure span ul li header nav aside[*]'
            });
        } 
        CKEDITOR.instances.Conteudo.setData(data.Publicacao.Detalhe.Conteudo, function () {
            this.checkDirty();
        });
        
        //Teste: CKEditor TimeStamp!
        //CKEDITOR.replace('editor1', {
        //    // Load the timestamp plugin.
        //    extraPlugins: 'timestamp'
        //    // Rearrange toolbar groups and remove unnecessary plugins.
        //});

        //Teste: CKEditor MediaCenter!
        //CKEDITOR.replace('editor1', {
        //    extraPlugins: 'mediacenter'
        //});

        //var editor = CKEDITOR.instances.editor1;
        //editor.insertHtml('Inserting image here: /File?Id=999');

        //CKEDITOR.replace('editor1', {
        //    extraPlugins: 'mediacenter'
        //});

    } else if (IdiomaId == 2) {
        //ES
        $("#TituloES").val(data.Publicacao.Detalhe.Titulo);
        $("#ResumoES").val(data.Publicacao.Detalhe.Resumo);
        $("#EditoriaES").val(data.Publicacao.Detalhe.Editora);
        $("#FonteES").val(data.Publicacao.Detalhe.Fonte);
        $("#PublicacaoConteudoES").val(data.Publicacao.Detalhe.Conteudo);
        //Instanciar caso ainda nao esteja.
        if (CKEDITOR.instances.ConteudoES == null) {
            CKEDITOR.replace('ConteudoES', {
                extraPlugins: 'mediacenter',
                allowedContent: true,
                extraAllowedContent: 'link section article figure span ul li header nav aside[*]'
            });
        } 
        CKEDITOR.instances.ConteudoES.setData(data.Publicacao.Detalhe.Conteudo, function () {
            this.checkDirty();
        });

    } else if (IdiomaId == 3) {
        //EN
        $("#TituloEN").val(data.Publicacao.Detalhe.Titulo);
        $("#ResumoEN").val(data.Publicacao.Detalhe.Resumo);
        $("#EditoriaEN").val(data.Publicacao.Detalhe.Editora);
        $("#FonteEN").val(data.Publicacao.Detalhe.Fonte);
        $("#PublicacaoConteudoEN").val(data.Publicacao.Detalhe.Conteudo);
        //Instanciar caso ainda nao esteja.
        if (CKEDITOR.instances.ConteudoEN == null) {
            CKEDITOR.replace('ConteudoEN', {
                extraPlugins: 'mediacenter',
                allowedContent: true,
                extraAllowedContent: 'link section article figure span ul li header nav aside[*]'
            });
        } 
        CKEDITOR.instances.ConteudoEN.setData(data.Publicacao.Detalhe.Conteudo, function () {
            this.checkDirty();
        });

    } else if (IdiomaId == 4) {
        //EN
        $("#TituloFR").val(data.Publicacao.Detalhe.Titulo);
        $("#ResumoFR").val(data.Publicacao.Detalhe.Resumo);
        $("#EditoriaFR").val(data.Publicacao.Detalhe.Editora);
        $("#FonteFR").val(data.Publicacao.Detalhe.Fonte);
        $("#PublicacaoConteudoFR").val(data.Publicacao.Detalhe.Conteudo);
        //Instanciar caso ainda nao esteja.
        if (CKEDITOR.instances.ConteudoFR == null) {
            CKEDITOR.replace('ConteudoFR', {
                extraPlugins: 'mediacenter',
                allowedContent: true,
                extraAllowedContent: 'link section article figure span ul li header nav aside[*]'
            });
        } 
        CKEDITOR.instances.ConteudoFR.setData(data.Publicacao.Detalhe.Conteudo, function () {
            this.checkDirty();
        });

    }

    if (data.Publicacao.Complemento.Liberado != null) {
        if (data.Publicacao.Complemento.Liberado == true) {
            $("#infoAprovado").attr("style", "display: block;");
            $("#infoAprovado").html("O conteúdo desta publicação foi liberado em <b>" + data.Publicacao.Complemento.Data + "</b> às <b>" + data.Publicacao.Complemento.Hora + "</b>.");

            $("#dvLiberacaoAcao").attr("style", "display: none");
            $("#dvLiberacaoInfo").html("Conteúdo APROVADO.")
        } else {
            $("#infoReprovado").attr("style", "display: block;");
            $("#infoReprovado").html("O conteúdo desta publicação foi reprovado em <b>" + data.Publicacao.Complemento.Data + "</b> às <b>" + data.Publicacao.Complemento.Hora + "</b>.");

            $("#dvLiberacaoAcao").attr("style", "display: none");
            $("#dvLiberacaoInfo").html("Conteúdo REPROVADO.")
        }
    } else {
        var publicacaoId = $("#PublicacaoId").val();
        $.get("../Publicacao/VerificarUsuarioElegivel", { PublicacaoId: publicacaoId }, function (data) {
            if (data != null) {
                var ultimaLiberacao = "";
                console.log("==" + data.Ok_Usuario);
                if (data.Data != null) {
                    ultimaLiberacao = "Sua resposta de liberação foi submetida<br />com sucesso em <b>" + data.Data + "</b> às <b>" + data.Hora + "</b>.";
                }

                if (data.Ok_Usuario != null && data.UsuarioElegivel != true) {
                    $("#dvLiberacaoInfo").html("Publicação em processo de liberação.<br />Aguardando a ação de outros usuários...<br/><br/>" + ultimaLiberacao);
                    $("#dvLiberacaoAcao").attr("style", "display: none;");
                } else {
                    if (data.Ok_Usuario == null) {
                        console.log("sem regra específica!");
                        $("#dvLiberacaoInfo").html("Não existem regras específicas para aprovar esta publicação.<br />Porém é necessário que um usuário efetue a liberação do conteúdo.");
                        $("#barPercentual").attr("style", "width: " + 0 + "%;");
                        $("#dvLiberacaoAcao").attr("style", "display: true;");
                    } else {
                        if (data.Liberado != null) {
                            $("#dvLiberacaoInfo").html("Publicação em processo de liberação.<br />Aguardando a ação de outros usuários...<br/><br/>" + ultimaLiberacao);
                            $("#dvLiberacaoAcao").attr("style", "display: none;");

                        } else {
                            $("#dvLiberacaoInfo").html("Você está apto a aprovar ou reprovar a publicação deste conteúdo, escolha uma opção e clique em confirmar.");
                            $("#dvLiberacaoAcao").attr("style", "display: block;");
                        }
                    }
                }
            }
        });
    }
    $("#dvLiberacaoInfo").attr("style", "display : visible;");

    $("#gridArquivos").attr("ownerid", data.Publicacao.PublicacaoId);
    $("#gridArquivos").attr("arquivocategoriatipoid", 1); //1 = Publicação!
    CarregarArquivos();

    GerarURLAmigavel();

    ListarEditoria(data.Publicacao.EditoriaId);

    
}

function CarregarPublicacao(publicacaoId) {
    ShowModal(true);
    //PT
    var idiomaId = 1;
    $.get("../Publicacao/CarregarPublicacao", { PublicacaoId: publicacaoId, IdiomaId: idiomaId }, function (data) {
        if (data.Publicacao.PublicacaoId != null) {
            PreencherCadastro(data, idiomaId);
            CarregarRegraPasso();
            CarregarPublicacaoAprovacaoHistorico();
            //ES
            idiomaId = 2;
            $.get("../Publicacao/CarregarPublicacao", { PublicacaoId: publicacaoId, IdiomaId: idiomaId }, function (data) {
                if (data.Publicacao.PublicacaoId != null) {
                    PreencherCadastro(data, idiomaId);
                    //EN
                    idiomaId = 3;
                    $.get("../Publicacao/CarregarPublicacao", { PublicacaoId: publicacaoId, IdiomaId: idiomaId }, function (data) {
                        if (data.Publicacao.PublicacaoId != null) {
                            PreencherCadastro(data, idiomaId);
                            //FR
                            idiomaId = 4;
                            $.get("../Publicacao/CarregarPublicacao", { PublicacaoId: publicacaoId, IdiomaId: idiomaId }, function (data) {
                                if (data.Publicacao.PublicacaoId != null) {
                                    PreencherCadastro(data, idiomaId);
                                    FormModelOld = SerializaForm("frmCadPublicacao");
                                    ShowModal(false);
                                } else {
                                    ShowModal(false);
                                    MensagemErro("Erro ao carregar dados da publicação.");
                                }
                            });
                        } else {
                            ShowModal(false);
                            MensagemErro("Erro ao carregar dados da publicação.");
                        }
                    });
                } else {
                    ShowModal(false);
                    MensagemErro("Erro ao carregar dados da publicação.");
                }
            });

        } else {
            ShowModal(false);
            MensagemErro("Erro ao carregar dados da publicação.");
        }
    });
}

function CarregarPublicacaoAprovacaoHistorico() {

    $("#listaPublicacaoAprovacaoHistorico").html("");

    var publicacaoId = $("#PublicacaoId").val();
    if (publicacaoId != null) {
        $.get("../Publicacao/ListarPublicacaoAprovacaoHistorico", { PublicacaoId: publicacaoId }, function (data) {
            if (data != null) {
                if (data.length == 0) {
                    $("#InfoHistorico").attr("style", "display: block;");
                } else {
                    $("#InfoHistorico").attr("style", "display: none;");
                }
                $.each(data, function (i, item) {
                    var ultimoNoLine = "";
                    var icone = "";
                    var cor = " timeline-grey";
                    console.log(i);
                    if (i+1 >= data.length) {
                        ultimoNoLine = " timeline-noline";
                        console.log(ultimoNoLine);
                    }
                    if (item.Liberado == true) {
                        icone = " fa-check";
                    } else if (item.Liberado == false) {
                        icone = " fa-stop";
                        cor = "timeline-red";

                    } else {
                        icone = " fa-question";
                    }
                    var hist = '<li class="' + cor + ultimoNoLine + '"><div class="timeline-time" style="padding: 10px 14px 0 0;"><span class="date">' + item.Data + '</span><span class="date">' + item.Hora + '</span></div><div class="timeline-icon"><i class="fa' + icone + '"></i></div><div class="timeline-body" style="margin: 0 0 15px 27%;"><div class="timeline-content"><h2 style="margin: 0; font-size: 22px;">' + item.NomeUsuario + '</h2>' + item.Descricao + '<br /></div></div></li>';
                    $("#listaPublicacaoAprovacaoHistorico").append(hist);
                });
                
            }
        });

    }

}

function CarregarRegraPasso() {

    $("#listaPassos").html("");

    var publicacaoId = $("#PublicacaoId").val();
    if (publicacaoId != null) {
        $.get("../Publicacao/CarregarPublicacaoRegraPasso", { PublicacaoId: publicacaoId }, function (data) {
            console.log(data.Regra.RegraPasso);
            if (data.Regra.RegraPasso != null) {
                $.each(data.Regra.RegraPasso, function (i, item) {
                    var icone = "";
                    if (item.Detalhes.Resultado == false) { icone = "fa-stop"; }
                    if (item.Detalhes.Resultado == true) { icone = "fa-check"; }
                    if (item.Detalhes.Resultado == null) { icone = "fa-question"; }
                    var passo = '<li id="Passo_' + item.Sequencia + '"><a href="#tab1" data-toggle="tab" class="step"><span id="Number_' + item.Sequencia + '" class="number">' + item.Sequencia + '</span><span id="Desc_' + item.Sequencia + '" class="desc"><i class="fa ' + icone + '"></i> ' + item.Descricao + '</span></a></li>';
                    $("#listaPassos").append(passo);
                });
                $('#tabAprovacao').bootstrapWizard({
                    'nextSelector': '.button-next',
                    'previousSelector': '.button-previous',
                    onTabClick: function (tab, navigation, index, clickedIndex) {
                        return false;
                        /*
                        success.hide();
                        error.hide();
                        if (form.valid() == false) {
                            return false;
                        }
                        handleTitle(tab, navigation, clickedIndex);
                        */
                    }
                });


                $("li", $("#tabAprovacao")).removeClass("active");
                var actived = false;
                var p = 0;
                var totalDone = 0;
                $.each(data.Regra.RegraPasso, function (i, item) {
                    if (item.Detalhes.Resultado == true) {
                        $("#Passo_" + item.Sequencia).addClass("done");
                        totalDone++;
                    }
                    if (item.Detalhes.Resultado == null && actived == false) {
                        actived = true;
                        $("#Passo_" + item.Sequencia).addClass("active");
                    }
                    if (item.Detalhes.Resultado == false) {
                        actived = true; 
                        $("#Number_" + item.Sequencia).attr("style", "background-color: red; color: white;");
                        $("#Desc_" + item.Sequencia).attr("style", "color: red;");
                    }
                    
                });
                p = (totalDone / data.Regra.RegraPasso.length) * 100;

                $("#barPercentual").attr("style", "width: " + p + "%;");

                if (data.Regra.RegraPasso.length == 0) {
                    $("#barPercentual").attr("style", "width: " + 100 + "%;");

                    $("#dvLiberacaoInfo").html("Não existem regras para aprovar esta publicação.");
                    $("#dvLiberacaoAcao").attr("style", "display: block;");
                }
                
            } 
        });
    }








}

function ListarPublicacao() {

    var optFunc_160 = $("#optFunc_160").val();
    var optFunc_170 = $("#optFunc_170").val();

    var regs = [];
    var reg = [];

    $.get("../Publicacao/ListarPublicacao", {}, function (data) {
        $.each(data, function (i, item) {
            reg = new Array();
            reg.push(item.PublicacaoId);
            reg.push(item.Complemento.PublicacaoTipo); //Tipo
            reg.push(item.Detalhe.Titulo);

            var destaque = "";
            if (item.Destaque == true) destaque = "Sim";
            if (item.Destaque == false) destaque = "Não";
            reg.push(destaque); //Destaque
            reg.push(item.Posicao); //Posição

            var d = FormatarDataJson(item.Data);

            if (d == null) {
                reg.push(null);
            } else {
                reg.push(moment(d).format('DD/MM/YYYY'));
            }

            var status = "";
            if (item.Ativo == true) status = "Ativo";
            else if (item.Ativo == false) status = "Inativo";
            reg.push(status); //Status: Ativo | Inativo.

            var liberado = ""
            if (item.Complemento.Liberado == true) liberado = "Aprovada";
            else if (item.Complemento.Liberado == false) liberado = "Reprovada";
            else if (item.Complemento.Liberado == null) liberado = "Aguardando...";
            reg.push(liberado); //Situação: Aguardando aprovação | Aprovada.

            var editar = "";
            var excluir = "";
            if (optFunc_160 != null) {
                editar = "<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarPublicacao(" + item.PublicacaoId + ")\"></span>";
            }
            if (optFunc_170 != null) {
                excluir = "<span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" title=\"Excluir\" onclick=\"javascript:ExcluirPublicacao(" + item.PublicacaoId + ")\"></span>";
            }

            reg.push(editar + " " + excluir);
            regs.push(reg);

        });

        $.fn.dataTable.moment('DD/MM/YYYY');

        $('#tblPublicacao').dataTable({
            //"bJQueryUI": true,
            "bDestroy": true,
            "sPaginationType": "full_numbers",
            "oLanguage": {
                "sUrl": "../Content/support/pt_BR.txt"
            },
            "aaData": regs
                ,
            "aoColumns": [
                { "sTitle": "ID", "width" : "80px" },
                { "sTitle": "Tipo" },
                { "sTitle": "Título" },
                { "sTitle": "Destaque", "width": "80px" },
                { "sTitle": "Posição", "width": "50px" },
                { "sTitle": "Data" },
                { "sTitle": "Status", "width": "60px" },
                { "sTitle": "Situação", "width": "60px" },
                { "sTitle": "", "orderable": false }
            ]

        });

    });
}

function EditarPublicacao(_PublicacaoId) {

    //Formulário Modal
    //$.get("../Publicacao/CarregarPublicacao", { PublicacaoId: _PublicacaoId }, function (data) {
    //    console.log(data);
    //    if (data.Publicacao.PublicacaoId != null) {
    //        PreencherCadastro(data);
    //        MostrarCadastro();
    //        FormModelOld = SerializaForm();
    //    } else {
    //        MensagemErro("Erro ao carregar dados da publicação.");
    //    }
    //});

    //Formulário Fixo (sem Modal)
    window.location.href = AppPath + "Publicacao/CadPublicacao?PublicacaoId=" + _PublicacaoId;

}

function GravarPublicacao(fechar) {
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

    var x = CKEDITOR.instances.Conteudo.getData();
    $("#PublicacaoConteudo").val(x);

    var xEN = CKEDITOR.instances.ConteudoEN.getData();
    $("#PublicacaoConteudoEN").val(xEN);

    var xES = CKEDITOR.instances.ConteudoES.getData();
    $("#PublicacaoConteudoES").val(xES);

    var xFR = CKEDITOR.instances.ConteudoFR.getData();
    $("#PublicacaoConteudoFR").val(xFR);

    FormModel = SerializaForm("frmCadPublicacao");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.post("../Publicacao/GravarPublicacao", { Publicacao: formdata, PublicacaoOld: formDataOld, ListaUsuarioGrupo: saidaGrupo, ListaUsuario: saidaUsuario }, function (data) {
        if (data.Resposta.Erro == false) {
            //console.log(data.Resposta);
            ShowModal(false);
            bootbox.dialog({
                message: "Dados gravados com sucesso!",
                title: "Sucesso",
                buttons: {
                    ok: {
                        label: "OK",
                        className: "btn-success",
                        callback: function () {
                            if (fechar == true) {
                                window.location.href = "../Publicacao/ListaPublicacao";
                            } else {
                                window.location.href = "../Publicacao/CadPublicacao?PublicacaoId=" + data.Publicacao.PublicacaoId;
                                
                            }
                        }
                    }
                }
            });
        } else {
            ShowModal(false);
            MensagemErro("Não foi possível gravar os dados da publicação.<br /><br />" + data.Resposta.Mensagem);
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
    
    //$('#UsuarioGrupo').html("");
    //$.get("../Usuario/ListarUsuarioGrupo", {}, function (data) {
    //    //console.log(">>>" + data);
    //    if (data != null) {
    //        $.each(data, function (i, item) {
    //            $('#UsuarioGrupo').append("<option value=\"" + item.UsuarioGrupoId + "\">" + item.Nome + "</option>");
    //        });
    //        $('#UsuarioGrupo').multiSelect();
    //        $('#UsuarioGrupo').multiSelect("refresh");
    //    }
    //});





        var registros = [];

        $('#tvUsuarioGrupo').jstree({
            "core": { "check_callback": true },
            "plugins": [ "search", "state", "checkbox"]
        });

        //$('#tvMenuSuperior').on("move_node.jstree", function (e, data) {
        //    if (emQ) return true;
        //    emQ = true;
        //    var _Posicao = parseInt(data.position);
        //    var _MenuId = parseInt(data.node.id);
        //    var MenuPaiId = data.parent;
        //    if (MenuPaiId == "#") MenuPaiId = 0;
        //    var _MenuPaiId = parseInt(MenuPaiId);
        //    bootbox.dialog({
        //        message: "Deseja realmente reposicionar este Menu?",
        //        title: "Confirmação",
        //        buttons: {
        //            sim: {
        //                label: "Sim",
        //                className: "btn-success",
        //                callback: function () {
        //                    $.get("../Menu/ReposicionarMenu", { MenuId: _MenuId, MenuPaiId: _MenuPaiId, Posicao: _Posicao }, function (data) {
        //                        emQ = false;
        //                        if (data.Resposta.Erro == false) {
        //                            MensagemSucesso("Menu atualizado com sucesso!");
        //                            ListarMenuSuperior();
        //                        } else if (data.Resposta.Mensagem != "") {
        //                            MensagemErro(data.Resposta.Mensagem);
        //                        }
        //                        else {
        //                            MensagemErro("Erro ao atualizar o menu.");
        //                        }
        //                    });
        //                }
        //            },
        //            nao: {
        //                label: "Não",
        //                className: "btn-danger",
        //                callback: function () {
        //                    emQ = false;
        //                    ListarMenuSuperior();
        //                }
        //            }
        //        }
        //    });
        //});

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

function ExcluirPublicacao(_PublicacaoId) {
    bootbox.dialog({
        message: "Deseja realmente excluir esta Publicação?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Publicacao/ExcluirPublicacao", { PublicacaoId: _PublicacaoId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Publicação excluída com sucesso!");
                            ListarPublicacao();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir a publicação.");
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

function LiberarPublicacao(_PublicacaoId) {

    var _LiberadoSelecionado = $("[name='Aprovado']:checked").val();
    var _Liberado = false;
    if (_LiberadoSelecionado == 1) _Liberado = true;
    //if ($(item).is(':checked')){

    bootbox.dialog({
        message: "Deseja realmente liberar esta Publicação?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Publicacao/LiberarPublicacao", { PublicacaoId: _PublicacaoId, Liberado: _Liberado }, function (data) {
                        if (data.Resposta.Erro == false) {
                            CarregarPublicacao(_PublicacaoId);
                            //MensagemSucesso("Sua avaliação para liberação da publicação foi submetida com sucesso!");
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao liberar a publicação.");
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
    var _publicacaoId = $("#PublicacaoId").val();
    var _titulo = $("#Titulo").val();
    var _publicacaoTipoId = $("#PublicacaoTipo").val();
    $.post("../Publicacao/GerarURLAmigavel", { PublicacaoTipoId: _publicacaoTipoId, PublicacaoId: _publicacaoId, Titulo: _titulo }, function (data) {
        if (data != null) {
            $("#LinkURL").text(data);
            $("#LinkURLEN").text(data + "/en-US");
            $("#LinkURLES").text(data + "/es-ES");
            $("#LinkURLFR").text(data + "/fr-CA");
        }
    });
}

$(function () {

    $("#btnCopiar").click(function () {
        //var _linkURL = $("#LinkURL").val();
        //if (window.clipboardData) { // Internet Explorer
        //    window.clipboardData.setData("Text", _linkURL);
        //} else {
        //    unsafeWindow.netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
        //    const clipboardHelper = Components.classes["@mozilla.org/widget/clipboardhelper;1"].getService(Components.interfaces.nsIClipboardHelper);
        //    clipboardHelper.copyString(_linkURL);
        //}
    });



    $("#Titulo").change(function () {
        GerarURLAmigavel();
    });

    $("#btnNovoPublicacao").attr("style", "display: block;");
    $("#mnuAcoes").attr("style", "display: block;");
    $("#btnNovoPublicacao").click(function () {
        //Formulário Modal
        //FormModelOld = null;
        //LimparCadastro();
        //MostrarCadastro();

        //Formulário Fixo (sem Modal)
        window.location.href = "../Publicacao/CadPublicacao?PublicacaoId=0";
    });

    $("#btnSalvarPublicacao").click(function () {
        GravarPublicacao();
    });

    $("#btnSalvarPublicacaoFechar").click(function () {
        GravarPublicacao(true);
    });

    $("#btnLiberar").click(function () {
        var _publicacaoId = $("#PublicacaoId").val();
        LiberarPublicacao(_publicacaoId);
    });

    $("#btnAtualizarPublicacao").click(function () {
        ListarPublicacao();
    });

    ListarPublicacao();


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

    $("[name=PublicacaoTipo]").change(function () {
        VerificaExibicaoPublicacaoTipo();
    });
    

    var publicacaoId = $("#PublicacaoId").val();
    console.log(publicacaoId);
    if (publicacaoId != null) {
        CarregarPublicacao(publicacaoId);
        if (publicacaoId == "0") {
            $("[name='Privado']").filter("[value='0']").attr("checked", true);
            $('[checked="checked"]').parent().addClass("checked");
            VerificaExibicao();

            $("#ulAbas li").attr("style", "display: none;");
            $("#liGeral").attr("style", "display: important!;");
        }
    } 
    ListarUsuarioGrupo();
    ListarUsuario();

});
