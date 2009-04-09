using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using Mubble.UI.WebControls;
using Mubble.Models.QueryEngine;
using Mubble.Models;

namespace Mubble.UI.Data
{
    public class RelatedContent : BaseQueryFilter
    {
        #region properties
        private float relevance = .5f;

        public float Relevance
        {
            get { return relevance; }
            set { relevance = value; }
        }


        private IActiveObject content;

        public IActiveObject Content
        {
            get
            {
                if (content == null)
                {
                    content = Control.GetCurrentScope<IActiveObject>(this.Parent.Parent);
                }
                return content;
            }
        }

        private string metadataName = "Tag";

        public string MetadataName
        {
            get { return metadataName; }
            set { metadataName = value; }
        }
        #endregion

        public override Mubble.Models.QueryEngine.Query BuildQuery(Mubble.Models.QueryEngine.Query current)
        {
            if (this.Content != null)
            {
                HasMetadata md = this.Content.DataManager.ActsAs(typeof(HasMetadata)) as HasMetadata;
                if (md != null && this.Content.DataManager[md.FieldName] is MetaDataCollection)
                {
                    MetaDataCollection metadata = this.Content.DataManager[md.FieldName] as MetaDataCollection;

                    List<Tag> tags = metadata.Get(this.MetadataName);

                    if (tags.Count == 0)
                    {
                        current.IsValid = false;
                        return current;
                    }

                    double maxNumericValue = 0;

                    foreach (Tag t in tags)
                    {
                        if (t.NumericValue > maxNumericValue) maxNumericValue = t.NumericValue;
                    }

                    Query q = current;

                    foreach (Tag t in tags)
                    {
                        float score = (float)Math.Abs(t.NumericValue - maxNumericValue);
                        if (score <= 1) score = 1;
                        TermClause tc = new TermClause(this.MetadataName, t.StringValueNormalized, TermClauseType.Fuzzy, score);
                        q.Add(tc);
                    }

                    q.OrderBy = "Score";
                    return q;
                }
            }
            return current;
        }

        public override void GetVisibleIndexes(QueryResults results, List<int> visibleIndexes)
        {
            for (int i = results.Count - 1; i >= 0; i--)
            {
                IActiveObject current = results[i];
                Content result = results.GetQueryObject(i);

                if (current.GetType().Equals(this.Content.GetType()) 
                    && current.DataManager.PrimaryKeyValue.Equals(this.Content.DataManager.PrimaryKeyValue))
                {
                    visibleIndexes.Remove(i);
                    continue;
                }
                //Content result = QueryResults.GetRawQueryContent(current);
                if (result.Score < this.Relevance)
                {
                    visibleIndexes.Remove(i);
                }
            }
        }
    }
}
