/* Hash=628BBD1437202156D1F20BB9D87F97C3 */
/* Version=71 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(Controller) )]

	public partial class File : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<File>();

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

		public String PhysicalPath
		{
			get { return this.DataManager.GetTypedProperty<String>("PhysicalPath"); }
			set { this.DataManager["PhysicalPath"] = value; }
		}

		public String MediaType
		{
			get { return this.DataManager.GetTypedProperty<String>("MediaType"); }
			set { this.DataManager["MediaType"] = value; }
		}

		public String MediaSubType
		{
			get { return this.DataManager.GetTypedProperty<String>("MediaSubType"); }
			set { this.DataManager["MediaSubType"] = value; }
		}

		public DateTime UpdatedAt
		{
			get { return this.DataManager.GetTypedProperty<DateTime>("UpdatedAt"); }
			set { this.DataManager["UpdatedAt"] = value; }
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

		public File(){ }

		public File(Guid controllerID, String fileName)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			parameters.Add("fileName", fileName);
			this.DataManager.Load(parameters);
		}

		public File(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public bool Load(Guid controllerID, String fileName)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			parameters.Add("fileName", fileName);
			return this.DataManager.Load(parameters);
		}

		public static File FindFirst(Guid controllerID, String fileName)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			parameters.Add("fileName", fileName);
			ActiveCollection<File> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static File FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<File> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<File> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<File> Find(Dictionary<string, object> parameters)
		{
			return (new File()).DataManager.List(parameters, new ActiveCollection<File>()) as ActiveCollection<File>;
		}
		#endregion

	}
}
