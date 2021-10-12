using Cryptography.Utilities;

namespace Cryptography.Ciphers.Caesar
{
    /// <summary>
    /// Mono-alphabetic Caesar cipher.
    /// </summary>
    public class CaesarCipher : Cipher
    {
        private readonly int _shift;

        public CaesarCipher(string alphabet, int shift = 3) : base(alphabet)
        {
            _shift = Utils.PositiveModulo(shift, alphabet.Length);
        }

        protected override char CharEncryption(char ch, int stringCharIndex)
        {
            return CaesarCipherUtils.ShiftChar(ch, _shift, Alphabet);
        }

        protected override char CharDecryption(char ch, int stringCharIndex)
        {
            return CaesarCipherUtils.ShiftChar(ch, -_shift, Alphabet);
        }
    }
}