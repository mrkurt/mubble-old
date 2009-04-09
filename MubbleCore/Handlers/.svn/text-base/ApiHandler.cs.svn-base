using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Handlers
{
    public abstract class ApiHandler : HttpHandler
    {
        private System.Web.HttpContext context;

        public System.Web.HttpContext Context
        {
            get { return context; }
            set { context = value; }
        }

        public override void ProcessMubbleRequest(System.Web.HttpContext context)
        {
            this.Context = context;

            
            throw new Exception("The method or operation is not implemented.");
        }

        public abstract void TranslateAndSendResponse(object response);
    }
}
