/* Hash=E0D43612F47BA0F2FB197C7D3DE7DD5E */
/* Version=50 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(Controller) )]

	public partial class Page : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<Page>();

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

		public Int32 PageNumber
		{
			get { return this.DataManager.GetTypedProperty<Int32>("PageNumber"); }
			set { this.DataManager["PageNumber"] = value; }
		}

		public String Name
		{
			get { return this.DataManager.GetTypedProperty<String>("Name"); }
			set { this.DataManager["Name"] = value; }
		}

		public String Body
		{
			get { return this.DataManager.GetTypedProperty<String>("Body"); }
			set { this.DataManager["Body"] = value; }
		}

		public Guid ControllerID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ControllerID"); }
			set { this.DataManager["ControllerID"] = value; }
		}
		#endregion

		#region Relationship properties

		public Controller Controller
		{
			get { return this.DataManager.GetTypedProperty<Controller>("Controller"); }
			set { this.DataManager["Controller"] = value; }
		}
		#endregion

		#region Constructors

		public Page(){ }

		public Page(Guid id)
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

		public static Page FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<Page> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<Page> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<Page> Find(Dictionary<string, object> parameters)
		{
			return (new Page()).DataManager.List(parameters, new ActiveCollection<Page>()) as ActiveCollection<Page>;
		}
		#endregion

	}
}
