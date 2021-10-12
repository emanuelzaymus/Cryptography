using System;
using System.Collections.Generic;
using System.Linq;

namespace Cryptography.Utilities
{
    public static class Utils
    {
        public static int PositiveModulo(int a, int b)
        {
            if (b <= 0)
                throw new ArgumentException("B cannot be 0 or negative.", nameof(b));

            int modulo = a % b;

            // If modulo is negative than calculate opposite number.
            if (modulo < 0)
            {
                return b + modulo;
            }

            return modulo;
        }

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
    }
}