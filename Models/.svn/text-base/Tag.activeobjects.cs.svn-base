/* Hash=16B3F358DAC800AEFAB0A406249C170D */
/* Version=39 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class Tag : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<Tag>();

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

		public String Name
		{
			get { return this.DataManager.GetTypedProperty<String>("Name"); }
			set { this.DataManager["Name"] = value; }
		}

		public String StringValueNormalized
		{
			get { return this.DataManager.GetTypedProperty<String>("StringValueNormalized"); }
			set { this.DataManager["StringValueNormalized"] = value; }
		}

		public Double NumericValue
		{
			get { return this.DataManager.GetTypedProperty<Double>("NumericValue"); }
			set { this.DataManager["NumericValue"] = value; }
		}

		public Guid ActiveObjectID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ActiveObjectID"); }
			set { this.DataManager["ActiveObjectID"] = value; }
		}

		public Boolean NormalizeStringValue
		{
			get { return this.DataManager.GetTypedProperty<Boolean>("NormalizeStringValue"); }
			set { this.DataManager["NormalizeStringValue"] = value; }
		}
		#endregion

		#region Relationship properties
		#endregion

		#region Constructors

		public Tag(){ }

		public Tag(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public static MetaDataCollection Find(String type, Guid activeObjectID)
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

		public static Tag FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			MetaDataCollection results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static MetaDataCollection Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static MetaDataCollection Find(Dictionary<string, object> parameters)
		{
			return (new Tag()).DataManager.List(parameters, new MetaDataCollection()) as MetaDataCollection;
		}
		#endregion

	}
}
