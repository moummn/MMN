// ==UserScript==
// @name        TBaoAutoSubmit
// @description 淘宝自动提交订单
// @namespace   tbas
// @include     *buy.taobao.com*confirm_order.htm*
// @include     *buy.taobao.com*buy_now.jhtml*
// @include     *buy.tmall.com*confirm_order.htm*
// @include     *buy.tmall.com*buy_now.jhtml*
// @version     2016.12.09.2
// @grant       none
// @run-at      document-end
// @downloadURL https://github.com/moummn/MMN/raw/master/TBaoAutoSubmit/TBaoAutoSubmit.user.js
// @updateURL   https://github.com/moummn/MMN/raw/master/TBaoAutoSubmit/TBaoAutoSubmit.user.js
// ==/UserScript==

var t = setTimeout("location.reload()", 1000);
var AllA = document.getElementsByTagName("a");
for (var A = 0; A < AllA.length; A++) {
    var SubmitElem = AllA[A];
    if (SubmitElem.innerHTML == "提交订单") {
        SubmitElem.click();
    };
};


