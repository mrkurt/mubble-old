using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Mubble.UI
{
    /// <summary>
    /// A subclass of System.Web.UI.HtmlTextWriter, all it does is fix the retarded "action" attributes in forms.  And this:
    /// Writes markup characters and text to an ASP.NET server control output stream. 
    /// This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.
    /// </summary>
    public class HtmlTextWriter : System.Web.UI.HtmlTextWriter
    {
        private bool useFieldName = true;

        public bool UseFieldName
        {
            get { return useFieldName; }
            set { useFieldName = value; }
        }


        /// <summary>
        /// Initializes a new instance of the HtmlTextWriter class with a specified tab string character. 
        /// </summary>
        /// <param name="writer">The TextWriter that renders the markup content.</param>
        /// <param name="tabString">The string to use to render a line indentation.</param>
        public HtmlTextWriter(System.Web.UI.HtmlTextWriter writer, string tabString)
            : base(writer, tabString)
        {

        }
        /// <summary>
        /// Initializes a new instance of the HtmlTextWriter class that uses a default tab string. 
        /// </summary>
        /// <param name="writer">The TextWriter that renders the markup content.</param>
        public HtmlTextWriter(System.Web.UI.HtmlTextWriter writer)
            : base(writer)
        {

        }
        private string currentTag;
        /// <summary>
        /// Writes any tab spacing and the opening tag of the specified markup element to the output stream. 
        /// </summary>
        /// <param name="tagName">The markup element of which to write the opening tag.</param>
        public override void WriteBeginTag(string tagName)
        {
            currentTag = tagName.ToLower();
            base.WriteBeginTag(tagName);
        }
        /// <summary>
        /// Writes a markup attribute and its value to the output stream. 
        /// </summary>
        /// <param name="name">The attribute to write to the output stream</param>
        /// <param name="value">The value of the attribute</param>
        public override void WriteAttribute(string name, string value)
        {
            if (name.ToLower() == "action")
            {
                base.WriteAttribute("action", HttpContext.Current.Request.RawUrl);
            }
            else if (this.UseFieldName && name.Equals("name", StringComparison.CurrentCultureIgnoreCase))
            {
                
            }
            else if (this.UseFieldName && name.Equals("FieldName", StringComparison.CurrentCultureIgnoreCase))
            {
                base.WriteAttribute("name", value);
            }
            else
            {
                base.WriteAttribute(name, value);
            }
        }
        /// <summary>
        /// Writes the specified markup attribute and value to the output stream, 
        /// and, if specified, writes the value encoded. 
        /// </summary>
        /// <param name="name">The attribute to write to the output stream</param>
        /// <param name="value">The value of the attribute</param>
        /// <param name="fEncode">true to encode the attribute and its assigned value; otherwise, false.</param>
        public override void WriteAttribute(string name, string value, bool fEncode)
        {
            if (name.ToLower() == "action")
            {
                base.WriteAttribute("action", HttpContext.Current.Request.RawUrl, fEncode);
            }
            else if (this.UseFieldName && name.Equals("name", StringComparison.CurrentCultureIgnoreCase))
            {

            }
            else if (this.UseFieldName && name.Equals("FieldName", StringComparison.CurrentCultureIgnoreCase))
            {
                base.WriteAttribute("name", value, fEncode);
            }
            else if (name.Equals("mubble-action", StringComparison.CurrentCultureIgnoreCase))
            {
                base.WriteAttribute("action", value, fEncode);
            }
            else
            {
                base.WriteAttribute(name, value, fEncode);
            }
        }
    }
}