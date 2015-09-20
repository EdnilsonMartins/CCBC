var editt;
var emQ = false;

var FormModel_Pasta, FormModelOld_Pasta;

function GravarPasta() {

    FormModel_Pasta = SerializaForm("frmCadPasta");
    var formdata = JSON.stringify(FormModel_Pasta);
    var formDataOld = JSON.stringify(FormModelOld_Pasta);

    $.post("../Pasta/GravarPasta", { Pasta: formdata, PastaOld: formDataOld }, function (data) {
        if (data.Resposta.Erro == false) {
            $("#CadPasta").modal("hide");
            //var pastaId = data.Pasta.PastaId;
            //ListarPasta(editoriaId);
            ListarPastas();
            bootbox.dialog({
                message: "Dados gravados com sucesso!",
                title: "Sucesso",
                buttons: {
                    ok: {
                        label: "OK",
                        className: "btn-success",
                        callback: function () {


                            //window.location.href = "../Publicacao/ListaPublicacao";
                        }
                    }
                }
            });
        } else {
            MensagemErro("Não foi possível gravar os dados da Pasta.<br /><br />" + data.Resposta.Mensagem);
        }
    });
}

function ListarPastas() {

    var registros = [];

    $('#tvPasta').jstree({
        "core": { "check_callback": true },
        "plugins": ["dnd", "search", "state"]
    });

    $('#tvPasta').on("select_node.jstree", function (e, data) {
        var _PastaId = parseInt(data.node.id);
        console.log("Selecionado!!! " + _PastaId);
        CarregarArquivosMediaCenter();
    });

    $('#tvPasta').on("move_node.jstree", function (e, data) {
        if (emQ) return true;
        emQ = true;
        var _Posicao = parseInt(data.position);
        var _PastaId = parseInt(data.node.id);
        var PastaPaiId = data.parent;
        if (PastaPaiId == "#") PastaPaiId = 0;
        var _PastaPaiId = parseInt(PastaPaiId);

        bootbox.dialog({
            message: "Deseja realmente reposicionar esta Pasta?",
            title: "Confirmação",
            buttons: {
                sim: {
                    label: "Sim",
                    className: "btn-success",
                    callback: function () {
                        $.get("../Pasta/ReposicionarPasta", { PastaId: _PastaId, PastaPaiId: _PastaPaiId, Posicao: _Posicao }, function (data) {
                            emQ = false;
                            if (data.Resposta.Erro == false) {
                                MensagemSucesso("Pasta atualizada com sucesso!");
                                ListarPastas();
                            } else if (data.Resposta.Mensagem != "") {
                                MensagemErro(data.Resposta.Mensagem);
                            }
                            else {
                                MensagemErro("Erro ao atualizar a parta.");
                            }
                        });
                    }
                },
                nao: {
                    label: "Não",
                    className: "btn-danger",
                    callback: function () {
                        emQ = false;
                        ListarPastas();
                    }
                }
            }
        });
    });

    $.get("../Pasta/ListarPasta", { }, function (data) {
        $.each(data, function (i, item) {
            var _parent = item.PastaPaiId;
            if (_parent == null) _parent = "#";
            var regi = {
                id: item.PastaId,
                parent: _parent,
                text: item.Descricao,
                icon: "fa fa-file icon-state-info jstree-drop",
                class: "jstree-drop"
            };
            registros.push(regi);
        });

        $('#tvPasta').jstree(true).settings.core.data = registros;
        $('#tvPasta').jstree(true).refresh();
    });

}

