         

var inputF;
var datinha;
var ArquivoEditadoId, ArquivoEditadoIndex;

//Teste: Capturar instancia do editor para seleção do arquivo no Media Center.
//var editt;

function CarregarArquivos() {
    
    var optFunc_210 = $("#optFunc_210").val();
    var optFunc_211 = $("#optFunc_211").val();

    console.log("Carregando dados nos arquivos");

    var _ownerId = $("#gridArquivos").attr("ownerid");
    var _arquivocategoriatipoid = $("#gridArquivos").attr("arquivocategoriatipoid");

    var gA = $("#gridArquivos").attr("ownerid");
    if (gA == null) {
        _ownerId = $("#gridMedia").attr("ownerid");
        _arquivocategoriatipoid = $("#gridMedia").attr("arquivocategoriatipoid");
    }

    $.post("../Arquivo/ListarArquivos", { OwnerId: _ownerId,  ArquivoCategoriaTipoId: _arquivocategoriatipoid }, function (data) {

        $("#gridArquivos").html("");

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
                editar = "<a id=\"btnEditarArquivo" + i + "\" class=\"mix-edit\" title=\"Editar\"><i class=\"fa fa-edit\"></i></a>";
            }
            if (optFunc_211 != null) {
                excluir = "<a id=\"btnExcluirArquivo" + i + "\" class=\"mix-delete\" title=\"Apagar\"><i class=\"fa fa-trash-o\"></i></a>";
            }

            $("#gridArquivos").append("<div id=\"dvArquivo" + i + "\" class=\"col-md-3 col-sm-4 mix mix_all\"><div class=\"mix-inner\" style=\"min-height: 167px;\"><img id=\"a" + i + "\" class=\"img-responsive\" src=\"" + fileName + "\" alt><div class=\"mix-details\"><h4 id=\"Legenda" + i + "\">" + item.Legenda + "</h4><a class=\"mix-link\" title=\"Copiar link: " + AppPath + "File?id=" + item.ArquivoId + "\"><i class=\"fa fa-link\"></i></a>" + editar + excluir + "<a id=\"btnPreview_Media" + i + "\" class=\"mix-preview fancybox-button\" href=\"" + hRefPreview + "\" title=\"" + item.Legenda + "\" rel=\"" + lightBox + "\"><i class=\"fa fa-search\"></i></a></div></div></div>");

            //$("#a" + i).prop("src", AppPath + "File?id=" + item.ArquivoId); //(new Date).getTime()

            $("#btnExcluirArquivo" + i).click(function () {
                ExcluirArquivo(item.ArquivoId, i);
            });
            $("#btnEditarArquivo" + i).click(function () {
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
                }
                svals = svals.slice(0, -1);
                svals += '';
                //console.log(svals);
            }
            /*************************/

        });

        $('.mix-grid').mixitup();

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

function PreencherCadastroArquivo(data) {
    $("#btnLimpar").trigger('click');
    inputF = null;

    $("#ArquivoId").val(data.ArquivoId);
    $("#Legenda").val(data.Legenda);
    $("#dvPrev").html("");
    $("#dvPrev").append("<img id=\"thumbArquivo\" src=\"\" alt=\"\" />");
    $("#thumbArquivo").prop("src", AppPath + "File?Id=" + data.ArquivoId + "&s=" + (new Date).getTime());

    var categoriaId;
    var vals = data.ListaCategoria.split(",");
    var svals = '[';
    for (i = 0; i < vals.length; i++) {
        svals += '"' + vals[i] + '",';
        categoriaId = vals[0];
    }
    svals = svals.slice(0, -1);
    svals += ']';

    //Multiple:
    //$("#Categoria").selectpicker('val', JSON.parse(svals));

    MostrarCadastroArquivo();

    //Single:
    $("#Categoria").selectpicker('val', categoriaId);
    $('#Categoria').selectpicker('refresh');

}

