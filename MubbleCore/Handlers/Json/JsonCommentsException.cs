using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Handlers.Json
{
    public class JsonCommentsException : JsonException
    {
        public JsonCommentsException(string message) : base(message) { }
    }
    public class JsonCommentsTimeout : JsonCommentsException
    {
        public JsonCommentsTimeout(string message) : base(message) { }
    }
    public class JsonCommentsInvalidResponse : JsonException
    {
        public JsonCommentsInvalidResponse(string message) : base(message) { }
    }
    public class JsonCommentsNoDiscussion : JsonException
    {
        public JsonCommentsNoDiscussion(string message) : base(message) { }
    }
}
