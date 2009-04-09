/* Hash=E69E1C9C6821228480DEC3EE9B28BE3D */
/* Version=57 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class ScheduledCommand : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<ScheduledCommand>();

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

		public DateTime RunAt
		{
			get { return this.DataManager.GetTypedProperty<DateTime>("RunAt"); }
			set { this.DataManager["RunAt"] = value; }
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

		public String Command
		{
			get { return this.DataManager.GetTypedProperty<String>("Command"); }
			set { this.DataManager["Command"] = value; }
		}
		#endregion

		#region Relationship properties
		#endregion

		#region Constructors

		public ScheduledCommand(){ }

		public ScheduledCommand(Guid id)
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

		public static ScheduledCommand FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<ScheduledCommand> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<ScheduledCommand> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<ScheduledCommand> Find(Dictionary<string, object> parameters)
		{
			return (new ScheduledCommand()).DataManager.List(parameters, new ActiveCollection<ScheduledCommand>()) as ActiveCollection<ScheduledCommand>;
		}
		#endregion

	}
}
