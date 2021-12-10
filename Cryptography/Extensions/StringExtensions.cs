using System;
using System.Text;

namespace Cryptography.Extensions
{
    public static class StringExtensions
    {
        private static readonly StringBuilder StringBuilder = new();

        /// <summary>
        /// Transforms every character of this string using <paramref name="transformation"/> function.
        /// </summary>
        /// <param name="str">This string</param>
        /// <param name="transformation">Transformation function which accepts character itself and its position (<c>stringCharIndex</c>) in the original string</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException is thrown when <paramref name="transformation"/> is null</exception>
        public static string Transform(this string str, Func<char, int, char> transformation)
        {
            if (transformation is null) throw new ArgumentNullException(nameof(transformation));

            StringBuilder.Clear();
            StringBuilder.Capacity = str.Length;

            for (int stringCharIndex = 0; stringCharIndex < str.Length; stringCharIndex++)
            {
                char ch = str[stringCharIndex];
                char newChar = transformation(ch, stringCharIndex);

                StringBuilder.Append(newChar);
            }

            return StringBuilder.ToString();
        }

        /// <summary>
        /// Gets index of the first occurence of character <paramref name="ch"/>.
        /// </summary>
        /// <returns>Index of the character</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int GetCharIndex(this string str, char ch)
        {
            int index = str.IndexOf(ch);

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ch),
                    $"Cannot find character '{ch}' in the string. The character is not valid.");
            }

            return index;
        }
    }
}