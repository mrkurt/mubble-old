/* Hash=5DE24988572CBD7118206D1A446E7A84 */
/* Version=43 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(Module) )]

	public partial class PermissionFlag : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<PermissionFlag>();

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

		public String Flag
		{
			get { return this.DataManager.GetTypedProperty<String>("Flag"); }
			set { this.DataManager["Flag"] = value; }
		}

		public Guid ModuleID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ModuleID"); }
			set { this.DataManager["ModuleID"] = value; }
		}
		#endregion

		#region Relationship properties

		public Module Module
		{
			get { return this.DataManager.GetTypedProperty<Module>("Module"); }
			set { this.DataManager["Module"] = value; }
		}
		#endregion

		#region Constructors

		public PermissionFlag(){ }

		public PermissionFlag(Guid moduleID, String flag)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("moduleID", moduleID);
			parameters.Add("flag", flag);
			this.DataManager.Load(parameters);
		}

		public PermissionFlag(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public bool Load(Guid moduleID, String flag)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("moduleID", moduleID);
			parameters.Add("flag", flag);
			return this.DataManager.Load(parameters);
		}

		public static PermissionFlag FindFirst(Guid moduleID, String flag)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("moduleID", moduleID);
			parameters.Add("flag", flag);
			ActiveCollection<PermissionFlag> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static PermissionFlag FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<PermissionFlag> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<PermissionFlag> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<PermissionFlag> Find(Dictionary<string, object> parameters)
		{
			return (new PermissionFlag()).DataManager.List(parameters, new ActiveCollection<PermissionFlag>()) as ActiveCollection<PermissionFlag>;
		}
		#endregion

	}
}
