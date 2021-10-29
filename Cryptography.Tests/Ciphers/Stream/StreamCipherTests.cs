using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Ciphers.Stream;
using Cryptography.RandomNumberGenerators;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.Stream
{
    [TestFixture]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class StreamCipherTests
    {
        [Test]
        public void Encrypt_SecreteMessage_ShouldEncryptCorrectly()
        {
            IRng rng = new BuildInRng(0);
            var cipher = new StreamCipher(Alphabets.ALPHABET, rng);

            var encrypted = cipher.Encrypt("TOTOJETAJNASPRAVA");

            Assert.That(encrypted, Is.EqualTo("LJMCOSQLIUHEFDZVW"));
        }

        [Test]
        public void Decrypt_SecreteMessage_ShouldDecryptCorrectly()
        {
            IRng rng = new BuildInRng(0);
            var cipher = new StreamCipher(Alphabets.ALPHABET, rng);

            var decrypted = cipher.Decrypt("LJMCOSQLIUHEFDZVW");

            Assert.That(decrypted, Is.EqualTo("TOTOJETAJNASPRAVA"));
        }

        [Test]
        public void Encrypt_Alphabet_ShouldReturnShiftedAlphabet()
        {
            IRng rng = new MockRng(3.0 / Alphabets.ALPHABET.Length);
            var cipher = new StreamCipher(Alphabets.ALPHABET, rng);

            var encrypted = cipher.Encrypt("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

            Assert.That(encrypted, Is.EqualTo("DEFGHIJKLMNOPQRSTUVWXYZABC"));
        }

        [Test]
        public void Decrypt_ShiftedAlphabet_ShouldReturnAlphabet()
        {
            IRng rng = new MockRng(3.0 / Alphabets.ALPHABET.Length);
            var cipher = new StreamCipher(Alphabets.ALPHABET, rng);

            var decrypted = cipher.Decrypt("DEFGHIJKLMNOPQRSTUVWXYZABC");

            Assert.That(decrypted, Is.EqualTo("ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
        }

        private class MockRng : IRng
        {
            private readonly double _number;

            public MockRng(double number)
            {
                _number = number;
            }

            public double Sample() => _number;

            public void Restart()
            {
            }
        }
    }
}