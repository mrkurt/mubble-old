using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Handlers.Json
{
    public class SecurityException : JsonException
    {
        private string[] requiredFlags;

        public string[] RequiredFlags
        {
            get { return requiredFlags; }
            set { requiredFlags = value; }
        }

        private string[] currentFlags;

        public string[] CurrentFlags
        {
            get { return currentFlags; }
            set { currentFlags = value; }
        }

        private string[] currentGroups;

        public string[] CurrentGroups
        {
            get { return currentGroups; }
            set { currentGroups = value; }
        }

        public SecurityException(string message) : base(message) { }
    }
}
