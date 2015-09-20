
var FormModel, FormModelOld;

function PreencherCadastro(data, IdiomaId) {

    $("#SiteId").val(data.Site.SiteId);
    $("#Descricao").val(data.Site.Descricao);

    if (data.Site.Tags != null && data.Site.Tags != "") {
        $('#tags_1').importTags(data.Site.Tags);
    }
    $("#HostBase").val(data.Site.Configuracao.HostBase);

    $("#TelefonePrincipal").val(data.Site.Configuracao.ContatoTelefonePrincipal);
    $("#EmailPrincipal").val(data.Site.Configuracao.ContatoEmailPrincipal);

    $("#Localizacao").val(data.Site.Configuracao.Localizacao);
    $("#LocalizacaoComplemento").val(data.Site.Configuracao.LocalizacaoComplemento);
    $("#LinkMapa").val(data.Site.Configuracao.LinkMapa);

    $("#Host").val(data.Site.Configuracao.EmailHost);
    $("#DisplayName").val(data.Site.Configuracao.EmailDisplayName);
    $("#Username").val(data.Site.Configuracao.EmailUsername);
    $("#Password").val(data.Site.Configuracao.EmailPassword);
    $("#EmailDestino").val(data.Site.Configuracao.EmailDestino);
    $("#EmailPorta").val(data.Site.Configuracao.EmailPorta);


}

function ListarSite() {

    var optFunc_52 = $("#optFunc_52").val();
    var optFunc_53 = $("#optFunc_53").val();

    var regs = [];
    var reg = [];

    $.get("../Site/ListarSite", {}, function (data) {
        
        $.each(data, function (i, item) {

            console.log(item);

            reg = new Array();
            reg.push(item.SiteId);
            reg.push(item.Descricao);

            var editar = "";
            var excluir = "";
            if (optFunc_52 != null) {
                editar = "<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarSite(" + item.SiteId + ")\"></span>";
            }
            if (optFunc_53 != null) {
                excluir = "<span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" title=\"Excluir\" onclick=\"javascript:ExcluirSite(" + item.SiteId + ")\"></span>";
            }

            reg.push(editar + " " + excluir);
            regs.push(reg);

        });

        $.fn.dataTable.moment('DD/MM/YYYY');

        $('#tblSite').dataTable({
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
                { "sTitle": "Descrição" },
                { "sTitle": "", "orderable": false }
            ]

        });

    });
}

function EditarSite(_SiteId) {
    window.location.href = AppPath + "Site/CadSite?SiteId=" + _SiteId;
}

function GravarSite() {
    ShowModal(true);
    
    FormModel = SerializaForm("frmCadSite");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.post("../Site/GravarSite", { Site: formdata, SiteOld: formDataOld }, function (data) {
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
                            window.location.href = "../Site/ListaSite";
                        }
                    }
                }
            });


        } else {
            ShowModal(false);
            MensagemErro("Não foi possível gravar os dados do site.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}

function ExcluirSite(_SiteId) {
    bootbox.dialog({
        message: "Deseja realmente excluir este Site?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Site/ExcluirSite", { SiteId: _SiteId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Site excluído com sucesso!");
                            ListarSite();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir o Site.");
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

    $("#btnNovoSite").attr("style", "display: block;");
    $("#mnuAcoes").attr("style", "display: block;");
    $("#btnNovoSite").click(function () {
        window.location.href = "../Site/CadSite?SiteId=0";
    });

    $("#btnSalvarSite").click(function () {
        GravarSite();
    });

    $("#btnAtualizarSite").click(function () {
        ListarSite();
    });

    $('#tags_1').tagsInput({
        width: 'auto',
        'onAddTag': function () {

        },
    });

    ListarSite();

    var siteId = $("#SiteId").val();
    if (siteId != null) {
        ShowModal(true);
        //PT
        var idiomaId = 1;
        $.get("../Site/CarregarSite", { SiteId: siteId }, function (data) {
            if (data.Site.SiteId != null) {
                PreencherCadastro(data, idiomaId);
                //ES
                idiomaId = 2;
                $.get("../Site/CarregarSite", { SiteId: siteId }, function (data) {
                    if (data.Site.SiteId != null) {
                        PreencherCadastro(data, idiomaId);
                        //EN
                        idiomaId = 3;
                        $.get("../Site/CarregarSite", { SiteId: siteId, IdiomaId: idiomaId }, function (data) {
                            if (data.Site.SiteId != null) {
                                PreencherCadastro(data, idiomaId);
                                //FR
                                idiomaId = 4;
                                $.get("../Site/CarregarSite", { SiteId: siteId, IdiomaId: idiomaId }, function (data) {
                                    if (data.Site.SiteId != null) {
                                        PreencherCadastro(data, idiomaId);
                                        FormModelOld = SerializaForm("frmCadSite");
                                        ShowModal(false);
                                    } else {
                                        ShowModal(false);
                                        MensagemErro("Erro ao carregar dados do site.");
                                    }
                                });
                            } else {
                                ShowModal(false);
                                MensagemErro("Erro ao carregar dados do site.1");
                            }
                        });
                    } else {
                        ShowModal(false);
                        MensagemErro("Erro ao carregar dados do site.2");
                    }
                });

            } else {
                ShowModal(false);
                MensagemErro("Erro ao carregar dados do site.3");
            }
        });




    }


});
