using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Ciphers.Caesar;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.Caesar
{
    [TestFixture]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class CaesarCipherTests
    {
        [Test]
        public void Encrypt_NormalShift_ShouldEncryptCorrectly()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            CaesarCipher cipher = new(Alphabets.ALPHABET, 3);
            const string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var encrypted = cipher.Encrypt(text);

            Assert.AreEqual("DEFGHIJKLMNOPQRSTUVWXYZABC", encrypted);
        }

        [Test]
        public void Encrypt_TooLargeShift_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.ALPHABET, 26 + 6);
            const string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var encrypted = cipher.Encrypt(text);

            Assert.AreEqual("GHIJKLMNOPQRSTUVWXYZABCDEF", encrypted);
        }

        [Test]
        public void Encrypt_NegativeShift_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.ALPHABET, -5);
            const string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var encrypted = cipher.Encrypt(text);

            Assert.AreEqual("VWXYZABCDEFGHIJKLMNOPQRSTU", encrypted);
        }


        [Test]
        public void Decrypt_NormalShift_ShouldEncryptCorrectly()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            CaesarCipher cipher = new(Alphabets.ALPHABET, 3);
            const string encrypted = "DEFGHIJKLMNOPQRSTUVWXYZABC";

            var decrypted = cipher.Decrypt(encrypted);

            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", decrypted);
        }

        [Test]
        public void Decrypt_TooLargeShift_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.ALPHABET, 26 + 6);
            const string encrypted = "GHIJKLMNOPQRSTUVWXYZABCDEF";

            var decrypted = cipher.Decrypt(encrypted);

            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", decrypted);
        }

        [Test]
        public void Decrypt_NegativeShift_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.ALPHABET, -5);
            const string encrypted = "VWXYZABCDEFGHIJKLMNOPQRSTU";

            var decrypted = cipher.Decrypt(encrypted);

            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", decrypted);
        }
    }
}