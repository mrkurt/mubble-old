/* Hash=0B08E98CBB6257CF5CE078D830105854 */
/* Version=74 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(Controller) )]
	[BelongsTo( typeof(ModuleControl) )]
	[BelongsTo( typeof(Route) )]
	[BelongsTo( typeof(Template) )]
	[HasMany( typeof(Controller) )]
	[HasMany( typeof(File) )]
	[HasMany( typeof(Page) )]
	[HasMany( typeof(Post) )]
	[HasMany( typeof(RssFeed) )]
	[HasMany( typeof(UrlRedirect) )]

	public partial class Controller : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<Controller>();

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

		public String FileName
		{
			get { return this.DataManager.GetTypedProperty<String>("FileName"); }
			set { this.DataManager["FileName"] = value; }
		}

		public String Body
		{
			get { return this.DataManager.GetTypedProperty<String>("Body"); }
			set { this.DataManager["Body"] = value; }
		}

		public String Path
		{
			get { return this.DataManager.GetTypedProperty<String>("Path"); }
		}

		public DateTime PublishDate
		{
			get { return this.DataManager.GetTypedProperty<DateTime>("PublishDate"); }
			set { this.DataManager["PublishDate"] = value; }
		}

		public DateTime ModifyDate
		{
			get { return this.DataManager.GetTypedProperty<DateTime>("ModifyDate"); }
			set { this.DataManager["ModifyDate"] = value; }
		}

		public DateTime UpdatedAt
		{
			get { return this.DataManager.GetTypedProperty<DateTime>("UpdatedAt"); }
			set { this.DataManager["UpdatedAt"] = value; }
		}

		public Guid TemplateID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("TemplateID"); }
			set { this.DataManager["TemplateID"] = value; }
		}

		public Guid ControllerID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ControllerID"); }
			set { this.DataManager["ControllerID"] = value; }
		}

		public String TemplateControl
		{
			get { return this.DataManager.GetTypedProperty<String>("TemplateControl"); }
			set { this.DataManager["TemplateControl"] = value; }
		}

		public String Excerpt
		{
			get { return this.DataManager.GetTypedProperty<String>("Excerpt"); }
			set { this.DataManager["Excerpt"] = value; }
		}

		public String Summary
		{
			get { return this.DataManager.GetTypedProperty<String>("Summary"); }
			set { this.DataManager["Summary"] = value; }
		}

		public String ActiveObjectType
		{
			get { return this.DataManager.GetTypedProperty<String>("ActiveObjectType"); }
			set { this.DataManager["ActiveObjectType"] = value; }
		}

		public Boolean IsContent
		{
			get { return this.DataManager.GetTypedProperty<Boolean>("IsContent"); }
			set { this.DataManager["IsContent"] = value; }
		}

		public Boolean IsContentContainer
		{
			get { return this.DataManager.GetTypedProperty<Boolean>("IsContentContainer"); }
			set { this.DataManager["IsContentContainer"] = value; }
		}

		public Guid ModuleControlID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ModuleControlID"); }
			set { this.DataManager["ModuleControlID"] = value; }
		}

		public String ModuleControlView
		{
			get { return this.DataManager.GetTypedProperty<String>("ModuleControlView"); }
			set { this.DataManager["ModuleControlView"] = value; }
		}

		public Guid RouteID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("RouteID"); }
			set { this.DataManager["RouteID"] = value; }
		}
		#endregion

		#region Relationship properties

		public Controller Parent
		{
			get { return this.DataManager.GetTypedProperty<Controller>("Controller"); }
			set { this.DataManager["Controller"] = value; }
		}

		public ModuleControl ModuleControl
		{
			get { return this.DataManager.GetTypedProperty<ModuleControl>("ModuleControl"); }
			set { this.DataManager["ModuleControl"] = value; }
		}

		public Route Route
		{
			get { return this.DataManager.GetTypedProperty<Route>("Route"); }
			set { this.DataManager["Route"] = value; }
		}

		public Template Template
		{
			get { return this.DataManager.GetTypedProperty<Template>("Template"); }
			set { this.DataManager["Template"] = value; }
		}

		public ActiveCollection<Controller> Controllers
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<Controller>>("Controllers"); }
		}

		public ActiveCollection<File> Files
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<File>>("Files"); }
		}

		public ActiveCollection<Page> Pages
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<Page>>("Pages"); }
		}

		public ActiveCollection<Post> Posts
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<Post>>("Posts"); }
		}

		public ActiveCollection<RssFeed> RssFeeds
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<RssFeed>>("RssFeeds"); }
		}

		public ActiveCollection<UrlRedirect> UrlRedirects
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<UrlRedirect>>("UrlRedirects"); }
		}
		#endregion

		#region Constructors

		public Controller(){ }

		public Controller(String path)
		{
			this.DataManager.Load("path", path);
		}

		public Controller(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public static ActiveCollection<Controller> Find(Guid controllerID)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("controllerID", controllerID);
			return Find(parameters);
		}

		public static ActiveCollection<Controller> Find(DateTime publishDate)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("publishDate", publishDate);
			return Find(parameters);
		}

		public bool Load(String path)
		{
			return this.DataManager.Load("path", path);
		}

		public static Controller FindFirst(String path)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("path", path);
			ActiveCollection<Controller> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static Controller FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<Controller> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<Controller> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<Controller> Find(Dictionary<string, object> parameters)
		{
			return (new Controller()).DataManager.List(parameters, new ActiveCollection<Controller>()) as ActiveCollection<Controller>;
		}
		#endregion

	}
}
