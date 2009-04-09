using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mubble.Models;
using System.Collections.Generic;
using Mubble;

public partial class Templates_ars_Jobs : Mubble.UI.UserControl
{
    public Templates_ars_Jobs()
    {
        this.Load += new EventHandler(Templates_ars_Jobs_Load);
    }

    void Templates_ars_Jobs_Load(object sender, EventArgs e)
    {
        this.Feed.Feed = "http://jobs.arstechnica.com/feeds/jumbled/";
    }

    public string FormatTitle(object t)
    {
        string title = t as string;
        if(title != null)
        {
            title = Regex.Replace(title, @"^\[.*\] - (.*)$", "$+");

        }
        return title;
    }

    protected string FormatLocation(object d)
    {
        string description = d as string;
        if(description != null)
        {
            description = Regex.Match(description, @".*<p><strong>Location:</strong> (?<Location>.*?)</p>.*").Groups["Location"].Value;
        }
        return description;
    }

    public override void DataBind()
    {
        base.DataBind();
        if (this.Feed.Items.Count <= 0)
        {
            this.Visible = false;
        }
    }
}
