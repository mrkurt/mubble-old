using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Conditionals
{
    public class Article : Conditional
    {
        private Mubble.Models.Controllers.Article currentArticle;
        protected Mubble.Models.Controllers.Article CurrentArticle
        {
            get
            {
                if (currentArticle == null && this.UseControl == null)
                {
                    currentArticle = Control.GetCurrentScope<Mubble.Models.Controllers.Article>(this);
                }
                return currentArticle;
            }
        }
        protected override bool Test()
        {
            return this.Test(conditions.Split(new char[] { ',' }));
        }

        protected bool Test(params string[] tests)
        {
            if (this.CurrentArticle == null) return false;
            foreach (string test in tests)
            {
                bool negative = (test.Length > 0 && test[0] == '!');
                string pattern = (negative) ? test.Substring(1) : test;
                bool fails = false;
                switch (pattern.ToLower())
                {
                    case "discussionexists":
                        fails = (this.CurrentArticle.Discussion.Status != Mubble.Models.DiscussionStatus.Exists);
                        break;
                }
                if ((fails && !negative) || (!fails && negative)) return false;
            }
            return true;
        }
    }
}
