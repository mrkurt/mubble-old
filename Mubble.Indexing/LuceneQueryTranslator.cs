using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Index;
using Lucene.Net.Analysis.Standard;

using LBooleanClause = Lucene.Net.Search.BooleanClause;

namespace Mubble.Indexing
{
    internal static class QueryTranslator
    {
        public static Lucene.Net.Search.Query Translate(Query query)
        {
            BooleanQuery bq = new BooleanQuery();

            if (query.Text != null && query.Text.Trim().Length > 0)
            {
                // TODO: Clean up default field using schema, if available
                QueryParser parser = new QueryParser("Text", new StandardAnalyzer());
                parser.SetDefaultOperator(QueryParser.Operator.AND);
                bq.Add(
                    parser.Parse(query.Text),
                    LBooleanClause.Occur.MUST
                    );
            }

            foreach (QueryClause term in query.Children)
            {
                ProcessClause(bq, term);
            }

            return bq;
        }

        public static LBooleanClause.Occur Translate(QueryClauseType type)
        {
            if (type == QueryClauseType.Required)
            {
                return LBooleanClause.Occur.MUST;
            }
            else if (type == QueryClauseType.Excluded)
            {
                return LBooleanClause.Occur.MUST_NOT;
            }
            else
            {
                return LBooleanClause.Occur.SHOULD;
            }
        }

        static void ProcessClause(BooleanQuery bq, QueryClause clause)
        {
            if (clause is TermClause)
            {
                ProcessTermClause(bq, (TermClause)clause);
            }
            else if (clause is BooleanClause)
            {
                ProcessBooleanClause(bq, (BooleanClause)clause);
            }
        }

        static void ProcessTermClause(BooleanQuery bq, TermClause term)
        {
            Term t = new Term(term.Field, term.Value.ToLower());

            Lucene.Net.Search.Query q = null;

            if (term.ValueType == TermClauseType.Wildcard && term.Value.IndexOf('*') == term.Value.Length - 1)
            {
                q = new PrefixQuery(new Term(term.Field, term.Value.Substring(0, term.Value.Length - 1)));
            }
            else if (term.ValueType == TermClauseType.Wildcard)
            {
                q = new WildcardQuery(t);
            }
            else if (term.ValueType == TermClauseType.Fuzzy)
            {
                q = new FuzzyQuery(t);
            }
            else
            {
                q = new TermQuery(t);
            }

            if (term.Boost > 0)
            {
                q.SetBoost(term.Boost);
            }

            bq.Add(
                q,
                Translate(term.Type)
                );
        }

        static void ProcessBooleanClause(BooleanQuery bq, BooleanClause bc)
        {
            BooleanQuery query = new BooleanQuery();
            foreach (QueryClause clause in bc.Children)
            {
                ProcessClause(query, clause);
            }
            bq.Add(
                query, 
                Translate(bc.Type)
                );
        }
    }
}
