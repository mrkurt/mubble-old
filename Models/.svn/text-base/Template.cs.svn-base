using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class Template
	{
        private Mubble.Models.Config.Template config;

        /// <summary>
        /// Gets or sets the configuration for this template
        /// </summary>
        public Mubble.Models.Config.Template Config
        {
            get
            {
                if (config == null)
                {
                    this.LoadConfig();
                }
                return config;
            }
            set { config = value; }
        }

        /// <summary>
        /// Loads the configuration for the current module instance
        /// </summary>
        protected void LoadConfig()
        {
            if (this.ID != Guid.Empty)
            {
                string configFile = this.Path + "template.config";
                configFile = System.Web.HttpContext.Current.Server.MapPath(configFile);
                config = Mubble.Models.Config.Template.Load(configFile);
            }
        }
	}
}
