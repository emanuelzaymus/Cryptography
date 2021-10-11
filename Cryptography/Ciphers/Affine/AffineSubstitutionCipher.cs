using System;
using System.Text;
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

            AffineCipherUtils.CheckKey1(alphabet.Length, key1);

            var alphabetBuilder = new StringBuilder(alphabet.Length);

            for (var i = 0; i < alphabet.Length; i++)
            {
                int newCharIndex = Utils.PositiveModulo(i * key1 + key2, alphabet.Length);
                char newChar = alphabet[newCharIndex];
                alphabetBuilder.Append(newChar);
            }

            return alphabetBuilder.ToString();
        }
    }
}