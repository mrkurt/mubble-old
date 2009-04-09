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

public partial class Templates_ars_OpenForum : Mubble.UI.UserControl
{
    public Templates_ars_OpenForum()
    {
        this.Load += new EventHandler(Templates_ars_OpenForum_Load);
    }

    void Templates_ars_OpenForum_Load(object sender, EventArgs e)
    {
        if (Mubble.Config.Discussions.Current.Enabled)
        {
            this.Feed.Feed = "http://episteme.arstechnica.com/eve/forums?a=ci&amp;ci_id=696006620731&amp;feedType=rss2";
        }
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
