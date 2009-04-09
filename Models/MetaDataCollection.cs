using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models
{
    public class MetaDataCollection : ActiveObjects.ActiveCollection<Tag>
    {
        /// <summary>
        /// Clears the values for the specified Metadata name
        /// </summary>
        /// <param name="name">The metadata key name to look for</param>
        public void Clear(string name)
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (this[i].Name == name)
                {
                    this.RemoveItem(i);
                }
            }
        }

        public void Set(string name, string value)
        {
            this.Set(name, value, 0);
        }

        public void Set(string name, string value, bool normalizeStringValue)
        {
            this.Set(name, value, 0, normalizeStringValue);
        }

        public void Set(string name, string value, double numericValue)
        {
            this.Set(name, value, numericValue, true);
        }

        public void Set(string name, string value, double numericValue, bool normalizeStringValue)
        {
            if (!this.TagValueExists(name, value))
            {
                Tag t = new Tag();
                t.NormalizeStringValue = normalizeStringValue;
                t.Name = name;
                t.StringValue = value;
                t.NumericValue = numericValue;
                this.Add(t);
            }
        }


        protected bool TagValueExists(string name, string value)
        {
            string normalizedValue = Filters.String.Normalize(value);
            foreach (Tag t in this)
            {
                if (t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) && t.StringValueNormalized == normalizedValue)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<KeyValuePair<string, string>> this[string name]
        {
            get
            {
                foreach (Tag t in this.Sort("NumericValue"))
                {
                    if (t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        yield return new KeyValuePair<string, string>(t.StringValueNormalized, t.StringValue);
                    }
                }
            }
            set
            {
                foreach (KeyValuePair<string,string> val in value)
                {
                    this.Set(name, val.Value);
                }
            }
        }

        /// <summary>
        /// Gets all string values with the specified name in the metadata collection.
        /// </summary>
        /// <param name="name">The metadata field name</param>
        /// <returns>An array of string values</returns>
        public string[] GetStringValues(string name)
        {
            List<string> values = new List<string>();
            foreach (Tag t in this.Get(name))
            {
                if (t.StringValue != null && t.StringValue.Trim().Length > 0)
                {
                    values.Add(t.StringValue);
                }
            }
            return values.ToArray();
        }
        public List<Tag> Get(string name)
        {
            List<Tag> list = new List<Tag>();
            foreach (Tag t in this.Sort("NumericValue"))
            {
                if (t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    list.Add(t);
                }
            }
            return list;
        }

        public string GetFirstStringValue(string name)
        {
            foreach (Tag t in this.Sort("NumericValue"))
            {
                if (t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return t.StringValue;
                }
            }
            return null;
        }
    }
}
