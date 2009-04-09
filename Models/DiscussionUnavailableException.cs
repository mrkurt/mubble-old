using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models
{
    public class DiscussionUnavailableException : Exception
    {
        public DiscussionUnavailableException(string message) : base(message) { }
        public DiscussionUnavailableException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class DiscussionCreationException : Exception
    {
        private DiscussionCreationExceptionType type;

        public DiscussionCreationExceptionType Type
        {
            get { return type; }
            set { type = value; }
        }

        public DiscussionCreationException(string message, Exception innerException, DiscussionCreationExceptionType type) 
            : base(message, innerException)
        {
            this.Type = type;
        }
    }

    public enum DiscussionCreationExceptionType
    {
        HttpError = 0,
        DuplicatePost = 1,
        Other = 10
    }
}
