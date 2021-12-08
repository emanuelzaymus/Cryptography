using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Cryptography.Utilities
{
    public static class Divisors
    {
        /// <summary>
        /// Returns all divisors of number <paramref name="n"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        /// <summary>
        /// Finds any divisor of a number with maximal number of threads.
        /// </summary>
        /// <param name="n">Number</param>
        public static BigInteger FindAnyDivisorParallel(BigInteger n)
        {
            return FindAnyDivisorParallel(n, Environment.ProcessorCount);
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
        /// <param name="maxDegreeOfParallelism">Set number of threads to use</param>
        /// <returns></returns>
        public static BigInteger FindAnyDivisorParallel(BigInteger n, int maxDegreeOfParallelism)
        {
            var stopwatch = Stopwatch.StartNew();
            var number = BigInteger.Parse("56341958081545199783");

            var bi1 = BigInteger.Multiply(938266005, 1);
            var bi2 = BigInteger.Multiply(938266005, 2);
            var bi3 = BigInteger.Multiply(938266005, 3);
            var bi4 = BigInteger.Multiply(938266005, 4);
            var bi5 = BigInteger.Multiply(938266005, 5);
            var bi6 = BigInteger.Multiply(938266005, 6);
            var bi7 = BigInteger.Multiply(938266005, 7);
            var bi8 = BigInteger.Multiply(938266005, 8) + 1;

            List<(BigInteger FromInclusive, BigInteger ToExclusive)> bounds = new()
            {
                (2, bi1),
                (bi1, bi2),
                (bi2, bi3),
                (bi3, bi4),
                (bi4, bi5),
                (bi5, bi6),
                (bi6, bi7),
                (bi7, bi8)
            };

            var primeFactors = new ConcurrentBag<BigInteger>();

            Parallel.ForEach(bounds, new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount},
                b =>
                {
                    var fromInclusive = b.FromInclusive;

                    // If fromInclusive is 2, try whether it is a divisor.
                    if (fromInclusive == 2 && number % 2 == 0)
                    {
                        primeFactors.Add(2);
                        return;
                    }

                    // Else make from fromInclusive an odd number by adding 1.
                    if (fromInclusive % 2 == 0)
                    {
                        fromInclusive++;
                    }

                    int counter = 1;
                    // This for-loop needs an odd initial value.
                    for (BigInteger i = fromInclusive; i < b.ToExclusive; i += 2)
                    {
                        if (number % i == 0)
                        {
                            primeFactors.Add(i);
                            return;
                        }

                        if (counter++ >= 1_000_000)
                        {
                            if (!primeFactors.IsEmpty)
                            {
                                return;
                            }

                            counter = 1;
                        }
                    }
                });

            var foundFactors = primeFactors.ToList();

            stopwatch.Stop();

            foreach (var foundFactor in foundFactors)
            {
                Console.WriteLine(foundFactor);
            }

            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            return primeFactors.First();
        }
    }
}