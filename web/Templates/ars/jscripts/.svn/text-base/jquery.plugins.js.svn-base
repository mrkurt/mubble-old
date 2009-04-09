eval(function(p,a,c,k,e,d){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--){d[e(c)]=k[c]||e(c)}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('6.l.X=d(3){3=6.1d({O:g,B:2.W(\'1n\')||\'\',x:2.W(\'x\')||\'10\',U:g,1a:g,14:g,r:I},3||{});3.Q=3.Q||3.1a;4 a=2.S(3.r);5(3.U&&3.U(a,2,3)===I)9;4 q=6.V(a);4 E=(3.x&&3.x.1e()==\'10\');5(E)3.B+=(3.B.1i(\'?\')>=0?\'&\':\'?\')+q;5(!3.14&&3.O)6(3.O).1j(3.B,E?g:a,3.Q);m{3.J=3.x;3.1g=E?g:q;6.1k(3)}9 2};6.l.1m=d(3){9 2.P(d(){6("G:L,G:M",2).1l(d(j){2.b.R=2;5(j.Y!=p){2.b.z=j.Y;2.b.H=j.1f}m 5(w 6.l.y==\'d\'){4 y=$(2).y();2.b.z=j.19-y.1b;2.b.H=j.15-y.1c}m{2.b.z=j.19-2.1h;2.b.H=j.15-2.1B}})}).L(d(e){6(2).X(3);9 I})};6.l.S=d(r){4 a=[];4 q=r?\':G\':\'G,1A,o,T\';6(q,2).P(d(){4 n=2.c;4 t=2.J;4 u=2.17.12();5(!n||2.16||t==\'18\'||(t==\'N\'||t==\'11\')&&!2.Z||(t==\'L\'||t==\'M\'||t==\'T\')&&2.b&&2.b.R!=2||u==\'o\'&&2.13==-1)9;5(t==\'M\'&&2.b.z!=p)9 a.k({c:n+\'1D\',f:2.b.z},{c:n+\'1F\',f:2.b.H});5(u==\'o\'){4 7=6.s(2,I);5(t==\'o-1E\'){C(4 i=0;i<7.F;i++)a.k({c:n,f:7[i]})}m a.k({c:n,f:7})}m a.k({c:n,f:2.f})});9 a};6.l.1C=d(r){9 6.V(2.S(r))};6.l.1o=d(h){4 a=[];2.P(d(){5(!2.c)9;4 7=6.s(2,h);5(7&&7.1r==1q){C(4 i=0;i<7.F;i++)a.k({c:2.c,f:7[i]})}m 5(7!==g&&w 7!=\'p\')a.k({c:2.c,f:7})});9 6.V(a)};6.l.s=d(h){4 D=[],K=g;C(4 i=0;i<2.F;i++){4 8=2[i];5(8.J==\'N\'){5(!K)K=8.c||\'1s\';5(K!=8.c)9 D;4 7=6.s(8,h);5(7!==g&&w 7!=\'p\')D.k(7)}m{4 7=6.s(8,h);5(7!==g&&w 7!=\'p\')9 7}}9 D};6.s=d(8,h){4 n=8.c;4 t=8.J;4 u=8.17.12();5(w h==\'p\')h=1v;5(h&&(!n||8.16||t==\'18\'||(t==\'N\'||t==\'11\')&&!8.Z||(t==\'L\'||t==\'M\'||t==\'T\')&&8.b&&8.b.R!=8||u==\'o\'&&8.13==-1))9 g;5(u==\'o\'){4 a=[];C(4 i=0;i<8.3.F;i++){4 A=8.3[i];5(A.1y){4 v=6.1z.1t&&!(A.1x[\'f\'].1u)?A.1w:A.f;5(t==\'o-1p\')9 v;a.k(v)}}9 a}9 8.f};',62,104,'||this|options|var|if|jQuery|val|el|return||form|name|function||value|null|successful||ev|push|fn|else||select|undefined||semantic|fieldValue||tag||typeof|method|offset|clk_x|op|url|for|cbVal|get|length|input|clk_y|false|type|cbName|submit|image|checkbox|target|each|success|clk|formToArray|button|before|param|attr|ajaxSubmit|offsetX|checked|GET|radio|toLowerCase|selectedIndex|dataType|pageY|disabled|tagName|reset|pageX|after|left|top|extend|toUpperCase|offsetY|data|offsetLeft|indexOf|load|ajax|click|ajaxForm|action|fieldSerialize|one|Array|constructor|unnamed|msie|specified|true|text|attributes|selected|browser|textarea|offsetTop|formSerialize|_x|multiple|_y'.split('|'),0,{}))


// DOM element creator for jQuery and Prototype by Michael Geary
// http://mg.to/topics/programming/javascript/jquery
// Inspired by MochiKit.DOM by Bob Ippolito
// Free beer and free speech. Enjoy!

$.defineTag = function( tag ) {
	$[tag.toUpperCase()] = function() {
		return $._createNode( tag, arguments );
	}
};

