using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;

namespace Mubble.Models
{
    public interface IContent : ILinkable, IMetaData
    {
        string Title { get; }
        string Excerpt { get; }
        DateTime PublishDate { get; }
        PublishStatus Status { get;}
        string Body { get; }
        IActiveObject GetContainer();
        string[] Authors { get; }
    }
}
