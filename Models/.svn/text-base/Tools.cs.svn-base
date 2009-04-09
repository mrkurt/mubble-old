using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ActiveObjects;

namespace Mubble.Models
{
    public static class Tools
    {
        public static IActiveObject GetActiveObject(DataManager dm)
        {
            IActiveObject obj = Activator.CreateInstance(dm.Settings.ActiveObjectType) as IActiveObject;
            if (obj != null)
            {
                obj.DataManager = dm;
            }
            return obj;
        }

        public static Type GetActiveObjectType(string name)
        {
            return ActiveObjects.DataManager.GetBaseActiveObjectType(Type.GetType(name, false, true));
        }

        public static string GetParametersString(Dictionary<string, object> parameters)
        {
            if (parameters == null) return "";
            StringBuilder where = new StringBuilder();
            foreach (string key in parameters.Keys)
            {
                if (where.Length > 0)
                {
                    where.Append(",");
                }
                where.Append("@");
                where.Append(key);
                where.Append(" = ");
                object value = parameters[key];
                if (value != null && (value is string || value.GetType().IsValueType))
                {
                    where.Append("'");
                    where.Append(value.ToString().Replace("'", "''"));
                    where.Append("'");
                }
                else if (value is IEnumerable)
                {
                    bool first = true;
                    where.Append("[");
                    foreach (object val in (IEnumerable)value)
                    {
                        if (!first)
                        {
                            where.Append(", ");
                        }
                        where.Append(val);
                        first = false;
                    }
                    where.Append("]");
                }
            }
            return where.ToString();
        }
    }
}
