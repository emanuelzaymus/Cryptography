using System;

namespace Cryptography.Utilities
{
    public class AlphabetShifting
    {
        public string Alphabet { get; }

        public AlphabetShifting(string alphabet)
        {
            Alphabet = !string.IsNullOrEmpty(alphabet)
                ? alphabet
                : throw new ArgumentException("Value cannot be null or empty.", nameof(alphabet));
        }

        public string ShiftEveryChar(string text, int shift)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            return text.Transform(ch => ShiftChar(ch, shift));
        }

        private char ShiftChar(char ch, int shift)
        {
            int charIndex = Alphabet.GetCharIndex(ch);
            int newCharIndex = ShiftIndex(charIndex, shift);
            return Alphabet[newCharIndex];
        }

        private int ShiftIndex(int index, int shift)
        {
            return Utils.PositiveModulo(index + shift, Alphabet.Length);
        }
    }
}