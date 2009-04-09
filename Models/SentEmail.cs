using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class SentEmail
	{
        private bool? isSpammer = null;
        public bool IsSpammer
        {
            get
            {
                if (isSpammer == null)
                {
                    if (SentEmail.Find(this.SenderIP, true).Count > 0)
                    {
                        isSpammer = true;
                    }
                }
                if (isSpammer == null)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("SenderIP", this.SenderIP);
                    parameters.Add("StartSentAt", DateTime.Now.AddMonths(-1));
                    parameters.Add("EndSentAt", DateTime.Now);
                    parameters.Add("RowIndex_start", 0);
                    parameters.Add("RowIndex_end", 1);

                    ActiveCollection<SentEmail> list = SentEmail.Find(parameters);

                    if (list.TotalResults > 100)
                    {
                        isSpammer = true;
                    }
                    else
                    {
                        isSpammer = false;
                    }
                }
                return isSpammer.Value;
            }
        }
	}
}
