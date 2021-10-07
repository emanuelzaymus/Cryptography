using System;
using Cryptography.Abstraction;

namespace Cryptography.Affine
{
    public class AffineCalculationCipher : ICipher
    {
        private readonly string _alphabet;

        private readonly int _key1;
        private readonly int _key2;

        private readonly int _decryptKey1;
        private readonly int _decryptKey2;

        public AffineCalculationCipher(string alphabet, int key1, int key2)
        {
            _alphabet = !string.IsNullOrEmpty(alphabet)
                ? alphabet
                : throw new ArgumentException("Value cannot be null or empty.", nameof(alphabet));

            AffineSubstitutionCipher.CheckKey1(alphabet.Length, key1);

            _key1 = key1;
            _key2 = key2;
        }

        public string Encrypt(string plainText)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string encryptedText)
        {
            throw new NotImplementedException();
        }
    }
}