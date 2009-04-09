using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Handlers.Json
{
    public class JsonResponse
    {
        private bool isException = false;

        public bool IsException
        {
            get { return isException; }
            set { isException = value; }
        }

        private object data;

        public object Data
        {
            get { return data; }
            set { data = value; }
        }

        private string dataType;

        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }


        public JsonResponse(bool isException, object data)
        {
            this.IsException = isException;
            this.Data = data;
            this.DataType = data.GetType().Name;
        }
    }
}
