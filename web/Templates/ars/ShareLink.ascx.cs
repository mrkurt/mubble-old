using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mubble.Models;
using System.Collections.Generic;
using Mubble;

public partial class Templates_ars_ShareLink : Mubble.UI.UserControl
{
    private string title = "$Title";

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    private string excerpt = "$Excerpt";

    public string Excerpt
    {
        get { return excerpt; }
        set { excerpt = value; }
    }

    private Link urlPair;

    public Link UrlPair
    {
        get { return urlPair; }
        set { urlPair = value; }
    }

    private MubbleUrl url;

    public override MubbleUrl Url
    {
        get { return (url != null) ? url : base.Url; }
        set { url = value; }
    }

    public string MailLink
    {
        get { return this.UrlPair != null ? MubbleUrl.Url(this.UrlPair, "Mailer") : null; }
    }

    string u;
    public string ContentUrl
    {
        get { return this.u; }
    }


    private Dictionary<string, string> linkFormats = new Dictionary<string, string>();
    private Dictionary<string, string> imageFormats = new Dictionary<string, string>();
    public Dictionary<string, string> Links
    {
        get { return linkFormats; }
    }

    public Dictionary<string, string> Images
    {
        get { return imageFormats; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = Mubble.UI.Control.GetReferencedValue(this, this.Title);
        this.Excerpt = Mubble.UI.Control.GetReferencedValue(this, this.Excerpt);

        linkFormats.Add("Digg", "http://castor.arstechnica.com/digg/link.ashx?url={0}&title={1}&bodytext={2}");
        imageFormats.Add("Digg", "http://castor.arstechnica.com/digg/image.ashx?url={0}");
        //linkFormats.Add("Digg", "http://digg.com/submit?phase=2&url={0}&title={1}&bodytext={2}");
        linkFormats.Add("Delicious", "http://del.icio.us/post?v=4&noui&jump=close&url={0}&title={1}&notes={2}");
        linkFormats.Add("Newsvine", "http://www.newsvine.com/_tools/seed&save?u={0}&h={1}&b={2}");

        if (this.UrlPair == null)
        {
            ILinkable scope = Mubble.UI.Control.GetCurrentScope<ILinkable>(this);
            this.UrlPair = scope != null ? scope.Url : null;
        }
        u = (this.UrlPair != null) ?
            MubbleUrl.Url(this.UrlPair.Path, this.UrlPair.Extra, "HtmlHandler") :
            this.Url.ToString();

        string[] keys = new string[linkFormats.Keys.Count];
        linkFormats.Keys.CopyTo(keys, 0);
        foreach (string key in keys)
        {
            linkFormats[key] = string.Format(
                linkFormats[key],
                Server.UrlEncode(u),
                Server.UrlEncode(this.Title),
                Server.UrlEncode(this.Excerpt)
                );
        }

        keys = new string[imageFormats.Keys.Count];
        imageFormats.Keys.CopyTo(keys, 0);

        foreach (string key in keys)
        {
            imageFormats[key] = string.Format(imageFormats[key], Server.UrlEncode(u));
        }
    }
}
