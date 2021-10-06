using System;
using System.Text;

namespace Cryptography
{
    /// <summary>
    /// Monoalphabetic Caesar cipher.
    /// </summary>
    public class CaesarCipher : ICipher
    {
        private readonly string _alphabet;

        private readonly int _shift;

        public CaesarCipher(string alphabet, int shift)
        {
            _alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet));

            _shift = (shift >= 0)
                ? Modulo(shift)
                : throw new ArgumentException(nameof(shift), "Shift cannot be negative.");
        }

        public string Encrypt(string text) => ShiftEveryChar(text, _shift);

        public string Decrypt(string text) => ShiftEveryChar(text, _alphabet.Length - _shift);

        private string ShiftEveryChar(string text, int shift)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            StringBuilder builder = new(text.Length);

            foreach (var ch in text)
            {
                char shiftedChar = ShiftChar(ch, shift);
                builder.Append(shiftedChar);
            }

            return builder.ToString();
        }

        private char ShiftChar(char ch, int shift)
        {
            int charIndex = GetCharIndex(ch);
            int newCharIndex = ShiftIndex(charIndex, shift);
            return _alphabet[newCharIndex];
        }

        private int GetCharIndex(char ch)
        {
            int index = _alphabet.IndexOf(ch);

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ch), $"Cannot find characted '{ch}' in the alphabet. The character is not valid.");
            }

            return index;
        }

        private int ShiftIndex(int index, int shift) => Modulo(index + shift);

        private int Modulo(int shift) => shift % _alphabet.Length;

    }
}
