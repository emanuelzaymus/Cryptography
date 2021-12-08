﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Cryptography.Utilities
{
    public static class Divisors
    {
        private static readonly BigInteger BigIntegerTwo = new(2);

        /// <summary>
        /// Returns all divisors of number <paramref name="n"/>.
        /// </summary>
        public static int[] GetDivisors(int n)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException(nameof(n), "Number n has to be positive.");

            var divisors = new List<int>();

            for (int i = 1; i <= n / 2; i++)
            {
                if (n % i == 0)
                {
                    divisors.Add(i);
                }
            }

            divisors.Add(n);

            return divisors.ToArray();
        }

        /// <summary>
        /// Returns divisors of number <paramref name="n"/> without number 1.
        /// </summary>
        public static int[] GetDivisorsWithout1(int n)
        {
            return GetDivisors(n).Skip(1).ToArray();
        }

        // ReSharper disable once UnusedMember.Global
        public static int CalculateGreatestCommonDivisor(int a, int b)
        {
            if (a < 0 || b < 0)
                throw new ArgumentException("Numbers A and B cannot be negative.");

            while (b > 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        /// <summary>
        /// Finds any divisor of a number.
        /// </summary>
        /// <param name="n">Number</param>
        public static BigInteger? FindAnyDivisorParallel(BigInteger n)
        {
            return FindAnyDivisorParallel(n, 2);
        }

        /// <summary>
        /// Finds any divisor of a number.
        /// </summary>
        /// <param name="n">Number</param>
        /// <param name="startWith">Starts finding a divisor from this value</param>
        public static BigInteger? FindAnyDivisorParallel(BigInteger n, BigInteger startWith)
        {
            int maxDegreeOfParallelism = Environment.ProcessorCount; // Count of logical processors
            var upperBound = n.Sqrt() + 1; // I am adding + 1 because SplitRange will exclude it.

            var divisorsConcurrentBag = new ConcurrentBag<BigInteger>();

            var splitsData = SplitRange(startWith, upperBound, maxDegreeOfParallelism)
                .Select(s => new SplitData(s.FromInclusive, s.ToExclusive, n, divisorsConcurrentBag))
                .ToList();

            Parallel.ForEach(
                splitsData,
                new ParallelOptions {MaxDegreeOfParallelism = maxDegreeOfParallelism},
                FindAnyDivisorAndAddToBag
            );

            return divisorsConcurrentBag.Any() ? divisorsConcurrentBag.First() : null;
        }

        private static void FindAnyDivisorAndAddToBag(SplitData splitData)
        {
            var fromInclusive = splitData.FromInclusive;
            var toExclusive = splitData.ToExclusive;
            var n = splitData.Number;
            var divisorsConcurrentBag = splitData.DivisorsConcurrentBag;

            // If fromInclusive is 2, try whether it is a divisor.
            if (fromInclusive == 2 && n % BigIntegerTwo == 0)
            {
                divisorsConcurrentBag.Add(BigIntegerTwo);
                return;
            }

            // Else make from fromInclusive an odd number by adding 1.
            if (fromInclusive % BigIntegerTwo == 0)
            {
                fromInclusive++;
            }

            int counter = 1;
            // This for-loop needs an odd initial value.
            for (BigInteger i = fromInclusive; i < toExclusive; i += BigIntegerTwo)
            {
                if (n % i == 0)
                {
                    divisorsConcurrentBag.Add(i);
                    return;
                }

                if (counter++ >= 1_000_000)
                {
                    if (!divisorsConcurrentBag.IsEmpty)
                    {
                        return;
                    }

                    counter = 1;
                }
            }
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

        private record SplitData(
            BigInteger FromInclusive,
            BigInteger ToExclusive,
            BigInteger Number,
            ConcurrentBag<BigInteger> DivisorsConcurrentBag
        );
    }
}