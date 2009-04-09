using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Mubble.UI
{
    /// <summary>
    /// A subclass of the System.Web.Ui.WebControls.Repeater class. This defines a new "Page" property, as well as adding Content and Url properties.
    /// </summary>
    public class Repeater : System.Web.UI.WebControls.Repeater, IControl
    {
        private string itemTemplatePath;

        public string ItemTemplatePath
        {
            get { return itemTemplatePath; }
            set { itemTemplatePath = value; }
        }

        [TemplateContainer(typeof(RepeaterTemplate)), PersistenceMode(PersistenceMode.InnerProperty), Browsable(false)]
        public override ITemplate HeaderTemplate
        {
            get { return base.HeaderTemplate; }
            set { base.HeaderTemplate = value; }
        }

        [TemplateContainer(typeof(RepeaterTemplate)), PersistenceMode(PersistenceMode.InnerProperty), Browsable(false)]
        public override ITemplate FooterTemplate
        {
            get { return base.FooterTemplate; }
            set { base.FooterTemplate = value; }
        }

        [TemplateContainer(typeof(RepeaterTemplate)), PersistenceMode(PersistenceMode.InnerProperty), Browsable(false)]
        public override ITemplate ItemTemplate
        {
            get { return base.ItemTemplate; }
            set { base.ItemTemplate = value; }
        }

        [TemplateContainer(typeof(RepeaterTemplate)), PersistenceMode(PersistenceMode.InnerProperty), Browsable(false)]
        public override ITemplate AlternatingItemTemplate
        {
            get { return base.AlternatingItemTemplate; }
            set { base.AlternatingItemTemplate = value; }
        }

        protected override System.Web.UI.WebControls.RepeaterItem CreateItem(int itemIndex, ListItemType itemType)
        {
            RepeaterTemplate template = new RepeaterTemplate(itemIndex, itemType);
            template.Controller = this.Content;
            return template;
        }

        /// <summary>
        /// Gets the request parameters
        /// </summary>
        public PathParameters Params
        {
            get { return Page.Params; }
        }

        private Mubble.UI.Page page;

        /// <summary>
        /// Gets the container page for this control
        /// </summary>
        new public Mubble.UI.Page Page
        {
            get
            {
                if (this.page == null)
                {
                    this.page = base.Page as Mubble.UI.Page;
                }
                return page;
            }
        }

        private Controller controller;
        /// <summary>
        /// Gets the content for the current request
        /// </summary>
        public Controller Content
        {
            get
            {
                if (controller == null)
                {
                    controller = this.Page.Controller;
                }
                return controller;
            }
            set { this.controller = value; }
        }

        /// <summary>
        /// Gets the parsed URL of the current request
        /// </summary>
        public MubbleUrl Url
        {
            get
            {
                return this.Page.Url;
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            string value = this.ItemTemplatePath;
            if (value != null && value.Length > 0)
            {
                ITemplate temp = Page.LoadTemplate(value);
                this.ItemTemplate = temp;
            }
            base.OnDataBinding(e);
        }
    }
}
