using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using Mubble.Models;

namespace Mubble.UI.Data
{
    public class Files : BaseDataSource
    {
        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }


        public override IActiveCollection GetData(int startIndex, int endIndex)
        {
            Controller context = string.IsNullOrEmpty(this.Path) ?
                Control.GetCurrentScope<Controller>(this.Parent)
                :
                Controller.FindFirst(this.Path);

            Dictionary<string, object> parameters = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);

            if (context != null)
            {
                parameters.Add("ControllerID", context.ID);
            }

            parameters.Add("RowIndex_start", startIndex);
            parameters.Add("RowIndex_end", endIndex + 1);

            return File.Find(parameters);
        }
    }
}
