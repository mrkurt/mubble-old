using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;

namespace Mubble.UI.Data
{
    public interface IDataSource
    {
        List Parent { get; set;}
        IActiveCollection GetData(int startIndex, int endIndex);
        IEnumerable<IActiveObject> FilterData(IActiveCollection data);
    }
}
