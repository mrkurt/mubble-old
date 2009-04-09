var Cookie = {
  set: function(name, value, daysToExpire) {
    var expire = '';
    if (daysToExpire !== undefined) {
      var d = new Date();
      d.setTime(d.getTime() + (86400000 * parseFloat(daysToExpire)));
      expire = '; expires=' + d.toGMTString();
    }
    return (document.cookie = escape(name) + '=' + escape(value || '') + expire + ';path=/');
  },
  get: function(name) {
    var cookie = document.cookie.match(new RegExp('(^|;)\\s*' + escape(name) + '=([^;\\s]*)'));
    return (cookie ? unescape(cookie[2]) : null);
  },
  erase: function(name) {
    var cookie = Cookie.get(name) || true;
    Cookie.set(name, '', -1);
    return cookie;
  },
  accept: function() {
    if (typeof navigator.cookieEnabled == 'boolean') {
      return navigator.cookieEnabled;
    }
    Cookie.set('_test', '1');
    return (Cookie.erase('_test') === '1');
  }
};

var Banners = {
    Tags: [],
    Sizes: {
        '728': { 'w': '728', 'h': '90' },
        '730': { 'w': '336', 'h': '280' },
        '1919': { 'w': '336', 'h': '630' },
        '1676': { 'w': '336', 'h': '630' },
        '1920': { 'w': '970', 'h': '90' },
        '721': { 'w': '160', 'h': '600' }
    },
    Setup: function(e) {
        var tags = Banners.GetTags();
        var d = new Date();
        var start = new Date(2009, 0, 12);
        if (d > start || window.location.search.indexOf('adDebug') >= 0) {
            $('#Content .Bubbles').prepend('<a href="/business/smb-resources.ars"><img src="http://media.arstechnica.com/ars.static/images/smb-resource-bubble.png" style="margin-bottom: 9px;" /></a>');
            if (zone && zone == "itbiz_smb") {
                $('.Bubble.SmbResources').show();
            }
        }
    },
    GetBannerZone: function(original) {
        if (window.location.pathname.indexOf('/index.') < 0) return original;

        var dates = [new Date(2008, 11, 8), new Date(2008, 11, 15)];

        var now = new Date();
        if (window.location.search.indexOf('adDebug') > 0)
            dates.push(new Date());

        var offset = 6 * 60 * 1000;
        var endoffset = 24 * 3600 * 1000;

        var ticks = now.getTime();

        for (var d in dates) {
            var start = dates[d].getTime();
            var end = start + endoffset;
            if (ticks >= start && ticks < end) {
                return '1920';
            }
        }
        return original;
    },
    AddItopiaBlurb: function() {
        jQuery("#Content .ContentBody .Body").prepend("<p><a href=\"/sponsored/itopia\"><img src=\"/Templates/ars/images/intel_article.png\" alt=\"IT Utopia\" /></a></p>");
    },
    GetTags: function() {
        var tags = [];
        var test = "";
        jQuery(".Content .Tags p a").not('.MoreLink').each(function(i) {
            var t = jQuery(this).text().toLowerCase();
            Banners.Tags.push(t);
            if (t == "itopia") {
                Banners.AddItopiaBlurb();
            }
            tags.push(escape(t));
            pageTracker._trackEvent('Metrics', 'Tag', t);
        });
        return tags.join(",");
    }
};

