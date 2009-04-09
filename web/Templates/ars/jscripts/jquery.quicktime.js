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
