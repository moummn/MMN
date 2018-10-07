// ==UserScript==
// @name         BilibiliWideScreenFix
// @namespace    BilibiliWideScreenFix
// @version      2018.10.07.0
// @description  Bilibili宽屏修正
// @author       M
// @match        *.bilibili.com/video/av*
// @include      *.bilibili.com/video/av*
// @grant        none
// @run-at      document-end
// @downloadURL  none
// @updateURL    none
// ==/UserScript==

//获得基本信息
//window.alert("OK");

var t1 = setTimeout(function () { fnFix(); }, 2000);
function fnFix(){
var VWrapClass = document.getElementsByClassName("v-wrap");
var FixContent = VWrapClass[0];
FixContent.setAttribute('style','width: 100%');
};