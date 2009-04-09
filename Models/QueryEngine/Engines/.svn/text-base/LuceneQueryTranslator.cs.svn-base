using System;
using System.Collections.Generic;
using System.Text;

using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;

namespace Mubble.Models.QueryEngine.Engines
{
    public static class LuceneQueryTranslator
    {
        public static global::Lucene.Net.Search.Query TranslateQuery(Query query)
        {
            BooleanQuery bq = new BooleanQuery();

            if (query.Text != null && query.Text.Trim().Length > 0)
            {

				QueryParser parser = new QueryParser(query.DefaultField, new StandardAnalyzer());
				parser.SetDefaultOperator(QueryParser.Operator.AND);
                bq.Add(
                    parser.Parse(query.Text),
                    true,
                    false
                );
            }

            foreach (QueryClause term in query.Terms)
            {
                ProcessClause(bq, term);
            }

            return bq;
        }

        public static void ProcessClause(BooleanQuery bq, QueryClause clause)
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

        public static void ProcessTermClause(BooleanQuery bq, TermClause term)
        {
            Term t = new Term(term.Field, term.Value.ToLower());

            global::Lucene.Net.Search.Query q = null;

            if (term.Type == TermClauseType.Wildcard && term.Value.IndexOf('*') == term.Value.Length - 1)
            {
                q = new PrefixQuery(new Term(term.Field,term.Value.Substring(0, term.Value.Length - 1)));
            }
            else if (term.Type == TermClauseType.Wildcard)
            {
                q = new WildcardQuery(t);
            }
            else if(term.Type == TermClauseType.Fuzzy)
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

            bq.Add(q, term.IsRequired, term.IsExcluded);
        }

        public static void ProcessBooleanClause(BooleanQuery bq, BooleanClause bc)
        {
            BooleanQuery query = new BooleanQuery();
            foreach (QueryClause clause in bc.Children)
            {
                ProcessClause(query, clause);
            }
            bq.Add(query, bc.IsRequired, bc.IsExcluded);
        }
    }
}
