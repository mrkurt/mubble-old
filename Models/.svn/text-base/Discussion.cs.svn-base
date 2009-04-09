using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;
using System.Threading;

namespace Mubble.Models
{
    public partial class Discussion
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected Dictionary<int, CommentCollection> comments = new Dictionary<int, CommentCollection>();

        public CommentCollection GetComments()
        {
            return this.GetComments(1);
        }
        public CommentCollection GetComments(int page)
        {
            if (!comments.ContainsKey(page))
            {
                comments.Add(page, this.DiscussionProvider.GetComments(this.DiscussionLink, page));
            }
            if (comments[page].TotalCount != this.CommentCount)
            {
                this.CommentCount = comments[page].TotalCount;
                this.Save();
            }
            return comments[page];
        }

        public DiscussionStatus Status
        {
            get { return this.DataManager.GetTypedProperty<DiscussionStatus>("Status"); }
            set { this.DataManager["Status"] = value; }
        }

        private IActiveObject owner;
        public IActiveObject GetOwner()
        {
            if (owner == null)
            {
                Type ownerType = Type.GetType(this.ActiveObjectType);
                if (this.DataManager.SingleObjectLoader == null)
                {
                    owner = DataManager.ActiveObjectLoader(ownerType, this.ActiveObjectID);
                }
                else
                {
                    owner = this.DataManager.SingleObjectLoader(ownerType, this.ActiveObjectID);
                }
            }
            return owner;
        }

        public DiscussionStatus Create()
        {
            if (this.Status == DiscussionStatus.PendingCreation || this.Status == DiscussionStatus.CreationException)
            {
                ILinkable linked = this.GetOwner() as ILinkable;
                Link url = null;

                if (linked != null)
                {
                    url = linked.Url;
                }
                if (this.Title == null) this.Title = "Discussion for content";
                if (this.Excerpt == null) this.Excerpt = "Click to read: ";
                try
                {
                    this.DiscussionLink = this.DiscussionProvider.CreateDiscussion(url, this.Title, this.Excerpt);
                    this.Status = DiscussionStatus.Exists;
                }
                catch (DiscussionCreationException ex)
                {
                    this.Status = DiscussionStatus.CreationException;
                    if (ex.Type == DiscussionCreationExceptionType.DuplicatePost)
                    {
                        this.Status = DiscussionStatus.Duplicate;
                    }
                    this.LastStatusMessage = ex.Message;
                    log.Error(ex.Message, ex);
                }
                this.Save();
            }
            return this.Status;
        }

        #region Discussion creation queue

        public static Queue<Guid> CreateQueue = new Queue<Guid>();

        static object threadLock = (object)false;

        static MiscUtil.Threading.CustomThreadPool workerPool = new MiscUtil.Threading.CustomThreadPool("Discussion creation");

        static Discussion()
        {
            workerPool.SetMinMaxThreads(1, 1);
        }

        static Thread worker;

        public static void QueueForCreation(Discussion d)
        {
            CreateQueue.Enqueue(d.ID);

            lock (threadLock)
            {
                if (worker == null || worker.ThreadState != ThreadState.Running)
                {
                    worker = new Thread(new ThreadStart(WatchQueue));
                    worker.IsBackground = false;
                    worker.Priority = ThreadPriority.BelowNormal;
                    worker.Start();
                }
            }
        }

        static void WatchQueue()
        {
            try
            {
                int loopCount = 0;
                while (true)
                {
                    if (CreateQueue.Count > 0)
                    {

                        loopCount = 0;
                        Discussion d = Discussion.FindFirst(CreateQueue.Dequeue());
                        if (d != null && d.Status == DiscussionStatus.PendingCreation)
                        {
                            log.Info("Creating discussion");

                            if (d.Create() == DiscussionStatus.CreationException)
                            {
                                CreateQueue.Enqueue(d.ID);
                                Thread.Sleep(TimeSpan.FromSeconds(15));
                            }
                        }
                    }
                    else if (loopCount < 3)
                    {
                        loopCount++;
                        Thread.Sleep(TimeSpan.FromSeconds(15));
                    }
                    else
                    {
                        lock (threadLock)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error occurred with the discussion worker.", ex);
            }
        }

        #endregion
    }
}
