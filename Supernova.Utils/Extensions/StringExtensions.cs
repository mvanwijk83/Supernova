using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Supernova.Utils.Extensions
{
    /// <summary>
    /// Contains extension methods operating on string values.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates a string to the specified number of characters, with an optional ellipsis appended.
        /// </summary>
        /// <param name="s">The current System.String instance.</param>
        /// <param name="numChars">The number of characters to truncate the System.String to.</param>
        /// <param name="ellipsis">Indicates whether an ellipsis should be appended to the truncated string.</param>
        public static string Truncate(this string s, int numChars, bool ellipsis = false)
        {
            if (s.Length > numChars)
            {
                string truncated = s.Substring(0, numChars);
                if (ellipsis) truncated += "...";
                return truncated;
            }
            else
            {
                return s;
            }
        }

        /// <summary>
        /// Returns the current string with the leading character capitalized.
        /// </summary>
        /// <param name="s">The current System.String instance.</param>
        public static string Capitalize(this string s)
        {
            if (s.Length > 1)
            {
                return Char.ToUpper(s[0]) + s.Substring(1, s.Length - 1);
            }
            else if (s.Length == 1)
            {
                return Char.ToUpper(s[0]).ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Determines whether the string instance starts with either one of the supplied string values.
        /// </summary>
        /// <param name="s">The current System.String instance.</param>
        /// <param name="values">A parameter array containing values to test the current instance against.</param>
        /// <returns>A boolean indicating whether the input string starts with one or more of the supplied values.</returns>
        public static bool StartsWithAny(this string s, params string[] values)
        {
            return StringExtensions.StartsWithAny(s, values);
        }

        /// <summary>
        /// Determines whether the string instance starts with either one of the supplied string values.
        /// </summary>
        /// <param name="s">The current System.String instance.</param>
        /// <param name="values">An enumerable collection containing values to test the current instance against.</param>
        /// <returns>A boolean indicating whether the input string starts with one or more of the supplied values.</returns>
        public static bool StartsWithAny(this string s, IEnumerable<string> values)
        {
            foreach (string value in values)
            {
                if (s.StartsWith(value)) return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the string instance contains either one of the supplied string values.
        /// </summary>
        /// <param name="s">The current System.String instance.</param>
        /// <param name="values">A parameter array containing values to test the current instance against.</param>
        /// <returns>A boolean indicating whether the input string contains one or more of the supplied values.</returns>
        public static bool ContainsAny(this string s, params string[] values)
        {
            return StringExtensions.ContainsAny(s, values);
        }

        /// <summary>
        /// Determines whether the string instance contains either one of the supplied string values.
        /// </summary>
        /// <param name="s">The current System.String instance.</param>
        /// <param name="values">An enumerable collection containing values to test the current instance against.</param>
        /// <returns>A boolean indicating whether the input string contains one or more of the supplied values.</returns>
        public static bool ContainsAny(this string s, IEnumerable<string> values)
        {
            foreach (string value in values)
            {
                if (s.Contains(value)) return true;
            }
            return false;
        }

    }
}
