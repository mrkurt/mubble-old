using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Data
{
    public class UrlParameters : FilterBase, IPostFilter, IAuthorFilter
    {

        public override void Before(Dictionary<string, object> parameters)
        {
            Mubble.PathParameters urlParams = this.Parent.Page.Params;
            if (this.Parent is Authors && urlParams["Author"] != null)
            {
                if (parameters.ContainsKey("UserName"))
                {
                    parameters["UserName"] = urlParams["Author"];
                }
                else
                {
                    parameters.Add("Username", urlParams["Author"]);
                }
            }
            if (this.Parent is Posts)
            {
                if (urlParams == null) return;
                if (urlParams["Slug"] != null)
                {
                    parameters.Add("Slug", urlParams["Slug"]);
                }
                else
                {
                    DateTime start = new DateTime(1970, 1, 1), end = DateTime.MaxValue;
                    if (urlParams["Year"] != null && urlParams["Month"] != null && urlParams["Day"] != null)
                    {
                        DateTime.TryParse(
                            string.Format("{0}/{1}/{2} 00:00:00",
                                urlParams["Month"],
                                urlParams["Day"],
                                urlParams["Year"]
                                ),
                            out start
                            );
                        end = start.AddDays(1);
                    }
                    else if (urlParams["Year"] != null && urlParams["Month"] != null)
                    {
                        DateTime.TryParse(
                            string.Format("{0}/1/{1} 00:00:00",
                                urlParams["Month"],
                                urlParams["Year"]
                                ),
                                out start
                            );
                        end = start.AddMonths(1);
                    }
                    else if (urlParams["Year"] != null)
                    {
                        DateTime.TryParse(
                            string.Format("1/1/{0} 00:00:00",
                                urlParams["Year"]
                                ),
                                out start
                            );
                        end = start.AddYears(1);
                    }
                    if (start < new DateTime(1970, 1, 1))
                    {
                        start = new DateTime(1970, 1, 1);
                    }
                    parameters.Add("StartPublishDate", start);
                    parameters.Add("EndPublishDate", end);
                }
            }
        }
    }
}
