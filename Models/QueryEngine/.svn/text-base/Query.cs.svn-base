using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml;

namespace Mubble.Models.QueryEngine
{
    public class Query
    {
        private bool isValid = true;

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }


        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private int startResultIndex = 0;

        public int StartResultIndex
        {
            get { return startResultIndex; }
            set { startResultIndex = value; }
        }


        private int endResultIndex = 100;

        public int EndResultIndex
        {
            get { return endResultIndex; }
            set { endResultIndex = value; }
        }

        private int totalResults = -1;

        public int TotalResults
        {
            get { return totalResults; }
            set { totalResults = value; }
        }


        private string defaultField;

        public string DefaultField
        {
            get { return defaultField; }
            set { defaultField = value; }
        }

        private string orderBy;

        public string OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }

        private List<QueryClause> terms = new List<QueryClause>();

        public List<QueryClause> Terms
        {
            get { return terms; }
        }


        public Query() { }

        public Query(string text)
        {
            this.Text = text;
        }

        public void Add(Query q)
        {
            if (q == null) return;
            if (this.Text != null && this.Text.Length > 0) this.Text += " " + q.Text;
            else this.Text = q.Text;

            if (q.OrderBy != null && q.OrderBy != this.OrderBy)
            {
                this.OrderBy = q.OrderBy;
            }

            if (q.DefaultField != null && q.DefaultField != this.DefaultField)
            {
                this.DefaultField = q.DefaultField;
            }

            foreach (QueryClause t in q.Terms)
            {
                this.Add(t);
            }
        }

        public void Add(QueryClause clause)
        {
            this.Terms.Add(clause);
        }

        public void AddTerm(string field, string value, bool isRequired, bool isExcluded)
        {
            this.AddTerm(field, value, false, isRequired, isExcluded);
        }

        public void AddTerm(string field, string value, bool isWildcard, bool isRequired, bool isExcluded)
        {
            this.Terms.Add(new TermClause(field, value, isWildcard, isRequired, isExcluded));
        }

        public void AddBoolean(bool isRequired, bool isExcluded, params QueryClause[] clauses)
        {
            this.Terms.Add(new BooleanClause(isRequired, isExcluded, clauses));
        }

        public void RestrictByGroups(IndexPermissions permission, string[] groups)
        {
            this.RestrictByGroups(new IndexPermissions[] { permission }, groups);
        }

        public void RestrictByGroups(IndexPermissions[] permissions, string[] groups)
        {
            if (groups != null && groups.Length > 0 && permissions != null && permissions.Length > 0)
            {
                foreach (IndexPermissions permission in permissions)
                {
                    BooleanClause bc = new BooleanClause(true, false);
                    foreach (string role in groups)
                    {
                        bc.AddClause(
                            new TermClause(
                                string.Format("GroupWith{0}Permissions", permission),
                                role,
                                false,
                                false)
                          );
                    }
                    this.Add(bc);
                }
            }
        }

        public override string ToString()
        {
            string temp = string.Format("LIMIT[{0},{1}]", this.StartResultIndex, this.EndResultIndex);
            temp += this.Text + " ";
            for (int i = 0; i < this.Terms.Count; i++)
            {
                if (i > 0) temp += " ";
                temp += this.Terms[i].ToString();
            }
            return temp.Trim();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static Query Parse(XmlDocument xml)
        {
            Query q = new Query();
            return Parse(xml, q);
        }

        public static Query Parse(XmlDocument xml, Query q)
        {
            XmlNode root = xml.FirstChild;
            q.OrderBy = root.Attributes["orderBy"] != null ? root.Attributes["orderBy"].Value : q.OrderBy;
            BooleanClause bc = new BooleanClause(
                GetBoolAttribute(root, "required", false),
                GetBoolAttribute(root, "excluded", false)
                );
            foreach (XmlNode node in root.ChildNodes)
            {
                ParseTag(node, bc);
            }
            q.Add(bc);
            return q;
        }

        static void ParseTag(XmlNode node, BooleanClause bc)
        {
            switch (node.Name)
            {
                case "group":
                    ParseGroupTag(node, bc);
                    break;
                case "term":
                    ParseTermTag(node, bc);
                    break;
            }
        }
        static void ParseGroupTag(XmlNode node, BooleanClause parent)
        {
            BooleanClause bc = new BooleanClause(GetBoolAttribute(node, "required", false), GetBoolAttribute(node, "excluded", false));
            foreach (XmlNode child in node.ChildNodes)
            {
                ParseTag(child, bc);
            }
            parent.AddClause(bc);
        }

        static bool GetBoolAttribute(XmlNode node, string name, bool defaultValue)
        {
            bool output = defaultValue;
            if (node.Attributes[name] != null)
            {
                bool.TryParse(node.Attributes[name].Value, out output);
            }
            return output;
        }

        static void ParseTermTag(XmlNode node, BooleanClause boolean)
        {
            string field = node.Attributes["field"] != null ? node.Attributes["field"].Value : null;
            string value = node.Attributes["value"] != null ? node.Attributes["value"].Value : null;
            if(value == null || field == null) return;
            bool isWildcard = value.Contains("*");
            boolean.AddClause(
                new TermClause(
                    field, 
                    value, 
                    isWildcard,
                    GetBoolAttribute(node, "required", false), 
                    GetBoolAttribute(node, "excluded", false)
                    )
                );
        }
    }

    public enum IndexPermissions
    {
        View = 0, 
        Edit = 1,
        Create = 1
    }
}
