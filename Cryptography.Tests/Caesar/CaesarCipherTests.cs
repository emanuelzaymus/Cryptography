using Cryptography.Caesar;
using NUnit.Framework;

namespace Cryptography.Tests.Caesar
{
    [TestFixture]
    public class CaesarCipherTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Encrypt_NormalShift_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.Alphabets.ALPHABET, 3);
            const string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var encrypted = cipher.Encrypt(text);

            // ReSharper disable once StringLiteralTypo
            Assert.AreEqual("DEFGHIJKLMNOPQRSTUVWXYZABC", encrypted);
        }

        [Test]
        public void Encrypt_TooLargeShift_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.Alphabets.ALPHABET, 26 + 6);
            const string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var encrypted = cipher.Encrypt(text);

            // ReSharper disable once StringLiteralTypo
            Assert.AreEqual("GHIJKLMNOPQRSTUVWXYZABCDEF", encrypted);
        }

        [Test]
        public void Encrypt_NegativeShift_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.Alphabets.ALPHABET, -5);
            const string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var encrypted = cipher.Encrypt(text);

            // ReSharper disable once StringLiteralTypo
            Assert.AreEqual("VWXYZABCDEFGHIJKLMNOPQRSTU", encrypted);
        }


        [Test]
        public void Decrypt_NormalShift_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.Alphabets.ALPHABET, 3);
            // ReSharper disable once StringLiteralTypo
            const string encrypted = "DEFGHIJKLMNOPQRSTUVWXYZABC";

            var decrypted = cipher.Decrypt(encrypted);

            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", decrypted);
        }

        [Test]
        public void Decrypt_TooLargeShift_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.Alphabets.ALPHABET, 26 + 6);
            // ReSharper disable once StringLiteralTypo
            const string encrypted = "GHIJKLMNOPQRSTUVWXYZABCDEF";

            var decrypted = cipher.Decrypt(encrypted);

            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", decrypted);
        }

        [Test]
        public void Decrypt_NegativeShift_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.Alphabets.ALPHABET, -5);
            // ReSharper disable once StringLiteralTypo
            const string encrypted = "VWXYZABCDEFGHIJKLMNOPQRSTU";

            var decrypted = cipher.Decrypt(encrypted);

            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", decrypted);
        }
    }
}