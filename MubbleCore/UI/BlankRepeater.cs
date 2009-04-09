using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI
{
    public class BlankRepeater : Mubble.UI.Repeater
    {
        #region Hiding generic templates
        new protected System.Web.UI.ITemplate ItemTemplate
        {
            get { return base.ItemTemplate; }
            set { base.ItemTemplate = value; }
        }
        new protected System.Web.UI.ITemplate AlternatingItemTemplate
        {
            get { return base.AlternatingItemTemplate; }
            set { base.AlternatingItemTemplate = value; }
        }
        new protected string ItemTemplatePath
        {
            get { return base.ItemTemplatePath; }
            set { base.ItemTemplatePath = value; }
        }
        #endregion
    }
}
