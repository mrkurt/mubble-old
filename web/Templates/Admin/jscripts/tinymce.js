    tinyMCE.init({
	    mode : "textareas",
	    editor_selector : "wysiwyg",
	    plugins : "iespell, paste, mubbleimage",
	    auto_cleanup_word : true,
	    theme : "advanced",
	    content_css : mceContentCss,
	    editor_css : mceEditorCss,
	    mubble_image_src : mceMubbleImagePopup,
	    mubble_image_styles : 'None=;Left=ImageLeft;Right=ImageRight',
	    relative_urls : false,
	    remove_script_host : false,
	    apply_source_formatting : true,
	    theme_advanced_buttons1 : "pasteword,separator,formatselect,separator,bold,italic,underline,separator",
	    theme_advanced_buttons1_add: "bullist,numlist,outdent,indent,separator,link,unlink,image,separator,undo,redo,iespell,code",
	    theme_advanced_buttons2 : "",
	    theme_advanced_buttons3 : "",
	    theme_advanced_toolbar_location : "top",
	    theme_advanced_toolbar_align : "left",
	    theme_advanced_path_location : "none",
	    theme_advanced_blockformats: "p,h1,h2,h3,h4",
	    paste_auto_cleanup_on_paste : true,
        paste_convert_middot_lists : true,
        paste_unindented_list_class : "",
	    extended_valid_elements : "a[class|name|href|title],img[class|src|alt|title|onclick],hr[class],span[class|style],style[type],more"
    });