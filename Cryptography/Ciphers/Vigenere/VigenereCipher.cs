using System;
using System.Linq;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Vigenere
{
    public class VigenereCipher : Cipher
    {
        private readonly string _password;

        public VigenereCipher(string alphabet, string password) : base(alphabet)
        {
            CheckPassword(password);

            _password = password;
        }

        private void CheckPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(password));
            }

            if (password.Any(ch => !Alphabet.Contains(ch)))
            {
                throw new ArgumentException("Password contains invalid character(s).", nameof(password));
            }
        }

        protected override char CharEncryption(char ch, int stringCharIndex)
        {
            return VigenereTransformation(ch, stringCharIndex, false);
        }

        protected override char CharDecryption(char ch, int stringCharIndex)
        {
            return VigenereTransformation(ch, stringCharIndex, true);
        }

        private char VigenereTransformation(char ch, int stringCharIndex, bool isDecryption)
        {
            int alphabetCharIndex = GetAlphabetCharIndex(ch);

            int passwordCharIndex = stringCharIndex % _password.Length;
            char passwordChar = _password[passwordCharIndex];

            int alphabetPasswordCharIndex = GetAlphabetCharIndex(passwordChar);

            if (isDecryption)
            {
                alphabetPasswordCharIndex = -alphabetPasswordCharIndex;
            }

            int newCharIndex = Utils.PositiveModulo(alphabetCharIndex + alphabetPasswordCharIndex, Alphabet.Length);
            return Alphabet[newCharIndex];
        }
    }
}