function ExcluirPasta(_PastaId) {
    bootbox.dialog({
        message: "Deseja realmente excluir esta Pasta?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.get("../Pasta/ExcluirPasta", { PastaId: _PastaId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            MensagemSucesso("Pasta excluída com sucesso!");
                            ListarPastas();
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir a pasta.");
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

function PreencherCadastroPasta(data) {
    $("#PastaId", "#frmCadPasta").val(data.Pasta.PastaId);
    $("#PastaPaiId", "#frmCadPasta").val(data.Pasta.PastaPaiId);
    $("#Descricao", "#frmCadPasta").val(data.Pasta.Descricao);
}





function SelecionarLink_MediaCenter(arquivoId, i) {
    if (arquivoId != null) {
        if (editt != null) {
            editt.insertHtml('<img src="/File?Id=' + arquivoId + '" style="width: 120px;"/>');
            $("#SelArquivo").modal("hide");
        }
    }
}

function CarregarArquivosMediaCenter() {

    var optFunc_210 = $("#optFunc_210").val();
    var optFunc_211 = $("#optFunc_211").val();

    console.log("Listar Arquivos do Media Center");
    var _ownerId = 0; // $("#gridArquivos").attr("ownerid");
    var _arquivocategoriatipoid = 4; //$("#gridArquivos").attr("arquivocategoriatipoid");
    var _pastaId = 0;

    if ($('#tvPasta').jstree(true).get_selected() != "") {
        _pastaId = parseInt($('#tvPasta').jstree(true).get_selected());
    }

    $.post("../Arquivo/ListarArquivos", { OwnerId: _ownerId, ArquivoCategoriaTipoId: _arquivocategoriatipoid, PastaId: _pastaId }, function (data) {

        $("#gridMedia").html("");

        $.each(data, function (i, item) {

            var tipo = item.Tipo;
            var fileName = "";
            var lightBox = "fancybox-button";
            var hRefPreview = "";
            if (tipo == "application/pdf") {
                fileName = AppPath + "Content/images/PDF.png";
                lightBox = "";
                hRefPreview = "#";
            } else if (tipo == "application/vnd.openxmlformats-officedocument.spre") {
                fileName = AppPath + "Content/images/XLSX.png";
                lightBox = "";
                hRefPreview = "#";
            } else if (tipo == "application/vnd.openxmlformats-officedocument.word") {
                fileName = AppPath + "Content/images/DOCX.png";
                lightBox = "";
                hRefPreview = "#";
            } else {
                fileName = AppPath + "File?id=" + item.ArquivoId + "&s=" + (new Date).getTime();
                hRefPreview = fileName;
            }
            
            var editar = "";
            var excluir = "";
            if (optFunc_210 != null) {
                editar = "<a id=\"btnEditarArquivo_Media" + i + "\" class=\"mix-edit\" title=\"Editar\"><i class=\"fa fa-edit\"></i></a>";
            }
            if (optFunc_211 != null) {
                excluir = "<a id=\"btnExcluirArquivo_Media" + i + "\" class=\"mix-delete\" title=\"Apagar\"><i class=\"fa fa-trash-o\"></i></a>";
            }

            $("#gridMedia").append("<div id=\"dvArquivo" + i + "\" class=\"col-md-3 col-sm-2 mix mix_all width250\"><div class=\"mix-inner\" style=\"min-height: 167px;\"><img id=\"a" + i + "\" class=\"img-responsive\" src=\"" + fileName + "\" alt><div class=\"mix-details\"><h4 id=\"Legenda" + i + "\">" + item.Legenda + "</h4><a id=\"btnSelecionarLink_Media" + i + "\" class=\"mix-link\" title=\"Copiar link: " + AppPath + "File?id=" + item.ArquivoId + "\"><i class=\"fa fa-link\"></i></a>" + editar + excluir + "<a id=\"btnPreview_Media" + i + "\" class=\"mix-preview \" href=\"" + hRefPreview + "\" title=\"" + item.Legenda + "\" rel=\"" + lightBox + "\"><i class=\"fa fa-search\"></i></a></div></div></div>");

            //O src passou a ser atribuído direto no Append acima, pois as vezes ocorria do SRC não ser carregado no html.
            //$("#a" + i).prop("src", AppPath + "File?id=" + item.ArquivoId); //(new Date).getTime()
            
            $("#btnSelecionarLink_Media" + i).click(function () {
                SelecionarLink_MediaCenter(item.ArquivoId, i);
            });
            $("#btnExcluirArquivo_Media" + i).click(function () {
                ExcluirArquivo(item.ArquivoId, i);
            });
            $("#btnEditarArquivo_Media" + i).click(function () {
                EditarArquivo(item.ArquivoId, i);
            });
            if (lightBox == "") {
                $("#btnPreview_Media" + i).click(function () {
                    window.open(AppPath + "File?id=" + item.ArquivoId + "&s=" + (new Date).getTime(), "_blank");
                });
            }


            /****************************
            * Categoria das Fotos       *
            *****************************/
            var vals = item.ListaCategoria.split(",");
            var svals = '';
            if (item.ListaCategoria.length > 0) {
                for (k = 0; k < vals.length; k++) {
                    svals += "category_" + vals[k] + " width250 height168 ";
                    $("#dvArquivo" + i).addClass(svals);
                    //$("#dvArquivo" + i).addAttribute("style", "display: block; opacity: 1; width: 250px;");
                }
                svals = svals.slice(0, -1);
                svals += '';
                //console.log(svals);
            }
            /*************************/

        });

        $('#gridMedia').mixitup();

        $("a[rel^='fancybox-button']").each(function () {
            //console.log(this);
            $(this).fancybox({
                minWidth: 200,
                minHeight: 200,
                nextClick: true,
                'transitionIn': 'none',
                'transitionOut': 'none',
                'titlePosition': 'over',
                'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
                    return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
                },
                content: $("<img/>").attr("src", this.href)
            });
        });

    });
}

$(function () {

    console.log("Carregando biblioteca Media Center.");

    $("#btnCriarPasta").click(function () {
        $("#PastaId", "#frmCadPasta").val("0");
        $("#PastaPaiId", "#frmCadPasta").val("");
        $("#Descricao", "#frmCadPasta").val("");

        if ($('#tvPasta').jstree(true).get_selected() != "") {
            var x = parseInt($('#tvPasta').jstree(true).get_selected());
            $("#PastaPaiId", "#frmCadPasta").val(x);
        } 

        $("#CadPasta").modal("show");
        $("#Descricao", "#frmCadPasta").focus();
    });

    $("#btnEditarPasta").click(function () {
        $("#PastaId", "#frmCadPasta").val("0");
        $("#PastaPaiId", "#frmCadPasta").val("");
        $("#Descricao", "#frmCadPasta").val("");

        if ($('#tvPasta').jstree(true).get_selected() != "") {
            var pastaId = parseInt($('#tvPasta').jstree(true).get_selected());
            $("#PastaPaiId", "#frmCadPasta").val(pastaId);

            $.get("../Pasta/CarregarPasta", { PastaId: pastaId }, function (data) {
                if (data.Pasta.PastaId != null) {
                    PreencherCadastroPasta(data);
                    FormModelOld_Pasta = SerializaForm("frmCadPasta");
                } else {
                    MensagemErro("Erro ao carregar dados da pasta");
                }
            });

            $("#CadPasta").modal("show");
            $("#Descricao", "#frmCadPasta").focus();
        } else {
            Mensagem("Nenhuma pasta selecionada!", "Validação");
        }

        
    });

    $("#btnSalvarPasta").click(function () {
        GravarPasta();
    });

    $("#btnExcluirPasta").click(function () {
        
        if ($('#tvPasta').jstree(true).get_selected() != "") {
            var x = parseInt($('#tvPasta').jstree(true).get_selected());
            ExcluirPasta(parseInt(x));
        } else {
            Mensagem("Nenhuma pasta selecionada!", "Validação");
        }
        
    });

    $("#btnSelecionarArquivo").click(function () {
        console.log("Arquivo Selecionado!");
        console.log(editt);
        //editt.insertHtml("asdad");

        editt.insertHtml('Inserting image here: <img src="/File?Id=30432"/>');

        $("#SelArquivo").modal("hide");
    });

    $(document).ready(function () {

        //$('#gridMedia').mixitup();
        //$("#MySplitter").splitter();

        ListarPastas();
    });

    //CarregarArquivosMediaCenter();

});