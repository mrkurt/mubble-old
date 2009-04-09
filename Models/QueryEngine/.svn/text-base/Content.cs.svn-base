using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.QueryEngine
{
    public class Content : IRssItem, IAuthors, IStringFields, ILinkable
    {
        int id;
        public int ID
        {
            get { return id; }
        }

        float score;
        public float Score
        {
            get { return score; }
        }

        string title;
        public string Title { get { return title; } }

        string excerpt;
        public string Excerpt { get { return excerpt; } }

        string body;
        public string Body { get { return body; } }

        string path;
        public string Path { get { return path; } }

        string pathExtra;
        public string PathExtra { get { return pathExtra; } }

        private Link url;

        public Link Url
        {
            get 
            {
                if (url == null)
                {
                    url = new Link(this.Path, this.PathExtra, this.Title);
                }
                return url; 
            }
        }


        private string containerFileName;
        public string ContainerFileName {
            get { return containerFileName; }
        }

        string type;
        public string Type { get { return type; } }

        Guid activeObjectsID;
        public Guid ActiveObjectID { get { return activeObjectsID; } }

        string[] authors;
        public string[] Authors { get { return authors; } }

        DateTime publishDate;
        public DateTime PublishDate { get { return publishDate; } }

        protected Content() { }

        public Content(IndexDocument document)
        {
            containerFileName = document.Get("ContainerFileName");
            type = document.Get("ActiveObjectsType");
            activeObjectsID = new Guid(document.Get("ActiveObjectsID"));
            pathExtra = document.Get("PathExtra");
            path = document.Get("Path");
            body = document.Get("Body");
            excerpt = document.Get("Excerpt");
            title = document.Get("Title");
            score = document.Score;
            id = document.IndexID;
            authors = document.GetValues("Author");
            publishDate = document.GetDate("PublishDate");
        }

        public static List<Content> Query(string query, int showCount)
        {
            return Query(query, 0, showCount);
        }
        public static List<Content> Query(string query, int startIndex, int showCount)
        {
            Query q = new Query(query);
            q.StartResultIndex = startIndex;
            q.EndResultIndex = showCount + startIndex;
            q.OrderBy = "PublishDate DESC";
            return Query(q);
        }


        public static ContentList Query(Query query)
        {
            return Query(query, null);
        }
        public static ContentList Query(Query query, string[] groups)
        {
            return Query(query, groups, new string[] {"View"});
        }
        public static ContentList Query(Query query, string[] groups, string[] permissions)
        {
            if (groups != null && groups.Length > 0 && permissions != null && permissions.Length > 0)
            {
                foreach (string permission in permissions)
                {
                    BooleanClause bc = new BooleanClause(true, false);
                    foreach (string role in groups)
                    {
                        bc.AddClause(
                            new TermClause(
                                string.Format("GroupWith{0}Permissions", permission), 
                                role, 
                                false, 
                                false)
                          );
                    }
                    query.Add(bc);
                }
            }

            if (query.OrderBy == null) query.OrderBy = "PublishDate DESC";
            if (query.DefaultField == null) query.DefaultField = "Text";

            ContentList results = new ContentList();
            try
            {

                IndexDocument[] docs = Engine.Search(query);
				Dictionary<Guid, bool> usedResults = new Dictionary<Guid, bool>(docs.Length);

                results.StartIndex = query.StartResultIndex;
                results.EndIndex = query.EndResultIndex;
                results.TotalResults = query.TotalResults;

                for (int i = 0; i < docs.Length; i++)
                {
					Guid id = new Guid(docs[i].Get("ActiveObjectsID"));
					if (!usedResults.ContainsKey(id))
					{
						usedResults.Add(id, true);
						results.Add(new Content(docs[i]));
					}
					else
					{
						LogDuplicate(id, docs[i].Get("Title"));
					}
                }
				usedResults.Clear();
            }
            catch { }

            return results;
        }

		public static Dictionary<Guid, string> GetDuplicates()
		{
			Dictionary<Guid, string> results = null;
			lock (duplicates)
			{
				results = new Dictionary<Guid, string>(duplicates);
			}
			return results;
		}
		public static void ClearDuplicates()
		{
			lock (duplicates)
			{
				duplicates.Clear();
			}
		}
		static Dictionary<Guid, string> duplicates = new Dictionary<Guid, string>();
		static void LogDuplicate(Guid id, string title)
		{
			lock (duplicates)
			{
				if (!duplicates.ContainsKey(id))
				{
					duplicates.Add(id, title);
				}
				else
				{
					duplicates[id] = title;
				}
			}
		}

        #region IAuthors Members

        public IList<string> GetAuthors()
        {
            return this.Authors;
        }

        #endregion

        #region IStringFields Members

        public object this[string field]
        {
            get 
            {
                if ("PublishDate".Equals(field, StringComparison.CurrentCultureIgnoreCase))
                {
                    return this.PublishDate;
                }
                return null; 
            }
        }

        #endregion
    }
}
