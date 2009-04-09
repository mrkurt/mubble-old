function preinit(){
    tinyMCE.setWindowArg('mce_windowresize', true);
    
    var url = tinyMCE.getParam('snippet_list_url');
    if(url != null){
        if(url.charAt(0) != '/' && url.indexOf('://') == -1)
            url = tinyMCE.documentBasePath + "/" + url;
    }else{
        url = 'jscripts/default_snippets.js';
    }
    document.write('<sc'+'ript language="javascript" type="text/javascript" src="' + url + '"></sc'+'ript>');
}

function init(){
    tinyMCEPopup.resizeToInnerSize();
    
    var formObj = document.forms[0];
    var inst = tinyMCE.getInstanceById(tinyMCE.getWindowArg('editor_id'));
    var html = "";
    
    html = getSnippetListHTML('snippetlist');

    if(html == "")
        document.getElementById('snippetlistrow').style.display = 'none';
    else
        document.getElementById('snippetlistcontainer').innerHTML = html;
   
}

function insertAction(){
    var html = "";
    var select = document.getElementById('snippetlist');
    
    var index = select.options[select.selectedIndex].value;
    html = tinyMCESnippetList[index][1];
    
    tinyMCEPopup.execCommand("mceInsertContent", false, html);
	
	tinyMCEPopup.close();
}

function cancelAction() {
	tinyMCEPopup.close();
}

function getSnippetListHTML(elm_id){
    if(typeof(tinyMCESnippetList) == "undefined" || tinyMCESnippetList.length == 0)
        return "";
        
    var html = "";
    
    html += '<select id="' + elm_id + '" name="' + elm_id + '"';
	html += ' class="mceSnippetList" onfocus="tinyMCE.addSelectAccessibility(event, this, window);" onchange="showSnippetPreview(this);"';

	html += '><option value="">---</option>';

	for (var i=0; i<tinyMCESnippetList.length; i++)
		html += '<option value="' + i + '">' + tinyMCESnippetList[i][0] + '</option>';

	html += '</select>';

	return html;    
}

function showSnippetPreview(select){
    var index = select.options[select.selectedIndex].value;
    var html = tinyMCESnippetList[index][1];
    var insertButton = document.getElementById('insert');
    
    document.frames[0].document.body.innerHTML = html;
    
    if(html != ""){
        insertButton.disabled = false;
    }else{
        insertButton.disabled = true;
    }
}

preinit();