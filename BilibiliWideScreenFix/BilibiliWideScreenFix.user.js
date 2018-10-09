// ==UserScript==
// @name         BilibiliWideScreenFix
// @namespace    BilibiliWideScreenFix
// @version      2018.10.09.0
// @description  Bilibili宽屏修正
// @author       M
// @match        *.bilibili.com/video/av*
// @include      *.bilibili.com/video/av*
// @grant        none
// @run-at       document-end
// @downloadURL  https://github.com/moummn/MMN/raw/master/BilibiliWideScreenFix/BilibiliWideScreenFix.user.js
// @updateURL    https://github.com/moummn/MMN/raw/master/BilibiliWideScreenFix/BilibiliWideScreenFix.user.js
// ==/UserScript==

//获得基本信息
//window.alert("OK");

var t1 = setTimeout(function () { fnFix(); }, 5000);
function fnFix(){
var VWrapClass = document.getElementsByClassName("v-wrap");
var FixContent = VWrapClass[0];
FixContent.setAttribute('style','width: 100%; padding: 0px 5px;');
};