function MostrarCadastroArquivo() {

    if (inputF != null) {
        var fileInput = inputF;
        fileInput.val('');
    }

    $("#fileupload").find(".files").empty();

    var OwnerId = 0;
    var ArquivoCategoriaTipoId = 0;
    var gA = $("#gridArquivos").attr("ownerid");
    if ( gA == null) {
        OwnerId = $("#gridMedia").attr("ownerid");
        $("#OwnerId").val(OwnerId);
        ArquivoCategoriaTipoId = $("#gridMedia").attr("arquivocategoriatipoid");
        $("#ArquivoCategoriaTipoId").val(ArquivoCategoriaTipoId);

        if ($('#tvPasta').jstree(true).get_selected() != "") {
            var pastaId = parseInt($('#tvPasta').jstree(true).get_selected());
            $("#PastaId", "#frmArquivo").val(pastaId);
        }
    } else {
        OwnerId = $("#gridArquivos").attr("ownerid");
        $("#OwnerId").val(OwnerId);
        ArquivoCategoriaTipoId = $("#gridArquivos").attr("arquivocategoriatipoid");
        $("#ArquivoCategoriaTipoId").val(ArquivoCategoriaTipoId);
        $("#PastaId", "#frmArquivo").val("0");
    }

    if ($('#tvPasta').jstree(true).get_selected() != "") {
        var pastaId = parseInt($('#tvPasta').jstree(true).get_selected());
        $("#PastaId", "#frmArquivo").val(pastaId);
    }

    $("#OwnerId", "#frmArquivo").val(OwnerId);
    $("#ArquivoCategoriaTipoId", "#frmArquivo").val(ArquivoCategoriaTipoId);

    console.log("OwnerId: " + OwnerId);
    console.log("ArquivoCategoriaTipoId: " + ArquivoCategoriaTipoId);



    $("#Categoria").html("");
    if (ArquivoCategoriaTipoId == 1) {
        $('#Categoria').append('<option value="1">Destaque</option>');
        $('#Categoria').append('<option value="2">Galeria de Fotos</option>');
        $('#Categoria').append('<option value="3">Diversos</option>');
    } else if (ArquivoCategoriaTipoId == 2) {
        $('#Categoria').append('<option value="4">Ícones de Menu</option>');
    } else if (ArquivoCategoriaTipoId == 3) {
        $('#Categoria').append('<option value="5">Imagem Primária</option>');
        $('#Categoria').append('<option value="6">Imagem Secundária</option>');
    }
    $('#Categoria').selectpicker('refresh');

    $("#CadArquivo").modal("show");
}

function GravarArquivo() {

    var fileInput = inputF;

    var _ownerId = 0;
    var _arquivocategoriatipoid = 0;
    var gA = $("#gridArquivos").attr("ownerid");
    if (gA == null) {
        _ownerId = $("#gridMedia").attr("ownerid");
        _arquivocategoriatipoid = $("#gridMedia").attr("arquivocategoriatipoid");
    } else {
        _ownerId = $("#gridArquivos").attr("ownerid");
        _arquivocategoriatipoid = $("#gridArquivos").attr("arquivocategoriatipoid");
    }


    var arquivoId = $("#ArquivoId").val();
    var legenda = $("#Legenda").val();
    var listaCategoria = "" + $("#Categoria").val() + "";
    
    if (fileInput != null) {
       
        var selectedFile = fileInput.val();

        if (selectedFile == null || selectedFile == "") {
            
        }
    } else {
        console.log("Categorias selecionadas para gravação: " + listaCategoria);
        //listaCategoria = "2,3";
        $.post("../Arquivo/GravarArquivo", { _OwnerId: _ownerId, _ArquivoId: arquivoId, _Legenda: legenda, _ListaCategoria: listaCategoria, _ArquivoCategoriaTipoId: _arquivocategoriatipoid }, function (data) {
            if (data == true) {
                $("#CadArquivo").modal("hide");
                

                //Recarrega todos
               
                if (gA == null) {
                    CarregarArquivosMediaCenter();
                } else {
                    CarregarArquivosMediaCenter();
                    CarregarArquivos();
                }

                $("#Legenda" + ArquivoEditadoIndex).html(legenda);

            }
        });

        
    }

}

function EditarArquivo(arquivoId, i) {
    if (arquivoId != null) {
        $.post("../Arquivo/CarregarArquivo", { ArquivoId: arquivoId, RetornarContent: false }, function (data) {
            if (data.ArquivoId != null) {
                ArquivoEditadoId = data.ArquivoId;
                ArquivoEditadoIndex = i;
                PreencherCadastroArquivo(data);
            } else {
                MensagemErro("Erro ao carregar dados do arquivo.");
            }
        });
    }
}

