using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Ciphers.MonoAlphabeticSubstitution;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.MonoAlphabeticSubstitution
{
    [TestFixture]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class MonoAlphabeticSubstitutionCipherTests
    {
        [Test]
        public void Encrypt_EncryptOriginalAlphabet_ShouldReturnSubstitutionAlphabet()
        {
            const string substitutionAlphabet = "QWERTYUIOPASDFGHJKLZXCVBNM";
            var cipher = new MonoAlphabeticSubstitutionCipher(Alphabets.ALPHABET, substitutionAlphabet);

            var encrypted = cipher.Encrypt(Alphabets.ALPHABET);

            Assert.AreEqual(substitutionAlphabet, encrypted);
        }

        [Test]
        public void Encrypt_PlainMessage_ShouldEncryptCorrectly()
        {
            // Alphabets.ALPHABET_            = "ABCDEFGHIJKLMNOPQRSTUVWXYZ "
            const string substitutionAlphabet = " QWERTYUIOPASDFGHJKLZXCVBNM";
            var cipher = new MonoAlphabeticSubstitutionCipher(Alphabets.ALPHABET_, substitutionAlphabet);

            var encrypted = cipher.Encrypt("MY AWESOME MESSAGE");

            Assert.AreEqual("SBM CRKFSRMSRKK YR", encrypted);
        }

        [Test]
        public void Decrypt_DecryptSubstitutionAlphabet_ShouldReturnOriginalAlphabet()
        {
            const string substitutionAlphabet = "QAZWSXEDCRF VTGBYHNUJMIKOLP";
            var cipher = new MonoAlphabeticSubstitutionCipher(Alphabets.ALPHABET_, substitutionAlphabet);

            var decrypted = cipher.Decrypt(substitutionAlphabet);

            Assert.AreEqual(Alphabets.ALPHABET_, decrypted);
        }


        [Test]
        public void Decrypt_EncryptedText_ShouldReturnOriginalText()
        {
            // Alphabets.ALPHABET_            = "ABCDEFGHIJKLMNOPQRSTUVWXYZ "
            const string substitutionAlphabet = "QAZWSXEDCRF VTGBYHNUJMIKOLP";
            var cipher = new MonoAlphabeticSubstitutionCipher(Alphabets.ALPHABET_, substitutionAlphabet);

            var decrypted = cipher.Decrypt("TGUPMSHOPNSZJHSPIQOPGXPSTZHOBUCTE");

            Assert.AreEqual("NOT VERY SECURE WAY OF ENCRYPTING", decrypted);
        }
    }
}