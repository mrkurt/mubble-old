/* Import plugin specific language pack */
tinyMCE.importPluginLanguagePack('template', 'en,es,'); // <- Add a comma separated list of all supported languages

/**
 * Information about the plugin.
 */
function TinyMCE_tokens_getInfo() {
	return {
		longname: 'tokens plugin',
		author: 'Andrew Northcutt',
		authorurl: '',
		infourl: '',
		version: '1.0'
	};
};

/**
 * Gets executed when a editor needs to generate a button.
 */
function TinyMCE_tokens_getControlHTML(control_name) {
	switch (control_name) {
		case "tokens":
      var tokenString = tinyMCE.getParam('tokens_token_list', '', false);
			var cmd = "tinyMCE.execInstanceCommand('{$editor_id}','mceTokens', true, this.value);return false;";

      var contentTxt = '<select id="tinymce_token_id" onchange="'+cmd+'">';
      contentTxt += '<option value="">Select Token</option>';
      var tokens = tokenString.split(';');
      for (var i=0; i < tokens.length; i++) {
        if (/=/.test(tokens[i])) {
          key_val = tokens[i].split('=');
          contentTxt += '<option value="'+key_val[1]+'">'+key_val[0]+'</option>';
        }
      }
      contentTxt += '</select>';
      return contentTxt;
	}

	return "";
}

/**
 * Gets executed when a command is called.
 */
function TinyMCE_tokens_execCommand(editor_id, element, command, user_interface, value) {
	switch (command) {
		case "mceTokens":
      tinyMCE.execCommand('mceInsertContent', false, value);
      var tokenSel = document.getElementById('tinymce_token_id');
      tokenSel.selectedIndex = 0;
			return true;
	}
	// Pass to next handler in chain
	return false;
}
