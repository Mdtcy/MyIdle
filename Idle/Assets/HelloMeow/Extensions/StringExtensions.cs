/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-12-10 13:12:14
 * @modify date 2019-12-10 13:12:14
 * @desc [description]
 */

using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HM.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            int index = 0;

            while (index + maxLength < str.Length)
            {
                yield return str.Substring(index, maxLength);
                index += maxLength;
            }

            yield return str.Substring(index);
        }

        /// <summary>
        /// Returns true if this string is null, empty, or contains only whitespace.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns><c>true</c> if this string is null, empty, or contains only whitespace; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrWhitespace(this string str)
        {
          if (!string.IsNullOrEmpty(str))
          {
            for (int index = 0; index < str.Length; ++index)
            {
              if (!char.IsWhiteSpace(str[index]))
                return false;
            }
          }
          return true;
        }

        public static char LastChar(this string self)
        {
            if (self.IsNullOrEmpty()) return '\0';
            return self[self.Length - 1];
        }

        public static bool IsNullOrEmpty(this string @this)
        {
          if (@this != null)
            return !@this.GetEnumerator().MoveNext();
          return true;
        }

		public static string Colored(this string self, Color color)
		{
			return string.Concat("<color=#", ColorUtility.ToHtmlStringRGBA(color), ">", self, "</color>");
		}

        private const string Glyphs= "abcdefghijklmnopqrstuvwxyz0123456789"; //add the characters you want

        public static string RandomString(int length)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(Glyphs[Random.Range(0, Glyphs.Length)]);
            }
            return sb.ToString();
        }
    }

}