using System;

namespace Cryptography.Utilities
{
    public static class Utils
    {
        public static int PositiveModulo(int a, int b)
        {
            int modulo = a % Math.Abs(b);

            if (modulo < 0)
            {
                return Math.Abs(b) + modulo;
            }

            return modulo;
        }
    }
}