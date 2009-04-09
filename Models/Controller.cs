using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;
using System.Text.RegularExpressions;

namespace Mubble.Models
{
    [HasMetadata]
    [LockedDown]
    [RequirePermissions(PermissionType.View, "view", "view unpublished", "draft", "publish", "full")]
    [IsIndexable( OptionsType=typeof(ControllerIndexingOptions) )]
    [Filters.UrlFriendlyFilter("FileName", FilterType.Set)]
	public partial class Controller : ICloneable, Mubble.Models.Controllers.IMailable, IContent
    {

        #region Properties

        public static string RootContentPath;

        private static Controller rootContent;
        /// <summary>
        /// Gets the root content
        /// </summary>
        public static Controller RootContent
        {
            get
            {
                if (rootContent == null) rootContent = Controller.FindFirst(RootContentPath);
                return rootContent;
            }
            set
            {
                rootContent = value;
            }
        }

        public MetaDataCollection MetaData
        {
            get { return this.DataManager.GetTypedProperty<MetaDataCollection>("MetaData"); }
        }

        private PermissionsCollection permissions;
        public PermissionsCollection Permissions
        {
            get 
            {
                if (permissions == null)
                {
                    permissions = this.DataManager.GetTypedProperty<PermissionsCollection>("Permissions");
                    if (this.PublishDate > DateTime.Now || this.Status != PublishStatus.Published)
                    {
                        permissions.ResetViewPermissions("view unpublished", "draft", "publish", "full");
                    }
                }
                return permissions;
            }
        }

        public string ParentFileName
        {
            get { return (this.Parent != null) ? this.Parent.FileName : null; }
        }

