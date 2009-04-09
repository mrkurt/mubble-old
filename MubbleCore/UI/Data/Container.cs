using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using ActiveObjects;

namespace Mubble.UI.Data
{
    public class Container : ContainerControl, IScoped
    {
        #region IScoped Members
        private Controller controller;

        public object Scope
        {
            get
            {
                this.LoadController();
                return controller;
            }
        }

        #endregion

        public Container()
        {
            this.Load += new EventHandler(Container_Load);
        }

        void Container_Load(object sender, EventArgs e)
        {
            this.LoadController();
        }

        bool controllerLoaded = false;
        void LoadController()
        {
            if (controllerLoaded) return;
            controllerLoaded = true;

            IActiveObject obj = Control.GetCurrentScope<IActiveObject>(this);
            if (obj == null) return;

            object idField = obj.DataManager["ControllerID"];

            if(idField is Guid)
            {
                controller = DataBroker.GetController((Guid)idField);
            }
        }
    }
}
