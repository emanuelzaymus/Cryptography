using Cryptography.Alphabet;
using Cryptography.Ciphers.MonoAlphabeticSubstitution;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.MonoAlphabeticSubstitution
{
    [TestFixture]
    public class MonoAlphabeticSubstitutionCipherTests
    {
        [Test]
        public void Encrypt_EncryptOriginalAlphabet_ShouldReturnSubstitutionAlphabet()
        {
            // ReSharper disable once StringLiteralTypo
            var substitutionAlphabet = "QWERTYUIOPASDFGHJKLZXCVBNM";
            var cipher = new MonoAlphabeticSubstitutionCipher(Alphabets.ALPHABET, substitutionAlphabet);

            var encrypted = cipher.Encrypt(Alphabets.ALPHABET);

            Assert.AreEqual(substitutionAlphabet, encrypted);
        }

        [Test]
        public void Encrypt_PlainMessage_ShouldEncryptCorrectly()
        {
            // ReSharper disable once StringLiteralTypo
            // Alphabets.ALPHABET_   = "ABCDEFGHIJKLMNOPQRSTUVWXYZ "
            var substitutionAlphabet = " QWERTYUIOPASDFGHJKLZXCVBNM";
            var cipher = new MonoAlphabeticSubstitutionCipher(Alphabets.ALPHABET_, substitutionAlphabet);

            var encrypted = cipher.Encrypt("MY AWESOME MESSAGE");

            // ReSharper disable once StringLiteralTypo
            Assert.AreEqual("SBM CRKFSRMSRKK YR", encrypted);
        }

        [Test]
        public void Decrypt_DecryptSubstitutionAlphabet_ShouldReturnOriginalAlphabet()
        {
            // ReSharper disable once StringLiteralTypo
            var substitutionAlphabet = "QAZWSXEDCRF VTGBYHNUJMIKOLP";
            var cipher = new MonoAlphabeticSubstitutionCipher(Alphabets.ALPHABET_, substitutionAlphabet);

            var decrypted = cipher.Decrypt(substitutionAlphabet);

            Assert.AreEqual(Alphabets.ALPHABET_, decrypted);
        }


        [Test]
        public void Decrypt_EncryptedText_ShouldReturnOriginalText()
        {
            // ReSharper disable once StringLiteralTypo
            // Alphabets.ALPHABET_   = "ABCDEFGHIJKLMNOPQRSTUVWXYZ "
            var substitutionAlphabet = "QAZWSXEDCRF VTGBYHNUJMIKOLP";
            var cipher = new MonoAlphabeticSubstitutionCipher(Alphabets.ALPHABET_, substitutionAlphabet);

            // ReSharper disable once StringLiteralTypo
            var decrypted = cipher.Decrypt("TGUPMSHOPNSZJHSPIQOPGXPSTZHOBUCTE");

            Assert.AreEqual("NOT VERY SECURE WAY OF ENCRYPTING", decrypted);
        }
    }
}