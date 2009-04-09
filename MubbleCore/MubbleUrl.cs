using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Mubble
{
    public class MubbleUrl
    {
        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private string pathExtra;

        public string PathExtra
        {
            get { return pathExtra; }
            set 
            { 
                pathExtra = value;
                this.pathItems = (pathExtra != null) ?
                    pathExtra.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries) : new string[0];
                for (int i = 0; i < pathItems.Length; i++)
                {
                    pathItems[i] = HttpUtility.UrlDecode(pathItems[i]);
                }
            }
        }

        private string handler = "HtmlHandler";

        public string Handler
        {
            get { return handler; }
            set { handler = value; }
        }

        private string action;

        public string Action
        {
            get { return action; }
            set { action = value; }
        }


        private static string applicationPath;

        public static string ApplicationPath
        {
            get { return applicationPath; }
            set { applicationPath = value; }
        }

        private static string defaultHost;
        public static string DefaultHost
        {
            get { return defaultHost; }
            set { defaultHost = value; }
        }

        private string[] pathItems = new string[0];

        public string[] PathItems
        {
            get { return pathItems; }
            set { pathItems = value; }
        }

        private Mubble.Models.LinkParameters _params;

        public Mubble.Models.LinkParameters Params
        {
            get { return _params; }
            set { _params = value; }
        }


        private static Uri firstUri = null;


        #region Constructors
        public MubbleUrl(string path)
        {
            this.Path = path;
        }

        public MubbleUrl(string path, string pathExtra)
            : this(path)
        {
            this.PathExtra = pathExtra;
        }

        public MubbleUrl(string path, string pathExtra, string handler)
            : this(path, pathExtra)
        {
            this.Handler = handler;
        }

        public MubbleUrl(Mubble.Models.Link link, string handler)
            : this(link.Path, link.Extra, handler)
        {
            this.Params = link.Params;
        }
        public MubbleUrl(string path, string pathExtra, string handler, string action)
            : this(path, pathExtra, handler)
        {
            this.action = action;
        }
        #endregion

        #region Path item functions
        /// <summary>
        /// Gets the specified path item index, if it exists.  Otherwise returns an empty string.
        /// </summary>
        /// <param name="index">Path Item index to retrieve</param>
        /// <returns>The path item, or an empty string</returns>
        public string GetPathItem(int index)
        {
            return this.GetPathItem(index, "");
        }

        /// <summary>
        /// Gets the specified path item index, if it exists.  Otherwise, returns the default value passed through.
        /// </summary>
        /// <param name="index">The path Item index to return</param>
        /// <param name="defaultValue">Default value to return if path item doesn't exist</param>
        /// <returns></returns>
        public string GetPathItem(int index, string defaultValue)
        {
            if (this.PathItems != null && this.PathItems.Length > index)
            {
                return this.PathItems[index];
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets the integer at the specified path index.  If there is no integer, returns default value.
        /// </summary>
        /// <param name="index">Path item integer to return</param>
        /// <param name="defaultValue">Default to return if there is no integer</param>
        /// <returns>The integer at the specified index</returns>
        public int GetPathItem(int index, int defaultValue)
        {
            int item = defaultValue;
            if (Int32.TryParse(this.GetPathItem(index), out item))
            {
                return item;
            }
            else
            {
                return defaultValue;
            }
        }
        #endregion

        public string GetHandlerExtension()
        {
            return Mubble.Handlers.Settings.Extensions.FindByKey(this.Handler);
        }

        #region Static helper functions
        public static MubbleUrl Create(string path)
        {
            return new MubbleUrl(path);
        }

        public static MubbleUrl Create(string path, string pathExtra)
        {
            return new MubbleUrl(path, pathExtra);
        }

        public static MubbleUrl Create(string path, string pathExtra, string handler)
        {
            return new MubbleUrl(path, pathExtra, handler);
        }
        public static MubbleUrl Create(Mubble.Models.Link link, string handler)
        {
            return new MubbleUrl(link, handler);
        }

        public static string Url(string path, string handler)
        {
            return Url(path, null, handler);
        }
        public static string Url(string path, string pathExtra, string handler)
        {
            return Create(path, pathExtra, handler).ToString();
        }
        public static string Url(Mubble.Models.Link link, string handler)
        {
            return Create(link, handler).ToString();
        }
        #endregion

        #region Parsing Functions
        public static MubbleUrl Parse(string url, string handler)
        {
            UriKind uriKind = url.IndexOf("http") == 0 ? UriKind.Absolute : UriKind.Relative;
            return Parse(new Uri(url, uriKind), handler);
        }

        /// <summary>
        /// Parses the specified URL using the request type
        /// </summary>
        /// <param name="url">The url to parse</param>
        /// <param name="requestType">The request type of the specified url</param>
        public static MubbleUrl Parse(Uri uri, string handler)
        {
            return Parse(uri, handler, false);
        }

        /// <summary>
        /// Parses the specified URL using the request type
        /// </summary>
        /// <param name="url">The url to parse</param>
        /// <param name="requestType">The request type of the specified url</param>
        /// <param name="findActionBeforeExtension">Search the url for an action parameter before the extension</param>
        public static MubbleUrl Parse(Uri uri, string handler, bool findActionBeforeExtension)
        {
            if (DefaultHost == null && uri.Host != null)
            {
                firstUri = uri;
                DefaultHost = uri.Host;
            }
            HttpContext context = HttpContext.Current;

            string path, pathExtra;
            string extension = Mubble.Handlers.Settings.Extensions.FindByKey(handler);

            path = (uri.IsAbsoluteUri) ? uri.AbsolutePath : uri.OriginalString;

            int extensionIndex = path.IndexOf(extension);
            if (extensionIndex > 0)
            {
                pathExtra = path.Substring(extensionIndex + extension.Length);
                path = path.Substring(0, extensionIndex);
                if (path[0] != '/')
                {
                    path = '/' + path;
                }
                if (ApplicationPath.Length > 1 && path.IndexOf(ApplicationPath, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    path = path.Substring(ApplicationPath.Length);
                }
                if (pathExtra == "")
                {
                    pathExtra = null;
                }

                int lastDot = path.LastIndexOf('.');
                string action = null;
                if (findActionBeforeExtension &&  lastDot > 1)
                {
                    action = path.Substring(lastDot + 1);
                    path = path.Substring(0, lastDot);
                }


                return new MubbleUrl(path, pathExtra, handler, action);
            }
            else
            {
                return null;
            }
        }
        #endregion

        public override string ToString()
        {
            return this.ToString(this.Handler, this.PathExtra, this.Params);
        }

        public string ToString(string handler)
        {
            return this.ToString(handler, this.PathExtra, this.Params);
        }

        public string ToString(string handler, string pathExtra)
        {
            return this.ToString(handler, pathExtra, null);
        }

        public string ToString(string handler, string pathExtra, Mubble.Models.LinkParameters parameters)
        {
            string url = string.Format(
                "{0}{1}{2}{3}",
                MubbleUrl.ApplicationPath == "/" ? "" : MubbleUrl.ApplicationPath,
                path,
                Mubble.Handlers.Settings.Extensions.FindByKey(handler),
                pathExtra
                );
            Uri current =  HttpContext.Current != null ? HttpContext.Current.Request.Url : firstUri;

            StringBuilder uri = new StringBuilder(url);
            uri.Insert(0, current.GetLeftPart(UriPartial.Authority));
            return this.AppendParamsToQueryString(uri, parameters).ToString();
        }

        protected StringBuilder AppendParamsToQueryString(StringBuilder builder, Mubble.Models.LinkParameters parameters)
        {
            if (parameters == null) return builder;
            if (parameters.Keys.Count > 0) builder.Append('?');
            int paramCount = 0;
            foreach (string key in parameters.Keys)
            {
                List<object> values = parameters[key] as List<object>;
                if(values == null) values = new List<object>();

                bool multi = values.Count > 1;
                if(values.Count == 0)
                {
                    if(paramCount > 0) builder.Append('&');
                    builder.Append(key); builder.Append('=');
                    paramCount++;
                }
                foreach(object value in values)
                {
                    if(paramCount > 0) builder.Append('&');
                    builder.Append(HttpUtility.UrlEncode(key)); 
                    builder.Append((multi) ? "[]=" : "="); 
                    builder.Append(HttpUtility.UrlEncode(value.ToString()));
                    paramCount++;
                }
            }
            return builder;
        }
    }
}
