using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using Mubble.Models;

namespace Mubble.UI.Html
{
    public class DiscussionLink : System.Web.UI.WebControls.HyperLink
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IActiveObject ao = Control.GetCurrentScope<IActiveObject>(this);
            Discussion d = ao as Discussion;
            if (d == null && (ao == null || ao.DataManager["Discussion"] == null)) return;
            if (d == null && ao != null)
            {
                d = ao.DataManager["Discussion"] as Discussion;
            }
            if (d.Status != DiscussionStatus.Exists) return;

            this.NavigateUrl = d.DiscussionLink;
            base.Render(writer);
        }
    }
}
