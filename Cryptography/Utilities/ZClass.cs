using System;
using System.Numerics;

namespace Cryptography.Utilities
{
    public class ZClass
    {
        private int Z { get; }

        public ZClass(int z)
        {
            Z = z;
        }

        public int Modulo(int a)
        {
            return Utils.PositiveModulo(a, Z);
        }

        public int Opposite(int a)
        {
            CheckElementInBounds(a);

            return Z - a;
        }

        public int? Inverse(int a)
        {
            CheckElementInBounds(a);

            for (int i = 1; i < Z; i++)
            {
                if (Utils.PositiveModulo(a * i, Z) == 1)
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Calculates inverse element to number <param name="b"></param> using Extended Euclidean Algorithm.
        /// </summary>
        public int? InverseByEea(int b)
        {
            CheckElementInBounds(b);

            if (b == 0)
            {
                return null;
            }

            int a = Z;

            // Calculate first q
            int q = (int) (a / (double) b);

            // Set first values t_a and t_b
            int ta = 0;
            int tb = 1;

            while (true)
            {
                int tempB = b;
                b = a % b;
                a = tempB;

                if (b <= 0)
                {
                    if (a == 1)
                    {
                        break;
                    }

                    return null;
                }

                int tempTb = tb;
                tb = (ta - tb * q) % Z;
                ta = tempTb;

                q = (int) (a / (double) b);
            }

            return tb;
        }

        private void CheckElementInBounds(int a)
        {
            if (a < 0 || a >= Z)
            {
                throw new ArgumentOutOfRangeException(nameof(a), $"Element must be between 0 and {Z}.");
            }
        }

        public static BigInteger? InverseByEea(BigInteger number, BigInteger modulo)
        {
            if (number < 0 || number >= modulo)
            {
                throw new ArgumentOutOfRangeException(nameof(number), $"Element must be between 0 and {modulo}.");
            }

            if (number == 0)
            {
                return null;
            }

            var a = modulo;
            var b = number;

            // Calculate first q
            var q = a / b;

            // Set first values t_a and t_b
            BigInteger ta = 0;
            BigInteger tb = 1;

            while (true)
            {
                var tempB = b;
                b = a % b;
                a = tempB;

                if (b <= 0)
                {
                    if (a == 1)
                    {
                        break;
                    }

                    return null;
                }

                var tempTb = tb;
                tb = Utils.PositiveModulo(ta - tb * q, modulo);
                ta = tempTb;

                q = a / b;
            }

            return tb;
        }
    }
}