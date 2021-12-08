using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Cryptography.Utilities
{
    public static class Utils
    {
        public static int PositiveModulo(int a, int b)
        {
            if (b <= 0)
                throw new ArgumentException("B cannot be 0 or negative.", nameof(b));

            var modulo = a % b;

            // If modulo is negative than calculate opposite number.
            if (modulo < 0)
            {
                return b + modulo;
            }

            return modulo;
        }

        public static BigInteger PositiveModulo(BigInteger a, BigInteger b)
        {
            if (b <= 0)
                throw new ArgumentException("B cannot be 0 or negative.", nameof(b));

            var modulo = a % b;

            // If modulo is negative than calculate opposite number.
            if (modulo < 0)
            {
                return b + modulo;
            }

            return modulo;
        }

        /// <summary>
        /// Creates permutation series. Every time returns only the same instance of <c>List</c> object!
        /// </summary>
        public static IEnumerable<List<int>> GeneratePermutationSeries(int seriesLength, int useNumberCount)
        {
            var oneSeries = Enumerable.Repeat(0, seriesLength).ToList();

            yield return oneSeries;

            for (int i = 0; i < Math.Pow(useNumberCount, seriesLength) - 1; i++)
            {
                for (int j = 0; j < seriesLength; j++)
                {
                    if (oneSeries[j] < useNumberCount - 1)
                    {
                        oneSeries[j]++;
                        break;
                    }

                    oneSeries[j] = 0;
                }

                yield return oneSeries;
            }
        }

        /// <summary>
        /// Effective power with modulus calculation: x^d mod n.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static decimal Power(decimal x, decimal d, decimal n)
        {
            var dBits = decimal.GetBits(d);
            var bitArray = new BitArray(dBits);

            decimal help = x;
            decimal y = 1;

            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    y = y * help % n;
                }

                help = help * help % n;
            }

            return y;
        }

        /// <summary>
        /// Formats <paramref name="stringToFormat"/> by <paramref name="formatTemplate"/>. Creates new string from
        /// <paramref name="stringToFormat"/> with formatting characters from <paramref name="formatTemplate"/>.
        /// Formatting characters are all characters which are not in <paramref name="alphabet"/>.
        /// </summary>
        /// <param name="stringToFormat">String to format - <c>decrypted text</c></param>
        /// <param name="formatTemplate">Original string - <c>encrypted text</c></param>
        /// <param name="alphabet">Valid characters</param>
        /// <exception cref="Exception">Throws <c>Exception</c> when <paramref name="stringToFormat"/> does not fit into
        /// <paramref name="formatTemplate"/></exception>
        public static string FormatString(string stringToFormat, string formatTemplate, string alphabet)
        {
            StringBuilder builder = new(formatTemplate.Length);
            using var enumerator = stringToFormat.GetEnumerator();

            foreach (char ch in formatTemplate)
            {
                if (alphabet.Contains(ch))
                {
                    if (enumerator.MoveNext())
                    {
                        builder.Append(enumerator.Current);
                    }
                    else
                    {
                        throw new Exception("You should not get here.");
                    }
                }
                else
                {
                    builder.Append(ch);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Calculates (p - 1) * (q - 1). 
        /// </summary>
        public static BigInteger CalculateEulerPhiFunction(BigInteger p, BigInteger q)
        {
            return (p - 1) * (q - 1);
        }
    }
}