using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Handlers.Json
{
    public class JsonException : Exception
    {
        public JsonException(string message) : base(message) { }
    }
}
