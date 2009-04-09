using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using skmRss.Engine;
using System.Web;
using System.Xml;

namespace Mubble.UI.WebControls.Listings
{
    public class RssFeed : Repeater
    {

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<string, DateTime> pendingRequests = new Dictionary<string, DateTime>(StringComparer.CurrentCultureIgnoreCase);

        private int maxItems = 10;

        public int MaxItems
        {
            get { return maxItems; }
            set { maxItems = value; }
        }


        public string Feed
        {
            get { return base.DataSource as string; }
            set { base.DataSource = value; }
        }

        public override void DataBind()
        {
            try
            {
                RssItemList rssData = new RssItemList();
                RssDocument rssDoc = null;

                // Get the rssData
                if (Feed != null)
                {
                    try
                    {
                        // get the proper dataSource (based on if the DataSource is a URL, file path, XmlReader, etc.)

                        RssEngine rssEng = new RssEngine();

                        // Get the RssDocument - either from cache or not...
                        if (DataSource is string)
                        {
                            // See if we have a cached version
                            object cachedRssDoc = HttpContext.Current.Cache.Get(string.Concat("RssFeed||", this.Feed));
                            if (cachedRssDoc != null)
                                rssDoc = (RssDocument)cachedRssDoc;
                            else
                            {
                                // get the DataSource and cache it
                                bool isPending = false;

                                lock (pendingRequests)
                                {
                                    isPending = pendingRequests.ContainsKey((string)DataSource);
                                    if (!isPending)
                                    {
                                        pendingRequests.Add((string)DataSource, DateTime.Now);
                                    }
                                    else
                                    {
                                        if (pendingRequests[(string)DataSource] < DateTime.Now.AddMinutes(-5))
                                        {
                                            pendingRequests.Remove((string)DataSource);
                                        }
                                    }
                                }

                                if (!isPending)
                                {
                                    try
                                    {
                                        rssDoc = rssEng.GetDataSource(DataSource);
                                        HttpContext.Current.Cache.Insert(string.Concat("RssFeed||", Feed), rssDoc, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
                                    }
                                    finally
                                    {
                                        pendingRequests.Remove((string)DataSource);
                                    }
                                }
                            }
                        }
                        else { }
                            // We are NOT using caching, so grab the DataSource
                            //rssDoc = rssEng.GetDataSource(DataSource);

                        if (rssDoc == null) return;

                        for (int i = 0; i < rssDoc.Items.Count && (this.MaxItems > 0 && i < this.MaxItems); i++)
                        {
                            rssData.Add(rssDoc.Items[i]);
                        }
                    }
                    catch (XmlException)
                    {
                        // whoops, there was a problem parsing the data.
                        //isValidXml = false;
                    }
                }

                this.DataSource = rssData;
            }
            catch (Exception ex)
            {
                //log.Error(string.Format("RSS control error for {0}", Url), ex);
                return;
            }

            base.DataBind();

        }
    }
}
