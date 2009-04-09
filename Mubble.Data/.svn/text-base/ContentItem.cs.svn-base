using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data
{
    public partial class ContentItem : ContentItemBase<ContentItem>, IContentItem
    {
        public static IEnumerable<string> GetKeys(IContentItem ci)
        {
            return Mapping.PropertyMap.GetMap(ci.GetType()).GetKeys();
        }

        public static IContentItem Resolve(ContentItem ci)
        {
            var ct = ContentType.Get(ci.ContentTypeID);

            switch (ct.SystemType)
            {
                case "Section":
                    return new Section { Record = ci.Record };
                case "Controller":
                    var controller = new Controller { Record = ci.Record };
                    controller.Ensure();
                    return controller;
                case "Post":
                    return new Post { Record = ci.Record };
                case "Article":
                    return new Article { Record = ci.Record };

            }
            return ci;
        }

        public static IContentItem GetResolved(string path)
        {
            var ci = Get(path);

            return Resolve(ci);
        }
    }
}
