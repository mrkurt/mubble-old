/* Hash=49A2C7D89C47502A86DC84906466728E */
/* Version=7 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[HasMany( typeof(Controller) )]

	public partial class Template : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<Template>();

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

		public ActiveCollection<Controller> Controllers
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<Controller>>("Controllers"); }
		}
		#endregion

		#region Constructors

		public Template(){ }

		public Template(Guid id)
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

		public static Template FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<Template> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<Template> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<Template> Find(Dictionary<string, object> parameters)
		{
			return (new Template()).DataManager.List(parameters, new ActiveCollection<Template>()) as ActiveCollection<Template>;
		}
		#endregion

	}
}
