using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using ActiveObjects;

namespace Mubble.Handlers
{
    public abstract class CachedHttpHandler : HttpHandler
    {

        private int cacheTime = 10;

        public int CacheTime
        {
            get { return cacheTime; }
            set { cacheTime = value; }
        }

        private bool useSlidingExpiration = false;

        public bool UseSlidingExpiration
        {
            get { return useSlidingExpiration; }
            set { useSlidingExpiration = value; }
        }

        private bool allowCaching = true;

        public virtual bool AllowCaching
        {
            get { return allowCaching; }
            set { allowCaching = value; }
        }


        private List<string> fileDependencies = new List<string>();

        private List<string> cacheKeyDependencies = new List<string>();

        public virtual string VaryByCustom
        {
            get { return null; }
        }

        public override void ProcessMubbleRequest(System.Web.HttpContext context)
        {
            bool force = context.Request["Recache"] != null && context.Request["Recache"].Equals("true", StringComparison.CurrentCultureIgnoreCase);

            string key = this.BuildCacheKey(context);
            CacheResponse response = context.Cache[key] as CacheResponse;

            context.Response.Cache.SetValidUntilExpires(true);
            context.Response.Cache.SetCacheability(System.Web.HttpCacheability.ServerAndNoCache);

            if (response == null || force || !this.AllowCaching)
            {
                response = new CacheResponse();
                response.Output = this.ProcessUncachedRequest(context);
                response.LastModified = DateTime.Now;

                DateTime absExp = (this.UseSlidingExpiration) ? Cache.NoAbsoluteExpiration : DateTime.Now.AddMinutes(this.CacheTime);
                TimeSpan slidingExp = (this.UseSlidingExpiration) ? TimeSpan.FromMinutes(this.CacheTime) : Cache.NoSlidingExpiration;

                CacheDependency dep = this.fileDependencies.Count > 0 || this.cacheKeyDependencies.Count > 0
                    ? 
                    new CacheDependency(this.fileDependencies.ToArray(), this.cacheKeyDependencies.ToArray()) : 
                    null;

                if (this.CacheTime > 0)
                {
                    context.Cache.Insert(key, response, dep, absExp, slidingExp);
                }
            }

            context.Response.Cache.SetLastModified(response.LastModified);
            context.Response.Write(response.Output);
        }


        public abstract string ProcessUncachedRequest(System.Web.HttpContext context);

        protected virtual string BuildCacheKey(System.Web.HttpContext context)
        {
            string key = string.Format("Output||{0}||{1}", this.GetType().Name, context.Request.RawUrl);
            if (this.VaryByCustom != null) key += string.Format("||{0}", Caching.VaryByCustom(context, this.VaryByCustom));
            return key;
        }

        protected void AddFileDependency(string fileName)
        {
            if (!this.fileDependencies.Contains(fileName))
            {
                this.fileDependencies.Add(fileName);
            }
        }

        protected void AddCachedObjectDependency(IActiveObject obj)
        {
            AddCacheKeyDependency(CacheBroker.GetKey(obj));
        }

        protected void AddCacheKeyDependency(string key)
        {
            if (!this.cacheKeyDependencies.Contains(key))
            {
                this.cacheKeyDependencies.Add(key);
            }
        }
    }
}
