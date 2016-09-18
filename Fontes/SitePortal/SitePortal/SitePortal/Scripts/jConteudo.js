$(function () {
    var conteudoId = $("#PublicacaoId").val();
    CarregarConteudo(conteudoId);
    
    $('#btnFace').click(function () {
        doShareFace();
        
    });

    
    $('#btnLinkedIn').click(function () {
        doShareLinkedIn();

    });

    $('#btnTwitter').click(function () {
        doShareTwitter();
    });
    

});

function doShareTwitter() {
    var _url = $("#UrlAtual").val();
    var _text = $("#ConteudoTitulo").html();
    window.open("https://twitter.com/intent/tweet?url=" + _url + "&text=" + _text + "&name=asset&linktype=socialnetworksharemodule%3Ashare+intent");
}

function doShareLinkedIn() {
    var _url = $("#UrlAtual").val();
    window.open("https://www.linkedin.com/shareArticle?mini=true&url=" + _url + "&name=asset&linktype=socialnetworksharemodule%3Ashare+intent");
}

function doShareFace() {
    //FB.ui({
    //    method: 'share_open_graph',
    //    action_type: 'og.likes',
    //    action_properties: JSON.stringify({
    //        object: 'https://developers.facebook.com/docs/',
    //    })
    //}, function (response) { });

    //FB.ui({
    //    method: 'feed',
    //    link: 'https://developers.facebook.com/docs/',
    //    caption: 'An example caption',
    //}, function (response) { });

    //https://www.facebook.com/dialog/feed?
    //    app_id=145634995501895
    //    &display=popup&caption=An%20example%20caption 
    //    &link=https%3A%2F%2Fdevelopers.facebook.com%2Fdocs%2F
    //    &redirect_uri=https%3A%2F%2Fdevelopers.facebook.com%2Ftools%2Fexplorer

    //FB.ui({
    //    method: 'share_open_graph',
    //    action_type: 'og.likes',
    //    action_properties: JSON.stringify({
    //        object: 'http://ccbc.org.br',
    //    })
    //});

    //FB.ui({
    //    method: 'share',
    //    href: 'http://ccbc.org.br/Noticias/1294/us$-1-milhao-para-carros-conectados',
    //}, function (response) { });


    // Feed: Funcionou!
    var _name = $("#ConteudoTitulo").html();
    var _conteudo = $("#ConteudoCorpoMini").val();
    var _arquivo = $("#ArquivoDestaqueId").val();
    var _url = $("#UrlAtual").val();
    if (_arquivo != null && _arquivo != 0 && _arquivo != "0") {
        FB.ui({
            method: 'feed',
            name: _name,
            description: _conteudo,
            display: 'popup',
            caption: 'ccbc.org.br',
            link: _url,
            picture: 'http://www.ccbc.org.br/File?Id=' + _arquivo,
            next: 'http://www.ccbc.org.br/thanks.html'
        }, function (response) { });
    } else { 
        FB.ui({
            method: 'feed',
            name: _name,
            description: _conteudo,
            display: 'popup',
            caption: 'ccbc.org.br',
            link: _url,
            picture: 'http://www.ccbc.org.br/Images/Logo_CCBC_122.jpg',
            next: 'http://www.ccbc.org.br/thanks.html'
        }, function (response) { });
    }
    

    
    
}

    function CarregarArquivo() {
        var publicacaoId = $("#PublicacaoId").val();
    
        //var AppPath = '@Url.Content("~/")';
        //alert(AppPath);
        $.post(AppPath + "Conteudo/ListarArquivosGaleria", { PublicacaoId: publicacaoId, ArquivoCategoriaId: 2 }, function (data) {
            if (data.length == 0) {
                $("#galeriaImagem").attr("style", "display: none");
            } else {
                $("#galeriaImagem").attr("style", "display: block");
            }
            $.each(data, function (i, item) {

                //Carregar a partir de Base64
                //$("#galeriaImagem").append("<a id=\"a" + i + "\" href=\"\" rel=\"example_group\"><img id=\"ex" + i + "\" class=\"last\" /></a>");
                //$("#a" + i).attr("href", "data:image/jpeg;base64," + item.Base64);
                //$("#a" + i).attr("title", item.Legenda);
                //$("#ex" + i).attr("src", "data:image/jpeg;base64," + item.Base64);
                //$("#ex" + i).attr("alt", item.Legenda);

                //Antigo - ate 2016-09-17
                $("#galeriaImagem").append("<a id=\"a" + i + "\" href=\"\" rel=\"example_group\"><img id=\"ex" + i + "\" class=\"last\" /></a>");
                $("#a" + i).attr("href", AppPath + "File?id=" + item.ArquivoId);
                $("#a" + i).attr("title", item.Legenda);
                $("#ex" + i).attr("src", AppPath + "File?id=" + item.ArquivoId);
                $("#ex" + i).attr("alt", item.Legenda);

            });
        
            //Fancy Base64
            //$("a[href^='data:image']").each(function () {
            //    $(this).fancybox({
            //        minWidth: 200,
            //        minHeight: 200,
            //        'transitionIn': 'none',
            //        'transitionOut': 'none',
            //        'titlePosition': 'over',
            //        'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
            //            return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
            //        },
            //        content: $("<img/>").attr("src", this.href)
            //    });
            //});

            $("a[rel^='example_group']").each(function () {
                $(this).fancybox({
                    minWidth: 200,
                    minHeight: 200,
                    'transitionIn': 'none',
                    'transitionOut': 'none',
                    'titlePosition': 'over',
                    'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
                        return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
                    },
                    content: $("<img/>").attr("src", this.href)
                });
            });

            //CarregarConteudo(conteudoId);
        });
    }

    function CarregarConteudo(id) {
        //$.post(AppPath + "Conteudo/CarregarConteudo", { ConteudoId: id }, function (data) {
        //alert(data.Titulo);
        //alert(data.Conteudo);

        //Carregar dados via model!
        //$("#ConteudoTitulo").html(data.Titulo);
        //$("#ConteudoCorpo").html(data.Descricao);

        CarregarArquivo();

        //if (data) {
        //    showModal(false);
        //    //$(this).showMessage("Sucesso", "Laudo Cancelado com Sucesso");
        //    //LaudoSomenteLeitura(1);

        //    $("<div></div>")
        //    .html("Laudo Cancelado com Sucesso")
        //    .dialog({
        //        autoOpen: true,
        //        modal: true,
        //        title: "Sucesso",
        //        buttons: {
        //            "OK": function () {
        //                window.location.reload();
        //            }
        //        }
        //    });
        //} else {
        //    showModal(false);
        //    $(this).showMessage(Aviso, "Ocorreu um erro no Cancelamento do Laudo!");
        //}

        //Fancy Normal
        //$("a#example1").fancybox();
        //$("a[rel=example_group]").fancybox({
        //    'transitionIn': 'none',
        //    'transitionOut': 'none',
        //    'titlePosition': 'over',
        //    'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
        //        return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
        //    }
        //});

        
        //});
    }
