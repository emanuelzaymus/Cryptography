using Cryptography.Abstraction;
using Cryptography.Utilities;

namespace Cryptography.Caesar
{
    /// <summary>
    /// Mono-alphabetic Caesar cipher.
    /// </summary>
    public class CaesarCipher : ICipher
    {
        private readonly AlphabetShifting _alphabetShifting;

        private readonly int _shift;

        public CaesarCipher(string alphabet, int shift)
        {
            _alphabetShifting = new AlphabetShifting(alphabet);
            _shift = Utils.PositiveModulo(shift, alphabet.Length);
        }

        public string Encrypt(string plainText)
        {
            return _alphabetShifting.ShiftEveryChar(plainText, _shift);
        }

        public string Decrypt(string encryptedText)
        {
            return _alphabetShifting.ShiftEveryChar(encryptedText, -_shift);
        }
    }
}