using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.Html
{
    public class RssHeaderLink : Control
    {
        public RssHeaderLink()
        {
            this.Load += new EventHandler(RssHeaderLink_Load);
        }

        void RssHeaderLink_Load(object sender, EventArgs e)
        {
            Controller c = this.Controller;
            do
            {
                foreach (RssFeed rss in c.RssFeeds)
                {
                    System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
                    link.Attributes.Add("rel", "alternate");
                    link.Attributes.Add("type", "application/rss+xml");

                    if (string.IsNullOrEmpty(rss.Title))
                    {
                        link.Attributes.Add("title", rss.Title);
                    }
                    else
                    {
                        link.Attributes.Add("title", c.Title);
                    }
                    if (string.IsNullOrEmpty(rss.RedirectUrl))
                    {
                        link.Attributes.Add("href", MubbleUrl.Url(c.Url.Path, rss.Slug, "RssHandler"));
                    }
                    else
                    {
                        link.Attributes.Add("href", rss.RedirectUrl);
                    }

                    this.Controls.Add(link);
                    this.Controls.Add(new System.Web.UI.LiteralControl("\r\n\t\t"));
                }
            } while ((c = DataBroker.GetController(c.ControllerID)) != null);
        }
    }
}
