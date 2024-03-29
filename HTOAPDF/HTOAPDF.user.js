// ==UserScript==
// @name        HTOAPDF
// @description OA系统PDF页面修正
// @namespace   HTOAPDF
// @match       *223.100.175.23:8989/OAapp/HtTrace*
// @match       *mfile.123nat.com:8989/OAapp/HtTrace*
// @match       *oaxt.com/OAapp/HtTrace*
// @match       *oaxt.com:8989/OAapp/HtTrace*
// @match       *192.168.0.11:8989/OAapp/HtTrace*
// @match       *10.10.5.11/OAapp/HtTrace*
// @match       *10.10.5.11:8989/OAapp/HtTrace*
// @match       *mfile.123nat.com:8989/OAapp/HtTrace*
// @version     2022.05.28.0
// @grant       none
// @run-at      document-end
// @downloadURL https://github.com/moummn/MMN/raw/master/HTOAPDF/HTOAPDF.user.js
// @updateURL   https://github.com/moummn/MMN/raw/master/HTOAPDF/HTOAPDF.user.js
// ==/UserScript==

//获得基本信息
var pdfTable1 = document.getElementById("pdfTable");
var wb1 = document.getElementById("wb");
var src1;

//获得PDF地址
var AllScripts = document.getElementsByTagName("script");
var SP1;
var SP2;
for (var A = 0; A < AllScripts.length; A++) {
        var SubmitElem = AllScripts[A].innerHTML;
  			SP1=SubmitElem.indexOf("wb.src=hostAdd + \"") ;
        if (SP1 >= 0) {
          	src1=SubmitElem.substring(SP1+17,SubmitElem.length);
          	SP2=src1.indexOf(".pdf") ;
          	src1=src1.substring(1,SP2+4);
          	break;
        };
    };

//添加embed元素以显示pdf
var newEm = document.createElement("embed");
newEm.id = "pdfTable_embed";
pdfTable1.appendChild(newEm);
newEm.setAttribute("src", src1);
newEm.setAttribute("style", "width:100%;  height:800px;");
