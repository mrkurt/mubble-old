using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Mubble;

public partial class Admin_Metrics_Application : System.Web.UI.Page
{
    protected int TotalQueries = 0;
    protected TimeSpan TimeToReRunQueries = TimeSpan.Zero;
    protected void Page_Load(object sender, EventArgs e)
    {
		Dictionary<Guid, string> duplicates = Mubble.Models.QueryEngine.Content.GetDuplicates();
		this.Duplicates.DataSource = duplicates;
		this.Duplicates.DataBind();
        //List<CachedQuery> queries = Mubble.QueryBroker.GetCachedQueries();
        //queries.Sort();
        //queries.Reverse();
        //TotalQueries = queries.Count;
        //for (int i = 0; i < queries.Count; i++)
        //{
        //    TimeToReRunQueries += queries[i].Time;
        //}
        //this.Queries.DataSource = queries.Count > 20 ? queries.GetRange(0, 20) : queries;
        //this.Queries.DataBind();
    }
}
