// ==UserScript==
// @name        HTOAPDF
// @description OA系统PDF页面修正
// @namespace   HTOAPDF
// @include     *223.100.151.35:8989/OAapp/HtTrace*
// @version     2017.06.18.3
// @grant       none
// @run-at      document-end
// @downloadURL https://github.com/moummn/MMN/raw/master/HTOAPDF/HTOAPDF.js
// @updateURL   https://github.com/moummn/MMN/raw/master/HTOAPDF/HTOAPDF.js
// ==/UserScript==

//获得PDF地址
var wb1 = document.getElementById("wb");
var src1 = wb1.src;
//window.alert(src1);

//添加embed元素以显示pdf
var pdfTable1 = document.getElementById("pdfTable");
var newEm = document.createElement("embed");
newEm.id = "pdfTable_embed";
pdfTable1.appendChild(newEm);
newEm.setAttribute("src", src1);
newEm.setAttribute("style", "width:100%;  height:800px;");
