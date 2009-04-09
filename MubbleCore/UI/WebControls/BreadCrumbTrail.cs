using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.WebControls
{
    public class BreadCrumbTrail : Repeater
    {

        private string contentPath;

        /// <summary>
        /// The path to the CMS content this should link to
        /// </summary>
        public string ContentPath
        {
            get { return contentPath; }
            set { contentPath = value; }
        }

        new protected object DataSource
        {
            set { base.DataSource = value; }
            get { return base.DataSource; }
        }

        public override void DataBind()
        {
            List<Controller> crumbs = new List<Controller>();
            if (this.ContentPath != null)
            {
                Content = new Controller(this.ContentPath);
            }
            Controller current = Content;
            do
            {
                crumbs.Add(current);
                current = current.Parent;
            }
            while (current != null);

            crumbs.Reverse();

            this.DataSource = crumbs;
            base.DataBind();
        }
    }
}