(function() {
	var tags = [
		'a', 'br', 'button', 'canvas', 'div', 'fieldset', 'form',
		'h1', 'h2', 'h3', 'hr', 'img', 'input', 'label', 'legend',
		'li', 'ol', 'optgroup', 'option', 'p', 'pre', 'select',
		'span', 'strong', 'table', 'tbody', 'td', 'textarea',
		'tfoot', 'th', 'thead', 'tr', 'tt', 'ul' ];
	for( var i = tags.length - 1;  i >= 0;  i-- ) {
		$.defineTag( tags[i] );
	}
})();

$.NBSP = '\u00a0';

$.TEXT = function(s) {
    return $(document.createTextNode(s));
};

$._createNode = function( tag, args ) {
	var fix = { 'class':'className', 'Class':'className' };
	var e;
	try {
		var attrs = args[0] || {};
		e = document.createElement( tag );
		for( var attr in attrs ) {
			var a = fix[attr] || attr;
			e[a] = attrs[attr];
		}
		for( var i = 1;  i < args.length;  i++ ) {
			var arg = args[i];
			if( arg == null ) continue;
			if( arg.constructor != Array ) append( arg );
			else for( var j = 0;  j < arg.length;  j++ )
				append( arg[j] );
		}
	}
	catch( ex ) {
		alert( 'Cannot create <' + tag + '> element:\n' +
			args.toSource() + '\n' + args );
		e = null;
	}

	function append( arg ) {
		if( arg == null ) return;
		var c = arg.constructor;
		switch( typeof arg ) {
			case 'number': arg = '' + arg;  // fall through
			case 'string': arg = document.createTextNode( arg );
		}
		e.appendChild( arg );
	}

	return $(e);
};


/**
 *  Flash plugin for converting anchors into flash objects.
 *  @author:  M. Alsup (malsup at gmail dot com)
 *  @version: 1.0.2
 *  http://www.malsup.com/jquery/media/
 *  Free beer and free speech. Enjoy!
 *
 *  This plugin converts anchor tags into flash objects.
 *  Demos can be found at: http://malsup.com/jquery/media/
 *
 *  NOTE:  This plugin uses (and requires) swfobject.js for proper flash detection.
 *         You must include swfobjects.js on any page that uses this plugin.
 *         @see http://blog.deconcept.com/swfobject/
 *
 *
 *  Sample HTML:
 *      before:  <a href="myMovie.swf" class="flash">Watch my movie!</a>
 *
 *      after:   <div class="flash"><object ... <embed ...   <br />Watch my movie!</div>
 *
 *  Usage:
 *      $('a.flash').flash();
 *
 *  Or using options:
 *      $('a.flash').flash({
 *          caption:  false // supress caption text
 *      });
 *
 *
 *  Notes:
 *  -----
 *  The base tag doesn't *have* to be an anchor but it makes a lot of sense to do it that way
 *  so please consider it. If you use something other than an anchor you'll need to provide
 *  an options argument with a src attribute set to the path of the media file.
 *
 *  By default, classnames assigned to the anchor will be assigned to the div.  This makes it
 *  convenient for styling:
 *
 *  <style type="text/css">
 *      a.flash { ... }
 *      div.flash { ... }
 *  </style>
 *
 *  Options are passed to the 'flash' function using a single Object.  The options
 *  Object is a hash of key/value pairs.  The following option keys are supported:
 *
 *  Options:
 *  -------
 *  width:       width of flash player* (default: 450)
 *  height:      height of flash player* (default: 370)
 *  version:     version of flash player* required (default: 7)
 *  background:  background color of flash player* (default: #fff)
 *  cls:         classname(s) to be applied to new element (default: anchor classname)
 *  src:         source location of flash .swf file (default: value of href attr)
 *  caption:     text to be used as caption; use false for no caption (default: value of anchor text)
 *  params:      object which holds parameters for the object/embed tags (default: empty object)
 *  flashvars:   object which holds parameters for the flashvars attribute (default: empty object)
 *  elementType: type of element to replace anchor (span, div, etc) (default: 'div')
 *
 *  * height, width, version and background values can be embedded in the classname using the following syntax:
 *    <a class="flash w:450 h:450 ver:7 bg:fff></a>
 */
