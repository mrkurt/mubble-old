using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
    [IsIndexable( OptionsType=typeof(PostIndexingOptions) )]
    [LockedDown(UsePermissionsFrom = "Controller")]
    [RequirePermissions(PermissionType.View, "view", "post draft", "post publish", "post full")]
    [HasMetadata]
    [HasDiscussion(CloneFromField="Controller", PublishDateField="PublishDate", PublishStatusField="Status", TitleField="Title", ExcerptField="Excerpt")]
    [Filters.UrlFriendlyFilter("Slug", FilterType.Set, LowerCase=true)]
	public partial class Post :  IAuthors, IContent, IDiscussable
	{

        public string[] Authors { get { return new string[] { this.UserName }; } }

        public PublishStatus Status
        {
            get { return this.DataManager.GetTypedProperty<PublishStatus>("Status"); }
            set { this.DataManager["Status"] = value; }
        }

        public MetaDataCollection MetaData
        {
            get { return this.DataManager.GetTypedProperty<MetaDataCollection>("MetaData"); }
        }

        public Discussion Discussion
        {
            get { return this.DataManager.GetTypedProperty<Discussion>("Discussion"); }
        }

        public string BodyShort
        {
            get { return this.GetBodyPart(0); }
        }

        public string BodyExtended
        {
            get { return this.GetBodyPart(1); }
        }

        protected string[] BodyParts()
        {
            return this.Body.Split(new string[] { "<more />" }, StringSplitOptions.RemoveEmptyEntries);
        }

        protected string GetBodyPart(int index)
        {
            string[] parts = this.BodyParts();

            if (parts != null && parts.Length > index)
            {
                return parts[index];
            }
            return null;
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
                        permissions.ResetViewPermissions("view unpublished", "draft", "publish", "full", "post draft", "post publish", "post full");
                    }
                }
                return permissions;
            }
        }

        protected Link url;
        public Link Url
        {
            get
            {
                Link l = this.Controller.BuildLink(
                    new RouteParameters(
                    new object[] { "Year", this.PublishDate.Year },
                    new object[] { "Month", this.PublishDate.Month },
                    new object[] { "Day", this.PublishDate.Day },
                    new object[] { "Slug", this.Slug }
                    )
                );
                l.Title = this.Title;
                return l;
            }
        }

        public void Save()
        {
            this.DataManager.Save();

            ScheduledCommand.ClearPendingCommands(this, "Save");

            if (this.Status == PublishStatus.Published && this.PublishDate > DateTime.Now)
            {
                ScheduledCommand.Schedule(this, "Save", this.PublishDate);
            }
        }

        private Post nextPost;
        public Post NextPost
        {
            get 
            {
                if (nextPost == null) nextPost = this.getSiblingPost("Next");
                return nextPost; 
            }
        }
        private Post previousPost;
        public Post PreviousPost
        {
            get 
            {
                if (previousPost == null) previousPost = this.getSiblingPost("Previous");
                return previousPost; 
            }
        }

        private Post getSiblingPost(string type)
        {
            string command = "Get" + type;
            Dictionary<string, object> parameters = new Dictionary<string,object>();
            parameters.Add("ID", this.ID);
            ActiveCollection<Post> results = this.DataManager.List(command, parameters, new ActiveCollection<Post>(), -1, -1)
                as ActiveCollection<Post>;
            if (results != null && results.Count == 1)
            {
                return results[0];
            }
            else
            {
                return null;
            }
        }

        public static Post GetNextWithoutTags(Guid id)
        {
            Post p = new Post();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("ID", id);
            ActiveCollection<Post> results = p.DataManager.List("GetNextWithoutTags", parameters, new ActiveCollection<Post>(), -1, -1)
                as ActiveCollection<Post>;
            
            if (results != null && results.Count > 0)
            {
                return results[0];
            }
            else
            {
                return null;
            }
        }

        #region IAuthors Members

        public IList<string> GetAuthors()
        {
            return new string[] { this.UserName };
        }

        #endregion

        #region IContent Members


        public IActiveObject GetContainer()
        {
            return this.Controller;
        }

        #endregion
    }
}
