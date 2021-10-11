using System;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    public class AffineCalculationCipher : AffineCipher
    {
        private readonly int _key1;
        private readonly int _key2;

        private readonly int _decryptKey1;
        private readonly int _decryptKey2;

        public AffineCalculationCipher(string alphabet, int key1, int key2) : base(alphabet)
        {
            CheckKey1(alphabet.Length, key1);

            _key1 = Utils.PositiveModulo(key1, alphabet.Length);
            _key2 = Utils.PositiveModulo(key2, alphabet.Length);

            _decryptKey1 = InverseElement(_key1, alphabet.Length);
            _decryptKey2 =
                Utils.PositiveModulo(OppositeElement(_key2, alphabet.Length) * _decryptKey1, alphabet.Length);
        }

        protected override char CharEncryption(char ch)
        {
            int charIndex = GetCharIndex(ch);
            int newCharIndex = Utils.PositiveModulo(charIndex * _key1 + _key2, Alphabet.Length);
            return Alphabet[newCharIndex];
        }

        protected override char CharDecryption(char ch)
        {
            int charIndex = GetCharIndex(ch);
            int newCharIndex = Utils.PositiveModulo(charIndex * _decryptKey1 + _decryptKey2, Alphabet.Length);
            return Alphabet[newCharIndex];
        }

        // TODO: maybe into Utils??

        private int InverseElement(int element, int alphabetLength)
        {
            if (!IsElementInBounds(element, alphabetLength))
            {
                throw new ArgumentOutOfRangeException(nameof(element),
                    $"Element must be between 0 and {nameof(alphabetLength)}.");
            }

            for (int i = 1; i < alphabetLength; i++)
            {
                if (Utils.PositiveModulo(element * i, alphabetLength) == 1)
                {
                    return i;
                }
            }

            throw new Exception("You should not get here.");
        }

        // TODO: maybe into Utils??
        private int OppositeElement(int element, int alphabetLength)
        {
            if (!IsElementInBounds(element, alphabetLength))
            {
                throw new ArgumentOutOfRangeException(nameof(element),
                    $"Element must be between 0 and {nameof(alphabetLength)}.");
            }

            return alphabetLength - element;
        }

        // TODO: maybe into Utils??
        private bool IsElementInBounds(int element, int alphabetLength)
        {
            return element >= 0 && element < alphabetLength;
        }
    }
}