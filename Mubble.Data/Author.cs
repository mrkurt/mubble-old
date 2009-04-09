using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Mubble.Data.Mapping;

namespace Mubble.Data
{
    public partial class Author : DataObject<Author, Raw.Author>
    {
        public new static Author Get(Guid id)
        {
            return Get(a => a.ID == id);
        }

        public static Author Get(string username)
        {
            return Get(a => a.UserName == username);
        }

        public string Alias { get; internal set; }
    }
}