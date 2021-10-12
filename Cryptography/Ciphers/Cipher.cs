using System;
using Cryptography.Alphabet;
using Cryptography.Utilities;

namespace Cryptography.Ciphers
{
    public abstract class Cipher : ICipher
    {
        protected string Alphabet { get; }

        protected Cipher(string alphabet)
        {
            Alphabets.CheckAlphabet(alphabet);

            Alphabet = alphabet;
        }

        public string Encrypt(string plainText) => TransformEveryChar(plainText, CharEncryption);

        public string Decrypt(string encryptedText) => TransformEveryChar(encryptedText, CharDecryption);

        protected abstract char CharEncryption(char ch);

        protected abstract char CharDecryption(char ch);

        protected int GetCharIndex(char ch) => Alphabet.GetCharIndex(ch);

        private string TransformEveryChar(string text, Func<char, char> transformation)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            return text.Transform(transformation);
        }
    }
}