using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.Conditionals
{
    public class Age : Conditional
    {
        private int days;

        public int Days
        {
            get { return days; }
            set { days = value; }
        }


        protected override bool Test()
        {
            IContent c = this.GetReferencedScope<IContent>();
            return c.PublishDate >= DateTime.Now.Subtract(TimeSpan.FromDays(this.days));
        }
    }
}
