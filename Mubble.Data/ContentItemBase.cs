using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Linq.Expressions;
using System.Data.Linq;
using Mubble.Data.Mapping;

namespace Mubble.Data
{
    public abstract partial class ContentItemBase<T> where T : ContentItemBase<T>, new()
    {
        //public virtual Guid ID { get { return this.Record.ID; } }
        //public virtual string Path { get { return this.Record.Path; } }
        //public virtual Guid? ParentID { get { return this.Record.ContentItemID; } }
        //public virtual DateTime PublishDate { get { return this.Record.PublishDate; } }

        public List<IContentItem> Breadcrumb
        {
            get
            {
                var items = new List<IContentItem>();

                var parent = ContentItem.Get(this.ParentID.GetValueOrDefault());
                if (parent != null)
                {
                    items.AddRange(parent.Breadcrumb);
                }
                items.Add(this);

                return items;
            }
        }

        internal ContentItemBase() { }

        static Mapping.Composer composer;
        static Mapping.PropertyMap map;

        static ContentItemBase()
        {
            composer = new Mubble.Data.Mapping.Composer
            {
                Properties = 
                    { 
                    new Alias 
                    { 
                        Property = new TypedName<Raw.ContentItem>("Record"), 
                        Ignore = { "TextBlockID" }, 
                        Rename = 
                        { 
                            {"ContentItemID", "ParentID" }
                        } 
                    },
                    new Alias { Property = new TypedName<Raw.TextBlock>("TextBlock"), Through = "Record", Ignore = { "ID" } }
                    }
            };

            Mapping.PropertyMap.Convert(composer, typeof(ContentItemBase<>));
            composer = ExtendComposer();
            map = Mapping.PropertyMap.Convert<T>(composer);
        }

        internal static Mapping.Composer ExtendComposer(params Mapping.IAlias[] properties)
        {
            return composer.Extend(properties);
        }

        internal virtual PropertyMap Map { get { return map; } }

        internal Raw.ContentItem Record { get; set; }

        public virtual object this[string name]
        {
            get
            {
                return Map.GetValue(name, this.Record);
            }
        }

        public static T Get(Guid id)
        {
            return Get(ci => ci.ID == id);
        }

        public static T Get(Guid parentID, string fileName)
        {
            return Get(ci => ci.ContentItemID == parentID && ci.FileName == fileName);
        }

        public static T Get(string path)
        {
            return Get(ci => ci.Path == path);
        }

        internal virtual DataLoadOptions GetLoadOptions()
        {
            var loadOptions = new System.Data.Linq.DataLoadOptions();
            loadOptions.LoadWith<Raw.ContentItem>(ci => ci.TextBlock);
            return loadOptions;
        }

        internal virtual Mapping.Composer GetComposer()
        {
            return composer;
        }

        //TODO: Figure out permissions filter
        //TODO: Clean this crap up
        //TODO: Handle type by type load options
        internal static T Get(Expression<Func<Raw.ContentItem, bool>> predicate)
        {
            var options = DatabaseHelper.GetLoadOptions<T>();
            using (var ctx = DatabaseHelper.GetReader(options))
            {               
                var typeID = DatabaseHelper.GetTypeID(typeof(T));
                var query = (from ci in ctx.ContentItems
                            select ci).Where(predicate);

                if (typeof(T) != typeof(ContentItem))
                {
                    query = query.Where(ci => ci.ContentTypeID == typeID);
                }

                var t = query.Select(ci => new T { Record = ci }).FirstOrDefault();
                return t;
            }
        }
    }
}
