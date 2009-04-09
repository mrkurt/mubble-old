using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects.SqlServer;
using System.Text.RegularExpressions;
using ActiveObjects;

namespace Mubble.Models.Controllers
{
    [HasDiscussion(TitleField = "Title", CloneFromField = "Controller", DefaultStatus = DiscussionStatus.NotCreated)]
    public class Article : ControllerWithDiscussion, IAuthors
    {

        private ActiveObjects.DataManager dataManager = new SqlDataManager<Article>();
        public override ActiveObjects.DataManager DataManager
        {
            get { return this.dataManager; }
            set { this.dataManager = value; }
        }

        public override string GetIndexableText()
        {
            string text = string.Format("{0} {1}", this.Title, Regex.Replace(this.Body, @"\<[^\>]*\>", " "));
            foreach (Page p in this.Pages)
            {
                text += string.Format(" {0} {1} ", p.Name, Regex.Replace(p.Body, @"\<[^\>]*\>", " "));
            }
            return text;
        }

        public static Article GetNextWithoutTags(Guid id)
        {
            Article a = new Article();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("ID", id);
            ActiveCollection<Controller> results = a.DataManager.List("GetNextWithoutTags", parameters, new ActiveCollection<Controller>(), -1, -1)
                as ActiveCollection<Controller>;
            if (results != null && results.Count > 0)
            {
                return results[0] as Article;
            }
            else
            {
                return null;
            }
        }

        #region IAuthors Members

        public IList<string> GetAuthors()
        {
            return this.MetaData.GetStringValues("Author");
        }

        #endregion
    }
}
