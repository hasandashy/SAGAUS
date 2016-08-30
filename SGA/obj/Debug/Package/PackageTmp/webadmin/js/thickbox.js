/*
 * Thickbox 3.1 - One Box To Rule Them All.
 * By Cody Lindley (http://www.codylindley.com)
 * Copyright (c) 2007 cody lindley
 * Licensed under the MIT License: http://www.opensource.org/licenses/mit-license.php
*/
		  


/*!!!!!!!!!!!!!!!!! edit below this line at your own risk !!!!!!!!!!!!!!!!!!!!!!!*/

// Mohammad Husain -- Extra syntax variable for jquery has been added, for removing conflication
var jqueryInstance;

//add thickbox to href & area elements that have a class of .thickbox
function tb_init(domChunk){
   
	jqueryInstance(domChunk).click(function(){
	var t = this.title || this.name || null;
	var a = this.href || this.alt;
	var g = this.rel || false;
	tb_show(t,a,g);
	this.blur();
	return false;
	});
}

function tb_show(caption, url, imageGroup) {//function called when the user clicks on a thickbox link
	try {
		if (typeof document.body.style.maxHeight === "undefined") {//if IE 6
			jqueryInstance("body","html").css({height: "100%", width: "100%"});
			jqueryInstance("html").css("overflow","hidden");
			if (document.getElementById("TB_HideSelect") === null) {//iframe to hide select elements in ie6
				jqueryInstance("body").append("<iframe id='TB_HideSelect'></iframe><div id='TB_overlay'></div><div id='TB_window'></div>");
				jqueryInstance("#TB_overlay").click(tb_remove);
			}
		}else{//all others
			if(document.getElementById("TB_overlay") === null){
				jqueryInstance("body").append("<div id='TB_overlay'></div><div id='TB_window'></div>");
				jqueryInstance("#TB_overlay").click(tb_remove);
			}
		}
		
		if(tb_detectMacXFF()){
			jqueryInstance("#TB_overlay").addClass("TB_overlayMacFFBGHack");//use png overlay so hide flash
		}else{
			jqueryInstance("#TB_overlay").addClass("TB_overlayBG");//use background and opacity
		}
		
		if(caption===null){caption="";}
		jqueryInstance("body").append("<div id='TB_load'><img src='"+imgLoader.src+"' /></div>");//add loader to the page
		jqueryInstance('#TB_load').show();//show loader
		
		var baseURL;
	   if(url.indexOf("?")!==-1){ //ff there is a query string involved
			baseURL = url.substr(0, url.indexOf("?"));
	   }else{ 
	   		baseURL = url;
	   }
	   
	   var urlString = /\.jpgjqueryInstance|\.jpegjqueryInstance|\.pngjqueryInstance|\.gifjqueryInstance|\.bmpjqueryInstance/;
	   var urlType = baseURL.toLowerCase().match(urlString);

		if(urlType == '.jpg' || urlType == '.jpeg' || urlType == '.png' || urlType == '.gif' || urlType == '.bmp'){//code to show images
				
			TB_PrevCaption = "";
			TB_PrevURL = "";
			TB_PrevHTML = "";
			TB_NextCaption = "";
			TB_NextURL = "";
			TB_NextHTML = "";
			TB_imageCount = "";
			TB_FoundURL = false;
			if(imageGroup){
				TB_TempArray = jqueryInstance("a[@rel="+imageGroup+"]").get();
				for (TB_Counter = 0; ((TB_Counter < TB_TempArray.length) && (TB_NextHTML === "")); TB_Counter++) {
					var urlTypeTemp = TB_TempArray[TB_Counter].href.toLowerCase().match(urlString);
						if (!(TB_TempArray[TB_Counter].href == url)) {						
							if (TB_FoundURL) {
								TB_NextCaption = TB_TempArray[TB_Counter].title;
								TB_NextURL = TB_TempArray[TB_Counter].href;
								TB_NextHTML = "<span id='TB_next'>&nbsp;&nbsp;<a href='#'>Next &gt;</a></span>";
							} else {
								TB_PrevCaption = TB_TempArray[TB_Counter].title;
								TB_PrevURL = TB_TempArray[TB_Counter].href;
								TB_PrevHTML = "<span id='TB_prev'>&nbsp;&nbsp;<a href='#'>&lt; Prev</a></span>";
							}
						} else {
							TB_FoundURL = true;
							TB_imageCount = "Image " + (TB_Counter + 1) +" of "+ (TB_TempArray.length);											
						}
				}
			}

			imgPreloader = new Image();
			imgPreloader.onload = function() {
			    imgPreloader.onload = null;

			    // Resizing large images - orginal by Christian Montoya edited by me.
			    var pagesize = tb_getPageSize();
			    var x = pagesize[0] - 150;
			    var y = pagesize[1] - 150;
			    var imageWidth = imgPreloader.width;
			    var imageHeight = imgPreloader.height;
			    if (imageWidth > x) {
			        imageHeight = imageHeight * (x / imageWidth);
			        imageWidth = x;
			        if (imageHeight > y) {
			            imageWidth = imageWidth * (y / imageHeight);
			            imageHeight = y;
			        }
			    } else if (imageHeight > y) {
			        imageWidth = imageWidth * (y / imageHeight);
			        imageHeight = y;
			        if (imageWidth > x) {
			            imageHeight = imageHeight * (x / imageWidth);
			            imageWidth = x;
			        }
			    }
			    // End Resizing

			    TB_WIDTH = imageWidth + 30;
			    TB_HEIGHT = imageHeight + 60;
			    jqueryInstance("#TB_window").append("<a href='' id='TB_ImageOff' title='Close'><img id='TB_Image' src='" + url + "' width='" + imageWidth + "' height='" + imageHeight + "' alt='" + caption + "'/></a>" + "<div id='TB_caption'>" + caption + "<div id='TB_secondLine'>" + TB_imageCount + TB_PrevHTML + TB_NextHTML + "</div></div><div id='TB_closeWindow'><a href='#' id='TB_closeWindowButton' title='Close'>close</a></div>");

			    jqueryInstance("#TB_closeWindowButton").click(tb_remove);

			    if (!(TB_PrevHTML === "")) {
			        function goPrev() {
			            if (jqueryInstance(document).unbind("click", goPrev)) { jqueryInstance(document).unbind("click", goPrev); }
			            jqueryInstance("#TB_window").remove();
			            jqueryInstance("body").append("<div id='TB_window'></div>");
			            tb_show(TB_PrevCaption, TB_PrevURL, imageGroup);
			            return false;
			        }
			        jqueryInstance("#TB_prev").click(goPrev);
			    }

			    if (!(TB_NextHTML === "")) {
			        function goNext() {
			            jqueryInstance("#TB_window").remove();
			            jqueryInstance("body").append("<div id='TB_window'></div>");
			            tb_show(TB_NextCaption, TB_NextURL, imageGroup);
			            return false;
			        }
			        jqueryInstance("#TB_next").click(goNext);

			    }

			    document.onkeydown = function(e) {
			        if (e == null) { // ie
			            keycode = event.keyCode;
			        } else { // mozilla
			            keycode = e.which;
			        }
			        if (keycode == 27) { // close
			            tb_remove();
			        } else if (keycode == 190) { // display previous image
			            if (!(TB_NextHTML == "")) {
			                document.onkeydown = "";
			                goNext();
			            }
			        } else if (keycode == 188) { // display next image
			            if (!(TB_PrevHTML == "")) {
			                document.onkeydown = "";
			                goPrev();
			            }
			        }
			    };

			    tb_position();
			    jqueryInstance("#TB_load").remove();
			    jqueryInstance("#TB_ImageOff").click(tb_remove);
			    jqueryInstance("#TB_window").css({ display: "block" }); //for safari using css instead of show
			};
			
			imgPreloader.src = url;
		}else{//code to show html
			
			var queryString = url.replace(/^[^\?]+\??/,'');
			var params = tb_parseQuery( queryString );

			TB_WIDTH = (params['width']*1) + 30 || 630; //defaults to 630 if no paramaters were added to URL
			TB_HEIGHT = (params['height']*1) + 40 || 440; //defaults to 440 if no paramaters were added to URL
			ajaxContentW = TB_WIDTH - 30;
			ajaxContentH = TB_HEIGHT - 45;
			
			if(url.indexOf('TB_iframe') != -1){// either iframe or ajax window		
					urlNoQuery = url.split('TB_');
					jqueryInstance("#TB_iframeContent").remove();
					if(params['modal'] != "true"){//iframe no modal
					    jqueryInstance("#TB_window").append("<div id='TB_title'><div id='TB_ajaxWindowTitle'></div><div id='TB_closeAjaxWindow'><a href='#' id='TB_closeWindowButton' title='Close'>close</a></div></div><iframe frameborder='0' hspace='0' src='" + urlNoQuery[0] + "' id='TB_iframeContent' name='TB_iframeContent" + Math.round(Math.random() * 1000) + "' onload='tb_showIframe()' style='width:" + (ajaxContentW + 29) + "px;height:" + (ajaxContentH + 17) + "px;' > </iframe>");
						//jqueryInstance("#TB_window").append("<div id='TB_title'><div id='TB_ajaxWindowTitle'>"+caption+"</div><div id='TB_closeAjaxWindow'><a href='#' id='TB_closeWindowButton' title='Close'>close</a></div></div><iframe frameborder='0' hspace='0' src='"+urlNoQuery[0]+"' id='TB_iframeContent' name='TB_iframeContent"+Math.round(Math.random()*1000)+"' onload='tb_showIframe()' style='width:"+(ajaxContentW + 29)+"px;height:"+(ajaxContentH + 17)+"px;' > </iframe>");
					}else{//iframe modal
					jqueryInstance("#TB_overlay").unbind();
						jqueryInstance("#TB_window").append("<iframe frameborder='0' hspace='0' src='"+urlNoQuery[0]+"' id='TB_iframeContent' name='TB_iframeContent"+Math.round(Math.random()*1000)+"' onload='tb_showIframe()' style='width:"+(ajaxContentW + 29)+"px;height:"+(ajaxContentH + 17)+"px;'> </iframe>");
					}
			}else{// not an iframe, ajax
					if(jqueryInstance("#TB_window").css("display") != "block"){
						if(params['modal'] != "true"){//ajax no modal
						jqueryInstance("#TB_window").append("<div id='TB_title'><div id='TB_ajaxWindowTitle'>"+caption+"</div><div id='TB_closeAjaxWindow'><a href='#' id='TB_closeWindowButton'>close</a></div></div><div id='TB_ajaxContent' style='width:"+ajaxContentW+"px;height:"+ajaxContentH+"px'></div>");
						}else{//ajax modal
						jqueryInstance("#TB_overlay").unbind();
						jqueryInstance("#TB_window").append("<div id='TB_ajaxContent' class='TB_modal' style='width:"+ajaxContentW+"px;height:"+ajaxContentH+"px;'></div>");	
						}
					}else{//this means the window is already up, we are just loading new content via ajax
						jqueryInstance("#TB_ajaxContent")[0].style.width = ajaxContentW +"px";
						jqueryInstance("#TB_ajaxContent")[0].style.height = ajaxContentH +"px";
						jqueryInstance("#TB_ajaxContent")[0].scrollTop = 0;
						jqueryInstance("#TB_ajaxWindowTitle").html(caption);
					}
			}
					
			jqueryInstance("#TB_closeWindowButton").click(tb_remove);

			if (url.indexOf('TB_inline') != -1) {
			    jqueryInstance("#TB_ajaxContent").append(jqueryInstance('#' + params['inlineId']).children()); 
					jqueryInstance("#TB_window").unload(function () {
						jqueryInstance('#' + params['inlineId']).append( jqueryInstance("#TB_ajaxContent").children() ); // move elements back when you're finished
					});
					tb_position();
					jqueryInstance("#TB_load").remove();
					jqueryInstance("#TB_window").css({display:"block"}); 
				}else if(url.indexOf('TB_iframe') != -1){
					tb_position();
					if(jqueryInstance.browser.safari){//safari needs help because it will not fire iframe onload
						jqueryInstance("#TB_load").remove();
						jqueryInstance("#TB_window").css({display:"block"});
					}
				}else{
					jqueryInstance("#TB_ajaxContent").load(url += "&random=" + (new Date().getTime()),function(){//to do a post change this load method
						tb_position();
						jqueryInstance("#TB_load").remove();
						tb_init("#TB_ajaxContent a.thickbox");
						jqueryInstance("#TB_window").css({display:"block"});
					});
				}
			
		}

		if(!params['modal']){
			document.onkeyup = function(e){ 	
				if (e == null) { // ie
					keycode = event.keyCode;
				} else { // mozilla
					keycode = e.which;
				}
				if(keycode == 27){ // close
					tb_remove();
				}	
			};
		}
		
	} catch(e) {
		//nothing here
	}
}

