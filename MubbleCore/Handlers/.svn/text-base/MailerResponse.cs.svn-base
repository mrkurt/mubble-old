using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Handlers
{
    public enum MailerResponseType
    {
        Success,
        Spam,
        InvalidFromAddress,
        InvalidRecipientAddress,
        TooManyRecipients,
        NoContentItem
    }
    public class MailerResponse
    {
        private MailerResponseType type;

        public MailerResponseType Type
        {
            get { return type; }
            set { type = value; }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

    }
}
