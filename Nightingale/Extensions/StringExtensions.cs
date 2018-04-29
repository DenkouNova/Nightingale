namespace Nightingale.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceWhitespaceSpecialCharacters(this string s)
        {
            return s
                .Replace("\r", @"{\r}")
                .Replace("\n", @"{\n}")
                .Replace("\t", @"{\t}");
        }
    }
}
