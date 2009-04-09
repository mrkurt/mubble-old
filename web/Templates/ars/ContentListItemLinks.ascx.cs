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

public partial class Templates_ars_ContentListItemLinks : System.Web.UI.UserControl
{
    private string discussionImage = "images/discussion.gif";

    public string DiscussionImage
    {
        get { return discussionImage; }
        set { discussionImage = value; }
    }

    private string fullStoryImage = "images/full-story.gif";

    public string FullStoryImage
    {
        get { return fullStoryImage; }
        set { fullStoryImage = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
