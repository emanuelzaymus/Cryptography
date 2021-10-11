using System;
using System.Linq;
using System.Text;
using Cryptography.Ciphers.Common;
using Cryptography.Ciphers.MonoAlphabeticSubstitution;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    public class AffineSubstitutionCipher : MonoAlphabeticSubstitutionCipher
    {
        public AffineSubstitutionCipher(string alphabet, int key1, int key2)
            : base(alphabet, CreateAffineSubstitutionAlphabet(alphabet, key1, key2))
        {
        }

        private static string CreateAffineSubstitutionAlphabet(string alphabet, int key1, int key2)
        {
            if (string.IsNullOrEmpty(alphabet))
                throw new ArgumentException("Value cannot be null or empty.", nameof(alphabet));

            CheckKey1(alphabet.Length, key1);

            var alphabetBuilder = new StringBuilder(alphabet.Length);

            for (var i = 0; i < alphabet.Length; i++)
            {
                int newCharIndex = Utils.PositiveModulo(i * key1 + key2, alphabet.Length);
                char newChar = alphabet[newCharIndex];
                alphabetBuilder.Append(newChar);
            }

            return alphabetBuilder.ToString();
        }

        // TODO: maybe put into a predecessor AffineCipher.cs
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