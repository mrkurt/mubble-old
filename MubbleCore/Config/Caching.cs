using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Mubble.Config
{
    public class Caching : BaseSection<Caching>
    {
        private string staticHost;

        public string StaticHost
        {
            get { return staticHost; }
            set { staticHost = value; }
        }

        private bool enableOutputCaching = true;

        public bool EnableOutputCaching
        {
            get { return enableOutputCaching; }
            set { enableOutputCaching = value; }
        }

        private int sharedObjectCacheTime = 600;

        public int SharedObjectCacheTime
        {
            get { return sharedObjectCacheTime; }
            set { sharedObjectCacheTime = value; }
        }

        public DateTime GetStandardExpiration()
        {
            return DateTime.Now.AddSeconds(this.SharedObjectCacheTime);
        }

        public TimeSpan GetSlidingExpiration()
        {
            return TimeSpan.FromSeconds(sharedObjectCacheTime);
        }

        public static string ResolveMediaPaths(string text)
        {
            if (string.IsNullOrEmpty(Current.StaticHost) || string.IsNullOrEmpty(text)) return text;

            text = ResolveOriginPaths(text);
            return ResolveRelativeMediaPaths(text);
        }

        public static string ResolveOriginPaths(string text)
        {
            if (string.IsNullOrEmpty(Current.StaticHost) || string.IsNullOrEmpty(text)) return text;

            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Url.Host != null)
            {
                var pattern = HttpContext.Current.Request.Url.Host.Replace(".", @"\.");
                pattern = @"\<img(?<OtherStuff>[^\>]+)src\=\""http\:\/\/" + pattern + @"(?<Url>\/[^\""]+)\""";
                return Regex.Replace(text, pattern, "<img${OtherStuff}src=\"http://" + Current.StaticHost + "${Url}\"");
            }
            else
            {
                return text;
            }
        }

        public static string ResolveRelativeMediaPaths(string text)
        {
            if (string.IsNullOrEmpty(Current.StaticHost) || string.IsNullOrEmpty(text)) return text;
            return Regex.Replace(text, @"\<img(?<OtherStuff>[^\>]+)src\=\""(?<Url>\/[^\""]+)\""", "<img${OtherStuff}src=\"http://" + Current.StaticHost + "${Url}\"");
        }

        public static string StaticFileHelperExtension = null;

        public static string GetCachedVersionUrl(string url)
        {
            return GetCachedVersionUrl(url, true);
        }

        public static string GetCachedVersionUrl(string url, bool useStaticFileHost)
        {
            if (url == null) return null;
            if (url.StartsWith("http://"))
            {
                url = url.Substring(7);
                url = url.Substring(url.IndexOf('/'));
            }
            string physicalPath = System.Web.HttpContext.Current.Server.MapPath(url);
            System.IO.FileInfo info = new System.IO.FileInfo(physicalPath);

            if (info.Exists && url.Contains("/Templates/") && !string.IsNullOrEmpty(StaticFileHelperExtension))
            {
                string templatePath = url.Substring(url.IndexOf("/Templates/") + "/Templates/".Length);
                string filePath = templatePath.Substring(templatePath.IndexOf('/'));
                templatePath = templatePath.Substring(0, templatePath.IndexOf('/'));

                filePath = string.Concat(
                    filePath.Substring(0, filePath.LastIndexOf('.')),
                    ".v",
                    Math.Abs(info.LastWriteTime.GetHashCode()),
                    filePath.Substring(filePath.LastIndexOf('.'))
                );

                url = string.Concat(
                    url.Substring(0, url.IndexOf("/Templates/") + 1),
                    templatePath,
                    StaticFileHelperExtension,
                    filePath);
            }
            else if (info.Exists)
            {
                DateTime modified = info.LastWriteTime;

                if (!url.Contains("?"))
                {
                    url += string.Concat("?", Math.Abs(modified.GetHashCode()));
                }
                else
                {
                    url += string.Concat("&", Math.Abs(modified.GetHashCode()));
                }
            }
            if (useStaticFileHost && !string.IsNullOrEmpty(Current.StaticHost))
            {
                return "http://" + Config.Caching.Current.StaticHost + url;
            }
            else
            {
                return url;
            }
        }
    }
}
