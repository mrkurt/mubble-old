using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class DiscussionProvider
	{
        private Dictionary<string,string> settingsCollection;

        public Dictionary<string,string> SettingsCollection
        {
            get 
            {
                if (settingsCollection == null)
                {
                    settingsCollection = this.ParseSettings();
                }
                return settingsCollection; 
            }
            set { settingsCollection = value; }
        }

        public virtual Dictionary<string, string> ParseSettings()
        {
            Dictionary<string, string> s = new Dictionary<string, string>();
            if (this.Settings == null || this.Settings == "")
            {
                return s;
            }
            string[] parts = this.Settings.Split("&".ToCharArray());
            foreach (string part in parts)
            {
                if (part.IndexOf("=") > 0)
                {
                    string[] pair = part.Split("=".ToCharArray());
                    s.Add(pair[0], pair[1]);
                }
            }
            return s;
        }

        public virtual string CreateDiscussion(Link contentLink, string title, string text)
        {
            return null;
        }
        public virtual string UpdateDiscussion(Link contentLink, string title, string text, string discussionLink)
        {
            return null;
        }
        public virtual string GetPostFormLink(string discussionLink)
        {
            return null;
        }
        public virtual CommentCollection GetComments(string discussionLink, int pageNumber)
        {
            return new CommentCollection();
        }
	}
}
