using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI
{
    public interface IControl
    {
        PathParameters Params { get; }
        Mubble.UI.Page Page { get; }
        Mubble.Models.Controller Content { get; }
        MubbleUrl Url { get; }
        System.Web.UI.Control Parent { get; }
    }
}
