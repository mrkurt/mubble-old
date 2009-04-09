using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Conditionals
{
    public class FirstPost : Post
    {
        protected override bool Test()
        {
            return this.Test("First");
        }
    }
}
