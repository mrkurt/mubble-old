using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using Mubble.Data.Mapping;

namespace Mubble.Data
{
    public partial class Controller : ContentItemBase<Controller>
    {
        static Composer composer;
        static PropertyMap map;
        static Controller()
        {
            composer = ExtendComposer(
                new Alias { Property = new TypedName<Raw.Controller>("Controller"), Through = "Record", Ignore = { "ContentItemID" } }
                );
            map = PropertyMap.Convert<Controller>(composer);
        }

        internal override PropertyMap Map { get { return map; } }

        internal override DataLoadOptions GetLoadOptions()
        {
            var options = new DataLoadOptions();
            options.LoadWith<Raw.ContentItem>(ci => ci.Controller);
            return options;
        }

        internal override Mapping.Composer GetComposer()
        {
            return composer;
        }

        internal void Ensure()
        {
            using (var ctx = DatabaseHelper.GetReader())
            {
                var raw = (from c in ctx.Controllers
                          where c.ContentItemID == this.ID
                          select c).FirstOrDefault();

                this.Record.Controller = raw;
            }
        }
    }
}
