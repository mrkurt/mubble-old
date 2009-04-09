using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models.QueryEngine;

namespace Mubble.Handlers.Json
{
    public class JsonContent
    {
        private Content underlyingContent;
        private MubbleUrl currentUrl;

        public DateTime PublishDate { get { return this.underlyingContent.PublishDate; } }
        public string Url
        {
            get
            {
                return MubbleUrl.Url(this.underlyingContent.Path, this.underlyingContent.PathExtra, "HtmlHandler");
            }
        }
        public string Title { get { return this.underlyingContent.Title; } }
        public string ContainerFileName { get { return this.underlyingContent.ContainerFileName; } }

        public JsonContent(Content content, MubbleUrl url)
        {
            this.underlyingContent = content;
            this.currentUrl = url;
        }

        public static List<JsonContent> ConvertContentList(IEnumerable<Content> list, MubbleUrl url, int count)
        {
            List<JsonContent> newList = new List<JsonContent>();
            int index = 0;
            foreach (Content c in list)
            {
                if (count > 0 && index >= count)
                {
                    break;
                }
                newList.Add(new JsonContent(c, url));
                index++;
            }
            return newList;
        }
    }
}
