using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data
{
    public partial class File : DataObject<File, Raw.File>
    {
        public static File Get(Guid contentItemID, string filename)
        {
            var result = Get(f => f.FileName == filename && f.ContentItemID == contentItemID);
            return result;
        }
    }
}
