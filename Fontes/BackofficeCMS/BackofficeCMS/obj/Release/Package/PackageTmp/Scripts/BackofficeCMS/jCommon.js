function FormatarDataJson(date) {
    var resp;
    if (date != null)
        resp = new Date(parseInt(date.substr(6)));
    return resp;
}

function FormatarDataJsonInt(date) {
    var resp;
    if (date != null)
        resp = parseInt(date.substr(6));
    return resp;
}

function SerializaForm(nomeForm) {
    var Model = new Object();

    if (nomeForm == null)
        nomeForm = "form";

    var form = $("#" + nomeForm).find("button,input[type='text'],input[type='hidden'],input[type='password'],input[type='text']:disabled,input[type='checkbox'],input[type='radio']:checked,select,textarea");

    //console.log("Rotina de Serialização do Formulário: " + nomeForm);

    $.each(form, function (i, item) {

        if ($(item).hasClass("noserial") == false) {


            var _campo = $(item);
            var _id = $(item).attr("id");
            var _tipo = _campo.get(0).tagName;

            var _camponome = _campo.attr("name");
            var _campovalor = _campo.val();
            var _campocomplemento = "";

            if (_campovalor == null) _campovalor = "";
            if (_tipo == "SELECT") {
                _campocomplemento = $(item).find("option:selected").text();
            }


            if ($(item).is(':checked')){
                _campocomplemento = true;
            }

            Model[_camponome] = [_campovalor, _campocomplemento];

            //console.log(_camponome + " -> " + _campovalor);
        }
    });

    //console.log(Model);

    return Model;
}

function LimparForm(nomeForm) {

    if (nomeForm == null)
        nomeForm = "form";

    var form = $("#" + nomeForm).find("input,select,textarea");
    $.each(form, function (i, item) {
        $(item).val("");
    });
}

function Mensagem(mensagem, titulo) {
    bootbox.dialog({
        message: mensagem == null ? "Dados gravados com sucesso!" : mensagem,
        title: titulo,
        buttons: {
            ok: {
                label: "OK",
                className: "btn-success"
            }
        }
    });
}

function MensagemSucesso(mensagem)
{
    bootbox.dialog({
        message: mensagem == null ? "Dados gravados com sucesso!" : mensagem,
        title: "Sucesso",
        buttons: {
            ok: {
                label: "OK",
                className: "btn-success"
            }
        }
    });
}

function MensagemErro(erro) {
    bootbox.dialog({
        message: erro == null ? "Ocorreu algum erro!" : erro,
        title: "Erro",
        buttons: {
            ok: {
                label: "OK",
                className: "btn-danger"
            }
        }
    });
}

function ShowModal(exibe) {
    if (exibe) {
        $(".modalInterna").fadeIn();
    } else {
        $(".modalInterna").fadeOut();
    }
}

