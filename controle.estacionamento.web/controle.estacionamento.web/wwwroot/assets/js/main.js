$.noConflict();

jQuery(document).ready(function ($) {

    "use strict";

    $('#menuToggle').on('click', function (event) {
        $('body').toggleClass('open');
    });

});


function imprimir(id) {
    var infoModelo = document.getElementById('infoModelo-' + id).innerHTML;

    var conteudoHTML = "<html>" +
        "<head>" +
        "<title>Informa&ccedil;&otilde;es do Modelo</title>" +
        "<meta charset='UTF-8'>" +
        "<style>" +
        "body {" +
        "font-family: Arial, sans-serif;" +
        "text-align: center;" +
        "font-size: 12px;" +
        "}" +
        "h1 {" +
        "text-align: center;" +
        "}" +
        "</style>" +
        "</head>" +
        "<body>" +
        "<h1>Seja Bem-vindo ao Estacionamento</h1>" +
        "<h2>Informa&ccedil;&otilde;es do Modelo</h2>" +
        infoModelo +
        "<h2>Obrigado! Volte sempre...</h2>" +
        "</body>" +
        "</html>";

    var blob = new Blob([conteudoHTML], { type: 'text/html;charset=utf-8' });
    var url = URL.createObjectURL(blob);

    var janelaImpressao = window.open(url, '_blank');
    janelaImpressao.print();
    janelaImpressao.onafterprint = function () {
        janelaImpressao.close();
        URL.revokeObjectURL(url);
    };
}