using System;
using Cryptography.Utilities;

namespace Cryptography.Ciphers
{
    public abstract class Cipher : ICipher
    {
        protected readonly string Alphabet;

        protected Cipher(string alphabet)
        {
            Alphabet = !string.IsNullOrEmpty(alphabet)
                ? alphabet
                : throw new ArgumentException("Value cannot be null or empty.", nameof(alphabet));
        }

        // protected string ShiftEveryChar(string text, int shift)
        // {
        //     if (text is null) throw new ArgumentNullException(nameof(text));
        //
        //     StringBuilder builder = new(text.Length);
        //
        //     foreach (var ch in text)
        //     {
        //         char shiftedChar = ShiftChar(ch, shift);
        //         builder.Append(shiftedChar);
        //     }
        //
        //     return builder.ToString();
        // }

        protected char ShiftChar(char ch, int shift)
        {
            int charIndex = GetCharIndex(ch);
            int newCharIndex = ShiftIndex(charIndex, shift);
            return Alphabet[newCharIndex];
        }

        protected int GetCharIndex(char ch)
        {
            int index = Alphabet.IndexOf(ch);

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ch),
                    $"Cannot find character '{ch}' in the alphabet. The character is not valid.");
            }

            return index;
        }

        protected int ShiftIndex(int index, int shift)
        {
            return Utils.PositiveModulo(index + shift, Alphabet.Length);
        }

        public abstract string Encrypt(string plainText);

        public abstract string Decrypt(string encryptedText);
    }
}