//helper functions below
function tb_showIframe(){
	jqueryInstance("#TB_load").remove();
	jqueryInstance("#TB_window").css({display:"block"});
}

function tb_remove() {
   	jqueryInstance("#TB_imageOff").unbind("click");
	jqueryInstance("#TB_closeWindowButton").unbind("click");
	jqueryInstance("#TB_window").fadeOut("fast",function(){jqueryInstance('#TB_window,#TB_overlay,#TB_HideSelect').trigger("unload").unbind().remove();});
	if (typeof document.body.style.maxHeight == "undefined") {//if IE 6
		jqueryInstance("body","html").css({height: "auto", width: "auto"});
		jqueryInstance("html").css("overflow","");
	}
	document.onkeydown = "";
	document.onkeyup = "";
	return false;
}

function tb_position() {
jqueryInstance("#TB_window").css({marginLeft: '-' + parseInt((TB_WIDTH / 2),10) + 'px', width: TB_WIDTH + 'px'});
	if ( !(jQuery.browser.msie && jQuery.browser.version < 7)) { // take away IE6
		jqueryInstance("#TB_window").css({marginTop: '-' + parseInt((TB_HEIGHT / 2),10) + 'px'});
	}
}

function tb_parseQuery ( query ) {
   var Params = {};
   if ( ! query ) {return Params;}// return empty object
   var Pairs = query.split(/[;&]/);
   for ( var i = 0; i < Pairs.length; i++ ) {
      var KeyVal = Pairs[i].split('=');
      if ( ! KeyVal || KeyVal.length != 2 ) {continue;}
      var key = unescape( KeyVal[0] );
      var val = unescape( KeyVal[1] );
      val = val.replace(/\+/g, ' ');
      Params[key] = val;
   }
   return Params;
}

function tb_getPageSize(){
	var de = document.documentElement;
	var w = window.innerWidth || self.innerWidth || (de&&de.clientWidth) || document.body.clientWidth;
	var h = window.innerHeight || self.innerHeight || (de&&de.clientHeight) || document.body.clientHeight;
	arrayPageSize = [w,h];
	return arrayPageSize;
}

function tb_detectMacXFF() {
  var userAgent = navigator.userAgent.toLowerCase();
  if (userAgent.indexOf('mac') != -1 && userAgent.indexOf('firefox')!=-1) {
    return true;
  }
}


