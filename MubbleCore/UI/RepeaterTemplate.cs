using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Mubble.Models;

namespace Mubble.UI
{
    public class RepeaterTemplate : System.Web.UI.WebControls.RepeaterItem
    {
        private Controller controller;

        public Controller Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        public RepeaterTemplate(int itemIndex, System.Web.UI.WebControls.ListItemType itemType) : base(itemIndex, itemType) { }
    }
}
