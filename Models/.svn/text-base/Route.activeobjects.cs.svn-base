/* Hash=BF1304B6C4A4495941431AA95CF44DAB */
/* Version=52 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(ModuleControl) )]
	[HasMany( typeof(Controller) )]

	public partial class Route : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<Route>();

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

		public String Pattern
		{
			get { return this.DataManager.GetTypedProperty<String>("Pattern"); }
			set { this.DataManager["Pattern"] = value; }
		}

		public Guid ModuleControlID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ModuleControlID"); }
			set { this.DataManager["ModuleControlID"] = value; }
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

		public String Name
		{
			get { return this.DataManager.GetTypedProperty<String>("Name"); }
			set { this.DataManager["Name"] = value; }
		}

		public String FormatString
		{
			get { return this.DataManager.GetTypedProperty<String>("FormatString"); }
			set { this.DataManager["FormatString"] = value; }
		}
		#endregion

		#region Relationship properties

		public ModuleControl ModuleControl
		{
			get { return this.DataManager.GetTypedProperty<ModuleControl>("ModuleControl"); }
			set { this.DataManager["ModuleControl"] = value; }
		}

		public ActiveCollection<Controller> Controllers
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<Controller>>("Controllers"); }
		}
		#endregion

		#region Constructors

		public Route(){ }

		public Route(Guid id)
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

		public static Route FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<Route> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<Route> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<Route> Find(Dictionary<string, object> parameters)
		{
			return (new Route()).DataManager.List(parameters, new ActiveCollection<Route>()) as ActiveCollection<Route>;
		}
		#endregion

	}
}
