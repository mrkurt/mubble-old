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
using Mubble;
using Mubble.Models;

namespace Mubble.Web.Admin
{
    public partial class ModulePicker : Mubble.UI.UserControl
    {
        private bool showWildCardControl = false;

        /// <summary>
        /// Gets or sets a flag indicating that the control listing should include a wildcard choice.
        /// </summary>
        public bool ShowWildCardControl
        {
            get { return showWildCardControl; }
            set { showWildCardControl = value; }
        }

        private Guid moduleId;
        /// <summary>
        /// Gets the selected ModuleId
        /// </summary>
        public Guid ModuleId
        {
            get
            {
                if (this.lstModules.SelectedValue != null)
                {
                    return new Guid(this.lstModules.SelectedValue);
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set
            {
                if (this.lstModules.Items.FindByValue(value.ToString()) != null)
                {
                    this.lstModules.SelectedValue = value.ToString();
                    this.LoadModuleControls(value);
                }
                moduleId = value;
            }
        }
        private Guid control;
        /// <summary>
        /// Gets the name of the selected control
        /// </summary>
        public Guid Control
        {
            get
            {
                return new Guid(this.lstControls.SelectedValue);
            }
            set
            {
                if (this.lstControls.Items.FindByValue(value.ToString()) != null)
                {
                    this.lstControls.SelectedValue = value.ToString();
                }
                control = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lstModules.DataBound += new EventHandler(lstModules_DataBound);
            this.lstControls.DataBound += new EventHandler(lstControls_DataBound);
            if (!Page.IsPostBack)
            {
                this.lstModules.DataSource = Mubble.Models.Module.Find();
                this.lstModules.DataValueField = "ID";
                this.lstModules.DataTextField = "Name";
                this.lstModules.DataBind();
            }
        }

        protected void LoadModuleControls(Guid moduleId)
        {
            Module m = new Module(moduleId);

            this.lstControls.DataSource = m.ModuleControls.Sort("Order");
            this.lstControls.DataValueField = "ID";
            this.lstControls.DataTextField = "Name";
            this.lstControls.DataBind();
        }

        protected void lstModules_Changed(object sender, System.EventArgs e)
        {
            this.LoadModuleControls(new Guid(this.lstModules.SelectedValue));
        }

        void lstModules_DataBound(object sender, EventArgs e)
        {
            if (this.moduleId != null && this.lstModules.Items.FindByValue(this.moduleId.ToString()) != null)
            {
                this.lstModules.SelectedValue = this.moduleId.ToString();
            }
            Guid mId = new Guid(this.lstModules.SelectedValue);
            this.LoadModuleControls(mId);
        }

        void lstControls_DataBound(object sender, EventArgs e)
        {
            if (this.control != null && this.lstControls.Items.FindByValue(this.control.ToString()) != null)
            {
                this.lstControls.SelectedValue = this.control.ToString();
            }
        }
    }
}
