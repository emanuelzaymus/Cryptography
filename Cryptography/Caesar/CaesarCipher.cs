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

        public string Alphabet => _alphabetShifting.Alphabet;

        public CaesarCipher(string alphabet, int shift)
        {
            _alphabetShifting = new AlphabetShifting(alphabet);
            _shift = Utils.PositiveModulo(shift, alphabet.Length);
        }

        public string Encrypt(string text)
        {
            return _alphabetShifting.ShiftEveryChar(text, _shift);
        }

        public string Decrypt(string text)
        {
            return _alphabetShifting.ShiftEveryChar(text, -_shift);
        }
    }
}