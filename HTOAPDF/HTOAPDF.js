// ==UserScript==
// @name        HTOAPDF
// @namespace   HTOAPDF
// @description OAϵͳPDFҳ������
// @include     *223.100.151.35:8989/OAapp/HtTrace*
// @version     2017.06.18.0
// @grant       none
// ==/UserScript==

//���PDF��ַ
var wb1 = document.getElementById("wb");
var src1 = wb1.src;
//window.alert(src1);

//���embedԪ������ʾpdf
var pdfTable1 = document.getElementById("pdfTable");
var newEm = document.createElement("embed");
newEm.id = "pdfTable_embed";
pdfTable1.appendChild(newEm);
newEm.setAttribute("src", src1);
newEm.setAttribute("style", "width:100%;  height:800px;");
