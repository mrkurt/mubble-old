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
using ActiveObjects;
using Mubble.Models;

public partial class Admin_UserControls_DiscussionOptions : Mubble.UI.Admin.ActiveObjectFieldEditor
{
	IDiscussable discussable = null;
	public override IActiveObject ActiveObject
	{
		get { return base.ActiveObject; }
		set { base.ActiveObject = value; discussable = value as IDiscussable; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		this.txtTitle.InnerHtml = this.Title;

		if (!Page.IsPostBack && discussable != null)
		{
			this.chckDiscussion.Checked = (int)discussable.Discussion.Status != 4;
			this.txtDiscussionLink.Text = discussable.Discussion.DiscussionLink;
		}
		else if (Page.IsPostBack && discussable != null)
		{
			if (!this.chckDiscussion.Checked)
			{
				discussable.Discussion.Status = DiscussionStatus.Inactive;
			}
			else if (this.chckDiscussion.Checked && discussable.Discussion.Status == DiscussionStatus.Inactive)
			{
				discussable.Discussion.Status = DiscussionStatus.NotCreated;
			}
			if (!string.IsNullOrEmpty(this.txtDiscussionLink.Text))
			{
				discussable.Discussion.DiscussionLink = this.txtDiscussionLink.Text;
				discussable.Discussion.Status = DiscussionStatus.Exists;
			}
		}
	}
}
