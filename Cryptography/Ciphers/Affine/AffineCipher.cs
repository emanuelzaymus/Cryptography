using System.Linq;
using Cryptography.Ciphers.Common;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    public abstract class AffineCipher : Cipher
    {
        protected AffineCipher(string alphabet) : base(alphabet)
        {
        }

        internal static void CheckKey1(int alphabetLength, int key1)
        {
            if (key1 == 1) return;

            int positiveModulo = Utils.PositiveModulo(key1, alphabetLength);

            // Skip number 1
            var divisors = Utils.GetDivisors(alphabetLength).Skip(1);

            if (divisors.Any(d => positiveModulo % d == 0))
            {
                throw new InvalidKeyException(
                    $"Value {key1} cannot be a {nameof(key1)} because the cipher would not be consistent.",
                    nameof(key1));
            }
        }
    }
}