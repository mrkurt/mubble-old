using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Exceptions
{
    public class ConfigurationException : System.Exception
    {
        public ConfigurationException()
            : base()
        {

        }

        public ConfigurationException(string message)
            : base(message)
        {

        }
        public ConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
