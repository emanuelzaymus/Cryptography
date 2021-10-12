using System;
using System.Text;

namespace Cryptography.Utilities
{
    public static class StringExtensions
    {
        public static string Transform(this string str, Func<char, char> transformation)
        {
            if (transformation is null) throw new ArgumentNullException(nameof(transformation));

            var stringBuilder = new StringBuilder(str.Length);

            foreach (char ch in str)
            {
                char newChar = transformation(ch);
                stringBuilder.Append(newChar);
            }

            return stringBuilder.ToString();
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