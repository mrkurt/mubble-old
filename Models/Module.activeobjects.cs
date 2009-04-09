/* Hash=EF3A9CFC2A4231EE9F27FBB8F21F9A09 */
/* Version=43 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[HasMany( typeof(ModuleControl) )]
	[HasMany( typeof(PermissionFlag) )]

	public partial class Module : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<Module>();

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

		public String Name
		{
			get { return this.DataManager.GetTypedProperty<String>("Name"); }
			set { this.DataManager["Name"] = value; }
		}

		public String Path
		{
			get { return this.DataManager.GetTypedProperty<String>("Path"); }
			set { this.DataManager["Path"] = value; }
		}

		public DateTime UpdatedAt
		{
			get { return this.DataManager.GetTypedProperty<DateTime>("UpdatedAt"); }
			set { this.DataManager["UpdatedAt"] = value; }
		}
		#endregion

		#region Relationship properties

		public ActiveCollection<ModuleControl> ModuleControls
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<ModuleControl>>("ModuleControls"); }
		}

		public ActiveCollection<PermissionFlag> PermissionFlags
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<PermissionFlag>>("PermissionFlags"); }
		}
		#endregion

		#region Constructors

		public Module(){ }

		public Module(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static Module FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<Module> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<Module> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<Module> Find(Dictionary<string, object> parameters)
		{
			return (new Module()).DataManager.List(parameters, new ActiveCollection<Module>()) as ActiveCollection<Module>;
		}
		#endregion

	}
}
