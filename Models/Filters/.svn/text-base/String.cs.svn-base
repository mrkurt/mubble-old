using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mubble.Models.Filters
{
    public static class String
    {
        public static string UrlFriendly(string input)
        {
            return UrlFriendly(input, true);
        }
        public static string UrlFriendly(string input, bool allowPeriods)
        {
            if (input == null) return input;
            string temp = input.Normalize(NormalizationForm.FormKD).Trim();
            string copy = "";
            bool lastIsDigit = false;
            for (int i = 0; i < temp.Length; i++)
            {
                if (char.IsLetterOrDigit(temp[i]))
                    copy += temp[i];
                else if (temp[i] == '-' || temp[i] == '_' || (temp[i] == '.' && allowPeriods))
                    copy += temp[i];
                //else if ((temp[i] == ' ' || char.IsPunctuation(temp[i])) && lastIsDigit && temp.Length > i - 1)
                //   copy += ".";
                else if ((char.IsSeparator(temp[i]) || char.IsWhiteSpace(temp[i]) || (temp[i] == '.' && !allowPeriods)) && lastIsDigit && i > 0 && temp.Length > i - 1)
                    copy += "-";

                lastIsDigit = i > 0 && copy.Length > 0 && char.IsLetterOrDigit(copy[copy.Length - 1]);
            }
            while(copy.Length > 0 && !char.IsLetterOrDigit(copy[copy.Length - 1]))
            {
                copy = copy.Substring(0, copy.Length - 1);
            }

            return copy;
        }

        public static string Normalize(string input)
        {
            if (input == null) return input;
            return UrlFriendly(input).ToLower().Replace('-', ' ');
        }

        public static string ToCamelCase(string s)
        {
            return ToMixedCase(s, false);
        }

        public static string ToPascalCase(string s)
        {
            return ToMixedCase(s, true);
        }

        public static string ToMixedCase(string s, bool firstLetterCapital)
        {
            string translated = "";

            bool lastLower = false;

            bool lastWasCapital = false;

            bool lastWasSeparator = false;

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                if (i == 0 && !firstLetterCapital)
                {
                    c = char.ToLower(c);
                }
                else if (lastWasCapital && lastLower && char.IsUpper(c))
                {
                    c = char.ToLower(c);
                }
                else if (
                    (lastWasCapital && char.IsUpper(c)) ||
                    (!lastLower && char.IsUpper(c)) ||
                    (i == 0 && firstLetterCapital) ||
                    (lastWasSeparator)
                    )
                {
                    c = char.ToUpper(c);
                }
                else if (
                    (lastLower && char.IsLower(c)) ||
                    (lastLower && lastWasCapital) ||
                    (char.IsLower(c) && i > 0)
                )
                {
                    c = char.ToLower(c);
                }

                if (!char.IsSeparator(c))
                {
                    translated += c;
                }
                lastLower = (char.IsLower(s[i]) || char.IsNumber(s[i]) || char.IsSeparator(s[i]));

                lastLower = (i == 0 && translated.Length > 0) ? true : lastLower;

                lastWasCapital = char.IsUpper(s[i]);

                lastWasSeparator = char.IsSeparator(s[i]);
            }
            return translated;
        }

        public static string GetPathFromGuid(Guid id)
        {
            string guid = Regex.Replace(id.ToString().ToLower(), @"[\{\-]", "");
            string fileName = "";
            for (int i = 0; i < guid.Length; i = i + 4)
            {
                if (i < guid.Length - 13)
                {
                    fileName += @"\" + guid.Substring(i, 4);
                }
                else
                {
                    fileName += @"\" + guid.Substring(i) + ".txt";
                    break;
                }
            }
            return fileName;
        }

        public static string Format(string format, Dictionary<string, string> values)
        {
            foreach (string key in values.Keys)
            {
                format = format.Replace("{" + key + "}", values[key]);
            }
            return format;
        }
    }
}
