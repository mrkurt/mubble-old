ReplaceTwitterLinks : function() {
	$('a.twitter').each(Site.InsertTwitterFrame);
},
InsertTwitterFrame : function(index){
	var options = {};
	var $this = jQuery(this);
	var href = $this.attr('href');
	var twitter_user = href.match(/http:\/\/twitter.com\/(\S+)/)[1];
    var cls = this.className;

    var opts = jQuery.extend({
        frameborder:  ((cls.match(/fb:(\d+)/)||[])[1]) || 0,
        marginwidth:  ((cls.match(/wm:(\d+)/)||[])[1]) || 0,
        marginheight: ((cls.match(/hm:(\d+)/)||[])[1]) || 0,
        width:        ((cls.match(/w:(\d+)/)||[])[1]) || 580,
        height:       ((cls.match(/h:(\d+)/)||[])[1]) || "1050",
        scrolling:    ((cls.match(/sc:(\w+)/)||[])[1]) || "auto",
        version:     '1,0,0,0',
        cls:          cls,
        src:          'http://arstechnica.com/Templates/ars/twitter/frame.html#' + twitter_user,
		id:			  'twitter_' + twitter_user,
        caption:      $this.text(),
        attrs:        {},
        elementType:  'div',
        xhtml:        false
    }, options || {});

    var endTag = opts.xhtml ? ' />' : '>';

    var a = ['<iframe src="' + opts.src + '"'];
	if(opts.id){
		a.push(' id="' + opts.id + '"');
	}else{
		a.push(' id="content_iframe"');
	}
	a.push(' frameborder="' + opts.frameborder + '"');
	a.push(' marginwidth="' + opts.marginwidth + '"');
    a.push(' marginheight="' + opts.marginheight + '"');
	a.push(' width="' + opts.width + '"');
	a.push(' height="' + opts.height + '"');
	a.push(' scrolling="' + opts.scrolling + '"');
	a.push(endTag);
	$el = jQuery(a.join(''));
    //$el.html(a.join(''));
	$this.after($el).remove();
}