using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Web.UI;
using System.ComponentModel;

namespace Mubble.UI.Data
{
    public class Tags : Mubble.UI.BlankRepeater
    {
        private string field = "Tag";

        public string Field
        {
            get { return field; }
            set { field = value; }
        }

        [TemplateContainer(typeof(TagRepeaterTemplate)), PersistenceMode(PersistenceMode.InnerProperty), Browsable(false)]
        public System.Web.UI.ITemplate TagTemplate
        {
            get { return this.ItemTemplate; }
            set { this.ItemTemplate = value; }
        }

        private bool databound = false;

        public override void DataBind()
        {
            if (!this.databound)
            {
                IMetaData metadata = Control.GetCurrentScope<IMetaData>(this);
                List<Tag> tags = null; ;
                if (metadata != null && metadata.MetaData != null && (tags = metadata.MetaData.Get(this.Field)) != null)
                {
                    this.DataSource = tags;
                    base.DataBind();
                }
            }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.DataBind();
            base.Render(writer);
        }
        protected override System.Web.UI.WebControls.RepeaterItem CreateItem(int itemIndex, System.Web.UI.WebControls.ListItemType itemType)
        {
            TagRepeaterTemplate item = new TagRepeaterTemplate(itemIndex, itemType);
            item.Controller = this.Content;
            return item;
        }

    }
}
