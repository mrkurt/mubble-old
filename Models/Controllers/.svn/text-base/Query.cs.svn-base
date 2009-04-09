using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Mubble.Models.Controllers
{
    public class Query : Controller
    {
        public Mubble.Models.QueryEngine.Query GetSpecifiedQuery()
        {
            Mubble.Models.QueryEngine.Query q = new Mubble.Models.QueryEngine.Query();
            string text = this.MetaData.GetFirstStringValue("Query");
            bool parsed = false;
            if (text != null && text.StartsWith("<"))
            {
                XmlDocument xml = new XmlDocument();
                try
                {
                    xml.LoadXml(text);
                    parsed = true;

                    Mubble.Models.QueryEngine.Query.Parse(xml, q);
                }
                catch (XmlException) { }
            }
            if(!parsed)
            {
                q.Text = this.MetaData.GetFirstStringValue("Query");
            }
            return q;
        }

        public override List<Mubble.Models.QueryEngine.Content> GetContentList(string[] groups)
        {
            Mubble.Models.QueryEngine.Query q = this.GetSpecifiedQuery();
            q.OrderBy = "PublishDate DESC";

            q.EndResultIndex = 20;

            return QueryEngine.Content.Query(q, groups);
        }
    }
}
