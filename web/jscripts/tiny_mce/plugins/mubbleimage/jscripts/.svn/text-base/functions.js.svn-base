            var preloadImg;

            function init(){           
        	    var formObj = document.forms[0];
                var inst = tinyMCE.getInstanceById(tinyMCE.getWindowArg('editor_id'));
                var elm = inst.getFocusElement();
                var action = "insert";
                var html = "";

                // Check action
	            if (elm != null && elm.nodeName == "IMG")
		            action = "update";

                addClassesToList('classlist', 'mubble_image_styles');
                if (action == "update") {
                    /*var onClickText = tinyMCE.getAttrib(elm, 'onclick');
                    if(onClickText.indexOf('popFullSizeImage') >= 0){
                        formObj.popup.checked = true;
                    }*/
                
                    formObj.alt.value = tinyMCE.getAttrib(elm, 'alt');
                    selectByValue(formObj, 'classlist', tinyMCE.getAttrib(elm, 'class'));
                }
            }
            
            function sizeFieldBlur(field){
                var type = 'width';
                var previousValue = width;
                if(!isMubbleMedia) return;
                if(field.id == 'txtImageHeight'){
                    type = 'height';
                    previousValue = height;
                }
                
                var newSrc = rawMediaUrl;
                if(previousValue != field.value){
                    if(type == 'width'){
                        width = field.value;
                        newSrc = buildNewImageSrc(field.value, height);
                    }else{
                        height = field.value;
                        newSrc = buildNewImageSrc(width, field.value);
                    }
                    setPreview(newSrc);
                    rawMediaUrl = newSrc;                    
                }
            }
            
            function titleFieldBlur(){
                document.getElementById('PreviewImage').alt = document.getElementById('alt').value;
            }
            
            function buildNewImageSrc(newWidth, newHeight){
                if(newWidth > 0 && newHeight > 0){
                    src = baseMediaUrl + '/' + newWidth + '/' + newHeight + mediaFile;
                }else if(newWidth > 0){
                    src = baseMediaUrl + '/' + newWidth + mediaFile;
                }else if(newHeight > 0){
                    src = baseMediaUrl + '/1000/' + newHeight + mediaFile;
                }else{
                    src = baseMediaUrl + mediaFile;
                }
                //src = "http://" + host + src;
                //alert(src);
                return src;
            }
            
            function setPreview(src){
                preloadImg = new Image();
                tinyMCE.addEvent(preloadImg, "load", previewImageLoaded);
                preloadImg.src = src;
            }
            function previewImageLoaded(){              
                document.getElementById('PreviewImage').src = preloadImg.src;
            }
            
            function insertAction() {
	            var inst = tinyMCE.getInstanceById(tinyMCE.getWindowArg('editor_id'));
	            var elm = inst.getFocusElement();
	            var formObj = document.forms[0];
	            var imgWidth = parseInt( document.getElementById('txtImageWidth').value );
	            var imgHeight = parseInt( document.getElementById('txtImageHeight').value );
	            var src = document.getElementById('PreviewImage').src; 
	            if(isMubbleMedia){
	                buildNewImageSrc(imgWidth, imgHeight);
	            }

	            if (elm != null && elm.nodeName == "IMG") {
		            setAttrib(elm, 'src', convertURL(src, tinyMCE.imgElement));
		            setAttrib(elm, 'mce_src', src);
		            setAttrib(elm, 'alt');
		            setAttrib(elm, 'title');
		            setAttrib(elm, 'border');
		            setAttrib(elm, 'vspace');
		            setAttrib(elm, 'hspace');
		            setAttrib(elm, 'width');
		            setAttrib(elm, 'height');
		            setAttrib(elm, 'onmouseover');
		            setAttrib(elm, 'onmouseout');
		            setAttrib(elm, 'id');
		            setAttrib(elm, 'dir');
		            setAttrib(elm, 'lang');
		            setAttrib(elm, 'longdesc');
		            setAttrib(elm, 'usemap');
		            setAttrib(elm, 'style');
		            setAttrib(elm, 'class');
		            setAttrib(elm, 'class', getSelectValue(formObj, 'classlist'));
		            
		            if(formObj.popup.checked){
		                var fullSize = buildNewImageSrc(0,0);
		                setAttrib(elm, 'onclick', "popFullSizeImage('" + fullSize + "'); return false;");
		            }else{
		                setAttrib(elm, 'onclick', '');
		            }
		            
		            //setAttrib(elm, 'align', getSelectValue(formObj, 'align'));

		            //tinyMCEPopup.execCommand("mceRepaint");

		            inst.repaint();

		            // Refresh in old MSIE
		            if (tinyMCE.isMSIE5)
			            elm.outerHTML = elm.outerHTML;
	            } else {
		            var html = "<img";

		            html += makeAttrib('src', convertURL(src, tinyMCE.imgElement));
		            html += makeAttrib('mce_src', src);
		            html += makeAttrib('alt');
		            html += makeAttrib('title');
		            html += makeAttrib('border');
		            html += makeAttrib('vspace');
		            html += makeAttrib('hspace');
		            html += makeAttrib('width');
		            html += makeAttrib('height');
		            //html += makeAttrib('onmouseover', onmouseoversrc);
		            //html += makeAttrib('onmouseout', onmouseoutsrc);
		            html += makeAttrib('id');
		            html += makeAttrib('dir');
		            html += makeAttrib('lang');
		            html += makeAttrib('longdesc');
		            html += makeAttrib('usemap');
		            html += makeAttrib('class');
		            //html += makeAttrib('style');
		            html += makeAttrib('class', getSelectValue(formObj, 'classlist'));
		            
		            if(formObj.popup.checked){
		                var fullSize = buildNewImageSrc(0,0);
		                html += makeAttrib('onclick', "popFullSizeImage('" + fullSize + "'); return false;");
		            }
		            
		            //html += makeAttrib('align', getSelectValue(formObj, 'align'));
		            html += " />";

		            tinyMCEPopup.execCommand("mceInsertContent", false, html);
	            }

	            tinyMCE._setEventsEnabled(inst.getBody(), false);
	            tinyMCEPopup.close();
            }
            function convertURL(url, node, on_save) {
	            return eval("tinyMCEPopup.windowOpener." + tinyMCE.settings['urlconverter_callback'] + "(url, node, on_save);");
            }
            function setAttrib(elm, attrib, value) {
                var formObj = document.forms[0];
                var valueElm = formObj.elements[attrib];

                if (typeof(value) == "undefined" || value == null) {
	                value = "";

	                if (valueElm)
		                value = valueElm.value;
                }

                if (value != "") {
	                elm.setAttribute(attrib, value);

	                if (attrib == "style")
		                attrib = "style.cssText";

	                if (attrib == "longdesc")
		                attrib = "longDesc";

	                if (attrib == "width") {
		                attrib = "style.width";
		                value = value + "px";
	                }

	                if (attrib == "height") {
		                attrib = "style.height";
		                value = value + "px";
	                }

	                if (attrib == "class")
		                attrib = "className";

	                eval('elm.' + attrib + "=value;");
                } else
	                elm.removeAttribute(attrib);
            }

            function makeAttrib(attrib, value) {
                var formObj = document.forms[0];
                var valueElm = formObj.elements[attrib];

                if (typeof(value) == "undefined" || value == null) {
	                value = "";

	                if (valueElm && typeof(valueElm.value) != 'undefined')
		                value = valueElm.value;
                }

                if (value == "")
	                return "";

                // XML encode it
                value = value.replace(/&/g, '&amp;');
                value = value.replace(/\"/g, '&quot;');
                value = value.replace(/</g, '&lt;');
                value = value.replace(/>/g, '&gr;');

                return ' ' + attrib + '="' + value + '"';
            }
            function cancelAction() {
	            tinyMCEPopup.close();
            }