using System;
using System.Collections;
using System.Collections.Generic;
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

        /// <summary>
        /// Creates <paramref name="numberOfSplits"/> splits from range from <paramref name="rangeFromInclusive"/> to <paramref name="rangeToExclusive"/>. 
        /// </summary>
        public static List<(BigInteger FromInclusive, BigInteger ToExclusive)> SplitRange(BigInteger rangeFromInclusive,
            BigInteger rangeToExclusive, int numberOfSplits)
        {
            if (rangeFromInclusive >= rangeToExclusive)
            {
                throw new ArgumentException("RangeFromInclusive needs to be smaller than rangeToExclusive");
            }

            List<(BigInteger FromInclusive, BigInteger ToExclusive)> splits = new(numberOfSplits);

            var totalSize = rangeToExclusive - rangeFromInclusive;

            var splitSize = totalSize / numberOfSplits;

            splits.Add(new(rangeFromInclusive, rangeFromInclusive + splitSize));
            for (int i = 1; i < numberOfSplits; i++)
            {
                var splitBefore = splits[i - 1];

                var start = splitBefore.ToExclusive;
                var end = start + splitSize;
                splits.Add(new(start, end));
            }

            return splits;
        }

        /// <summary>
        /// Creates <paramref name="numberOfSplits"/> splits from range from <c>0</c> to <paramref name="rangeTotalSize"/>. 
        /// </summary>
        public static List<(int FromInclusive, int RangeSize)> SplitRange(int rangeTotalSize, int numberOfSplits)
        {
            List<(int FromInclusive, int RangeSize)> splits = new(numberOfSplits);

            double splitSize = rangeTotalSize / (double) numberOfSplits;

            var firstRangeSize = (int) Math.Round(splitSize, MidpointRounding.AwayFromZero);
            splits.Add(new(0, firstRangeSize));

            for (int i = 1; i < numberOfSplits; i++)
            {
                var splitBefore = splits[i - 1];

                var startIndex = splitBefore.FromInclusive + splitBefore.RangeSize;
                var rangeSize = (int) Math.Round(splitSize * (i + 1), MidpointRounding.AwayFromZero) - startIndex;
                splits.Add(new(startIndex, rangeSize));
            }

            return splits;
        }
    }
}