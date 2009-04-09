using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

using Mubble.Models;
using ActiveObjects;

namespace Mubble.UI.WebControls
{
    public class ContentList : Mubble.UI.Repeater
    {
        #region Properties

        private int firstRecord;

        public int FirstRecord
        {
            get { return firstRecord; }
            set { firstRecord = value; }
        }

        private int lastRecord;

        public int LastRecord
        {
            get { return lastRecord; }
            set { lastRecord = value; }
        }

        private Controller rootContent;

        /// <summary>
        /// Gets or sets the base content for this listing
        /// </summary>
        public Controller RootContent
        {
            get 
            {
                if (rootContent == null && Page != null)
                {
                    rootContent = Page.Controller;
                }
                return rootContent; 
            }
            set { rootContent = value; }
        }

        private Guid moduleId;

        /// <summary>
        /// Gets or sets the ModuleId to include in the listing
        /// </summary>
        public Guid ModuleId
        {
            get 
            {
                if (moduleId == Guid.Empty)
                {
                    moduleId = this.RootContent.ModuleControl.ModuleID;
                }
                return moduleId; 
            }
            set { moduleId = value; }
        }

        private string moduleControl;

        /// <summary>
        /// Gets or sets the ModuleControl to include in the listing
        /// </summary>
        public string ModuleControl
        {
            get 
            {
                if (moduleControl == null)
                {
                    moduleControl = this.RootContent.ModuleControl.FileName;
                }
                return moduleControl; 
            }
            set { moduleControl = value; }
        }

        private string orderBy;

        /// <summary>
        /// Gets or sets the name of the field to sort content by
        /// </summary>
        public string OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }

        private bool excludeDrafts = true;

        /// <summary>
        /// Gets or sets a flag indicating whether the listing should exclude draft content.
        /// </summary>
        public bool ExcludeDrafts
        {
            get { return excludeDrafts; }
            set { excludeDrafts = value; }
        }

        private bool recursive = false;

        /// <summary>
        /// Gets or sets a flag indicating whether the listing should include children's children, etc.
        /// </summary>
        public bool Recursive
        {
            get { return recursive; }
            set { recursive = value; }
        }

        #endregion

        /// <summary>
        /// Creates a new ContentList object
        /// </summary>
        public ContentList()
        {
        }
        public override void DataBind()
        {
            this.OnDataBinding(EventArgs.Empty);

            Dictionary<string, object> parameters = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
            parameters.Add("ControllerID", this.RootContent.ID);
            if (this.ModuleId != Guid.Empty)
            {
                parameters.Add("ModuleID", this.ModuleId);
            }
            if (this.ModuleControl != null)
            {
                parameters.Add("ModuleControl", this.ModuleControl);
            }
            if (this.ExcludeDrafts)
            {
                parameters.Add("Status", PublishStatus.Published);
            }
            //if (this.OrderBy != null)
            //{
            //    list.OrderBy(this.OrderBy);
            //}

            ActiveCollection<Controller> list = Mubble.Models.Controller.Find(parameters);
            this.DataSource = list.Range(this.FirstRecord, this.LastRecord);

            base.DataBind();
        }
    }
}
