
var FormModel, FormModelOld;

function PreencherCadastro(data) {
    
    $("EmailTemplateId").val(data.EmailTemplate.EmailTemplateId);

    $('select[name=EmailGrupoTemplate]').selectpicker('val', data.EmailTemplate.EmailGrupoTemplateId);

    $("#Comentario").val(data.EmailTemplate.Comentario);
    $("#Assunto").val(data.EmailTemplate.Assunto);
    $("#Corpo").val(data.EmailTemplate.Corpo);

    $('[checked="checked"]').parent().addClass("checked");
    
}

function ListarEmailTemplate() {

    var optFunc_430 = $("#optFunc_430").val();

    var regs = [];
    var reg = [];

    $.get("../EmailTemplate/ListarEmailTemplate", {}, function (data) {
        $.each(data, function (i, item) {
            reg = new Array();
            reg.push(item.EmailTemplateId);
            reg.push(item.Comentario);

            var editar = "";
            if (optFunc_430 != null) {
                editar = "<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarEmailTemplate(" + item.EmailTemplateId + ")\"></span>";
            }

            reg.push(editar);
            regs.push(reg);

        });

        $.fn.dataTable.moment('DD/MM/YYYY');

        $('#tblEmailTemplate').dataTable({
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
                { "sTitle": "Comentario" },
                { "sTitle": "", "orderable": false }
            ]

        });

    });
}

function EditarEmailTemplate(_EmailTemplateId) {
    window.location.href = AppPath + "EmailTemplate/CadEmailTemplate?EmailTemplateId=" + _EmailTemplateId;
}

function GravarEmailTemplate() {
    ShowModal(true);

    FormModel = SerializaForm("frmCadEmailTemplate");
    var formdata = JSON.stringify(FormModel);
    var formDataOld = JSON.stringify(FormModelOld);

    $.post("../EmailTemplate/GravarEmailTemplate", { EmailTemplate: formdata, EmailTemplateOld: formDataOld }, function (data) {
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
                            window.location.href = "../EmailTemplate/ListaEmailTemplate";
                        }
                    }
                }
            });


        } else {
            ShowModal(false);
            MensagemErro("Não foi possível gravar os dados do Template de E-mail.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}

$(function () {

    $("#btnNovoEmailTemplate").attr("style", "display: block;");
    $("#mnuAcoes").attr("style", "display: block;");
    $("#btnNovoEmailTemplate").click(function () {
        window.location.href = "../EmailTemplate/CadEmailTemplate?EmailTemplateId=0";
    });

    $("#btnSalvarEmailTemplate").click(function () {
        GravarEmailTemplate();
    });

    $("#btnAtualizarEmailTemplate").click(function () {
        ListarEmailTemplate();
    });

    ListarEmailTemplate();

    var emailTemplateId = $("#EmailTemplateId").val();
    if (emailTemplateId != null) {
        ShowModal(true);
        //PT
        var idiomaId = 1;
        $.get("../EmailTemplate/CarregarEmailTemplate", { EmailTemplateId: emailTemplateId }, function (data) {
            if (data.EmailTemplate.EmailTemplateId != null) {
                PreencherCadastro(data);
                ShowModal(false);
            } else {
                ShowModal(false);
                MensagemErro("Erro ao carregar dados do Template de E-mail.");
            }
        });
    }


});
