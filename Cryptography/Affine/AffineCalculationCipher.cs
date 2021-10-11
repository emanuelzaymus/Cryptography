using System;
using System.Text;
using Cryptography.Abstraction;
using Cryptography.Utilities;

namespace Cryptography.Affine
{
    public class AffineCalculationCipher : Cipher
    {
        private readonly int _key1;
        private readonly int _key2;

        private readonly int _decryptKey1;
        private readonly int _decryptKey2;

        public AffineCalculationCipher(string alphabet, int key1, int key2) : base(alphabet)
        {
            AffineSubstitutionCipher.CheckKey1(alphabet.Length, key1);

            _key1 = Utils.PositiveModulo(key1, alphabet.Length);
            _key2 = Utils.PositiveModulo(key2, alphabet.Length);

            _decryptKey1 = InverseElement(_key1, alphabet.Length);
            _decryptKey2 =
                Utils.PositiveModulo(OppositeElement(_key2, alphabet.Length) * _decryptKey1, alphabet.Length);
        }

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

        private int OppositeElement(int element, int alphabetLength)
        {
            if (!IsElementInBounds(element, alphabetLength))
            {
                throw new ArgumentOutOfRangeException(nameof(element),
                    $"Element must be between 0 and {nameof(alphabetLength)}.");
            }

            return alphabetLength - element;
        }

        private bool IsElementInBounds(int element, int alphabetLength)
        {
            return element >= 0 && element < alphabetLength;
        }

        public override string Encrypt(string plainText)
        {
            if (plainText is null) throw new ArgumentNullException(nameof(plainText));

            var stringBuilder = new StringBuilder(plainText.Length);

            foreach (char ch in plainText)
            {
                int charIndex = GetCharIndex(ch);
                int newCharIndex = Utils.PositiveModulo(charIndex * _key1 + _key2, Alphabet.Length);
                char newChar = Alphabet[newCharIndex];

                stringBuilder.Append(newChar);
            }

            return stringBuilder.ToString();
        }

        public override string Decrypt(string encryptedText)
        {
            if (encryptedText is null) throw new ArgumentNullException(nameof(encryptedText));

            var stringBuilder = new StringBuilder(encryptedText.Length);

            foreach (char ch in encryptedText)
            {
                int charIndex = GetCharIndex(ch);
                int newCharIndex = Utils.PositiveModulo(charIndex * _decryptKey1 + _decryptKey2, Alphabet.Length);
                char newChar = Alphabet[newCharIndex];

                stringBuilder.Append(newChar);
            }

            return stringBuilder.ToString();
        }
    }
}