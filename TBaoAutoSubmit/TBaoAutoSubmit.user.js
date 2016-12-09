// ==UserScript==
// @name        TBaoAutoSubmit
// @description 淘宝自动提交订单
// @namespace   tbas
// @include     *buy.taobao.com*confirm_order.htm*
// @include     *buy.tmall.com*confirm_order.htm*
// @version     2016.12.09.1
// @grant       none
// @run-at      document-end
// @downloadURL https://github.com/moummn/MMN/raw/master/TBaoAutoSubmit/TBaoAutoSubmit.user.js
// @updateURL   https://github.com/moummn/MMN/raw/master/TBaoAutoSubmit/TBaoAutoSubmit.user.js
// ==/UserScript==

var t = setTimeout("location.reload()", 1000);
var SubmitArea = document.getElementById("submitOrder_1");
var SubmitElem = SubmitArea.getElementsByTagName("a")[1];
//window.alert(SubmitElem.innerHTML);
SubmitElem.click();