var Utilities = {
    EventTargetElement : function(e, nowrap) {
        var targ;
	    if (!e) {
	        e = window.event;
	    }
	    if (e.target) {
	        targ = e.target;
	    }
	    else if (e.srcElement){
	        targ = e.srcElement;
	    }
	    if (targ.nodeType == 3){ // defeat Safari bug
		    targ = targ.parentNode;
		}
		
		if(!nowrap){
		    return jQuery(targ);
		}else{
		    return targ;
		}
    },
    ReplaceExtension : function(url, oldextension, replacement){
        var index = url.lastIndexOf(oldextension);
        return url.substring(0, index) + replacement + url.substring(index + oldextension.length);
    },
    DiggCleanTitle : function(story_title) {
        // Remove single quotes
		story_title = story_title.replace( '\'', ' ');
		// Replace everything that's not Alpha+Num with spaces
		story_title = story_title.replace( /[^\w,\d,' ']/gi, ' ');
		 // Replace commas
		story_title = story_title.replace( /,/gi, ' ');
		// Trim whitespace
		story_title = story_title.replace(/^\s+|\s+$/g,"");
        // Title case everything
        story_title = Utilities.TitleCase(story_title, '');
		// Replace all spaces with underscores
		story_title = story_title.replace( / +/gi, '_');
		// Trim to 75 chars
		story_title = story_title.substr( 0, story_title.length>75 ? 75 : story_title.length);
		return story_title;
	},
	TitleCase : function(title, small) {
	    if(small === undefined) {
    	    small = "(a|an|and|as|at|but|by|en|for|if|in|of|on|or|the|to|v[.]?|via|vs[.]?)";
    	}
    	var punct = "([!\"#$%&'()*+,./:;<=>?@[\\\\\\]^_`{|}~-]*)";

    	var parts = [], split = /[:.;?!] |(?: |^)["Ò]/g, index = 0;
		while (true) {
			var m = split.exec(title);

			parts.push( title.substring(index, m ? m.index : title.length)
				.replace(/\b([A-Za-z][a-z.'Õ]*)\b/g, function(all){
					return (/[A-Za-z]\.[A-Za-z]/).test(all) ? all : Utilities.Upper(all);
				})
				.replace(new RegExp("\\b" + small + "\\b", "ig"), Utilities.Downer)
				.replace(new RegExp("^" + punct + small + "\\b", "ig"), function(all, punct, word){
					return punct + Utilities.Upper(word);
				})
				.replace(new RegExp("\\b" + small + punct + "$", "ig"), Utilities.Upper));

			index = split.lastIndex;

			if ( m ){
			    parts.push( m[0] );
			}
			else {
			    break;
			}
		}

		return parts.join("").replace(/ V(s?)\. /ig, " v$1. ")
			.replace(/(['Õ])S\b/ig, "$1s")
			.replace(/\b(AT&T|Q&A)\b/ig, function(all){
				return all.toUpperCase();
			});
    },
    Upper : function(word){
        if(typeof(word) == 'string') {
            return word.substr(0,1).toUpperCase() + word.substr(1);
        } else {
            return "";
        }
	},
	Downer : function(word){
	    if(typeof(word) == 'string') {
            return word.toLowerCase();
        } else {
            return "";
        }
    },
    FlashVersion : function(){
    	// ie
	    try {
		    try {
			    // avoid fp6 minor version lookup issues
			    // see: http://blog.deconcept.com/2006/01/11/getvariable-setvariable-crash-internet-explorer-flash-6/
			    var axo = new ActiveXObject('ShockwaveFlash.ShockwaveFlash.6');
			    try { axo.AllowScriptAccess = 'always';	} 
			    catch(e) { return '6,0,0'; }				
		    } catch(e) {}
		    return new ActiveXObject('ShockwaveFlash.ShockwaveFlash').GetVariable('$version').replace(/\D+/g, ',').match(/^,?(.+),?$/)[1];
	    // other browsers
	    } catch(e) {
		    try {
			    if(navigator.mimeTypes["application/x-shockwave-flash"].enabledPlugin){
				    return (navigator.plugins["Shockwave Flash 2.0"] || navigator.plugins["Shockwave Flash"]).description.replace(/\D+/g, ",").match(/^,?(.+),?$/)[1];
			    }
		    } catch(e) {}		
	    }
	    return '0,0,0';
    }
};

var Discussion = {
    DiscussionLink: '',
    Setup: function(e) {
        Discussion.LinkNewsDiscussions();
        var container = jQuery('.Content .Comments');
        if (container.length == 1) {
            Discussion.DiscussionLink = jQuery('.PostOptions a.RespondLink').attr("href");
            jQuery('.PostOptions a.RespondLink').attr("href", Discussion.DiscussionLink.replace('tpc', 'prply'));
            container.show();
            container.after(jQuery('.PostOptions').clone());
			var page = window.location.search.replace(/(.*)comments\=(\d+)(.*)/, '$2');
			page = parseInt(page, 10);
			if(isNaN(page)){
			    page = 1;
			}
            Discussion.GetComments(page);
        }
    },
    LinkNewsDiscussions: function() {
        //if (window.location.search.indexOf('c=true') < 0) return;
        //if (Utilities.FlashVersion() == '0,0,0') return;
        if (window.location.pathname.indexOf('news.') > 0) {
            jQuery('.PostOptions .DiscussionLink').attr('href', window.location.pathname + '?comments=1');
        }
        jQuery(".Options .FullStoryLink").each(function() {
            var a = jQuery(this).attr('href');
            if (a.indexOf('/news.') > 0) {
                var d = jQuery(this).next('.DiscussionLink').attr('href', a + "?comments=1");
            }
        });
    },
	Loaded : false,
    FixEveComments: function(id, pages) {
		Discussion.Loaded = true;
        Discussion.HijackPageNumbers(id, pages);
        //Discussion.UnhideOptions(id, pages);
    },
    HijackPageNumbers: function(id, pages) {
        jQuery('.comment-pagination a').each(function() {
            var page = jQuery(this).html();
            jQuery(this).attr('href', window.location.pathname + "?comments=" + page);
        });
    },
    UnhideOptions: function(id, pages) {
        //jQuery('.comment-tools').remove();
    },
    PageClick: function(e) {
        jQuery('.PostOptions').hide();
        var link = Utilities.EventTargetElement(e);
        jQuery('.Content .Comments .Pages a').attr('class', 'Inactive');
        Discussion.GetComments(jQuery(link).attr('page'));
        jQuery('.PostOptions').show();
        return false;
    },
    GetComments: function(page) {
        var link = "http://" + location.hostname + location.pathname;
        jQuery('.Content .Comments .Comment').remove();
        Discussion.ShowMessage('Loading Comments', 'Attempting to retrieve comments from forum system.', 'Loading');
        Discussion.LayoutChangedCallback();
        jQuery('.Content .Comments h3 + .Pages').after(jQuery('<div class="Comment Loading">&nbsp;</div>'));
        var json = Utilities.ReplaceExtension(link, Mubble.HtmlHandler, Mubble.JsonHandler + '/m-comments').replace('#Comments', '');
        $.getJSON(json, { 'p': page }, Discussion.LoadComments);
    },
    LoadComments: function(obj) {
        jQuery('.PostOptions').hide();
        jQuery('.Content .Comments .Pages').hide().siblings('.Comment').remove();
        if (!obj.IsException) {
            json = obj.Data;
            var start = jQuery('.Content .Comments #topcomments');
            if (json.Comments.length === 0) {
                Discussion.ShowMessage('No comments, yet.','There are not yet any comments in this discussion.  You could be the first.', 'Error');
            }
            for (i = json.Comments.length - 1; i >= 0; i--) {
                var comment = json.Comments[i];
                var body = jQuery('<div class="Body"></div>').html(json.Comments[i].Body);
                var a = $.A().attr("href", json.Comments[i].UserLink);
                var h4 = jQuery('<h4></h4>').append(a.html(json.Comments[i].UserName));
                var div = $.DIV({ Class: 'Comment' }).append(h4).append(body).append(jQuery('<p class="Tag">' + json.Comments[i].PrettyDate + '</p>'));
                if (i % 2 === 0) {
                    div.addClass('Light');
                }

                start.after(div);
            }
            jQuery('.Content .Comments .Pages a').remove();
            if (json.PageCount > 1) {
				var linkTo = jQuery('body.news').length == 1;
                for (i = 1; i <= json.PageCount; i++) {
					var link = window.location.pathname + "?comments=" + i;
                    jQuery('.Content .Comments .Pages')
                        .append($.TEXT(' ')).append(jQuery('<a href="' + link + '"></a>').attr('class', i == json.Page ? 'Inactive' : '')
                        .html(i).attr('page', i));
                }
				if(!linkTo){
				    jQuery('.Content .Comments .Pages a').click(Discussion.PageClick);
				}
                jQuery('.Content .Comments .Pages').show();
            }
            jQuery('.Content .Comments').show();
        } else {
            Discussion.ShowError(obj);
        }
        jQuery('.PostOptions').show();
        Discussion.LayoutChangedCallback();
    },
    ShowError: function(json) {
        switch (json.DataType) {
            case 'JsonCommentsTimeout':
                Discussion.ShowMessage('Timed out while waiting for comments','The forum timed out while we were attempting to retrieve comments for this post.  You may have better luck visiting the ' + '<a href="' + Discussion.DiscussionLink + '">thread directly</a>.', 'Error');
                break;
            default:
                Discussion.ShowMessage('An error occurred while retrieving comments','We were unable to retrieve comments from the forum.  You may have better luck visiting the ' + '<a href="' + Discussion.DiscussionLink + '">thread directly</a>.', 'Error');
                break;
        }
    },
    ShowMessage: function(title, message, type) {
        jQuery('.Content .Comments #topcomments').after(jQuery('<div class="Comment Light"></div>').append(jQuery('<h4></h4>').html(title)).append(jQuery('<p class="Message"></p>').html(message)).append(jQuery('<p class="Tag">&nbsp;</p>')).addClass(type));
    },
    LayoutChangedCallback: function() { }
};

var Journals = {
    Images : [],
    HeadlinesContainer : '.JournalsBox ul.Headlines',
    SetupPanel : function(e)
    {
        jQuery('.JournalsBox ul.Tabs li a').click(Journals.TabClick);

        var userJournalPref = Cookie.get('JournalTabPreference');
        if(userJournalPref === null || userJournalPref[0] != '#' || jQuery(userJournalPref).length != 1){
			userJournalPref = '#AllJournalTab';
		}
        
        if(userJournalPref != '#AllJournalTab'){
            Journals.Load(userJournalPref);
        }
    },
    Load : function(which){
        var tab = Journals.HighlightTab(which);
        if(tab !== null)
        {
            Journals.GetHeadlines(tab);
        }
    },
    TabClick : function(e){
        var link = Utilities.EventTargetElement(e);
		var id = '#' + link.parent().attr('id');
		Journals.HighlightTab(id);
        Cookie.set('JournalTabPreference', id, 365);
        Journals.GetHeadlines(id);
        return false;
    },
    GetHeadlines : function(tab){
		if(typeof tab == "string"){
		    tab = jQuery(tab);
		}
		var link = tab.children('a');
		if(link.length != 1) {
		    return;
		}
        var json = Utilities.ReplaceExtension(link.attr("href"),Mubble.HtmlHandler, Mubble.JsonHandler);
        jQuery(Journals.HeadlinesContainer + ' li').remove();
        jQuery(Journals.HeadlinesContainer).addClass('Loading');
        json = json + '/list/17';
        $.getJSON(json, {'target' : 'frontpage-journal-box'}, Journals.LoadHeadlines);
        jQuery('.JournalsBox h3 a').attr("href", link.attr("href"));
    },
    LoadHeadlines : function(obj){
        var json = obj.Data;
        var headlines = jQuery(Journals.HeadlinesContainer);
        for(i in json){
            var li = $.LI({ Class: json[i].ContainerFileName });
            var a = $.A().attr("href", json[i].Url);
            headlines.append(li.append(a.append($.TEXT(json[i].Title))));
        }
        headlines.removeClass('Loading');
    },
    HighlightTab : function(id){
		jQuery('.JournalsBox ul.Tabs li').removeClass('on');
		return jQuery(id).addClass('on');
    }
};

var Tags = {
    Setup : function(e)
    {
        if(jQuery('.Content .Tags p').children().length > 8)
        {
            jQuery('.Content .Tags p')
                .children()
                    .gt(7).hide()
                    .end()
                .end()
                .append(jQuery('<a href="#" class="MoreLink">more...</a>').click(Tags.MoreClick));
        }
    },
    MoreClick : function(e)
    {
        jQuery('.Content .Body .Tags p')
            .children().show().end()
            .children('a.MoreLink').hide();
        return false;
    }
};

var Digg = {
  Setup : function(e)
  {
      var digg_cookie_name = "digg_panel_location";
      var ref = Cookie.get("origin_ref");
      if ( (((window.location.pathname.indexOf('news.') > 0) || 
            (window.location.pathname.indexOf('guides/') > 0) ||
            (window.location.pathname.indexOf('reviews/') > 0) ||
            (window.location.pathname.indexOf('journals/') > 0) ||
            (window.location.pathname.indexOf('articles/') > 0)) && (ref.indexOf('digg.com') >= 0))) {
          var clean_title = Utilities.DiggCleanTitle(document.title);
          var article_body = jQuery('.Body');
          var cls = '';
          var options = { scrolling: "no" };

          var randomnumber=Math.floor(Math.random()*101);
          var position = Cookie.get(digg_cookie_name);

          if(position === null) {
              if(randomnumber <= 49) {
                  position = 'top';
                  Cookie.set(digg_cookie_name, "top", 365);
              } else {
                  Cookie.set(digg_cookie_name, "bottom", 365);
                  position = 'bottom';
              }
          }
          
          try {
		    pageTracker._trackEvent("DiggPanel", position + "Show", "");
		  } catch(e) {}
          
          if(window.location.pathname.indexOf('journals/') > 0) {
              jQuery('div.Tags').css('margin-bottom', '10px');
          }
          
          var opts = jQuery.extend({
              frameborder: ((cls.match(/fb:(\d+)/) || [])[1]) || 0,
              marginwidth: ((cls.match(/wm:(\d+)/) || [])[1]) || 0,
              marginheight: ((cls.match(/hm:(\d+)/) || [])[1]) || 0,
              width: ((cls.match(/w:(\d+)/) || [])[1]) || 580,
              height: ((cls.match(/h:(\d+)/) || [])[1]) || "86",
              scrolling: ((cls.match(/sc:(\w+)/) || [])[1]) || "auto",
              version: '1,0,0,0',
              cls: cls,
              src: 'http://arstechnica.com/Templates/ars/digg/frame.html?position='+position+'#' + clean_title,
              id: 'diggbox',
              caption: '',
              attrs: {},
              elementType: 'div',
              xhtml: false
          }, options || {});

          var endTag = opts.xhtml ? ' />' : '>';

          var a = ['<iframe src="' + opts.src + '"'];
          if (opts.id) {
              a.push(' id="' + opts.id + '"');
          } else {
              a.push(' id="content_iframe"');
          }
          a.push(' frameborder="' + opts.frameborder + '"');
          a.push(' marginwidth="' + opts.marginwidth + '"');
          a.push(' marginheight="' + opts.marginheight + '"');
          a.push(' width="' + opts.width + '"');
          a.push(' height="' + opts.height + '"');
          a.push(' scrolling="' + opts.scrolling + '"');
          a.push(endTag);
          
          var $el = jQuery(a.join(''));
          
          if(position == 'top') {
              article_body.prepend($el);
          } else {
              article_body.append($el);
          }
          
      } else {
          return;
      }
  }
};

var Site = {
    Popup: null,
    Setup: function(e) {
        Site.MarkReferrer();
        //Site.PromoteSurvey();
        Site.ReplaceTwitterLinks();
        Site.ReplaceFlashLinks();
        Site.ReplaceQuicktimeLinks();
        Site.ReplaceVimeoLinks();
        Site.SetupPopups();
        Site.SetupContactForm();
        Site.TrackAuthors();
        try{ Discussion.Setup(e); } catch(err){}
        Digg.Setup(e);
        Tags.Setup(e);
        Journals.SetupPanel(e);
        jQuery('form .FormSubmit').click(function() { jQuery(this).parents('form').get(0).submit(); }).css('cursor', 'pointer');

        Site.MoveRelatedDown();
        jQuery('select.DropdownNav').change(function() { window.location = this.value; });
        Banners.Setup();
        //Site.PromoteSurvey();
        Site.HighlightCode();
    },
    TrackAuthors: function() {
        var authorBlocks = $('p.Tag.Full');
        var type = "Content";
        if (authorBlocks.length > 1) {
            type = "Listing";
        }
        authorBlocks.find('a').each(function() {
            var l = $(this);
            if (l.attr('href').indexOf('authors.ars') >= 0) {
                pageTracker._trackEvent("Metrics", "Author" + type, l.html());
            }
        });
    },
    PromoteSurvey: function() {
        //if (window.location.search.indexOf('survey=true') < 0) return;
        var show = false;
        for (var t in Banners.Tags) {
            if (Banners.Tags[t] == 'business'){
                show = true;
            }
            if (Banners.Tags[t] == 'gadgets'){
                show = true;
            }
        }
        var hide_survey_promotion = Cookie.get("hide_survey_promotion3");
        if (hide_survey_promotion != 'true' && show) {
            var surveyLink = "http://survey.questionmarket.com/surv/489062/ai_start.php?site=6&from_ec=0";
            var html = "<p class=\"Survey\"><a href=\"" + surveyLink + "\"><img src=\"/Templates/ars/images/survey_bubble.gif\" alt=\"Take the survey!\" /></a>";
            html += "<a href=\"#\" class=\"Close\"><img src=\"/Templates/ars/images/survey-close-button.png\" alt=\"Close\" /></a></p>";
            var target = jQuery('.Body');
            if (target.length != 1) {
                target = jQuery('.Content .ContentBody');
            }
            target.append(html);
            jQuery(".Close").click(Site.CloseSurvey);
        }
    },
    CloseSurvey: function() {
        jQuery(this).parent().remove();
        Cookie.set("hide_survey_promotion3", "true", 365);
        return false;
    },
    
    MarkReferrer: function() {
        var ref = document.referrer;
        var old = Cookie.get("origin_ref");
        if (ref === null || ref === "") {
            ref = "(direct)";
        } else {
            ref = ref.replace(/http\:\/\/(www\.)?([^\/]+).*/, '$2');
        }
        var current = window.location.href.replace(/http\:\/\/(www\.)?([^\/]+).*/, '$2');
        if (ref != current) {
            Cookie.set("origin_ref", ref, 365);
            try {
                pageTracker._trackEvent("Traffic", "Source", ref);
            } catch (e) { }
        }
    },
    ReplaceTwitterLinks: function() {
        jQuery('a.twitter').each(Site.InsertTwitterFrame);
    },
    InsertTwitterFrame: function(index) {
        var options = { scrolling: "no" };
        var $this = jQuery(this);
        var href = $this.attr('href');
        var twitter_user = href.match(/http:\/\/twitter.com\/(\S+)/)[1];
        var cls = this.className;

        var opts = jQuery.extend({
            frameborder: ((cls.match(/fb:(\d+)/) || [])[1]) || 0,
            marginwidth: ((cls.match(/wm:(\d+)/) || [])[1]) || 0,
            marginheight: ((cls.match(/hm:(\d+)/) || [])[1]) || 0,
            width: ((cls.match(/w:(\d+)/) || [])[1]) || 580,
            height: ((cls.match(/h:(\d+)/) || [])[1]) || "1050",
            scrolling: ((cls.match(/sc:(\w+)/) || [])[1]) || "auto",
            version: '1,0,0,0',
            cls: cls,
            src: 'http://arstechnica.com/Templates/ars/twitter/frame.html#' + twitter_user,
            id: 'twitter_' + twitter_user,
            caption: $this.text(),
            attrs: {},
            elementType: 'div',
            xhtml: false
        }, options || {});

        var endTag = opts.xhtml ? ' />' : '>';

        var a = ['<iframe src="' + opts.src + '"'];
        if (opts.id) {
            a.push(' id="' + opts.id + '"');
        } else {
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
    },
    SetupContactForm: function() {
        if (window.location.toString().indexOf('contact.ars?sent=true') > 0) {
            jQuery('#ContactForm').hide();
            jQuery('#FormConfirmation').show();
        }
    },
    MoveRelatedDown: function() {
        jQuery('.RelatedStories').siblings('p:first').after(jQuery('.RelatedStories').remove());
        var count = jQuery('.RelatedStories li a').length;
        if (count > 0) {
            try {
                pageTracker._trackEvent("Related", "Displayed", count.toString() + " - " + window.location.toString());
            } catch (e) { }
            jQuery('.RelatedStories li a').click(function() {
                try {
                    pageTracker._trackEvent("Related", "Clicked", window.location.toString());
                } catch (e) { }
            });
        }
    },
    ReplaceFlashLinks: function() {
        jQuery('a.flash').flash();
        jQuery('a.flv').each(Site.ConvertToFlv);
    },
    ReplaceVimeoLinks: function() {
        jQuery('a.vimeo').each(Site.ConvertToVimeo);
    },
    ConvertToVimeo: function(index) {
        var $this = jQuery(this);
        var cls = this.className;
        var opts = {
            width: ((cls.match(/w:(\d+)/) || [])[1]) || 540, // default to youtube width
            height: ((cls.match(/h:(\d+)/) || [])[1]) || 408, // default to youtube height
            version: ((cls.match(/ver:(\d+)/) || [])[1]) || 7,
            background: ((cls.match(/bg:#?([0-9a-fA-F]+)/) || [])[1]) || 'eee',
            cls: cls,
            src: $this.attr('href') || $this.attr('src'),
            caption: $this.text(),
            params: {},
            flashvars: {},
            elementType: 'div'
        };

        var re1 = '.*?'; // Non-greedy match on filler
        var re2 = '(\\d+)'; // Integer Number 1
        var p = new RegExp(re1 + re2, ["i"]);
        var m = p.exec(opts.src);
        if (m.length > 0) {
            var vimeo_id = m[1];
            var player = 'http://vimeo.com/moogaloop.swf?clip_id=' + vimeo_id + '&amp;server=vimeo.com&amp;show_title=1&amp;show_byline=1&amp;show_portrait=1&amp;color=ff0179&amp;fullscreen=1';

            var id = this.id ? (' id="' + this.id + '"') : '';
            var $el = jQuery('<' + opts.elementType + id + ' class="CenteredImage ' + opts.cls + '" style="width:' + opts.width + 'px"></' + opts.elementType + '>');
            $this.after($el).remove();

            var so = new SWFObject(player, 'movie_player-' + index, opts.width, opts.height, opts.version, '#' + opts.background);

            so.addParam("allowfullscreen", "true");
            so.addParam("allowscriptaccess", "always");
            so.write($el[0]);

            if (opts.caption) {
                $el.append('<div>' + opts.caption + '</div>');
            }
        }
    },
    ConvertToFlv: function(index) {
        var player = 'http://media.arstechnica.com/video/flvplayer.swf';

        var $this = jQuery(this);
        var cls = this.className;

        var opts = {
            width: ((cls.match(/w:(\d+)/) || [])[1]) || 450, // default to youtube width
            height: ((cls.match(/h:(\d+)/) || [])[1]) || 356, // default to youtube height
            version: ((cls.match(/ver:(\d+)/) || [])[1]) || 7,
            background: ((cls.match(/bg:#?([0-9a-fA-F]+)/) || [])[1]) || 'eee',
            cls: cls,
            src: $this.attr('href') || $this.attr('src'),
            caption: $this.text(),
            params: {},
            flashvars: {},
            elementType: 'div'
        };

        var id = this.id ? (' id="' + this.id + '"') : '';
        var $el = jQuery('<' + opts.elementType + id + ' class="' + opts.cls + '" style="width:' + opts.width + 'px"></' + opts.elementType + '>');
        var image = $this.children('img').attr('src');
        $this.after($el).remove();

        var so = new SWFObject(player, 'movie_player-' + index, opts.width, opts.height, opts.version, '#' + opts.background);
        so.addVariable('file', opts.src);
        if (image !== '') {
            so.addVariable('image', image);
        }
        so.addParam("allowfullscreen", "true");
        so.write($el[0]);

        if (opts.caption){
            $el.append('<div>' + opts.caption + '</div>');
        }
    },
    ReplaceQuicktimeLinks: function() {
        jQuery('a.quicktime').quicktime({ attrs: { autoplay: false} });
    },
    SetupPopups: function() {
        jQuery('a.Popup').click(Site.PopImage);
    },
    PopImage: function(e) {

        var loc = jQuery(this).attr("href");
        var img = new Image();

        if (Site.Popup && Site.Popup.close) {
            Site.Popup.close();
        }
        Site.Popup = window.open('about:blank', 'ars_img', 'resizable=yes,width=400,height=300');
        jQuery(img).load(Site.OpenPopup);
        img.src = loc;

        return false;
    },
    OpenPopup: function(e) {
        Site.Popup.resizeBy(this.width - 400, this.height - 300);
        Site.Popup.location = this.src;
    },
    HighlightCode: function(e) {
        if($.browser.msie){
            // Internet explorer tries to run this before the 
            //  libraries are done loading (but the DOM is done)
            //  so we use this delaying tactic to wait, then try again
            //  for up to 25 times.  Kind of stupid :P
            var attempts = 0;
            var checkIfHilighterLoaded = function() {
                try{
                    dp.SyntaxHighlighter.ClipboardSwf = '/flash/clipboard.swf';      
                    dp.SyntaxHighlighter.HighlightAll('code');    
                    clearTimeout(timerHandle);
                }
                catch(e){
                    clearTimeout(timerHandle);
                    if(attempts < 25){
                        timerHandle = setTimeout(checkIfHilighterLoaded, 350);
                    }
                    attempts++;
                }
            };
            var timerHandle = setTimeout(checkIfHilighterLoaded, 350);
        } else {
            // Normal browsers have no such issues :P
            dp.SyntaxHighlighter.ClipboardSwf = '/jscripts/code/clipboard.swf';
            dp.SyntaxHighlighter.HighlightAll('code');
        } 
    }
};

jQuery(document).ready(Site.Setup);
