using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	public partial class Tag
	{
		// Stick your code here

        public string StringValue
        {
            get { return this.DataManager.GetTypedProperty<string>("StringValue"); }
            set
            {
                this.DataManager["StringValue"] = value;
                this.StringValueNormalized = (this.NormalizeStringValue) ? Filters.String.Normalize(value) : value;
            }
        }

        public static ActiveCollection<Tag> GetPopularTags(string name)
        {
            Tag p = new Tag();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Name", name);
            ActiveCollection<Tag> results = p.DataManager.List("GetDistinctStringValues", parameters, new ActiveCollection<Tag>(), -1, -1)
                as ActiveCollection<Tag>;
            return results;
        }
	}
}
