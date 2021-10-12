// ReSharper disable IdentifierTypo

using System;

namespace Cryptography.Ciphers.Vigenere
{
    public class VigenereCipher : Cipher
    {
        private readonly string _password;

        public VigenereCipher(string alphabet, string password) : base(alphabet)
        {
            _password = password;
        }

        protected override char CharEncryption(char ch)
        {
            throw new NotImplementedException();
        }

        protected override char CharDecryption(char ch)
        {
            throw new NotImplementedException();
        }
    }
}