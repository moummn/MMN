// ==UserScript==
// @name        JDAutoSubmit
// @description 京东自动提交订单
// @namespace   jdas
// @include     *cart.jd.com*cart_index*
// @version     2021.04.12.1
// @grant       none
// @run-at      document-end
// @downloadURL https://github.com/moummn/MMN/raw/master/JDAutoSubmit/JDAutoSubmit.user.js
// @updateURL   https://github.com/moummn/MMN/raw/master/JDAutoSubmit/JDAutoSubmit.user.js
// ==/UserScript==

//var t1 = setTimeout("location.reload()", 1000);
var t2 = setInterval(function () { fnAutoSubmit(); }, 100);
function fnAutoSubmit() {
    var AllA = document.getElementsByTagName("a");
    for (var A = 0; A < AllA.length; A++) {
        var SubmitElem = AllA[A].innerHTML;
        if (SubmitElem.indexOf("去结算") >= 0) {
            AllA[A].click();
        };
    };
};
