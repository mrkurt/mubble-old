using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data.Raw
{
    internal interface IColumn
    {
        string Name { get; }
        object GetValue(IRecord obj);
        void SetValue(IRecord obj, object value);
        Type ColumnType { get; }
    }

    internal interface IAssociation : IColumn { }

    internal delegate K Getter<T, K>(T obj) where T : IRecord;
    internal delegate void Setter<T, K>(T obj, K value) where T : IRecord;

    internal class Column<T, K> : IColumn where T : IRecord
    {
        public Getter<T, K> Getter { get; set; }
        public Setter<T, K> Setter { get; set; }

        #region IColumn Members

        public Type ColumnType
        {
            get { return typeof(K); }
        }

        public string Name { get; set; }

        public object GetValue(IRecord obj)
        {
            if (!(obj is T))
            {
                throw new ArgumentException("obj must be of type " + typeof(T).FullName);
            }
            return Getter((T)obj);
        }

        public void SetValue(IRecord obj, object value)
        {
            if (!(value is K))
            {
                throw new ArgumentException("value must be of type " + typeof(K).FullName);
            }
            if (!(obj is T))
            {
                throw new ArgumentException("obj must be of type " + typeof(T).FullName);
            }
            Setter((T)obj, (K)value);
        }

        #endregion
    }

    internal class Association<T, K> : Column<T, K>, IAssociation
        where T : IRecord
        where K : IRecord
    {
    }
}
