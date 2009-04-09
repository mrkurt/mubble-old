using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.Controllers
{
    public class Email
    {
        private EmailType emailType = EmailType.Internal;

        public EmailType Type
        {
            get { return emailType; }
            set { emailType = value; }
        }

        private string subject;

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        private string body;

        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        private bool isValid = true;

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

    }

    public enum EmailType
    {
        Internal,
        External
    }
}
