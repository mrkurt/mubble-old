/* Hash=C8C518DF845A0C85519F0DC48AC706C2 */
/* Version=67 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(Module) )]
	[HasMany( typeof(AdminControl) )]
	[HasMany( typeof(Controller) )]
	[HasMany( typeof(Route) )]

	public partial class ModuleControl : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<ModuleControl>();

		public virtual DataManager DataManager
		{
			get{ return dataManager; }
			set{ dataManager = value; }
		}
		#endregion

		#region Properties

		public Guid ID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ID"); }
			set { this.DataManager["ID"] = value; }
		}

		public Int32 Order
		{
			get { return this.DataManager.GetTypedProperty<Int32>("Order"); }
			set { this.DataManager["Order"] = value; }
		}

		public String Name
		{
			get { return this.DataManager.GetTypedProperty<String>("Name"); }
			set { this.DataManager["Name"] = value; }
		}

		public String FileName
		{
			get { return this.DataManager.GetTypedProperty<String>("FileName"); }
			set { this.DataManager["FileName"] = value; }
		}

		public String Type
		{
			get { return this.DataManager.GetTypedProperty<String>("Type"); }
			set { this.DataManager["Type"] = value; }
		}

		public Guid ModuleID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ModuleID"); }
			set { this.DataManager["ModuleID"] = value; }
		}

		public String ControllerActiveObjectType
		{
			get { return this.DataManager.GetTypedProperty<String>("ControllerActiveObjectType"); }
			set { this.DataManager["ControllerActiveObjectType"] = value; }
		}

		public Boolean IsContent
		{
			get { return this.DataManager.GetTypedProperty<Boolean>("IsContent"); }
			set { this.DataManager["IsContent"] = value; }
		}

		public Boolean IsContentContainer
		{
			get { return this.DataManager.GetTypedProperty<Boolean>("IsContentContainer"); }
			set { this.DataManager["IsContentContainer"] = value; }
		}
		#endregion

		#region Relationship properties

		public Module Module
		{
			get { return this.DataManager.GetTypedProperty<Module>("Module"); }
			set { this.DataManager["Module"] = value; }
		}

		public ActiveCollection<AdminControl> AdminControls
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<AdminControl>>("AdminControls"); }
		}

		public ActiveCollection<Controller> Controllers
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<Controller>>("Controllers"); }
		}

		public ActiveCollection<Route> Routes
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<Route>>("Routes"); }
		}
		#endregion

		#region Constructors

		public ModuleControl(){ }

		public ModuleControl(Guid moduleID, String fileName)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("moduleID", moduleID);
			parameters.Add("fileName", fileName);
			this.DataManager.Load(parameters);
		}

		public ModuleControl(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public bool Load(Guid moduleID, String fileName)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("moduleID", moduleID);
			parameters.Add("fileName", fileName);
			return this.DataManager.Load(parameters);
		}

		public static ModuleControl FindFirst(Guid moduleID, String fileName)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("moduleID", moduleID);
			parameters.Add("fileName", fileName);
			ActiveCollection<ModuleControl> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static ModuleControl FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<ModuleControl> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<ModuleControl> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<ModuleControl> Find(Dictionary<string, object> parameters)
		{
			return (new ModuleControl()).DataManager.List(parameters, new ActiveCollection<ModuleControl>()) as ActiveCollection<ModuleControl>;
		}
		#endregion

	}
}
