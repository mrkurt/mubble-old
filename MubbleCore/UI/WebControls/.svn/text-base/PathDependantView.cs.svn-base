using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Web.UI.Design;

namespace Mubble.UI.WebControls
{
    /// <summary>
    /// This control renders a single child <typeparamref name="PathDependantView"/> 
    /// template based on the <typeparamref name="Mubble.Content"/> path 
    /// of the current request.
    /// </summary>
    [
    ParseChildren(false),
    ToolboxData("<{0}:PathDependantView runat=\"server\"> </{0}:PathDependantView>")
    ]
    public class PathDependantView : Mubble.UI.CompositeControl
    {
        private List<PathDependantViewTemplate> views = new List<PathDependantViewTemplate>();
        private PathDependantViewTemplate view;

        [
        PersistenceMode(PersistenceMode.InnerProperty),
        TemplateInstance(TemplateInstance.Multiple)
        ]
        public PathDependantViewTemplate View
        {
            get { return view; }
            set { view = value; }
        }
    }

    [ParseChildren(false, "Template")]
    [ToolboxItem(false)]
    public class PathDependantViewTemplate : Mubble.UI.PathDependantRule
    {
        internal sealed class PathDependantViewTemplateContainer : System.Web.UI.WebControls.WebControl
        {

        }

        private ITemplate template;

        [
        TemplateContainer(typeof(PathDependantViewTemplate))
        ]
        public ITemplate Template
        {
            get { return template; }
            set { template = value; }
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();
            if (this.Template != null)
            {
                PathDependantViewTemplateContainer i = new PathDependantViewTemplateContainer();
                this.Template.InstantiateIn(i);
                Controls.Add(i);
            }
        }
    }
}