$(function () {

    if (!jQuery.plot) {
        return;
    }

    function showChartTooltip(x, y, xValue, yValue) {
        $('<div id="tooltip" class="chart-tooltip">' + yValue + '<\/div>').css({
            position: 'absolute',
            display: 'none',
            top: y - 40,
            left: x - 40,
            border: '0px solid #ccc',
            padding: '2px 6px',
            'background-color': '#fff'
        }).appendTo("body").fadeIn(200);
    }

    var data = [];
    var totalPoints = 250;

    // random data generator for plot charts

    function getRandomData() {
        if (data.length > 0) data = data.slice(1);
        // do a random walk
        while (data.length < totalPoints) {
            var prev = data.length > 0 ? data[data.length - 1] : 50;
            var y = prev + Math.random() * 10 - 5;
            if (y < 0) y = 0;
            if (y > 100) y = 100;
            data.push(y);
        }
        // zip the generated y values with the x values
        var res = [];
        for (var i = 0; i < data.length; ++i) res.push([i, data[i]])
        return res;
    }

    function randValue() {
        return (Math.floor(Math.random() * (1 + 50 - 20))) + 10;
    }

    var visitors = [
        //['02/2014', 150],
        //['03/2014', 250],
        //['04/2014', 170],
        //['05/2014', 80],
        //['06/2014', 150],
        //['07/2014', 235],
        //['08/2014', 150],
        ['09/2014', 130],
        ['10/2014', 240],
        ['11/2014', 460],
        ['12/2014', 330],
        ['01/2015', 390],
        ['02/2015', 460]
    ];


    if ($('#site_statistics').size() != 0) {

        $('#site_statistics_loading').hide();
        $('#site_statistics_content').show();

        var plot_statistics = $.plot($("#site_statistics"),
            [{
                data: visitors,
                lines: {
                    fill: 0.6,
                    lineWidth: 0
                },
                color: ['#f89f9f']
            }, {
                data: visitors,
                points: {
                    show: true,
                    fill: true,
                    radius: 5,
                    fillColor: "#f89f9f",
                    lineWidth: 3
                },
                color: '#fff',
                shadowSize: 0
            }],

            {
                xaxis: {
                    tickLength: 0,
                    tickDecimals: 0,
                    mode: "categories",
                    min: 0,
                    font: {
                        lineHeight: 14,
                        style: "normal",
                        variant: "small-caps",
                        color: "#6F7B8A"
                    }
                },
                yaxis: {
                    ticks: 5,
                    tickDecimals: 0,
                    tickColor: "#eee",
                    font: {
                        lineHeight: 14,
                        style: "normal",
                        variant: "small-caps",
                        color: "#6F7B8A"
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: true,
                    tickColor: "#eee",
                    borderColor: "#eee",
                    borderWidth: 1
                }
            });

        var previousPoint = null;
        $("#site_statistics").bind("plothover", function (event, pos, item) {
            $("#x").text(pos.x.toFixed(2));
            $("#y").text(pos.y.toFixed(2));
            if (item) {
                if (previousPoint != item.dataIndex) {
                    previousPoint = item.dataIndex;

                    $("#tooltip").remove();
                    var x = item.datapoint[0].toFixed(2),
                        y = item.datapoint[1].toFixed(2);

                    showChartTooltip(item.pageX, item.pageY, item.datapoint[0], item.datapoint[1] + ' visualizações');
                }
            } else {
                $("#tooltip").remove();
                previousPoint = null;
            }
        });
    }


    if ($('#site_activities').size() != 0) {
        //site activities
        var previousPoint2 = null;
        $('#site_activities_loading').hide();
        $('#site_activities_content').show();

        var data1 = [
            ['MAI', 300],
            ['JUN', 600],
            ['JUL', 1100],
            ['AGO', 1200],
            ['SET', 860],
            ['OUT', 1200],
            ['NOV', 1450],
            ['DEZ', 1370],
            ['JAN', 1200],
            ['FEV', 600]
        ];


        var plot_statistics = $.plot($("#site_activities"),

            [{
                data: data1,
                lines: {
                    fill: 0.2,
                    lineWidth: 0,
                },
                color: ['#BAD9F5']
            }, {
                data: data1,
                points: {
                    show: true,
                    fill: true,
                    radius: 4,
                    fillColor: "#9ACAE6",
                    lineWidth: 2
                },
                color: '#9ACAE6',
                shadowSize: 1
            }, {
                data: data1,
                lines: {
                    show: true,
                    fill: false,
                    lineWidth: 3
                },
                color: '#9ACAE6',
                shadowSize: 0
            }],

            {

                xaxis: {
                    tickLength: 0,
                    tickDecimals: 0,
                    mode: "categories",
                    min: 0,
                    font: {
                        lineHeight: 18,
                        style: "normal",
                        variant: "small-caps",
                        color: "#6F7B8A"
                    }
                },
                yaxis: {
                    ticks: 5,
                    tickDecimals: 0,
                    tickColor: "#eee",
                    font: {
                        lineHeight: 14,
                        style: "normal",
                        variant: "small-caps",
                        color: "#6F7B8A"
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: true,
                    tickColor: "#eee",
                    borderColor: "#eee",
                    borderWidth: 1
                }
            });

        $("#site_activities").bind("plothover", function (event, pos, item) {
            $("#x").text(pos.x.toFixed(2));
            $("#y").text(pos.y.toFixed(2));
            if (item) {
                if (previousPoint2 != item.dataIndex) {
                    previousPoint2 = item.dataIndex;
                    $("#tooltip").remove();
                    var x = item.datapoint[0].toFixed(2),
                        y = item.datapoint[1].toFixed(2);
                    showChartTooltip(item.pageX, item.pageY, item.datapoint[0], item.datapoint[1] + ' Clicks');
                }
            }
        });

        $('#site_activities').bind("mouseleave", function () {
            $("#tooltip").remove();
        });
    }

});