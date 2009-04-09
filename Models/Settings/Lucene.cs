using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Xml;

namespace Mubble.Models.Settings
{
    public class Lucene
    {
        private string indexLocation;

        public string IndexLocation
        {
            get { return indexLocation; }
            set { indexLocation = value; }
        }

    }
}
