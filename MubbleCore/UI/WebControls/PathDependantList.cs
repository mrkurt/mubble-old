using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.ComponentModel;
using Mubble.Models;

namespace Mubble.UI.WebControls
{
    [
    ToolboxData("<{0}:PathDependantList runat=\"server\"> </{0}:PathDependantList>")
    ]
    public class PathDependantList : Mubble.UI.CompositeControl
    {
        private ContentList listTemplates;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ContentList ListTemplates
        {
            get { return listTemplates; }
            set { listTemplates = value; }
        }


        private List<PathDependantListRule> rules = new List<PathDependantListRule>();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public PathDependantListRule Rule
        {
            get { return rules[rules.Count - 1]; }
            set { rules.Add(value); }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            PathDependantListRule rule = null;
            foreach (PathDependantListRule r in rules)
            {
                if (r.MatchesPath(this.Content))
                {
                    rule = r;
                    break;
                }
            }
            if (this.ListTemplates != null && rule != null)
            {
                this.ListTemplates.RootContent = new Controller(rule.RootContentPath);
                if(rule.OrderBy != null && rule.OrderBy != string.Empty) this.ListTemplates.OrderBy = rule.OrderBy;
                //this.ListTemplates.Recursive = rule.Recursive;
                if (rule.ModuleId != null && rule.ModuleId != Guid.Empty) this.ListTemplates.ModuleId = rule.ModuleId;
                if (rule.ModuleControl != null && rule.ModuleControl != string.Empty) this.ListTemplates.ModuleControl = rule.ModuleControl;
                this.Controls.Add(this.ListTemplates);
            }
        }
    }

    public class PathDependantListRule : PathDependantRule
    {
        private string rootContentPath;

        [PersistenceMode(PersistenceMode.Attribute)]
        public string RootContentPath
        {
            get { return rootContentPath; }
            set { rootContentPath = value; }
        }

        private string orderBy;

        /// <summary>
        /// Gets or sets the name of the field to sort content by
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public string OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }

        private bool excludeDrafts = true;

        /// <summary>
        /// Gets or sets a flag indicating whether the listing should exclude draft content.
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public bool ExcludeDrafts
        {
            get { return excludeDrafts; }
            set { excludeDrafts = value; }
        }

        private Guid moduleId;

        /// <summary>
        /// Gets or sets the ModuleId to include in the listing
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public Guid ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        private string moduleControl;

        /// <summary>
        /// Gets or sets the ModuleControl to include in the listing
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public string ModuleControl
        {
            get { return moduleControl; }
            set { moduleControl = value; }
        }

        private bool recursive = false;

        /// <summary>
        /// Gets or sets a flag indicating whether the listing should include children's children, etc.
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public bool Recursive
        {
            get { return recursive; }
            set { recursive = value; }
        }
    }
}
