using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mubble.Data;

namespace Mubble.Web.Output
{
    public interface ISerializer
    {
        void Start(IData data, TypeInfo type);
        void AppendField(string name, object data, TypeInfo type);
        void StartRelated(string name, IData data, TypeInfo type);
        void EndBlock();
    }
}
