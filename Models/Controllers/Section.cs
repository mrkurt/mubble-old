using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects.SqlServer;

namespace Mubble.Models.Controllers
{
    [RequirePermissions(PermissionType.Create, "full")]
    [RequirePermissions(PermissionType.Edit, "full")]
    public class Section : ControllerWithDiscussion
    {

        private ActiveObjects.DataManager dataManager = new SqlDataManager<Section>();
        public override ActiveObjects.DataManager DataManager
        {
            get { return this.dataManager; }
            set { this.dataManager = value; }
        }
    }
}
