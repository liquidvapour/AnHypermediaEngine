using System.Linq;
using System.Text.RegularExpressions;

namespace Siren.Extensions
{
    public static class StringExtensions
    {
        public static string FormatAsName(this string @string)
        {
            return @string.First().ToString().ToLower() + string.Join("", @string.Skip(1));
        }

        public static string FormatAsTitle(this string @string)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return r.Replace(@string, " ");
        }
    }
}