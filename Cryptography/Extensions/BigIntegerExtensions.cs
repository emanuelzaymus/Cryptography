using System;
using System.Numerics;

namespace Cryptography.Extensions
{
    public static class BigIntegerExtensions
    {
        /// <summary>
        /// Square-roots this number and floors it down.
        /// Source: https://stackoverflow.com/questions/3432412/calculate-square-root-of-a-biginteger-system-numerics-biginteger/6084813
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Square root of this number</returns>
        /// <exception cref="ArithmeticException">When number is negative</exception>
        public static BigInteger Sqrt(this BigInteger number)
        {
            if (number == 0)
            {
                return 0;
            }

            if (number < 0)
            {
                throw new ArithmeticException("NaN");
            }

            int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(number, 2)));
            var root = BigInteger.One << (bitLength / 2);

            while (!IsSqrt(number, root))
            {
                root += number / root;
                root /= 2;
            }

            return root;
        }

        private static bool IsSqrt(BigInteger number, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (number >= lowerBound && number < upperBound);
        }
    }
}