using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mubble.UI.WebControls
{
    public class PathConditional : ConditionalView
    {
        private string pathPattern;

        public string PathPattern
        {
            get { return pathPattern; }
            set { pathPattern = value; }
        }

        private string pathExtraPattern;

        public string PathExtraPattern
        {
            get { return pathExtraPattern; }
            set { pathExtraPattern = value; }
        }

        private string moduleControlView;

        public string ModuleControlView
        {
            get { return moduleControlView; }
            set { moduleControlView = value; }
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (matches && this.PathPattern != null && 
                !Regex.IsMatch(this.Content.Path, this.PathPattern, RegexOptions.IgnoreCase))
            {
                matches = false;
            }
            if (matches && this.PathExtraPattern != null &&
                !Regex.IsMatch(this.Page.Url.PathExtra, this.PathExtraPattern, RegexOptions.IgnoreCase))
            {
                matches = false;
            }

            if (matches && this.ModuleControlView != null &&
                !this.ModuleControlView.Equals(Content.ModuleControlView, StringComparison.CurrentCultureIgnoreCase))
            {
                matches = false;
            }

            if (this.Matches)
            {
                base.Render(writer);
            }
        }
    }
}
