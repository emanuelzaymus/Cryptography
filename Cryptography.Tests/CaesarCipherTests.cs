using NUnit.Framework;

namespace Cryptography.Tests
{
    public class CaesarCipherTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Encrypt_Shift3_ShouldEncryptCorrectly()
        {
            CaesarCipher cipher = new(Alphabets.ALPHABET, 3);
            const string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var encrypted = cipher.Encrypt(text);

            Assert.That(encrypted == "DEFGHIJKLMNOPQRSTUVWXYZABC");
        }
    }
}