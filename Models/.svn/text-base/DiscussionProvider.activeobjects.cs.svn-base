/* Hash=7358E671A5FEFEDB4E5CB4CE8E7F8E23 */
/* Version=54 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
	[HasMany( typeof(Discussion) )]

	public partial class DiscussionProvider : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<DiscussionProvider>();

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

		public String ActiveObjectType
		{
			get { return this.DataManager.GetTypedProperty<String>("ActiveObjectType"); }
			set { this.DataManager["ActiveObjectType"] = value; }
		}

		public String Settings
		{
			get { return this.DataManager.GetTypedProperty<String>("Settings"); }
			set { this.DataManager["Settings"] = value; }
		}

		public Boolean IsDefault
		{
			get { return this.DataManager.GetTypedProperty<Boolean>("IsDefault"); }
			set { this.DataManager["IsDefault"] = value; }
		}
		#endregion

		#region Relationship properties

		public ActiveCollection<Discussion> Discussions
		{
			get { return this.DataManager.GetTypedProperty<ActiveCollection<Discussion>>("Discussions"); }
		}
		#endregion

		#region Constructors

		public DiscussionProvider(){ }

		public DiscussionProvider(Guid id)
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

		public static DiscussionProvider FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<DiscussionProvider> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<DiscussionProvider> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<DiscussionProvider> Find(Dictionary<string, object> parameters)
		{
			return (new DiscussionProvider()).DataManager.List(parameters, new ActiveCollection<DiscussionProvider>()) as ActiveCollection<DiscussionProvider>;
		}
		#endregion

	}
}
