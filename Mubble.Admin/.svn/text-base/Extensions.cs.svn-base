using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Mubble.Data;

namespace Mubble.Admin
{
    public static class Extensions
    {
        internal static long InitialJavaScriptDateTicks;
        internal static DateTime MinimumJavaScriptDate;

        static Extensions()
        {
            InitialJavaScriptDateTicks = (new DateTime(1970, 1, 1)).Ticks;
            MinimumJavaScriptDate = new DateTime(100, 1, 1);
        }

        public static long ToJavascript(this DateTime dateTime)
        {
            if (dateTime < MinimumJavaScriptDate)
                dateTime = MinimumJavaScriptDate;

            long javaScriptTicks = (dateTime.Ticks - InitialJavaScriptDateTicks) / (long)10000;

            return javaScriptTicks;
        }

        public static long ToJavascriptUtc(this DateTime dateTime)
        {
            return ToJavascript(dateTime.ToUniversalTime());
        }

        public static string Escape(this string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return HttpUtility.HtmlEncode(text);
        }

        public static string ToLines<T>(this IEnumerable<T> list)
        {
            return ToLines(list, s => s.ToString());
        }

        public static string ToLines<T>(this IEnumerable<T> list, Func<T, string> retriever)
        {
            var blah = "";
            foreach (var data in list)
            {
                blah += retriever(data) + Environment.NewLine;
            }
            return blah;
        }

        public static K Fold<T, K>(this IEnumerable<T> list, K initial, Func<T, K, K> acc)
            where T : IData
        {
            var blah = initial;
            foreach (var data in list)
            {
                blah = acc(data, blah);
            }
            return blah;
        }
    }
}
