using System;
using System.Text;

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
            if (text is null) throw new ArgumentNullException(nameof(text));

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
            return Alphabet[newCharIndex];
        }

        private int GetCharIndex(char ch)
        {
            int index = Alphabet.IndexOf(ch);

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ch),
                    $"Cannot find character '{ch}' in the alphabet. The character is not valid.");
            }

            return index;
        }

        private int ShiftIndex(int index, int shift)
        {
            return Utils.PositiveModulo(index + shift, Alphabet.Length);
        }
    }
}