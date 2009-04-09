using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Web.Caching;
using ActiveObjects;
using System.Collections;
using System.Threading;

namespace Mubble
{
    public static partial class DataBroker
    {
        public static ActiveCollection<Post> GetPosts(Dictionary<string, object> parameters)
        {
            string key = string.Concat("PostList[", Mubble.Models.Tools.GetParametersString(parameters), "]");
            CachePostList p = null;
            try
            {
                using (dbLock.Lock(key))
                {
                    p = CacheBroker.Get(key) as CachePostList;

                    if (p == null)
                    {
                        ActiveCollection<Post> posts = Mubble.Models.Post.Find(parameters);
                        p = new CachePostList(posts);

                        CacheDependency dep = null;

                        if (parameters.ContainsKey("ControllerID"))
                        {
                            dep = new CacheDependency(new string[0], new string[] { string.Concat("Controller[", parameters["ControllerID"], "]") });
                        }

                        CacheBroker.Set(key, p, dep, Config.Caching.Current.GetStandardExpiration());
                    }
                }
            }
            catch (NamedLock<string>.TimeoutException)
            {
                log.InfoFormat("Post collection lock timed out: {0}", key);
            }

            if (p == null)
            {
                return new ActiveCollection<Post>();
            }
            else
            {
                return p.GetFullPostList();
            }
        }

        public static Post GetPost(Guid id)
        {
            string key = string.Concat("Post[", id, "]");

            Post p = null;
            try
            {
                using (dbLock.Lock(key))
                {
                    p = CacheBroker.Get(key) as Post;
                    if (p == null)
                    {
                        p = Post.FindFirst(id);
                        if (p == null)
                        {
                            p = new Post();
                            p.Slug = "unknown";
                            p.ControllerID = Guid.Empty;
                        }
                        Cache(p, string.Concat("Post", GetControllerItemKey(p.ControllerID, p.Slug)));
                    }
                }
            }
            catch (NamedLock<string>.TimeoutException)
            {
                log.InfoFormat("Post lock timed out: {0}", key);
            }
            return p != null && p.ID != Guid.Empty ? p : null;
        }

        public static Post GetPost(Guid controllerID, string slug)
        {
            string key = string.Concat("Post", GetControllerItemKey(controllerID, slug));

            Post p = null;
            try
            {
                using (dbLock.Lock(key))
                {
                    p = CacheBroker.Get(key) as Post;
                    if (p == null)
                    {
                        p = Post.FindFirst(controllerID, slug);
                        if (p == null)
                        {
                            p = new Post();
                            p.Slug = "unknown";
                            p.ControllerID = Guid.Empty;
                        }
                        Cache(p, key);
                    }
                }
            }
            catch (NamedLock<string>.TimeoutException)
            {
                log.InfoFormat("Post lock timed out: {0}", key);
            }
            return p != null && p.ID != Guid.Empty ? p : null;
        }
    }
}
