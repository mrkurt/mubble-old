/* Hash=B059905F9015BFEEFF86EAFE5D08F86A */
/* Version=0 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(Controller) )]

	public partial class RssFeed : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<RssFeed>();

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

		public String Title
		{
			get { return this.DataManager.GetTypedProperty<String>("Title"); }
			set { this.DataManager["Title"] = value; }
		}

		public String Link
		{
			get { return this.DataManager.GetTypedProperty<String>("Link"); }
			set { this.DataManager["Link"] = value; }
		}

		public String Description
		{
			get { return this.DataManager.GetTypedProperty<String>("Description"); }
			set { this.DataManager["Description"] = value; }
		}

		public String ManagingEditor
		{
			get { return this.DataManager.GetTypedProperty<String>("ManagingEditor"); }
			set { this.DataManager["ManagingEditor"] = value; }
		}

		public String ItemFormat
		{
			get { return this.DataManager.GetTypedProperty<String>("ItemFormat"); }
			set { this.DataManager["ItemFormat"] = value; }
		}

		public String Slug
		{
			get { return this.DataManager.GetTypedProperty<String>("Slug"); }
			set { this.DataManager["Slug"] = value; }
		}

		public String RedirectUrl
		{
			get { return this.DataManager.GetTypedProperty<String>("RedirectUrl"); }
			set { this.DataManager["RedirectUrl"] = value; }
		}

		public String RedirectExceptions
		{
			get { return this.DataManager.GetTypedProperty<String>("RedirectExceptions"); }
			set { this.DataManager["RedirectExceptions"] = value; }
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

		public RssFeed(){ }

		public RssFeed(Guid controllerID, String slug)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			parameters.Add("slug", slug);
			this.DataManager.Load(parameters);
		}

		public RssFeed(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public bool Load(Guid controllerID, String slug)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			parameters.Add("slug", slug);
			return this.DataManager.Load(parameters);
		}

		public static RssFeed FindFirst(Guid controllerID, String slug)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			parameters.Add("slug", slug);
			ActiveCollection<RssFeed> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static RssFeed FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<RssFeed> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<RssFeed> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<RssFeed> Find(Dictionary<string, object> parameters)
		{
			return (new RssFeed()).DataManager.List(parameters, new ActiveCollection<RssFeed>()) as ActiveCollection<RssFeed>;
		}
		#endregion

	}
}
