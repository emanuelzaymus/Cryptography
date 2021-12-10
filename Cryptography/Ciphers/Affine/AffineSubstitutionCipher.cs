using Cryptography.Alphabet;
using Cryptography.Ciphers.MonoAlphabeticSubstitution;
using Cryptography.Extensions;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    /// <summary>
    /// Affine cipher based on substitution principle.
    /// </summary>
    public class AffineSubstitutionCipher : MonoAlphabeticSubstitutionCipher
    {
        public AffineSubstitutionCipher(string alphabet, int key1, int key2)
            : base(alphabet, CreateAffineSubstitutionAlphabet(alphabet, key1, key2))
        {
        }

        private static string CreateAffineSubstitutionAlphabet(string alphabet, int key1, int key2)
        {
            Alphabets.CheckAlphabet(alphabet);
            AffineCipherUtils.CheckKey1(alphabet.Length, key1);

            return alphabet.Transform((_, stringCharIndex) =>
            {
                int newCharIndex = Utils.PositiveModulo(stringCharIndex * key1 + key2, alphabet.Length);
                return alphabet[newCharIndex];
            });
        }
    }
}