using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models.QueryEngine;
using System.Text.RegularExpressions;

namespace Mubble.UI.Data
{
    public class UserSearch : BaseQueryFilter
    {
        public override Query BuildQuery(Query current)
        {
            System.Web.HttpRequest request = this.Parent.Page.Request;
            System.Web.HttpResponse response = this.Parent.Page.Response;
            string freetext = request.QueryString["search"];
            Query q = new Query();

            bool isValid = false;
            if (freetext != null && freetext.Length > 0)
            {
                q.Text += freetext;
                isValid = true;
            }
            if ("true".Equals(request.QueryString["featured"], StringComparison.CurrentCultureIgnoreCase))
            {
                q.AddTerm("IsFeatured", "true", true, false);
                q.AddTerm("IsFeatured", "false", false, true);
                isValid = true;
            }

            string t = request.QueryString["tag"];
            if (t != null && t.Trim().Length > 0)
            {
                string[] tags = t.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Regex rx = new Regex(@"(?<Term>\w+)(?<Boost>\^\d+)");
                foreach (string tag in tags)
                {
                    Match m = rx.Match(tag);
                    if (m.Success)
                    {
                        q.Add(new TermClause("Tag", m.Groups["Term"].Value, float.Parse(m.Groups["Boost"].Value.Substring(1))));
                    }
                    else
                    {
                        q.AddTerm("Tag", t, true, false);
                    }
                }
                isValid = true;
            }
            string author = request.QueryString["author"];
            if (author != null && author.Length > 0)
            {
                q.AddTerm("Author", author, true, false);
                isValid = true;
            }
            string section = request.QueryString["path"];
            if (section != null && section.Length > 1)
            {
                q.AddTerm("Path", section + "*", true, true, false);
                isValid = true;
            }

            if ("score".Equals(request.QueryString["sort"], StringComparison.CurrentCultureIgnoreCase))
            {
                q.OrderBy = "Score";
            }

            current.Add(q);

            if (!isValid)
            {
                current.IsValid = false;
            }

            return current;
        }
    }
}
