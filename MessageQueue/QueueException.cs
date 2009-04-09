using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.MessageQueue
{
    public class QueueException : Exception
    {
        public QueueException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class QueueSendException : Exception
    {
        public object QueueMessage { get; set; }

        public QueueSendException(string message, object queueMessage, Exception innerException)
            : base(message, innerException) 
        {
            this.QueueMessage = queueMessage;
        }
    }
}
