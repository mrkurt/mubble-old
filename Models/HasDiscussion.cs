using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;

namespace Mubble.Models
{
    public class HasDiscussion : ActsAsAttribute
    {
        private string cloneFromField;

        public string CloneFromField
        {
            get { return cloneFromField; }
            set { cloneFromField = value; }
        }

        private string publishDateField = "PublishDate";

        public string PublishDateField
        {
            get { return publishDateField; }
            set { publishDateField = value; }
        }

        private string publishStatusField = "Status";

        public string PublishStatusField
        {
            get { return publishStatusField; }
            set { publishStatusField = value; }
        }

        private Discussion discussion;

        public override string FieldName
        {
            get { return (base.FieldName == null) ? "Discussion" : base.FieldName; }
            set { base.FieldName = value; }
        }

        private string titleField = "Title";

        public string TitleField
        {
            get { return titleField; }
            set { titleField = value; }
        }

        private string excerptField = "Excerpt";

        public string ExcerptField
        {
            get { return excerptField; }
            set { excerptField = value; }
        }

        private DiscussionStatus defaultStatus = DiscussionStatus.NotCreated;

        public DiscussionStatus DefaultStatus
        {
            get { return defaultStatus; }
            set { defaultStatus = value; }
        }

        public override bool Save(DataManager dm)
        {
            if (dm["IgnoreDiscussionPlz"] != null && dm["IgnoreDiscussionPlz"] is bool)
            {
                return true;
            }
            Discussion d = this.GetValue(dm) as Discussion;

            if (d != null && d.DiscussionProviderID == Guid.Empty)
            {
                d = null;
            }

            if (d == null && dm["DiscussionProviderID"] is Guid && dm.PrimaryKeyValue is Guid)
            {
                d = new Discussion();
                d.DiscussionProviderID = (Guid)dm["DiscussionProviderID"];
                d.Status = this.DefaultStatus;
                d.CommentCount = 0;
            }

            if (d == null && this.CloneFromField != null && dm.PrimaryKeyValue is Guid)
            {
                IActiveObject obj = dm[this.CloneFromField] as IActiveObject;
                if (obj != null)
                {
                    HasDiscussion hd = obj.DataManager.ActsAs(this.GetType()) as HasDiscussion;

                    if (hd != null)
                    {
                        Discussion sourced = obj.DataManager[hd.FieldName] as Discussion;
                        if (sourced != null && sourced.ID != Guid.Empty)
                        {
                            d = new Discussion();
                            d.DiscussionProviderID = sourced.DiscussionProviderID;
                            d.Status = this.DefaultStatus;
                            d.CommentCount = 0;
                        }
                    }
                }
            }

            if (d != null && dm.PrimaryKeyValue is Guid)
            {
                d.ActiveObjectID = (Guid)dm.PrimaryKeyValue;
                d.ActiveObjectType = dm.Settings.BaseActiveObjectType.FullName;

                d.Title = (this.TitleField != null) ? dm[this.TitleField] as string : null;
                d.Excerpt = (this.ExcerptField != null) ? dm[this.ExcerptField] as string : null;

                if (this.PublishDateField != null && dm[this.PublishDateField] is DateTime)
                {
                    d.PublishDate = (DateTime)dm[this.PublishDateField];
                }
                if (this.PublishStatusField != null && dm[this.PublishStatusField] is int)
                {
                    PublishStatus status = (PublishStatus)dm[this.PublishStatusField];
                    if (status == PublishStatus.Draft || d.PublishDate > DateTime.Now)
                    {
                        d.PublishDate = DateTime.MaxValue;
                        if (d.Status == DiscussionStatus.PendingCreation || d.Status == DiscussionStatus.CreationException)
                        {
                            d.Status = DiscussionStatus.NotCreated;
                        }
                    }
                    else if (status == PublishStatus.Published && d.Status == DiscussionStatus.NotCreated && d.PublishDate <= DateTime.Now)
                    {
                        d.Status = DiscussionStatus.PendingCreation;
                    }
                }
                if (d.DataManager.IsDirty)
                {
                    d.Save();
                }

                if (d.Status == DiscussionStatus.PendingCreation)
                {
                    Discussion.QueueForCreation(d);
                }
            }
            return true;
        }

        public override object GetValue(DataManager manager)
        {
            if (discussion == null && manager.PrimaryKeyValue is Guid)
            {
                discussion = Discussion.FindFirst(
                    manager.Settings.BaseActiveObjectType.FullName,
                    (Guid)manager.PrimaryKeyValue
                );
            }
            if (discussion == null)
            {
                discussion = new Discussion();
                discussion.Status = DiscussionStatus.NotCreated;
            }

            return discussion;
        }
    }
}
