/* Hash=AF993F23E5BA2861C5A25CC15D5394AA */
/* Version=63 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(Controller) )]

	public partial class UrlRedirect : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<UrlRedirect>();

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

		public String Url
		{
			get { return this.DataManager.GetTypedProperty<String>("Url"); }
			set { this.DataManager["Url"] = value; }
		}

		public String PathExtra
		{
			get { return this.DataManager.GetTypedProperty<String>("PathExtra"); }
			set { this.DataManager["PathExtra"] = value; }
		}

		public Guid ControllerID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ControllerID"); }
			set { this.DataManager["ControllerID"] = value; }
		}

		public String Handler
		{
			get { return this.DataManager.GetTypedProperty<String>("Handler"); }
			set { this.DataManager["Handler"] = value; }
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

		public UrlRedirect(){ }

		public UrlRedirect(String url)
		{
			this.DataManager.Load("url", url);
		}

		public UrlRedirect(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public bool Load(String url)
		{
			return this.DataManager.Load("url", url);
		}

		public static UrlRedirect FindFirst(String url)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("url", url);
			ActiveCollection<UrlRedirect> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static UrlRedirect FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<UrlRedirect> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<UrlRedirect> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<UrlRedirect> Find(Dictionary<string, object> parameters)
		{
			return (new UrlRedirect()).DataManager.List(parameters, new ActiveCollection<UrlRedirect>()) as ActiveCollection<UrlRedirect>;
		}
		#endregion

	}
}
