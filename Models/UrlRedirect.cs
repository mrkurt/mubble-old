using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;
using System.Security.Cryptography;

namespace Mubble.Models
{

	public partial class UrlRedirect
	{
        /// <summary>
        /// Searches redirects for a specific url
        /// </summary>
        /// <param name="url">The url to search for</param>
        /// <returns>true if found, false if not</returns>
        public static ActiveCollection<UrlRedirect> Search(Uri url)
        {
            string contentPath = url.AbsolutePath;
            if (url.AbsolutePath[url.AbsolutePath.Length - 1] == '/')
            {
                contentPath = url.AbsolutePath.Substring(0, url.AbsolutePath.Length - 1);
            }

            if (Mubble.Models.Settings.Application.Web.ApplicationPath.Length > 1
                && contentPath.IndexOf(Mubble.Models.Settings.Application.Web.ApplicationPath) == 0)
            {
                contentPath = contentPath.Substring(Mubble.Models.Settings.Application.Web.ApplicationPath.Length);
            }

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("Url", contentPath);

            ActiveCollection<UrlRedirect> list = (new UrlRedirect()).DataManager.List(values) as ActiveCollection<UrlRedirect>;
            return list;
        }

        public static Link GetRedirectedUrl(Uri requestedUrl)
        {
            ActiveCollection<UrlRedirect> list = UrlRedirect.Search(requestedUrl);
            if (list.Count > 0)
            {
                Link url = new Link(list[0].Controller.Path, list[0].PathExtra);
                return url;
            }
            else
            {
                return null;
            }
        }

        public void Save()
        {
            this.DataManager.Save();

            if (File.FileStoreBase != null)
            {
                string redirectLoc = System.IO.Path.Combine(File.FileStoreBase, "Redirects");
                string redirectUrlFormat = string.Format("{0}\r\n{1}\r\n{2}", this.Controller.Path, this.Handler, this.PathExtra);
                string fileLoc = System.IO.Path.Combine(redirectLoc, GetPathFromUrl(this.Url));

                System.IO.FileInfo file = new System.IO.FileInfo(fileLoc);

                if (file.Exists)
                {
                    file.Delete();
                }

                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }

                System.IO.File.WriteAllText(fileLoc, redirectUrlFormat);
            }
        }

        public static bool FindRedirectUrl(string url, out string path, out string handler, out string extra)
        {
            Uri uri = new Uri(url, UriKind.Relative);
            string redirectLoc = System.IO.Path.Combine(File.FileStoreBase, "Redirects");
            string fileLoc = System.IO.Path.Combine(redirectLoc, GetPathFromUrl(url));

            if (!System.IO.File.Exists(fileLoc) && url.IndexOf('?') > 0)
            {
                fileLoc = System.IO.Path.Combine(
                    redirectLoc,
                     GetPathFromUrl(url.Substring(0, url.IndexOf('?')))
                     );
            }

            path = handler = extra = null;

            if (System.IO.File.Exists(fileLoc))
            {
                string[] lines = System.IO.File.ReadAllLines(fileLoc);
                if (lines.Length > 0 && lines[0].Trim().Length > 0)
                {
                    path = lines[0].Trim();
                }
                if (lines.Length > 1 && lines[1].Trim().Length > 0)
                {
                    handler = lines[1].Trim();
                }
                if (lines.Length > 2 && lines[2].Trim().Length > 0)
                {
                    extra = lines[2].Trim();
                }

                return true;
            }
            
            return false;
        }

        public static string GetPathFromUrl(string url)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(url));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataMd5.Length; i++)
            {
                sb.AppendFormat("{0:x2}", dataMd5[i]);
            }
            sb.Append(".txt");
            string hash = sb.ToString();
            return hash;
        }
	}
}
