// ==UserScript==
// @name        WEBRefresh
// @namespace   WEBRefresh
// @description 自定义刷新网页
// @version     2017.08.31.3
// @include     *www.natbbs.com*
// @grant       none
// @run-at      document-end
// @downloadURL https://github.com/moummn/MMN/raw/master/UserJS/WEBRefresh.user.js
// @updateURL   https://github.com/moummn/MMN/raw/master/UserJS/WEBRefresh.user.js
// ==/UserScript==

//检测网页的URL，根据URL判断模式
var thisURL = document.URL;
if (thisURL.indexOf("www.natbbs.com") > 0) {
    var t = setTimeout("location.reload()", 60000);
    //window.close();
};
