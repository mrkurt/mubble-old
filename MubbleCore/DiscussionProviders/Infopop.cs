using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using ActiveObjects.SqlServer;
using System.Net;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Mubble.DiscussionProviders
{
    public class Infopop : DiscussionProvider
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<string, DateTime> pendingRequests = new Dictionary<string, DateTime>(StringComparer.CurrentCultureIgnoreCase);

        private ActiveObjects.DataManager dataManager = new SqlDataManager<Infopop>();
        public override ActiveObjects.DataManager DataManager
        {
            get { return this.dataManager; }
            set { this.dataManager = value; }
        }

        public override CommentCollection GetComments(string discussionLink, int pageNumber)
        {
            if (!Config.Discussions.Current.Enabled || discussionLink == null || discussionLink.Trim() == "")
            {
                throw new DiscussionUnavailableException("Discussion unavailable.");
            }

            string pendingKey = discussionLink + "-p" + pageNumber.ToString();

            bool isPending = false;

            lock (pendingRequests)
            {
                if (pendingRequests.ContainsKey(pendingKey))
                {
                    isPending = true;
                }
                else
                {
                    pendingRequests.Add(pendingKey, DateTime.Now);
                }
            }

            if (isPending) throw new DiscussionUnavailableException("Discussion request pending");

            string directLink = discussionLink;
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            CommentCollection comments = new CommentCollection();
            System.Xml.XmlDocument xml = new XmlDocument();
            discussionLink = discussionLink.Replace("&amp;", "&");
            discussionLink += "&xsl=xml&p=" + pageNumber;
            discussionLink += string.Format("&LOGIN_USERNAME={0}&LOGIN_PASSWORD={1}", this.SettingsCollection["LOGIN_USERNAME"], this.SettingsCollection["LOGIN_PASSWORD"]);
            discussionLink = Regex.Replace(discussionLink, "&r=\\d+", "");
            CommentCollection container = new CommentCollection();
            container.PageNumber = pageNumber;
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(discussionLink);
                req.Timeout = 5000;

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                using (StreamReader stream = new StreamReader(resp.GetResponseStream()))
                {
                    xml.LoadXml(stream.ReadToEnd());
                }
                XmlNode pages = xml.SelectSingleNode("/page/pages");
                if (pages != null && pages.Attributes["total"] != null)
                {
                    container.Pages = int.Parse(pages.Attributes.GetNamedItem("total").Value);
                    container.PageNumber = int.Parse(pages.Attributes.GetNamedItem("current").Value);
                }
                else
                {
                    container.Pages = 1;
                }
                int loopcount = 0;
                XmlNode profileLinkNode = xml.SelectSingleNode("/page/info/community/profile");
                if (profileLinkNode == null)
                {
                    throw new DiscussionUnavailableException(string.Format("<a href=\"{0}\">Discussion in the Open Forum</a>", directLink));
                }
                string profileLink = xml.SelectSingleNode("/page/info/community/profile").Attributes.GetNamedItem("url").Value;
                profileLink += "?x_myspace_page=profile&u={0}";
                foreach (XmlNode comment in xml.SelectNodes("/page/content/topic/message"))
                {
                    if (loopcount++ == 0 && pageNumber == 1)
                    {
                        continue;
                    }
                    Comment c = new Comment();
                    c.PostDate = DateTime.ParseExact(comment.Attributes.GetNamedItem("date-ietf").Value, "ddd, dd MMM yyyy HH:mm:ss G\\MTzz00", CultureInfo.InvariantCulture);
                    c.Body = comment.SelectSingleNode("content/message-body").InnerText;
                    if (comment.SelectSingleNode("author/user") != null)
                    {
                        c.UserName = comment.SelectSingleNode("author/user").Attributes.GetNamedItem("name").Value;
                        c.UserLink = string.Format(profileLink, comment.SelectSingleNode("author/user").Attributes.GetNamedItem("oid").Value);
                    }
                    else if (comment.SelectSingleNode("author/guest") != null)
                    {
                        c.UserName = string.Format("&lt;{0}&gt;", comment.SelectSingleNode("author/guest").Attributes.GetNamedItem("name").Value);
                        c.UserLink = null;
                    }
                    container.Add(c);
                }
                if (container.Pages == 1)
                {
                    container.TotalCount = loopcount;
                }
                else
                {
                    int pageToGet = (container.PageNumber == container.Pages) ? 1 : container.Pages;
                    discussionLink = Regex.Replace(discussionLink, "&p=\\d+", "");
                    discussionLink += "&p=" + pageToGet;

                    req = (HttpWebRequest)HttpWebRequest.Create(discussionLink);
                    req.Timeout = 5000;

                    resp = (HttpWebResponse)req.GetResponse();
                    using (StreamReader stream = new StreamReader(resp.GetResponseStream()))
                    {
                        xml.LoadXml(stream.ReadToEnd());
                    }

                    XmlNodeList commentList = xml.SelectNodes("/page/content/topic/message");
                    if (commentList != null)
                    {
                        if (pageToGet == container.Pages)
                        {
                            container.TotalCount = loopcount * (container.Pages - 1) + commentList.Count;
                        }
                        else
                        {
                            container.TotalCount = commentList.Count * (container.Pages - 1) + loopcount;
                        }
                    }
                }
                container.TotalCount--;
                //container.PostFormLink = this.GetPostFormLink(discussionLink);

                pendingRequests.Remove(pendingKey);
            }
            catch (WebException wex)
            {
                pendingRequests.Remove(pendingKey);
                throw new DiscussionUnavailableException(string.Format("<a href=\"{0}\">Discussion in the Open Forum</a>", directLink), wex);
            }
            catch (XmlException xmlex)
            {
                pendingRequests.Remove(pendingKey);
                throw new DiscussionUnavailableException(string.Format("<a href=\"{0}\">Discussion in the Open Forum</a>", directLink), xmlex);
            }
            finally { pendingRequests.Remove(pendingKey); }
            return container;
        }

        public override string CreateDiscussion(Link u, string title, string text)
        {
            string contentLink = u != null ? MubbleUrl.Url(u, "HtmlHandler") : null;
            string postLink = this.SettingsCollection["post_link"] as string;
            if (postLink == null)
            {
                throw new ArgumentException("No post link specified", "postLink");
            }
            text += "\r\n\r\n" + string.Format("<a href=\"{0}\">Read More</a>", contentLink);
            Random rand = new Random();
            string postData = "IS_EXISTING_MESSAGE=N&" +
                "POST_TYPE=PTYP_MSG&" +
                string.Format("f={0}&", this.SettingsCollection["f"]) +
                "USE_USER_PREF_SUBSCRIPTIONS=Y&" +
                string.Format("POSTING_FORM_ID={0}&", Math.Floor(100000 * rand.NextDouble())) +
                "USE_USER_PREF_SIGNATURE=Y&" +
                "ON_COMPLETE_REDIRECT_URL=http%3A%2F%2Fepisteme.arstechnica.com%2Feve%2Fubb.x%2Fa%2Ftpc%2Ff%2F893003462731%2Fm%2F300000862731&" +
                string.Format("SUBJECT={0}&", System.Web.HttpUtility.UrlEncode(title)) +
                "GUEST_NAME=&" +
                string.Format("MESSAGE_BODY={0}&", System.Web.HttpUtility.UrlEncode(text)) +
                string.Format("LOGIN_USERNAME={0}&", this.SettingsCollection["LOGIN_USERNAME"]) +
                string.Format("LOGIN_PASSWORD={0}&", this.SettingsCollection["LOGIN_PASSWORD"]) +
                "xsl=xml&UPDATE_MESSAGE=Post+Now";

            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create((string)this.SettingsCollection["post_link"]);

                req.Method = "POST";
                req.Timeout = 120000;
                req.ContentLength = postData.Length;
                req.ContentType = "application/x-www-form-urlencoded";

                System.IO.StreamWriter wrtr = new System.IO.StreamWriter(req.GetRequestStream());
                wrtr.Write(postData);
                wrtr.Close();

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                System.IO.StreamReader rdr = new System.IO.StreamReader(resp.GetResponseStream());

                string response = rdr.ReadToEnd();

                rdr.Close();

                Regex regex = new Regex(
                    @"<TEXT_MESSAGE_BODY>(?<Message>.*?)</TEXT_MESSAGE_BODY>(.*)<T"
                    + @"ARGET_URL>(?<ThreadUrl>.*?)</TARGET_URL>",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Singleline
                    | RegexOptions.ExplicitCapture
                    | RegexOptions.CultureInvariant
                    );

                Match match = regex.Match(response);

                if (match.Success)
                {
                    string url = match.Groups["ThreadUrl"].Value;
                    if (url.IndexOf("#") > 0)
                    {
                        url = url.Substring(0, url.IndexOf("#"));
                    }
                    return url;
                }
                else
                {
                    string rx = @"<TEXT_MESSAGE_BODY>.*duplicate\sof\san\sexisting\spost.*</TEXT_MESSAGE_BODY>";
                    if (Regex.IsMatch(response, rx, RegexOptions.IgnoreCase | RegexOptions.Singleline))
                    {
                        string message = string.Format("Duplicate forum message for: {0}", title);
                        log.Error(message);
                        throw new DiscussionCreationException(message, null, DiscussionCreationExceptionType.DuplicatePost);
                    }
                    else
                    {
                        string message = string.Format("Infopop error for: {0}\r\n{1}", title, response);
                        log.Error(message);
                        throw new DiscussionCreationException(message, null, DiscussionCreationExceptionType.Other);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(
                    string.Format("Discussion error for: {0} - {1}", title, ex.Message),
                    ex
                    );
                throw new DiscussionCreationException(ex.Message, ex, DiscussionCreationExceptionType.HttpError);
            }
        }

        public override string GetPostFormLink(string discussionLink)
        {
            /*http://episteme.arstechnica.com/eve/ubb.x?a=prply&x_popup=Y&f=942005082731&m=639006082731*/
            Regex regex = new Regex(
                @"f=(?<f>\d*)&m=(?<m>\d*)",
                RegexOptions.IgnoreCase
                | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace
                );

            Match match = regex.Match(discussionLink);
            if (!match.Success)
            {
                throw new DiscussionUnavailableException("Comment post form not available");
            }
            ///http://episteme.arstechnica.com/eve/ubb.x?xsl=custom_3
            string postLink = string.Format("{0}?xsl=custom_3&f={1}&m={2}&return_url={3}", this.SettingsCollection["post_link"], match.Groups["f"].Value, match.Groups["m"].Value, "{0}");
            return postLink;
        }
    }
}
