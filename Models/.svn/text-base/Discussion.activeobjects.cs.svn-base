/* Hash=3210678E460185D665007D3ABE3FDFCC */
/* Version=59 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[BelongsTo( typeof(DiscussionProvider) )]

	public partial class Discussion : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<Discussion>();

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

		public String ActiveObjectType
		{
			get { return this.DataManager.GetTypedProperty<String>("ActiveObjectType"); }
			set { this.DataManager["ActiveObjectType"] = value; }
		}

		public Guid ActiveObjectID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("ActiveObjectID"); }
			set { this.DataManager["ActiveObjectID"] = value; }
		}

		public String DiscussionLink
		{
			get { return this.DataManager.GetTypedProperty<String>("DiscussionLink"); }
			set { this.DataManager["DiscussionLink"] = value; }
		}

		public String LastStatusMessage
		{
			get { return this.DataManager.GetTypedProperty<String>("LastStatusMessage"); }
			set { this.DataManager["LastStatusMessage"] = value; }
		}

		public Guid DiscussionProviderID
		{
			get { return this.DataManager.GetTypedProperty<Guid>("DiscussionProviderID"); }
			set { this.DataManager["DiscussionProviderID"] = value; }
		}

		public DateTime PublishDate
		{
			get { return this.DataManager.GetTypedProperty<DateTime>("PublishDate"); }
			set { this.DataManager["PublishDate"] = value; }
		}

		public Int32 CommentCount
		{
			get { return this.DataManager.GetTypedProperty<Int32>("CommentCount"); }
			set { this.DataManager["CommentCount"] = value; }
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
		#endregion

		#region Relationship properties

		public DiscussionProvider DiscussionProvider
		{
			get { return this.DataManager.GetTypedProperty<DiscussionProvider>("DiscussionProvider"); }
			set { this.DataManager["DiscussionProvider"] = value; }
		}
		#endregion

		#region Constructors

		public Discussion(){ }

		public Discussion(String activeObjectType, Guid activeObjectID)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("activeObjectType", activeObjectType);
			parameters.Add("activeObjectID", activeObjectID);
			this.DataManager.Load(parameters);
		}

		public Discussion(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public bool Load(String activeObjectType, Guid activeObjectID)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("activeObjectType", activeObjectType);
			parameters.Add("activeObjectID", activeObjectID);
			return this.DataManager.Load(parameters);
		}

		public static Discussion FindFirst(String activeObjectType, Guid activeObjectID)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("activeObjectType", activeObjectType);
			parameters.Add("activeObjectID", activeObjectID);
			ActiveCollection<Discussion> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static Discussion FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<Discussion> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<Discussion> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<Discussion> Find(Dictionary<string, object> parameters)
		{
			return (new Discussion()).DataManager.List(parameters, new ActiveCollection<Discussion>()) as ActiveCollection<Discussion>;
		}
		#endregion

	}
}
