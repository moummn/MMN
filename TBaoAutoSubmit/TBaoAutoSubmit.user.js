// ==UserScript==
// @name        TBaoAutoSubmit
// @description 淘宝自动提交订单
// @namespace   tbas
// @include     *buy.taobao.com/auction/order/confirm_order.htm*
// @version     1
// @grant       none
// ==/UserScript==

var SubmitArea = document.getElementById("submitOrder_1");
var SubmitElem = SubmitArea.getElementsByTagName("a")[1];
//window.alert(SubmitElem.innerHTML);
SubmitElem.click();
