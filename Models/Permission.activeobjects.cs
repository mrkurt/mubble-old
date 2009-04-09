/* Hash=8D9F51BDD54628F7096B73FD8FD85541 */
/* Version=29 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class Permission : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<Permission>();

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

		public String Type
		{
			get { return this.DataManager.GetTypedProperty<String>("Type"); }
			set { this.DataManager["Type"] = value; }
		}

		public Guid ActiveObjectID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ActiveObjectID"); }
			set { this.DataManager["ActiveObjectID"] = value; }
		}

		public String Group
		{
			get { return this.DataManager.GetTypedProperty<String>("Group"); }
			set { this.DataManager["Group"] = value; }
		}

		public String Flag
		{
			get { return this.DataManager.GetTypedProperty<String>("Flag"); }
			set { this.DataManager["Flag"] = value; }
		}
		#endregion

		#region Relationship properties
		#endregion

		#region Constructors

		public Permission(){ }

		public Permission(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public static PermissionsCollection Find(String type, Guid activeObjectID)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("type", type);
			parameters.Add("activeObjectID", activeObjectID);
			return Find(parameters);
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static Permission FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			PermissionsCollection results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static PermissionsCollection Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static PermissionsCollection Find(Dictionary<string, object> parameters)
		{
			return (new Permission()).DataManager.List(parameters, new PermissionsCollection()) as PermissionsCollection;
		}
		#endregion

	}
}
