using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Mubble.UI
{
    public class AdminPage : Mubble.UI.Page
    {
        private string tab;

        public string Tab
        {
            get { return tab; }
            set { tab = value; }
        }

        public string AdminTemplateBase
        {
            get
            {
                return Page.ResolveUrl(Mubble.Config.Administration.Current.TemplatePath);
            }
        }

        private Mubble.Models.ModuleControl controlConfig;

        /// <summary>
        /// Gets or sets the control configuration object for this page
        /// </summary>
        public Mubble.Models.ModuleControl ControlConfig
        {
            get { return controlConfig; }
            set { controlConfig = value; }
        }


        private Mubble.Models.Module moduleConfig;
        /// <summary>
        /// Gets or sets the module configuration object for this page
        /// </summary>
        public Mubble.Models.Module ModuleConfig
        {
            get { return moduleConfig; }
            set { moduleConfig = value; }
        }

        /// <summary>
        /// Gets the AdminPage instance for this class
        /// </summary>
        new public AdminPage Page
        {
            get
            {
                return this;
            }
        }
    }
}
