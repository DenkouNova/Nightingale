using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Nightingale
{
    public class FeatherStrings
    {
        public static string ReplaceWhitespaceSpecialCharacters(string s)
        {
            return s
                .Replace("\r", @"{\r}")
                .Replace("\n", @"{\n}")
                .Replace("\t", @"{\t}");
        }

        public static string GetTextBetween(string baseText, string text1, string text2)
        {
            string returnText = null;
            if (baseText.IndexOf(text1) != -1)
            {
                var newBaseText = baseText.Substring(baseText.IndexOf(text1) + text1.Length);
                if (newBaseText.IndexOf(text2) != -1)
                    returnText = newBaseText.Substring(0, newBaseText.IndexOf(text2));
            }

            return returnText;
        }

        public static string GetTextBetweenAndUpdateText(ref string baseText, string text1, string text2)
        {
            string returnText = null;
            if (baseText.IndexOf(text1) != -1)
            {
                var newBaseText = baseText.Substring(baseText.IndexOf(text1) + text1.Length);
                if (newBaseText.IndexOf(text2) != -1)
                {
                    returnText = newBaseText.Substring(0, newBaseText.IndexOf(text2));
                    baseText = newBaseText;
                }
            }
            return returnText;
        }

        public static string TraceString(int i)
        {
            return i.ToString();
        }

        public static string TraceString(int? i)
        {
            return (i == null ? "(null)" : i.ToString());
        }

        public static string TraceString(float f)
        {
            return f.ToString();
        }

        public static string TraceString(float? f)
        {
            return (f == null ? "(null)" : f.ToString());
        }

        public static string TraceString(string s)
        {
            return (s == null ? "(null)" : "'" + s + "'");
        }
    }
}
