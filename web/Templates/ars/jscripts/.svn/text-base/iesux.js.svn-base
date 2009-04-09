var iesux = {
    stupid_highlight_bug : function(e){ document.body.style.height = document.documentElement.scrollHeight+'px'; },
    stupid_sticky_positioning : function(e){ var body = document.body; body.style.display = 'none';body.style.display = 'block'; },
    fix_stupid_bugs: function(){ iesux.stupid_highlight_bug(); iesux.stupid_sticky_positioning(); $('#Sidebar').show(); document.recalc(); }
}
$(window).resize(iesux.stupid_sticky_positioning);
$(window).load(iesux.stupid_highlight_bug);
Discussion.LayoutChangedCallback = function(){ iesux.fix_stupid_bugs(); } 