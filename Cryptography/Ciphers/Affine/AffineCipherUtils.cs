using System.Linq;
using Cryptography.Ciphers.Common;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    internal static class AffineCipherUtils
    {
        internal static void CheckKey1(int alphabetLength, int key1)
        {
            if (key1 == 1) return;

            int positiveModulo = Utils.PositiveModulo(key1, alphabetLength);

            var divisors = Utils.GetDivisorsWithout1(alphabetLength);

            if (divisors.Any(d => positiveModulo % d == 0))
            {
                throw new InvalidKeyException(
                    $"Value {key1} cannot be a {nameof(key1)} because the cipher would not be consistent.",
                    nameof(key1));
            }
        }
    }
}