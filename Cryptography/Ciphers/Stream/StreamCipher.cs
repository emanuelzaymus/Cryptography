using Cryptography.RandomNumberGenerators;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Stream
{
    public class StreamCipher : Cipher
    {
        private readonly IRng _rng;

        public StreamCipher(string alphabet, IRng rng) : base(alphabet)
        {
            _rng = rng;
        }

        public override string Encrypt(string plainText)
        {
            _rng.Restart();
            return base.Encrypt(plainText);
        }

        public override string Decrypt(string encryptedText)
        {
            _rng.Restart();
            return base.Decrypt(encryptedText);
        }

        protected override char CharEncryption(char ch, int _) => StreamTransformation(ch, false);

        protected override char CharDecryption(char ch, int _) => StreamTransformation(ch, true);

        private char StreamTransformation(char ch, bool isDecryption)
        {
            int alphabetCharIndex = GetAlphabetCharIndex(ch);
            int randomNumber = GetRandomNumber();

            if (isDecryption)
            {
                randomNumber = -randomNumber;
            }

            int newCharIndex = Utils.PositiveModulo(alphabetCharIndex + randomNumber, Alphabet.Length);
            return Alphabet[newCharIndex];
        }

        private int GetRandomNumber()
        {
            return (int) (_rng.Sample() * Alphabet.Length);
        }
    }
}