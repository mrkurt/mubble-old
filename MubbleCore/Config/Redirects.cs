using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Xml;
using System.Text.RegularExpressions;
using Mubble.Models;
using ActiveObjects;

namespace Mubble.Config
{
    public class Redirects : BaseSection<Redirects>
    {
        private List<Regex> patterns = new List<Regex>();

        public List<Regex> Patterns
        {
            get { return patterns; }
            set { patterns = value; }
        }

        public string FindRedirect(string url)
        {
            foreach (Regex pattern in this.Patterns)
            {
                Match match = pattern.Match(url);
                if (match.Success && match.Groups["FileName"] != null)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("FileName", match.Groups["FileName"].Value);
                    parameters.Add("RowIndex_start", 0);
                    parameters.Add("RowIndex_end", 10);
                    ActiveCollection<Controller> controllers = Controller.Find(parameters);

                    if (controllers.Count > 0)
                    {
                        return MubbleUrl.Url(controllers[0].Url, "HtmlHandler");
                    }
                }
            }
            return null;
        }
    }
}
