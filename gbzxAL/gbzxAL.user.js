// ==UserScript==
// @name        干部在线自动学习
// @namespace   gbzxal
// @include     *gbzx.dl.gov.cn/student*
// @version     2016.11.17
// @grant       GM_getValue
// @grant       GM_setValue
// @run-at      document-start
// @downloadURL https://
// @updateURL   https://
// ==/UserScript==

var thisURL = document.URL;
//window.alert(thisURL);
if (thisURL.indexOf("playNew")+thisURL.indexOf("playScorm")>0 ) {
	//var Player = document.getElementById("player");
	//Player.remove();
	window.close();
	//window.alert();
};


//写入Script
//var FNClickPlay = "";
//FNClickPlay +="function fnClickPlay() {\n";
//FNClickPlay +="var AllElemA = document.getElementById(\"right\");\n";
//FNClickPlay +="var PlayElem = AllElemA.getElementsByTagName(\"a\")[0];\n";
//FNClickPlay +="PlayElem.click();\n";
////FNClickPlay +="window.alert(ChElem.checked);\n";
//FNClickPlay +="};";
//var ElemScript =document.createElement("script")
//ElemScript.setAttribute("type","text/JavaScript");
//document.head.appendChild(ElemScript).textContent = FNClickPlay;

//载入时
var i1 = setInterval(function(){fnWaitEnd();},100);
function fnWaitEnd(){
	//window.alert(document.readyState)
	if (document.readyState=="complete"){
		
		if (thisURL.indexOf("scormList")>0){
			var AllElemA = document.getElementById("right");
      var PlayElem = AllElemA.getElementsByTagName("img")[1];
			PlayElem.click();
	    //window.alert(PlayElem.innerHTML);
	    //window.close();
    }
		else{
			fnAddButton();
		  fnCheckAutoLN();
		};
		
		
		
		window.clearInterval(i1)
	};
};

//应用脚本样式
function addStyle(css) {
	document.head.appendChild(document.createElement("style")).textContent = css;
};
//var AutoLNCSS= "";
//AutoLNCSS +="";
//addStyle(AutoLNCSS);

//添加元素
//function AddElementById()

//添加自动学习
function fnAddButton(){
var newEmParent = document.getElementsByTagName("div")[6];
newEmParent = newEmParent.getElementsByTagName("div")[0];
var newEm = document.createElement("span");
newEm.id = "AutoLN";
newEmParent.appendChild(newEm);

newEmParent = newEm
newEm = document.createElement("input");
newEm.id = "AutoLN_CHECK";
newEm.setAttribute("type","checkbox");
newEm.addEventListener('click', fnCheckChange, true);
//newEm.setAttribute("onclick","javascript:FNCheckChange()");
newEmParent.appendChild(newEm);
newEm.checked = GM_getValue("ALChecked");
newEm = document.createElement("label");
newEm.setAttribute("for","AutoLN_CHECK");
//newEm.setAttribute("onclick","javascript:StartLN()");
newEm.innerHTML = "开启自动学习";
newEmParent.appendChild(newEm);
};

//newEm.addEventListener('click', StartLN, true);


//自动学习的CSS样式
var AutoLNCSS = "";
AutoLNCSS += "#AutoLN { font-family: \"黑体\"; font-size: 24px; font-weight: bold; background-color: yellow; padding-left: 5px; padding-right: 5px;";
AutoLNCSS +="border-top-width: medium; border-right-width: medium; border-bottom-width: medium; border-left-width: medium; border-top-style: outset; border-right-style: outset; border-bottom-style: outset; border-left-style: outset;}"
addStyle(AutoLNCSS)



//点击Check事件
function fnCheckChange (){
	var ChElem = document.getElementById("AutoLN_CHECK");
	GM_setValue ("ALChecked",ChElem.checked);
//	window.alert(ChElem.innerHTML);
	if (ChElem.checked==true)
		{
			
			fnClickPlay();
			
			var t=setTimeout("location.reload()",5000);
			//location.reload();
		}
	else
		{
			
		}
};

function fnClickPlay (){
	var AllElemA = document.getElementById("right");
  var PlayElem = AllElemA.getElementsByTagName("a")[0];
	//window.alert(PlayElem.innerHTML);
		PlayElem.click();
	//window.alert("Clicked");
	
};



//检查是否开始学习
function fnCheckAutoLN(){
var ChElem = document.getElementById("AutoLN_CHECK");
//window.alert(ChElem.checked);
if (ChElem.checked==true)
{
	
	var t1 = setTimeout(function(){fnClickPlay();},2000);
	//fnClickPlay();
	var t2 = setTimeout("location.reload()",15000);
	//location.reload();
};
};
//window.alert(document.readyState);
//http://gbzx.dl.gov.cn/student/course!play
//var t=setTimeout("location.reload()",5000);



