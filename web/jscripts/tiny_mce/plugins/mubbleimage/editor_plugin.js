/* Import plugin specific language pack */
tinyMCE.importPluginLanguagePack('mubbleimage', 'en');

var TinyMCE_MubbleImagePlugin = {
    getInfo : function () {
        return {
		    longname : 'Mubble image',
		    author : 'Kurt',
		    authorurl : 'http://www.mrkurt.com',
		    infourl : 'http://trac.mrkurt.com',
		    version : '1.1'
	    };    
    },
        
    getControlHTML : function(cn){
		switch (cn) {
			case "image":
				return tinyMCE.getButtonHTML(cn, 'lang_image_desc', '{$themeurl}/images/image.gif', 'mceMubbleImage');
		}

		return "";    
    },
    
    execCommand : function(editor_id, element, command, user_interface, value){
        switch (command) {
		    case "mceMubbleImage":    		    
		        var popupUrl = tinyMCE.getParam("mubble_image_src", false);
    		    
		        if(popupUrl == false){
		            alert('No mubble image popup URL set.');
		            return false;
		        } 
		        
		        var inst = tinyMCE.getInstanceById(editor_id);
				var elm = inst.getFocusElement();
		        // Check action
	            if (elm != null && elm.nodeName == "IMG"){
		            popupUrl += '&src=' + escape(elm.src);
		        }
    		
			    // Show UI/Popup
			    // Open a popup window and send in some custom data in a window argument
			    var template = new Array();

			    template['file'] = popupUrl; 
			    template['width'] = 500;
			    template['height'] = 400;

			    tinyMCE.openWindow(template, {editor_id : editor_id, inline : "yes"});
			    return true;
	    }

	    return false;
    },

	cleanup : function(type, content) {
		switch (type) {
			case "insert_to_editor_dom":
				var imgs = content.getElementsByTagName("img");
				for (var i=0; i<imgs.length; i++) {
					var onmouseover = tinyMCE.cleanupEventStr(tinyMCE.getAttrib(imgs[i], 'onmouseover'));
					var onmouseout = tinyMCE.cleanupEventStr(tinyMCE.getAttrib(imgs[i], 'onmouseout'));

					if ((src = this._getImageSrc(onmouseover)) != "") {
						if (tinyMCE.getParam('convert_urls'))
							src = tinyMCE.convertRelativeToAbsoluteURL(tinyMCE.settings['base_href'], src);

						imgs[i].setAttribute('onmouseover', "this.src='" + src + "';");
					}

					if ((src = this._getImageSrc(onmouseout)) != "") {
						if (tinyMCE.getParam('convert_urls'))
							src = tinyMCE.convertRelativeToAbsoluteURL(tinyMCE.settings['base_href'], src);

						imgs[i].setAttribute('onmouseout', "this.src='" + src + "';");
					}
				}
				break;

			case "get_from_editor_dom":
				var imgs = content.getElementsByTagName("img");
				for (var i=0; i<imgs.length; i++) {
					var onmouseover = tinyMCE.cleanupEventStr(tinyMCE.getAttrib(imgs[i], 'onmouseover'));
					var onmouseout = tinyMCE.cleanupEventStr(tinyMCE.getAttrib(imgs[i], 'onmouseout'));

					if ((src = this._getImageSrc(onmouseover)) != "") {
						if (tinyMCE.getParam('convert_urls'))
							src = eval(tinyMCE.settings['urlconverter_callback'] + "(src, null, true);");

						imgs[i].setAttribute('onmouseover', "this.src='" + src + "';");
					}

					if ((src = this._getImageSrc(onmouseout)) != "") {
						if (tinyMCE.getParam('convert_urls'))
							src = eval(tinyMCE.settings['urlconverter_callback'] + "(src, null, true);");

						imgs[i].setAttribute('onmouseout', "this.src='" + src + "';");
					}
				}
				break;
		}

		return content;
	},
	
	handleNodeChange : function(editor_id, node, undo_index, undo_levels, visual_aid, any_selection) {
		if (node == null)
			return;

		do {
			if (node.nodeName == "IMG" && tinyMCE.getAttrib(node, 'class').indexOf('mceItem') == -1) {
				tinyMCE.switchClass(editor_id + '_advimage', 'mceButtonSelected');
				return true;
			}
		} while ((node = node.parentNode));

		tinyMCE.switchClass(editor_id + '_advimage', 'mceButtonNormal');

		return true;
	},

	/**
	 * Returns the image src from a scripted mouse over image str.
	 *
	 * @param {string} s String to get real src from.
	 * @return Image src from a scripted mouse over image str.
	 * @type string
	 */
	_getImageSrc : function(s) {
		var sr, p = -1;

		if (!s)
			return "";

		if ((p = s.indexOf('this.src=')) != -1) {
			sr = s.substring(p + 10);
			sr = sr.substring(0, sr.indexOf('\''));

			return sr;
		}

		return "";
	}
};

tinyMCE.addPlugin("mubbleimage", TinyMCE_MubbleImagePlugin);