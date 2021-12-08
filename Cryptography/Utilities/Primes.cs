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
                    for (long j = i * 2; j < upperBound; j += i)
                    {
                        sieve[(int) j] = false;
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

        public static BigInteger? FindFirstPrimeFactor(BigInteger number)
        {
            return FindFirstPrimeFactor(number, 2);
        }

        public static BigInteger? FindFirstPrimeFactor(BigInteger number, BigInteger startWith)
        {
            if (number <= 1)
            {
                throw new ArgumentException($"There are no prime factors for number {number}.", nameof(number));
            }

            if (startWith < 2)
            {
                throw new ArgumentException($"Algorithm can start with minimal value of 2. Not less.",
                    nameof(startWith));
            }

            if (startWith == 2 && number % 2 == 0)
            {
                return 2;
            }

            var upperBound = number / 2;
            for (var i = startWith; i < upperBound; i += 2)
            {
                if (number % i == 0)
                {
                    return i;
                }
            }

            return null;
        }
    }
}