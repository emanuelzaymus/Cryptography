using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Cryptography.Utilities
{
    public static class Primes
    {
        /// <summary>
        /// Generates all primes from 2 up to <paramref name="upperBound"/> exclusive using Sieve of Eratosthenes.
        /// </summary>
        /// <param name="upperBound">Upper bound exclusive</param>
        public static IEnumerable<int> GeneratePrimes(int upperBound)
        {
            var bitArray = GetSieveOfEratosthenes(upperBound);

            for (int i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                {
                    yield return i;
                }
            }
        }

        private static BitArray GetSieveOfEratosthenes(int upperBound)
        {
            if (upperBound <= 2)
            {
                throw new ArgumentOutOfRangeException(nameof(upperBound), "Upper bound needs to be greater than 2.");
            }

            BitArray sieve = new(upperBound, true)
            {
                [0] = false,
                [1] = false
            };

            for (int i = 0; i < upperBound; i++)
            {
                if (sieve[i])
                {
                    for (int j = i * 2; j < upperBound; j += i)
                    {
                        sieve[j] = false;
                    }
                }
            }

            return sieve;
        }

        public static IEnumerable<int> Factorize(int number)
        {
            var upperBound = (int) Math.Sqrt(number);
            var primes = GeneratePrimes(upperBound + 1);

            foreach (var prime in primes)
            {
                while (true)
                {
                    if (number % prime == 0)
                    {
                        yield return prime;
                        number /= prime;
                    }
                    else
                    {
                        break;
                    }
                }

                if (number == 1)
                {
                    break;
                }
            }
        }

        public static bool IsPrime(int number)
        {
            var factors = Factorize(number);

            return !factors.Any();
        }

        public static bool IsPrime(BigInteger number)
        {
            var factors = Factorize(int.MaxValue);

            return !factors.Any();
        }
    }
}