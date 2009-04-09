using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Data
{
    public class FreeText : BaseQueryFilter
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public override Mubble.Models.QueryEngine.Query BuildQuery(Mubble.Models.QueryEngine.Query current)
        {
            if (!string.IsNullOrEmpty(current.Text))
            {
                current.Text += " ";
            }
            current.Text += this.Text;
            return current;
        }
    }
}
