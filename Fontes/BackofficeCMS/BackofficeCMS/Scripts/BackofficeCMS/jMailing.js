
function MailingExportarExcel() {
    $('form').get(0).setAttribute('action', AppPath + 'Mailing/ExportarExcel');
    $("form:first").submit();
    $('form').get(0).setAttribute('action', AppPath + 'Mailing/ListaMailing');
}

function ListarMailing() {

    var regs = [];
    var reg = [];

    $.get("../Mailing/ListarMailing", {}, function (data) {
        $.each(data, function (i, item) {
            reg = new Array();
            reg.push(item.MailingId);
            reg.push(item.Nome);
            reg.push(item.Email);
            reg.push(item.Segmento);

            var d = FormatarDataJson(item.DataInclusao);
            if (d == null) {
                reg.push(null);
            } else {
                reg.push(moment(d).format('DD/MM/YYYY'));
            }

            var ativo = "";
            if (item.Ativo == true) ativo = "Ativo";
            if (item.Ativo == false) ativo = "Inativo";
            reg.push(ativo); //Status: Ativo | Inativo.

            //var editar = "";
            //var excluir = "";
            //if (optFunc_160 != null) {
            //    editar = "<span class=\"icon-editar glyphicon glyphicon-edit\" title=\"Editar\" onclick=\"javascript:EditarPublicacao(" + item.PublicacaoId + ")\"></span>";
            //}
            //if (optFunc_170 != null) {
            //    excluir = "<span class=\"glyphicon glyphicon-trash icon-excluir\" style=\"margin-right: 10px;\" title=\"Excluir\" onclick=\"javascript:ExcluirPublicacao(" + item.PublicacaoId + ")\"></span>";
            //}

            //reg.push(editar + " " + excluir);

            regs.push(reg);

        });

        $.fn.dataTable.moment('DD/MM/YYYY');

        $('#tblMailing').dataTable({
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
                { "sTitle": "Nome" },
                { "sTitle": "Email" },
                { "sTitle": "Segmento", "width": "80px" },
                { "sTitle": "Data Registro", "width": "50px" },
                { "sTitle": "Status", "width": "60px" }
                //{ "sTitle": "", "orderable": false }
            ]

        });

    });
}

$(function () {

    $("#btnMailingExportarExcel").attr("style", "display: block;");
    $("#mnuAcoes").attr("style", "display: block;");

    ListarMailing();

    $("#btnMailingExportarExcel").click(function () {
        MailingExportarExcel();
    });

});