function ExcluirArquivo(_ArquivoId, i) {
    bootbox.dialog({
        message: "Deseja realmente excluir este arquivo?",
        title: "Confirmação",
        buttons: {
            sim: {
                label: "Sim",
                className: "btn-success",
                callback: function () {
                    $.post("../Arquivo/ExcluirArquivo", { ArquivoId: _ArquivoId }, function (data) {
                        if (data.Resposta.Erro == false) {
                            var gA = $("#gridArquivos").attr("ownerid");
                            if (gA == null) {
                                CarregarArquivosMediaCenter();
                            } else {
                                CarregarArquivosMediaCenter();
                                $("#dvArquivo" + i).removeClass("mix_all");
                                $('.mix-grid').mixitup('filter', 'mix_all');
                            }
                        } else if (data.Resposta.Mensagem != "") {
                            MensagemErro(data.Resposta.Mensagem);
                        }
                        else {
                            MensagemErro("Erro ao excluir o arquivo.");
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

    $("#btnNovoArquivo").click(function () {
        $("#SelArquivo").modal("show");
        ////////ArquivoEditadoId = null;
        ////////ArquivoEditadoIndex = null;
        ////////$("#btnLimpar").trigger('click');

        ////////$("#ArquivoId").val("");
        ////////$("#Legenda").val("");
        ////////$("#dvPrev").html("");
        ////////$("#dvPrev").append("<img id=\"thumbArquivo\" src=\"" + AppPath + "Content/assets/global/img/no-image.gif" + "\" alt=\"\" />");

        //////////$('select[name=ArquivoCategoriaTipoId]').selectpicker('val', 0);

        //////////$("#fileupload").fileupload("clear");
        //////////var ff = $("#fileupload").find(".files");
        //////////console.log(ff);
        //////////$("#fileupload").find(".files").empty();
        
        //////////$("#dvImagem").show("fold", 0);
        //////////$("#dvImagemNote").show("fold", 0);

        ////////MostrarCadastroArquivo();
    });

    $("#btnNovoArquivoMedia").click(function () {
        console.log("Novo Upload!");

        ArquivoEditadoId = null;
        ArquivoEditadoIndex = null;
        $("#btnLimpar").trigger('click');

        $("#ArquivoId").val("");
        $("#Legenda").val("");
        $("#dvPrev").html("");
        $("#dvPrev").append("<img id=\"thumbArquivo\" src=\"" + AppPath + "Content/assets/global/img/no-image.gif" + "\" alt=\"\" />");

        //$('select[name=ArquivoCategoriaTipoId]').selectpicker('val', 0);

        //$("#fileupload").fileupload("clear");
        //var ff = $("#fileupload").find(".files");
        //console.log(ff);
        //$("#fileupload").find(".files").empty();

        //$("#dvImagem").show("fold", 0);
        //$("#dvImagemNote").show("fold", 0);

        MostrarCadastroArquivo();
    });

    $("#btnSalvarArquivo").click(function () {
        GravarArquivo();
    });

    $("#btnLimpar").click(function () {

    });

    //Piloto para seleção de arquivo via CKEditor customizado.
    //$("#btnSelecionarArquivo").click(function () {
    //    editt.insertHtml('Inserting image here: /File?Id=999' + '<img alt="" src="../File?id=30433" style="height:100px; width:100px" />');
    //    $("#SelArquivo").modal("hide");
    //});

    $(document).ready(function () {
        
    });

    $(document).ready(function () {

        $('.mix-grid').mixitup();

        $('#fileupload').fileupload({
            dataType: 'json',
            url: '../Arquivo/UploadFiles',
            autoUpload: false,
            replaceFileInput: false,
            add: function (e, data) {
                datinha = data;
                $("#btnSalvarArquivo").on('click', function () {
                    //data.submit();
                    if (data != null) {
                        data.submit();
                        data = null;
                    }
                });
                $("#btnLimpar").on('click', function () {
                    //data.submit();
                    data = null;
                    //alert('somente aqui');
                });
            },
            done: function (e, data) {

                console.log("done...");
                console.log(data);

                data.files = null;
                //document.forms[0].reset();
                console.log('depois do empty');
                console.log(data);

                //console.log(data);
                $("#CadArquivo").modal("hide");

                //alert(ArquivoEditadoIndex);
                //CarregarArquivos();
                if (ArquivoEditadoId == null) {
                    CarregarArquivos();
                } else {
                    console.log("Atualizar arquivo na tela.");
                    $("#a" + ArquivoEditadoIndex).prop("src", AppPath + "File?id=" + ArquivoEditadoId + "&s=" + (new Date).getTime()); //(new Date).getTime()
                }
                
                var gA = $("#gridArquivos").attr("ownerid");
                if (gA == null) {
                    CarregarArquivosMediaCenter();
                } else {
                    CarregarArquivosMediaCenter();
                }

                //Exemplo Multiplo upload
                //$('.file_name').html(data.result.name);
                //$('.file_type').html(data.result.type);
                //$('.file_size').html(data.result.size);
            }
        }).on('fileuploadprogressall', function (e, data) {
            //var progress = parseInt(data.loaded / data.total * 100, 10);
            //$('.progress .progress-bar').css('width', progress + '%');
        });


        $('#fileupload').change(function () {
            inputF = $(this);
        });

    });

    
});