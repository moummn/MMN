// ==UserScript==
// @name     WebFixEx
// @description ������ҳ����
// @namespace   WebFixEx
// @include     *www.moon-soft.com/book/*
// @version  2020.05.22.0
// @grant    none
// @run-at      document-end
// ==/UserScript==

//�ж���ҳ��ַ
var thisURL = document.URL;
if (thisURL.indexOf("www.moon-soft.com/book/") > 0) {
    var newStyle = "table[width=\"673\"]{margin:auto;}";
    addStyle(newStyle);
};


//Ӧ����ʽ
function addStyle(newStyle) {
    var styleElement = document.createElement('style');
    styleElement.type = 'text/css';
    //styleElement.id = 'styles_js';
    document.getElementsByTagName('head')[0].appendChild(styleElement);
    styleElement.appendChild(document.createTextNode(newStyle));
};