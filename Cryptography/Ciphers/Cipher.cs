using System;
using Cryptography.Alphabet;
using Cryptography.Extensions;

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

        public virtual string Encrypt(string plainText) => TransformEveryChar(plainText, CharEncryption);

        public virtual string Decrypt(string encryptedText) => TransformEveryChar(encryptedText, CharDecryption);

        protected abstract char CharEncryption(char ch, int stringCharIndex);

        protected abstract char CharDecryption(char ch, int stringCharIndex);

        protected int GetAlphabetCharIndex(char ch) => Alphabet.GetCharIndex(ch);

        private string TransformEveryChar(string text, Func<char, int, char> transformation)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            return text.Transform(transformation);
        }
    }
}