using System;
using System.Text;

namespace Cryptography.Ciphers
{
    public abstract class Cipher : ICipher
    {
        protected string Alphabet { get; }

        protected Cipher(string alphabet)
        {
            Alphabet = !string.IsNullOrEmpty(alphabet)
                ? alphabet
                : throw new ArgumentException("Value cannot be null or empty.", nameof(alphabet));
        }

        public string Encrypt(string plainText) => TransformEveryChar(plainText, CharEncryption);

        public string Decrypt(string encryptedText) => TransformEveryChar(encryptedText, CharDecryption);

        protected abstract char CharEncryption(char ch);

        protected abstract char CharDecryption(char ch);

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

        private string TransformEveryChar(string text, Func<char, char> transformation)
        {
            if (text is null) throw new ArgumentNullException(nameof(text));

            var stringBuilder = new StringBuilder(text.Length);

            foreach (char ch in text)
            {
                char newChar = transformation(ch);
                stringBuilder.Append(newChar);
            }

            return stringBuilder.ToString();
        }
    }
}