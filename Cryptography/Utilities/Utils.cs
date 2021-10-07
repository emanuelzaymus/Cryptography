using System;
using System.Collections.Generic;

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
    }
}