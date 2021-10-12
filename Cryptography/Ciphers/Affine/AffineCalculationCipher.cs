using System;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    /// <summary>
    /// Affine cipher based on calculation principle.
    /// </summary>
    public class AffineCalculationCipher : Cipher
    {
        private ZClass Z { get; }

        private readonly int _key1;
        private readonly int _key2;

        private readonly int _decryptKey1;
        private readonly int _decryptKey2;

        public AffineCalculationCipher(string alphabet, int key1, int key2) : base(alphabet)
        {
            AffineCipherUtils.CheckKey1(alphabet.Length, key1);

            Z = new ZClass(alphabet.Length);

            _key1 = Z.Modulo(key1);
            _key2 = Z.Modulo(key2);

            _decryptKey1 = Z.Inverse(_key1) ?? throw
                new ArgumentException("For key1 does not exist inverse element. Choose different key1.", nameof(_key1));
            _decryptKey2 = Z.Modulo(Z.Opposite(_key2) * _decryptKey1);
        }

        protected override char CharEncryption(char ch, int stringCharIndex)
        {
            int alphabetCharIndex = GetAlphabetCharIndex(ch);
            int newCharIndex = Z.Modulo(alphabetCharIndex * _key1 + _key2);
            return Alphabet[newCharIndex];
        }

        protected override char CharDecryption(char ch, int stringCharIndex)
        {
            int alphabetCharIndex = GetAlphabetCharIndex(ch);
            int newCharIndex = Z.Modulo(alphabetCharIndex * _decryptKey1 + _decryptKey2);
            return Alphabet[newCharIndex];
        }
    }
}