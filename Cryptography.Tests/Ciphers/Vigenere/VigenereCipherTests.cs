using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Ciphers.Vigenere;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.Vigenere
{
    [TestFixture]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class VigenereCipherTests
    {
        [Test]
        public void Encrypt_Alphabet_ShouldEncryptCorrectly()
        {
            VigenereCipher cipher = new(Alphabets.ALPHABET, "PASSWORD");

            var encrypted = cipher.Encrypt(Alphabets.ALPHABET);

            Assert.That(encrypted, Is.EqualTo("PBUVATXKXJCDIBFSFRKLQJNANZ"));
        }

        [Test]
        public void Decrypt_EncryptedAlphabet_ShouldReturnPlainAlphabet()
        {
            VigenereCipher cipher = new(Alphabets.ALPHABET, "PASSWORD");

            var decrypted = cipher.Decrypt("PBUVATXKXJCDIBFSFRKLQJNANZ");

            Assert.That(decrypted, Is.EqualTo(Alphabets.ALPHABET));
        }

        [Test]
        public void Encrypt_SecreteMessage_ShouldEncryptCorrectly()
        {
            VigenereCipher cipher = new(Alphabets.ALPHABET, "HESLO");

            var encrypted = cipher.Encrypt("TOTOJETAJNASPRAVA");

            Assert.That(encrypted, Is.EqualTo("ASLZXLXSUBHWHCOCE"));
        }

        [Test]
        public void Decrypt_EncryptedMessage_ShouldReturnPlainMessage()
        {
            VigenereCipher cipher = new(Alphabets.ALPHABET, "HESLO");

            var decrypted = cipher.Decrypt("ASLZXLXSUBHWHCOCE");

            Assert.That(decrypted, Is.EqualTo("TOTOJETAJNASPRAVA"));
        }
    }
}