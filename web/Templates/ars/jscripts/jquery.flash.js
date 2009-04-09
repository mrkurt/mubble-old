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
