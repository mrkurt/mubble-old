/* Hash=8251C1589AE833498ADE58206932CCA4 */
/* Version=48 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class Author : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<Author>();

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

		public String UserName
		{
			get { return this.DataManager.GetTypedProperty<String>("UserName"); }
			set { this.DataManager["UserName"] = value; }
		}

		public String Email
		{
			get { return this.DataManager.GetTypedProperty<String>("Email"); }
			set { this.DataManager["Email"] = value; }
		}

		public String DisplayName
		{
			get { return this.DataManager.GetTypedProperty<String>("DisplayName"); }
			set { this.DataManager["DisplayName"] = value; }
		}

		public String Bio
		{
			get { return this.DataManager.GetTypedProperty<String>("Bio"); }
			set { this.DataManager["Bio"] = value; }
		}
		#endregion

		#region Relationship properties
		#endregion

		#region Constructors

		public Author(){ }

		public Author(String userName)
		{
			this.DataManager.Load("userName", userName);
		}

		public Author(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public bool Load(String userName)
		{
			return this.DataManager.Load("userName", userName);
		}

		public static Author FindFirst(String userName)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("userName", userName);
			ActiveCollection<Author> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static Author FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<Author> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<Author> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<Author> Find(Dictionary<string, object> parameters)
		{
			return (new Author()).DataManager.List(parameters, new ActiveCollection<Author>()) as ActiveCollection<Author>;
		}
		#endregion

	}
}
