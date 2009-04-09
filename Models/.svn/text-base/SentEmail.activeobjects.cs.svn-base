/* Hash=A633787CB9BFD327784F4DD909EB0750 */
/* Version=68 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class SentEmail : IActiveObject
	{

		#region IActiveObject implementation
		private DataManager dataManager = new SqlDataManager<SentEmail>();

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

		public String SenderIP
		{
			get { return this.DataManager.GetTypedProperty<String>("SenderIP"); }
			set { this.DataManager["SenderIP"] = value; }
		}

		public String SenderName
		{
			get { return this.DataManager.GetTypedProperty<String>("SenderName"); }
			set { this.DataManager["SenderName"] = value; }
		}

		public String SenderEmail
		{
			get { return this.DataManager.GetTypedProperty<String>("SenderEmail"); }
			set { this.DataManager["SenderEmail"] = value; }
		}

		public String RecipientEmail
		{
			get { return this.DataManager.GetTypedProperty<String>("RecipientEmail"); }
			set { this.DataManager["RecipientEmail"] = value; }
		}

		public DateTime SentAt
		{
			get { return this.DataManager.GetTypedProperty<DateTime>("SentAt"); }
			set { this.DataManager["SentAt"] = value; }
		}

		public String Message
		{
			get { return this.DataManager.GetTypedProperty<String>("Message"); }
			set { this.DataManager["Message"] = value; }
		}

		public Boolean IsSpam
		{
			get { return this.DataManager.GetTypedProperty<Boolean>("IsSpam"); }
			set { this.DataManager["IsSpam"] = value; }
		}
		#endregion

		#region Relationship properties
		#endregion

		#region Constructors

		public SentEmail(){ }

		public SentEmail(Guid id)
		{
			this.DataManager.Load("id", id);
		}
		#endregion

		#region Standard Functions

		public void Save()
		{
			this.DataManager.Save();
		}

		public static ActiveCollection<SentEmail> Find(String senderIP)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("senderIP", senderIP);
			return Find(parameters);
		}

		public static ActiveCollection<SentEmail> Find(String senderIP, Boolean isSpam)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("senderIP", senderIP);
			parameters.Add("isSpam", isSpam);
			return Find(parameters);
		}

		public bool Load(Guid id)
		{
			return this.DataManager.Load("id", id);
		}

		public static SentEmail FindFirst(Guid id)
		{
			Dictionary<string, object> parameters = new Dictionary<string,object>();
			parameters.Add("id", id);
			ActiveCollection<SentEmail> results = Find(parameters);;
			return (results.Count > 0) ? results[0] : null;;
		}

		public static ActiveCollection<SentEmail> Find()
		{
			return Find(new Dictionary<string, object>());
		}

		public static ActiveCollection<SentEmail> Find(Dictionary<string, object> parameters)
		{
			return (new SentEmail()).DataManager.List(parameters, new ActiveCollection<SentEmail>()) as ActiveCollection<SentEmail>;
		}
		#endregion

	}
}
