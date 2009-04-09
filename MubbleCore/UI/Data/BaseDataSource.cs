using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;

namespace Mubble.UI.Data
{
    public abstract class BaseDataSource : IDataSource
    {
        #region IDataSource Members

        private List parent;

        public List Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public abstract ActiveObjects.IActiveCollection GetData(int startIndex, int endIndex);

        public virtual IEnumerable<ActiveObjects.IActiveObject> FilterData(ActiveObjects.IActiveCollection data)
        {
            IEnumerable<IActiveObject> e = data as IEnumerable<IActiveObject>;
            if (e == null)
            {
                List<IActiveObject> list = new List<IActiveObject>(data.Count);
                foreach (IActiveObject obj in data)
                {
                    list.Add(obj);
                }
                e = list;
            }
            return e;
        }

        #endregion
    }
}
