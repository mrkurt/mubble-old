/* Hash=C79F0327917045023BF31DB52C077FD8 */
/* Version=41 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(ModuleControl) )]

	public partial class AdminControl : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<AdminControl>();

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

		public String FileName
		{
			get { return this.DataManager.GetTypedProperty<String>("FileName"); }
			set { this.DataManager["FileName"] = value; }
		}

		public Int32 Order
		{
			get { return this.DataManager.GetTypedProperty<Int32>("Order"); }
			set { this.DataManager["Order"] = value; }
		}

		public Boolean IsDefault
		{
			get { return this.DataManager.GetTypedProperty<Boolean>("IsDefault"); }
			set { this.DataManager["IsDefault"] = value; }
		}

		public Guid ModuleControlID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ModuleControlID"); }
			set { this.DataManager["ModuleControlID"] = value; }
		}
		#endregion

		#region Relationship properties

		public ModuleControl ModuleControl
		{
			get { return this.DataManager.GetTypedProperty<ModuleControl>("ModuleControl"); }
			set { this.DataManager["ModuleControl"] = value; }
		}
		#endregion

		#region Constructors

		public AdminControl(){ }

		public AdminControl(Guid moduleControlID, String fileName)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("moduleControlID", moduleControlID);
			parameters.Add("fileName", fileName);
			this.DataManager.Load(parameters);
		}

		public AdminControl(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public bool Load(Guid moduleControlID, String fileName)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("moduleControlID", moduleControlID);
			parameters.Add("fileName", fileName);
			return this.DataManager.Load(parameters);
		}

		public static AdminControl FindFirst(Guid moduleControlID, String fileName)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("moduleControlID", moduleControlID);
			parameters.Add("fileName", fileName);
			ActiveCollection<AdminControl> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static AdminControl FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<AdminControl> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<AdminControl> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<AdminControl> Find(Dictionary<string, object> parameters)
		{
			return (new AdminControl()).DataManager.List(parameters, new ActiveCollection<AdminControl>()) as ActiveCollection<AdminControl>;
		}
		#endregion

	}
}
