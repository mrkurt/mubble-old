/* Hash=81F2D1285668C4D7F1E3FA68AF697808 */
/* Version=73 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(Controller) )]

	public partial class Post : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<Post>();

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

		public String Slug
		{
			get { return this.DataManager.GetTypedProperty<String>("Slug"); }
			set { this.DataManager["Slug"] = value; }
		}

		public String UserName
		{
			get { return this.DataManager.GetTypedProperty<String>("UserName"); }
			set { this.DataManager["UserName"] = value; }
		}

		public String Title
		{
			get { return this.DataManager.GetTypedProperty<String>("Title"); }
			set { this.DataManager["Title"] = value; }
		}

		public String Excerpt
		{
			get { return this.DataManager.GetTypedProperty<String>("Excerpt"); }
			set { this.DataManager["Excerpt"] = value; }
		}

		public String Body
		{
			get { return this.DataManager.GetTypedProperty<String>("Body"); }
			set { this.DataManager["Body"] = value; }
		}

		public DateTime PublishDate
		{
			get { return this.DataManager.GetTypedProperty<DateTime>("PublishDate"); }
			set { this.DataManager["PublishDate"] = value; }
		}

		public String DiscussionUrl
		{
			get { return this.DataManager.GetTypedProperty<String>("DiscussionUrl"); }
			set { this.DataManager["DiscussionUrl"] = value; }
		}

		public String MoreUrl
		{
			get { return this.DataManager.GetTypedProperty<String>("MoreUrl"); }
			set { this.DataManager["MoreUrl"] = value; }
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

		public Post(){ }

		public Post(Guid controllerID, String slug)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			parameters.Add("slug", slug);
			this.DataManager.Load(parameters);
		}

		public Post(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public static ActiveCollection<Post> Find(Guid controllerID)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			return Find(parameters);
		}

		public static ActiveCollection<Post> Find(DateTime publishDate)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("publishDate", publishDate);
			return Find(parameters);
		}

		public bool Load(Guid controllerID, String slug)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			parameters.Add("slug", slug);
			return this.DataManager.Load(parameters);
		}

		public static Post FindFirst(Guid controllerID, String slug)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			parameters.Add("slug", slug);
			ActiveCollection<Post> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static Post FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<Post> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<Post> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<Post> Find(Dictionary<string, object> parameters)
		{
			return (new Post()).DataManager.List(parameters, new ActiveCollection<Post>()) as ActiveCollection<Post>;
		}
		#endregion

	}
}