jQuery.fn.flash = function(options) {
    if (typeof SWFObject == 'undefined')  return this; // fast fail (swfobjects.js required)
    return this.each(function(index) {
        var $this = jQuery(this);
        var cls = this.className;

        var opts = jQuery.extend({
            width:        ((cls.match(/w:(\d+)/)||[])[1]) || 450, // default to youtube width
            height:       ((cls.match(/h:(\d+)/)||[])[1]) || 356, // default to youtube height
            version:      ((cls.match(/ver:(\d+)/)||[])[1]) || 7,
            background:   ((cls.match(/bg:#?([0-9a-fA-F]+)/)||[])[1]) || 'eee',
            cls:          cls,
            src:          $this.attr('href') || $this.attr('src'),
            caption:      $this.text(),
            params:       {},
            flashvars:    {},
            elementType:  'div'
        }, options || {});

        // convert anchor to span/div/whatever...
        var id = this.id ? (' id="'+this.id+'"') : '';
        var $el = jQuery('<' + opts.elementType + id  + ' class="' + opts.cls + '" style="width:'+opts.width+'px"></' + opts.elementType + '>');
        $this.after($el).remove();

        var so = new SWFObject(opts.src, 'movie_player-'+index, opts.width, opts.height, opts.version, '#'+opts.background);
        for (var p in opts.params)
            so.addParam(p, opts.params[p]);
        for (var fv in opts.flashvars)
            so.addVariable(fv, opts.flashvars[fv]);
        so.write($el[0]);

        if (opts.caption) $el.append('<div>' + opts.caption + '</div>');
    });
};


/**
 *  Quicktime plugin for converting anchors into Quicktime objects.
 *  @author:  M. Alsup (malsup at gmail dot com)
 *  @version: 1.1.0 (3/21/2007)
 *  http://www.malsup.com/jquery/media/
 *  Free beer and free speech. Enjoy!
 *
 *  This plugin converts anchor tags into Quicktime objects.
 *  Examples and documentation at: http://malsup.com/jquery/media/
 *
 *  Options are passed to the 'quicktime' function using an object literal.
 *  The following option keys are supported:
 *
 *  Options:
 *  -------
 *  width:       width of quicktime player[1] (default: 200)
 *  height:      height of quicktime player[1] (default: 200)
 *  version:     version of quicktime player required (default: '6,0,2,0')
 *  cls:         classname(s) to be applied to new element (default: anchor's classname)
 *  src:         source location of quicktime .mov file (default: value of href attr)
 *  caption:     text to be used as caption; use false for no caption (default: value of anchor text)
 *  attrs:       hash of attributes for the object/embed tags; eg: { autoplay: false } (default: empty object)
 *  elementType: type of element to replace anchor (span, div, etc) (default: 'div')
 *
 *  [1] height and width values can be embedded in the classname like this: <a class="quicktime w:450 h:450></a>
 *
 *  Example: convert all anchors with class of 'quicktime' to quicktime objects:
 *  $('a.quicktime').quicktime();
 *
 *  Example: convert myMovie anchor to quicktime object with no caption and explicit values
 *           for autoplay, tabindex and title:
 *  $('#myMovie').quicktime( {
 *      attrs:   { autoplay: false, tabindex: 10, title: 'My Title' },
 *      caption: false
 *  } );
 */
jQuery.fn.quicktime = function(options) {
    return this.each(function() {
        var $this = jQuery(this);
        var cls = this.className;

        var opts = jQuery.extend({
            width:        ((cls.match(/w:(\d+)/)||[])[1]) || 200,
            height:       ((cls.match(/h:(\d+)/)||[])[1]) || 200,
            version:     '6,0,2,0',
            cls:          cls,
            src:          $this.attr('href') || $this.attr('src'),
            caption:      $this.text(),
            attrs:        {},
            elementType:  'div'
        }, options || {});

        var allowObjectAtts = 'name,id,accesskey,title,class,tabindex,noexternaldata';
        var skip = 'width,height,src,classid,codebase';
        var skipEmbedAttrs  = skip + ',id,class,title,accesskey,noexternaldata,pluginspage';

        var objectAttrs = [], objectParms = [], embedAttrs = [];
        for (var key in opts.attrs) {
            if (allowObjectAtts.indexOf(key) > -1)
                objectAttrs.push(key +'="' + opts.attrs[key] + '"');
            else if (skip.indexOf(key) == -1)
                objectParms.push('<param name="'+ key +'" value="' + opts.attrs[key] + '">');
            if (skipEmbedAttrs.indexOf(key) == -1)
                embedAttrs.push(key +'="' + opts.attrs[key] + '"');
        }

        if (jQuery.browser.msie) {
            var a = ['<object width="' + opts.width + '" height="' + opts.height + '" '];
            if (objectAttrs) a.push(objectAttrs.join(' '));
            a.push(' classid="clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"');
            a.push(' codebase="http://www.apple.com/qtactivex/qtplugin.cab#version=' + opts.version + '">');
            a.push('</ob'+'ject'+'>');
            var p = ['<param name="src" value="' + opts.src + '">'];
            if (objectParms.length) p.push(objectParms.join(''));

            var o = document.createElement(a.join(''));
            for (var i=0; i < p.length; i++)
                o.appendChild(document.createElement(p[i]));
        }
        else {
            var a = ['<embed width="' + opts.width + '" height="' + opts.height + '" src="' + opts.src + '" '];
            if (embedAttrs) a.push(embedAttrs.join(' '));
            a.push(' pluginspage="http://www.apple.com/quicktime/download/"> </em'+'bed>');
        }

        // convert anchor to span/div/whatever...
        var id = this.id ? (' id="'+this.id+'"') : '';
        var $el = jQuery('<' + opts.elementType + id + ' class="' + opts.cls + '" style="width: ' + opts.width + 'px">');
        $this.after($el).remove();
        jQuery.browser.msie ? $el.append(o) : $el.html(a.join(''));
        if (opts.caption) $el.append('<div>' + opts.caption + '</div>');
    });
};
