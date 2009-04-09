using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.Tools
{
    public class Http
    {
        public static void RedirectPermanently(Mubble.Models.Link url)
        {
            RedirectPermanently(MubbleUrl.Create(url.Path, url.Extra));
        }

        public static void RedirectPermanently(MubbleUrl url)
        {
            RedirectPermanently(url.ToString());
        }

        public static void RedirectPermanently(string location)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            context.Response.StatusCode = 301;
            context.Response.StatusDescription = "Object moved permanently";
            context.Response.RedirectLocation = location;
            context.Response.Write("This object has been permanently moved to " + location);
            context.Response.End();
        }

        public static string MakeUrlFriendlyString(string original)
        {

            string temp = original.Trim();
            string copy = "";
            bool lastIsDigit = false;
            for (int i = 0; i < temp.Length; i++)
            {
                if (char.IsLetterOrDigit(temp[i]))
                    copy += temp[i];
                else if (temp[i] == '-' || temp[i] == '_')
                    copy += temp[i];
                //else if ((temp[i] == ' ' || char.IsPunctuation(temp[i])) && lastIsDigit && temp.Length > i - 1)
                //   copy += ".";
                else if ((char.IsSeparator(temp[i]) || char.IsWhiteSpace(temp[i])) && lastIsDigit && temp.Length > i - 1)
                    copy += "-";

                lastIsDigit = char.IsLetterOrDigit(copy[copy.Length - 1]);
            }
            if (copy.Length > 0 && !char.IsLetterOrDigit(copy[copy.Length - 1]))
                copy = copy.Substring(0, copy.Length - 1);
            return copy;
        }

        public static void Logout(object sender, EventArgs e)
        {

        }
    }
}
