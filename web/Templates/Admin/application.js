var Permissions = {
    SrcJson: window.location.href.replace(Mubble.AdminHandler, '.permissions' + Mubble.JsonHandler).replace(/\.permissions\.([^\/]*).*/,'.permissions.$1'),
    List: null,
    CurrentGroup: null,
    Current: Array(),
    Bind: function(e){
        var checks = document.getElementsBySelector("input.permissions-checkbox");
        for(i = 0; i < checks.length; i++){
            Event.observe(checks[i], 'click', Permissions.Change);
        }
        var selectors = document.getElementsBySelector("select.permissions-group-selector");
        for(i = 0; i < selectors.length; i++){
            Event.observe(selectors[i], 'change', Permissions.GroupChange);
        }        
        new Ajax.Request(Permissions.SrcJson + '/list', {onSuccess: Permissions.Load});
    },
    Load: function(res){
        Permissions.List = eval('(' + res.responseText + ')');
        var selectors = document.getElementsBySelector("select.permissions-group-selector");
        for(i = 0; i < selectors.length; i++){
            Permissions.LoadChecks(selectors[i].value);
        }
    },
    Change: function(e){
        var flag = Event.element(e);
        var postBody = "Group=" + escape(Permissions.CurrentGroup) + "&Flag=" + escape(flag.value) + "&Enabled=" + flag.checked;
        new Ajax.Request(Permissions.SrcJson + "/update", {method: 'post', postBody: postBody, onSuccess: Permissions.Load});
    },
    GroupChange: function(e){
        Permissions.LoadChecks(Event.element(e).value);        
    },
    LoadChecks: function(group){
        if(Permissions.List == null) return;
        Permissions.CurrentGroup = group;
        Permissions.Current = [];
        for(i = 0; i < Permissions.List.Data.length; i++){
            if(group == Permissions.List.Data[i].Group){
             Permissions.Current[Permissions.List.Data[i].Flag] = true;
            }
        }
        
        var checks = document.getElementsBySelector("input.permissions-checkbox");
        for(i = 0; i < checks.length; i++){
            checks[i].checked = Permissions.Current[checks[i].value];
        }  
    }
}
Event.observe(window, 'load', Permissions.Bind);