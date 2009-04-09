using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Mubble.Config
{
    /// <summary>
    /// Defines a module, includes control definitions
    /// </summary>
    public class Module
    {
        #region Properties
        private Guid id;
        /// <summary>
        /// The GUID associated with this module
        /// </summary>
        [XmlAttribute]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }


        private string name;

        /// <summary>
        /// The name of this module
        /// </summary>
        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        private List<Control> controls = new List<Control>();

        /// <summary>
        /// Gets or sets the controls associated with this module
        /// </summary>
        public List<Control> Controls
        {
            get { return controls; }
            set { controls = value; }
        }

        private List<PermissionFlag> permissionFlags = new List<PermissionFlag>();

        public List<PermissionFlag> PermissionFlags
        {
            get { return permissionFlags; }
            set { permissionFlags = value; }
        }


        #endregion

        #region Control functions
        /// <summary>
        /// Finds the specified module control
        /// </summary>
        /// <param name="fileName">Control filename</param>
        /// <returns>The requested Control.  Null if it does not exist</returns>
        public Control Find(string fileName)
        {
            foreach (Control c in Controls)
            {
                if (c.FileName == fileName)
                {
                    return c;
                }
            }
            return null;
        }
        #endregion

        #region Loading functions
        /// <summary>
        /// Loads the backend database with available modules in the Modules directory
        /// </summary>
        /// <param name="basePath">The full physical path to the modules directory to search for</param>
        /// <param name="relPath">The path relative to the application</param>
        public static void LoadDbFromFileSystem(string basePath, string relPath)
        {
            DirectoryInfo baseDir = new DirectoryInfo(basePath);
            foreach (DirectoryInfo dir in baseDir.GetDirectories())
            {
                string configFile = dir.FullName + "\\module.config";
                if (!File.Exists(configFile)) continue;
                Module config = Module.Load(configFile);

                Mubble.Models.Module module = new Mubble.Models.Module(config.Id);
                if (config.Controls.Count > 0)
                {
                    module.ID = config.Id;
                    module.Name = config.Name;
                    module.Path = relPath + dir.Name + "/";
                    foreach (Mubble.Config.Control c in config.Controls)
                    {
                        Mubble.Models.ModuleControl mc = new Mubble.Models.ModuleControl(c.Id);
                        mc.ID = c.Id;
                        mc.Name = c.Name;
                        mc.FileName = c.FileName;
                        mc.Order = c.Order;
                        mc.Type = c.Type;
                        mc.ControllerActiveObjectType = c.ActiveObjectsType;

                        mc.IsContent = c.IsContent;
                        mc.IsContentContainer = c.IsContentContainer;

                        foreach (AdminControl ac in c.AdminControls)
                        {
                            Mubble.Models.AdminControl mac = new Mubble.Models.AdminControl(c.Id, ac.FileName);
                            mac.Name = ac.Name;
                            mac.FileName = ac.FileName;
                            mac.IsDefault = ac.IsDefault;
                            mac.Order = ac.Order;

                            mc.AdminControls.Add(mac);
                        }

                        foreach (Route r in c.Routes)
                        {
                            Mubble.Models.Route route = new Mubble.Models.Route(r.ID);
                            route.Pattern = r.Pattern;
                            route.Order = r.Order;
                            route.Name = r.Name;
                            route.FormatString = r.FormatString;
                            route.IsDefault = r.IsDefault;

                            if (route.ID == Guid.Empty)
                            {
                                route.ID = r.ID;
                                mc.Routes.Add(route);
                            }
                            else
                            {
                                route.Save();
                            }
                        }
                        
                        module.ModuleControls.Add(mc);
                    }

                    module.PermissionFlags.Clear();
                    foreach (PermissionFlag f in config.PermissionFlags)
                    {
                        Mubble.Models.PermissionFlag pf = new Mubble.Models.PermissionFlag();
                        pf.Name = f.Name;
                        pf.Flag = f.Flag;
                        module.PermissionFlags.Add(pf);
                    }

                    module.Save();
                }
            }
        }

        /// <summary>
        /// Gets the configuration object from the specified XML file
        /// </summary>
        /// <param name="fileName">The full path to the serialized XML</param>
        /// <returns>Requested Module configuration object</returns>
        public static Module Load(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                XmlSerializer s = new XmlSerializer(typeof(Mubble.Config.Module));
                TextReader txt = new StreamReader(fileName);

                Module m = (Module)s.Deserialize(txt);

                txt.Close();

                return m;
            }
            else
            {
                Module m = new Module();
                System.IO.FileInfo info = new FileInfo(fileName);
                int count = 0;
                foreach (FileInfo file in info.Directory.GetFiles())
                {
                    if (file.Name.EndsWith(".aspx"))
                    {
                        Control c = new Control();
                        c.Name = file.Name.Substring(0, file.Name.LastIndexOf('.')) ;
                        c.FileName = file.Name;
                        c.Order = count++;

                        m.Controls.Add(c);
                    }
                }
                return m;
            }
        }
        #endregion
    }
    /// <summary>
    /// Defines a module control.  This definition includes various admin pages associated with the module
    /// </summary>
    public class Control
    {
        /// <summary>
        /// Gets the default AdminControl for this particular control.
        /// If there is no control marked as default, it returns the first defined AdminControl.
        /// Returns null when there are no admin controls defined.
        /// </summary>
        public AdminControl DefaultAdminControl
        {
            get
            {
                foreach (AdminControl control in AdminControls)
                {
                    if (control.IsDefault)
                    {
                        return control;
                    }
                }
                if (AdminControls.Count > 0)
                {
                    return AdminControls[0];
                }
                return null;
            }
        }
        private List<AdminControl> adminControls = new List<AdminControl>();

        /// <summary>
        /// Gets or sets the the admin controls associated with this module control
        /// </summary>
        public List<AdminControl> AdminControls
        {
            get { return adminControls; }
            set { adminControls = value; }
        }

        private List<Route> routes;

        /// <summary>
        /// Gets or sets the list of routes for this control
        /// </summary>
        public List<Route> Routes
        {
            get { return routes; }
            set { routes = value; }
        }
        private Guid id;

        [XmlAttribute]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        private bool isContent = false;

        [XmlAttribute]
        public bool IsContent
        {
            get { return isContent; }
            set { isContent = value; }
        }

        private bool isContentContainer = false;

        [XmlAttribute]
        public bool IsContentContainer
        {
            get { return isContentContainer; }
            set { isContentContainer = value; }
        }



        private string activeObjectsType;

        [XmlAttribute]
        public string ActiveObjectsType
        {
            get { return activeObjectsType; }
            set { activeObjectsType = value; }
        }

        private string name;

        /// <summary>
        /// Gets or sets the English control name
        /// </summary>
        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string type;

        /// <summary>
        /// Gets or sets the type of this control
        /// </summary>
        [XmlAttribute]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
	

        private int order;

        /// <summary>
        /// Gets or sets the control display order
        /// </summary>
        [XmlAttribute]
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        private string fileName;

        /// <summary>
        /// The FileName for this module control
        /// </summary>
        [XmlAttribute]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// Finds the specified admin control using its filename
        /// </summary>
        /// <param name="fileName">fileName of the admin control to find</param>
        /// <returns>The admin control.  Null if it does not exist</returns>
        public AdminControl Find(string fileName)
        {
            if (this.AdminControls == null)
            {
                return null;
            }
            foreach (AdminControl control in this.AdminControls)
            {
                if (control.FileName.ToLower() == fileName.ToLower())
                {
                    return control;
                }
            }
            return null;
        }	
    }
    /// <summary>
    /// Defines an admin page relevant to a module
    /// </summary>
    public class AdminControl
    {
        private bool isDefault = false;
        /// <summary>
        /// Gets or sets a flag indicating whether this is the default admin control for the specified module control
        /// </summary>
        [XmlAttribute]
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }
        private string name;

        /// <summary>
        /// Gets or sets the English name of this admin control
        /// </summary>
        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [XmlIgnore]
        public string Key
        {
            get
            {
                int lastDot = fileName.LastIndexOf('.');
                return fileName.Substring(0,lastDot).ToLower();
            }
        }
        private string fileName;

        /// <summary>
        /// Gets or sets the FileName for this admin control
        /// </summary>
        [XmlAttribute]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private int order;

        /// <summary>
        /// Gets or sets the visual order of this control (for menus and such)
        /// </summary>
        [XmlAttribute]
        public int Order
        {
            get { return order; }
            set { order = value; }
        }
    }

    public class Route
    {
        private Guid id;

        [XmlAttribute]
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }


        private string pattern;

        [XmlAttribute]
        public string Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }

        private bool isDefault = false;

        [XmlAttribute]
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        private int order;

        [XmlAttribute]
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        private string name;

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string formatString;

        [XmlAttribute]
        public string FormatString
        {
            get { return formatString; }
            set { formatString = value; }
        }
    }

    public class PermissionFlag
    {
        private string name;

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string flag;

        [XmlAttribute]
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

    }
}
