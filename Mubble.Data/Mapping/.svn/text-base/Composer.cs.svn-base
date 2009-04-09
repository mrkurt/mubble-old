using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data.Mapping
{
    public class Composer
    {
        public List<IAlias> Properties { get; private set; }
        public int FirstNonExtended { get; set; }

        public Composer()
        {
            this.Properties = new List<IAlias>();
        }

        public Composer Extend(params IAlias[] properties)
        {
            var c = new Composer
            {
                Properties = this.Properties
            };

            c.Properties.AddRange(properties);
            c.FirstNonExtended = this.Properties.Count;

            return c;
        }
    }
}
