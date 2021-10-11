using Cryptography.Utilities;

namespace Cryptography.Ciphers.Caesar
{
    /// <summary>
    /// Mono-alphabetic Caesar cipher.
    /// </summary>
    public class CaesarCipher : Cipher
    {
        private readonly int _shift;

        public CaesarCipher(string alphabet, int shift) : base(alphabet)
        {
            _shift = Utils.PositiveModulo(shift, alphabet.Length);
        }

        protected override char CharEncryption(char ch) => ShiftChar(ch, _shift);

        protected override char CharDecryption(char ch) => ShiftChar(ch, -_shift);

        private char ShiftChar(char ch, int shift)
        {
            int charIndex = GetCharIndex(ch);
            int newCharIndex = ShiftIndex(charIndex, shift);
            return Alphabet[newCharIndex];
        }

        private int ShiftIndex(int index, int shift)
        {
            return Utils.PositiveModulo(index + shift, Alphabet.Length);
        }
    }
}