using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Mubble.Indexing;

namespace Mubble.Tests
{
    [TestFixture]
    public class Search
    {
        [Test]
        public void BuildQuery()
        {
            var q = new Query
            {
                Children =
                {
                    new TermClause{ Field = "Title", Value = "kurt"},
                    new TermClause{ Field = "Text", Value = "kurt", Type = QueryClauseType.Required },
                    new BooleanClause
                    {
                        Type = QueryClauseType.Required,
                        Boost = .8f,
                        Children = 
                        {
                            new TermClause{ Field = "Title", Value = "kurt"},
                            new TermClause{ Field = "Text", Value = "kurt" }
                        }
                    }
                }
            };

            Console.WriteLine(q.ToString());
        }
    }
}
