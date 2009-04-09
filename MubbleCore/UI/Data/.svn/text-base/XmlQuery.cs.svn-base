using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Xml;

namespace Mubble.UI.Data
{
    [ParseChildren(true, "xml")]
    public class XmlQuery : BaseQueryFilter
    {
        private string xml;

        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public string Xml
        {
            get { return xml; }
            set { xml = value; }
        }

        public override Mubble.Models.QueryEngine.Query BuildQuery(Mubble.Models.QueryEngine.Query current)
        {
            XmlDocument x = new XmlDocument();
            x.LoadXml(this.Xml);
            Mubble.Models.QueryEngine.Query.Parse(x, current);
            return current;
        } 
    }
}
