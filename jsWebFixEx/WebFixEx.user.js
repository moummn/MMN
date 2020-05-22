// ==UserScript==
// @name     WebFixEx
// @description 修正网页问题
// @namespace   WebFixEx
// @include     *www.moon-soft.com/book/*
// @version  2020.05.22.0
// @grant    none
// @run-at      document-end
// @downloadURL https://raw.githubusercontent.com/moummn/MMN/master/jsWebFixEx/WebFixEx.user.js
// @updateURL   https://raw.githubusercontent.com/moummn/MMN/master/jsWebFixEx/WebFixEx.user.js
// ==/UserScript==

//判断网页网址
var thisURL = document.URL;
if (thisURL.indexOf("www.moon-soft.com/book/") > 0) {
    var newStyle = "table[width=\"673\"]{margin:auto;}";
    addStyle(newStyle);
};


//应用样式
function addStyle(newStyle) {
    var styleElement = document.createElement('style');
    styleElement.type = 'text/css';
    //styleElement.id = 'styles_js';
    document.getElementsByTagName('head')[0].appendChild(styleElement);
    styleElement.appendChild(document.createTextNode(newStyle));
};