using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Mubble.UI.HtmlControls
{
    public static class FormField
    {
        public static void SetValue(System.Web.UI.HtmlControls.HtmlInputCheckBox control, System.Web.HttpContext context)
        {
            if (control.Value == null || control.Value.Length == 0) return;
            string value = GetValue(control.Attributes["FieldName"], context);
            control.Checked = control.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public static void SetValue(System.Web.UI.HtmlControls.HtmlInputControl control, System.Web.HttpContext context)
        {
            if (control.Value != null && control.Value.Length > 0) return;
            string value = GetValue(control.Attributes["FieldName"], context);
            control.Value = value;
        }

        public static void SetValue(System.Web.UI.HtmlControls.HtmlSelect control, System.Web.HttpContext context)
        {
            if (control.SelectedIndex >= 1) return;
            string value = GetValue(control.Attributes["FieldName"], context);
            if (value == null) return;
            foreach (ListItem item in control.Items)
            {
                if (item.Value != null && item.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
        }

        static string GetValue(string fieldname, System.Web.HttpContext context)
        {
            if (fieldname != null)
            {
                string val = context.Request.Form[fieldname];
                if (val == null || val.Length <= 0) val = context.Request.QueryString[fieldname];
                return val;
            }
            return null;
        }

        public static HtmlTextWriter PrepareWriter(System.Web.UI.HtmlControls.HtmlControl control, System.Web.UI.HtmlTextWriter writer)
        {
            HtmlTextWriter wrtr = writer as HtmlTextWriter;
            if (wrtr == null) wrtr = new HtmlTextWriter(writer);
            wrtr.UseFieldName = control.Attributes["FieldName"] != null;

            return wrtr;
        }
    }
}
