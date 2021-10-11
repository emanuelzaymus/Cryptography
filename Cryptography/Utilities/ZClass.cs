using System;

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

        public int Inverse(int a)
        {
            CheckElementInBounds(a);

            for (int i = 1; i < Z; i++)
            {
                if (Utils.PositiveModulo(a * i, Z) == 1)
                {
                    return i;
                }
            }

            throw new Exception("You should not get here.");
        }

        private void CheckElementInBounds(int a)
        {
            if (a < 0 || a >= Z)
            {
                throw new ArgumentOutOfRangeException(nameof(a), $"Element must be between 0 and {nameof(Z)}.");
            }
        }
    }
}