// ==UserScript==
// @name         BilibiliWideScreenFix
// @namespace    BilibiliWideScreenFix
// @version      2022.05.26.0
// @description  Bilibili宽屏修正
// @author       M
// @match      *.bilibili.com/video/av*
// @match      *.bilibili.com/video/BV*
// @grant        none
// @run-at       document-end
// @downloadURL  https://raw.githubusercontent.com/moummn/MMN/master/BilibiliWideScreenFix/BilibiliWideScreenFix.user.js
// @updateURL    https://raw.githubusercontent.com/moummn/MMN/master/BilibiliWideScreenFix/BilibiliWideScreenFix.user.js
// ==/UserScript==

//获得基本信息
//window.alert("OK");

var t1 = setTimeout(function () { fnFix(); }, 5000);
function fnFix(){
    var VWrapClass = document.getElementsByClassName("video-container-v1");
    var FixContent = VWrapClass[0];
    FixContent.setAttribute('style','padding-left: 0px;padding-right: 100px;');
};