        private ContentSettings settings;
        /// <summary>
        /// Gets or sets the settings object for the current content instance.
        /// </summary>
        public ContentSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    string xmlSettings = this.DataManager.GetTypedProperty("Settings", string.Empty);
                    if (string.Empty == xmlSettings)
                    {
                        settings = new ContentSettings();
                    }
                    else
                    {
                        settings = ContentSettings.Load(xmlSettings);
                    }
                }
                return settings;
            }
            set
            {
                settings = value;
                this.DataManager["Settings"] = settings.Serialize();
            }
        }

        public PublishStatus Status
        {
            get
            {
                return (PublishStatus)this.DataManager.GetTypedProperty("Status", 0);
            }
            set
            {
                this.DataManager["Status"] = (int)value;
            }
        }

        private Link url;

        public Link Url
        {
            get
            {
                if(url == null || url.Path != this.Path)
                {
                    url = new Link(this.Path, null, this.Title);
                }
                return url;
            }
        }

        public string[] Authors
        {
            get
            {
                List<Tag> tags = this.MetaData.Get("Author");
                List<string> authors = new List<string>();
                foreach (Tag t in tags)
                {
                    authors.Add(t.StringValue);
                }
                return authors.ToArray();
            }
        }

        #endregion

        #region Child listing functions
        public ActiveCollection<Controller> GetAllChildren(string key, object value)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            if (value != null)
            {
                values.Add(key, value);
            }
            return this.GetAllChildren(values);
        }

        public ActiveCollection<Controller> GetAllChildren(Guid moduleControlID)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            if (moduleControlID != Guid.Empty)
            {
                values.Add("ModuleControlID", moduleControlID);
            }

            return this.GetAllChildren(values);
        }

        public ActiveCollection<Controller> GetAllChildren(Dictionary<string, object> values)
        {
            values.Add("Path", this.Path + "/%");
            return this.DataManager.List(values) as ActiveCollection<Controller>;
        }

        #endregion

        #region Page operations
        /// <summary>
        /// Returns the page specified by page number
        /// </summary>
        /// <param name="pageNumber">Page number, duh</param>
        /// <returns>A FeaturedContentPage with the appropriate page number</returns>
        public Page GetPage(int pageNumber)
        {
            foreach (Page page in this.Pages)
            {
                if (page.PageNumber == pageNumber)
                {
                    return page;
                }
            }
            return null;
        }

        /// <summary>
        /// Adds a page to the featured content and returns the page number.
        /// </summary>
        /// <param name="page">The page to add.</param>
        /// <returns>The page number of the new page</returns>
        public int AddPage(Page page)
        {
            bool pageExists = false;
            int maxPage = 0;
            foreach (Page p in this.Pages)
            {
                if (p.PageNumber == page.PageNumber)
                {
                    pageExists = true;
                }
                if (p.PageNumber > maxPage)
                {
                    maxPage = p.PageNumber;
                }
            }
            if (pageExists)
            {
                page.PageNumber = maxPage + 1;
            }
            page.DataManager.IsDirty = true;
            this.Pages.Add(page);
            return page.PageNumber;
        }

        /// <summary>
        /// Moves a page from its initial page number to a new one, renumbers other pages
        /// </summary>
        /// <param name="pageNumber">The working page number</param>
        /// <param name="newPageNumber">New page number</param>
        public void MovePage(int pageNumber, int newPageNumber)
        {
            // 2 to 4: 3 is now 2, 4 is now 3, 2 is now 4
            // 4 to 2: 3 is now 4, 4 is now 2, 2 is now 3

            if (pageNumber < 1) pageNumber = 1;
            if (pageNumber > this.Pages.Count) pageNumber = this.Pages.Count;
            if (newPageNumber < 1) newPageNumber = 1;
            if (newPageNumber > this.Pages.Count) newPageNumber = this.Pages.Count;

            Page selectedPage = this.GetPage(pageNumber);

            for (int i = 0; i < this.Pages.Count; i++)
            {
                Page workingPage = this.Pages[i];
                if (workingPage.PageNumber > pageNumber && workingPage.PageNumber <= newPageNumber)
                {
                    workingPage.PageNumber--;
                }
                else if (workingPage.PageNumber < pageNumber && workingPage.PageNumber >= newPageNumber)
                {
                    workingPage.PageNumber++;
                }
            }

            selectedPage.PageNumber = newPageNumber;

            //this.Pages.Sort();
        }

        /// <summary>
        /// Removes the specified page from the collection
        /// </summary>
        /// <param name="pageNumber">The page to remove</param>
        public void RemovePage(int pageNumber)
        {
            int removeAt = -1;
            for (int i = 0; i < this.Pages.Count; i++)
            {
                if (this.Pages[i].PageNumber > pageNumber)
                {
                    this.Pages[i].PageNumber--;
                    continue;
                }
                if (this.Pages[i].PageNumber == pageNumber)
                {
                    removeAt = i;
                }
            }
            if (removeAt >= 0)
            {
                this.Pages.RemoveAt(removeAt);
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            Controller c = new Controller();
            Dictionary<string, object> values = new Dictionary<string, object>();
            foreach (string key in new List<string>(this.DataManager.Keys))
            {
                object value = this.DataManager[key];
                if (value != null && (value.GetType().IsValueType || value is string))
                {
                    values.Add(key, value);
                }
            }
            c.DataManager.Populate(values);
            return c;
        }

        #endregion

        #region Handler helpers
        public virtual List<QueryEngine.Content> GetContentList(string[] groups)
        {
            QueryEngine.Query q = new Mubble.Models.QueryEngine.Query();

            if (this.ControllerID != null && this.ControllerID != Guid.Empty)
            {
                q.AddTerm("Path", this.Path + "/*", true, true, false);
            }

            q.OrderBy = "PublishDate DESC";

            q.EndResultIndex = 20;

            return QueryEngine.Content.Query(q, groups);
        }

        public virtual IEnumerable<IRssItem> GetRssItems(string[] groups)
        {
            foreach (QueryEngine.Content c in this.GetContentList(groups))
            {
                yield return (IRssItem)c;
            }
        }

        public virtual IRssItem GetCurrentRssItem(string path, string extra, string[] groups)
        {
            QueryEngine.Query q = new Mubble.Models.QueryEngine.Query();

            q.AddTerm("Path", path, true, false);
            q.AddTerm("PathExtra", extra, true, false);

            q.EndResultIndex = 10;

            List<QueryEngine.Content> results = QueryEngine.Content.Query(q);
            if (results.Count == 1)
            {
                return (IRssItem)results[0];
            }
            return null;
        }

        #endregion

        #region Saving functions
        public void Save()
        {
            this.DataManager.Save();

            ScheduledCommand.ClearPendingCommands(this, "Save");

            if (this.Status == PublishStatus.Published && this.PublishDate > DateTime.Now)
            {
                ScheduledCommand.Schedule(this, "Save", this.PublishDate);
            }
        }
        #endregion

        public IActiveObject GetContainer()
        {
            return this.Parent;
        }

        /// <summary>
        /// Builds a formatted string from the properties of this MubbleObject
        /// </summary>
        /// <param name="format">The format to parse</param>
        /// <returns>A formatted string</returns>
        public string ToString(string format)
        {
            string[] keys = new string[this.DataManager.Keys.Count];
            this.DataManager.Keys.CopyTo(keys, 0);
            foreach (string key in keys)
            {
                string value = this.DataManager[key] as string;
                if (value != null)
                {
                    format = format.Replace("{" + key.ToLower() + "}", value);
                }
            }
            return format;
        }

        public virtual string GetIndexableText()
        {
            string body = this.Body != null ? this.Body : "";
            return string.Format("{0} {1}", this.Title, Regex.Replace(body, @"\<[^\>]*\>", " "));
        }

        public virtual Link BuildLink(RouteParameters parameters)
        {
            Link l = new Link(this.Url.Path, null, this.Url.Title);
            if (parameters.ContainsKey("Page"))
            {
                l.Extra = string.Format("/p{0}", parameters["Page"]);
            }
            return l;
        }

        public Route GetRoute()
        {
            Route r = this.Route;
            if (r == null || r.ModuleControlID != this.ModuleControlID)
            {
                foreach (Route route in this.ModuleControl.Routes)
                {
                    if (route.IsDefault)
                    {
                        r = route;
                        break;
                    }
                }
            }
            return r;
        }

        #region IMailable Members

        public Mubble.Models.Controllers.Email BuildEmail(string[] emails)
        {
            //IRssItem item = this.GetCurrentRssItem(this.Url.Path, this.Url.Extra, Security.User.GetRoles());
            return null;
        }

        #endregion
    }
}
