using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Conditionals
{
    public class NotSinglePost : SinglePost
    {
        protected override bool Test()
        {
            return this.Test("!SinglePost");
        }
    }
}
