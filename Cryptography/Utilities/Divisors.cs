using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Cryptography.Extensions;

namespace Cryptography.Utilities
{
    public static class Divisors
    {
        private static readonly BigInteger BigIntegerTwo = new(2);
        private static readonly BigInteger BigIntegerThree = new(3);
        private static readonly BigInteger BigIntegerFour = new(4);

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

            var splitsData = SplitRange(startWith, upperBound, maxDegreeOfParallelism);

            var divisorsConcurrentBag = new ConcurrentBag<BigInteger>();

            Parallel.ForEach(
                splitsData,
                // Do not use CancellationToken - It slows down the performance significantly.
                new ParallelOptions {MaxDegreeOfParallelism = maxDegreeOfParallelism},
                splitData =>
                    FindAnyDivisorAndAddToBag(splitData.FromInclusive, splitData.ToExclusive, n, divisorsConcurrentBag)
            );

            return divisorsConcurrentBag.Any() ? divisorsConcurrentBag.First() : null;
        }

        private static void FindAnyDivisorAndAddToBag(BigInteger fromInclusive, BigInteger toExclusive, BigInteger n,
            ConcurrentBag<BigInteger> divisorsConcurrentBag)
        {
            // If fromInclusive is 2, try whether it is a divisor.
            if (fromInclusive == 2 && n % BigIntegerTwo == 0)
            {
                divisorsConcurrentBag.Add(BigIntegerTwo);
                return;
            }

            // Else make from fromInclusive AN ODD number.
            if (fromInclusive % BigIntegerTwo == 0)
            {
                fromInclusive++;
            }

            // If fromInclusive is 3, try whether it is a divisor.
            if (fromInclusive == 3 && n % BigIntegerThree == 0)
            {
                divisorsConcurrentBag.Add(BigIntegerThree);
                return;
            }

            // Else make from fonInclusive AN ODD number which is NOT MULTIPLE OF 3.
            if (fromInclusive % BigIntegerThree == 0)
            {
                fromInclusive += BigIntegerTwo;
            }

            // If predecessor od fromInclusive is multiple of 3, try whether it is a divisor and add 4.
            if ((fromInclusive - 1) % BigIntegerThree == 0)
            {
                if (n % fromInclusive == 0)
                {
                    divisorsConcurrentBag.Add(fromInclusive);
                    return;
                }

                fromInclusive += BigIntegerFour;
            }

            int counter = 1;
            bool isTwo = false; // isTwo is inverted in the first loop and added in the second loop for the first time.

            // This for-loop needs an odd initial value.
            for (BigInteger i = fromInclusive; i < toExclusive; i += (isTwo ? BigIntegerTwo : BigIntegerFour))
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

                isTwo = !isTwo;
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

        /// <summary>
        /// Creates <paramref name="numberOfSplits"/> splits from range from <c>0</c> to <paramref name="rangeTotalSize"/>. 
        /// </summary>
        public static List<(int FromInclusive, int ToExclusive)> SplitRange(int rangeTotalSize, int numberOfSplits)
        {
            List<(int FromInclusive, int ToExclusive)> splits = new(numberOfSplits);

            double splitSize = rangeTotalSize / (double) numberOfSplits;

            splits.Add(new(0, (int) splitSize));

            for (int i = 1; i < numberOfSplits; i++)
            {
                var splitBefore = splits[i - 1];

                var start = splitBefore.ToExclusive;
                var end = (int) Math.Round(splitSize * (i + 1), MidpointRounding.AwayFromZero);
                splits.Add(new(start, end));
            }

            return splits;
        }
    }
}