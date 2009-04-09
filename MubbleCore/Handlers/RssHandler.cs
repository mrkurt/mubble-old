using System;
using System.Collections.Generic;
using System.Text;

using Antlr.StringTemplate;
using System.Xml;
using Mubble.Models;

namespace Mubble.Handlers
{
    public class RssHandler : HttpHandler
    {
        public override void ProcessMubbleRequest(System.Web.HttpContext context)
        {
            string slug = this.Url.GetPathItem(1);
            if (slug == null || slug.Length == 0) slug = "";

            Mubble.Models.RssFeed feed = null;

            foreach (RssFeed f in this.Controller.RssFeeds)
            {
                if (f.Slug.Equals(slug, StringComparison.CurrentCultureIgnoreCase))
                {
                    feed = f;
                    break;
                }
            }


            if (feed != null)
            {
                if (feed.RedirectUrl != null && feed.RedirectUrl.Length > 0)
                {
                    
                    bool matches = false;
                    if (feed.RedirectExceptions.Length > 0 && context.Request.UserAgent != null && context.Request.UserAgent.Length > 0)
                    {
                        string[] tests = feed.RedirectExceptions.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string test in tests)
                        {
                            if (context.Request.UserAgent.Contains(test))
                            {
                                matches = true;
                                break;
                            }
                        }
                    }

                    if (!matches)
                    {
                        context.Response.Redirect(feed.RedirectUrl);
                    }
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(
@"<?xml version=""1.0"" encoding=""utf-8""?>
<rss version=""2.0"">
</rss>");
                XmlNode channel = this.AppendChild(doc.SelectSingleNode("rss"), "channel", "");

                if (feed.Title != null && feed.Title.Length > 0)
                {
                    this.AppendChild(channel, "title", feed.Title);
                }
                else
                {
                    this.AppendChild(channel, "title", this.Controller.Title);
                }

                if (feed.Link != null && feed.Link.Length > 0)
                {
                    this.AppendChild(channel, "link", feed.Link);
                }
                else
                {
                    this.AppendChild(channel, "link", this.Url.ToString("HtmlHandler", ""));
                }

                if (feed.Description != null && feed.Description.Length > 0)
                {
                    this.AppendChild(channel, "description", feed.Description);
                }
                else
                {
                    this.AppendChild(channel, "description", this.Controller.Excerpt);
                }

                StringTemplate t = new StringTemplate(
                    feed.ItemFormat != null && feed.ItemFormat.Length > 0 ? feed.ItemFormat : "<p>$item.Excerpt$</p><p><a href=\"$link$\">Read More...</a></p>"
                    );
                foreach (IRssItem i in this.Controller.GetRssItems(Security.User.GetRoles()))
                {
                    t.Reset();
                    XmlNode item = this.AppendChild(channel, "item", "");

                    string url = MubbleUrl.Create(i.Url.Path, i.Url.Extra).ToString("HtmlHandler");
                    t.SetAttribute("link", url);
                    t.SetAttribute("item", i);
                    t.SetAttribute("guid", url);
                    t.SetAttribute("escaped.{Url, Title, Excerpt}",
                        System.Web.HttpUtility.UrlEncode(url),
                        System.Web.HttpUtility.UrlEncode(i.Title),
                        System.Web.HttpUtility.UrlEncode(i.Excerpt)
                        );

                    this.AppendChild(item, "title", i.Title);
                    this.AppendChild(item, "link", url);
					this.AppendChild(item, "guid", url);
                    this.AppendChild(item, "pubDate", i.PublishDate.ToUniversalTime().ToString("R"));
                    foreach (string a in i.Authors)
                    {
                        Author author = Author.FindFirst(a);
                        if (author == null)
                        {
                            this.AppendChild(item, "author", string.Format("{0} ({1})", "nobody@dev.null", a));
                        }
                        else
                        {
                            this.AppendChild(item, "author", string.Format("{0} ({1})", author.Email, author.DisplayName));
                        }
                        break;
                    }

                    string body = t.ToString();
                    if (!string.IsNullOrEmpty(body))
                    {
                        body = body.Replace("href=\"/", "href=\"http://" + context.Request.Url.Host + "/");
                    }
                    this.AppendChild(item, "description", body);
                }

                context.Response.ContentType = "text/xml";
                doc.Save(context.Response.OutputStream);
            }
        }

        protected XmlElement AppendChild(XmlNode parent, string name, string value)
        {
            XmlElement element = parent.OwnerDocument.CreateElement(name);
            element.InnerText = value;

            parent.AppendChild(element);

            return element;
        }
    }
}
