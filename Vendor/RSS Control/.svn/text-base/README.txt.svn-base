RssFeed Readme
=================================

RssFeed is a custom ASP.NET server control designed to display an RSS feed in an
ASP.NET Web page.  For more information about this project, visit:
http://scottonwriting.net/sowBlog/RssFeed.htm


HISTORY:
========
Oct. 25, 2005 (version 1.9.3):
-----------------------------
- Added capability for specifying Credentials in RssFeed. Enables page developers to
	display RSS feeds that are protected via authentication schemes such as Basic, Digest,
	NTML, and so on. For more information on making authenticated HTTP web requests see
	http://aspnet.4guysfromrolla.com/articles/102605-1.aspx
	

Oct. 13, 2005 (version 1.9.2):
-----------------------------
- When creating version 1.9, I factored out the RSS slurping piece to the RssEngine.
	This resulted in accidentally removing the CacheDuration support.  I have added this
	back in in the RssFeed.cs class's CreateControlHierarchy() method. (Thanks to David M.
	for catching this bug.)
	

Oct. 4, 2005 (version 1.9.1):
-----------------------------
- Fixes MaxItems bug.  See the first comment at http://scottonwriting.net/sowblog/posts/4478.aspx


Sept. 27, 2005 (version 1.9):
-----------------------------
  (The jump in version numbers is because I am going to eventually release
  version 2.0, which will include ATOM support...  This will be the last version
  before the 2.0 release...)

- Moved RSS feed retrieval code/logic to separate namespace and classes
  (see skmRss.Engine namespace).  This will allow developers to use RssFeed
  to grab RSS and RDF content without needing to use the associated ASP.NET Web
  control to display the results.

- Added an RssEnclosure class and associated property to the RssItem class.
  Ergo, RssFeed can now be used to display information about podcast feeds.
  This included the addition of two new properties to the RssFeed control:
  ShowEnclosure and EnclosureLinkText.
  

Oct. 1, 2004 (version 1.5.1735.17373):
--------------------------------------
 - Fixed a bug on parsing <pubDate> entries that used time zones other than GMT.
   Essentially used code from the RssBandit project that parses dates according to
   the RFC 822 specs (credit to Dare Obasanjo for the code).
   

Mar. 9th, 2004 (version 1.5):
------------------------------
 - Added Proxy property so RssFeed could be used to request external
 	RssFeeds on sites that use a Proxy.
 - Added Timeout property, specifying how many milliseconds RssFeed should
 	wait when accessing an external URL.


Feb. 12th, 2004 (version 1.4):
------------------------------
 - Added ItemIndex property to RssFeedItem class
 

Feb. 5th, 2004 (version 1.3):
-----------------------------
 - Converted XmlDocument usage to XPathDocument


Jan. 8th, 2004:
---------------
RssFeed 1.2
 - Added capability for title/link to content to be automatically encorporated
 - Added template support
 - Added XML comments to C# source code - documentation available online at:
 	http://scottonwriting.net/sowBlog/CodeProjectFiles/RssFeedDocs/skmRss.html
 - Added some live demos examples at:
 	http://scottonwriting.net/demos/RssFeedDemos.aspx


Dec. 13, 2003:
--------------
RssFeed 1.1
 - Added Target property
 - Added support for RDF (RSS 1.0) syndication feeds
 

Oct. 25, 2003:
--------------
RssFeed 1.0
(This control was created by Scott Mitchell in October 2003.
http://www.4guysfromrolla.com/ScottMitchell.shtml)