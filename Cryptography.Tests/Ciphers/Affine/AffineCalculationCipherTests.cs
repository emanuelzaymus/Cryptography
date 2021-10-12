using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Ciphers.Affine;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.Affine
{
    [TestFixture]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class AffineCalculationCipherTests
    {
        // Key1 and Key2 validity is tested in AffineSubstitutionCipherTests.

        [Test]
        public void Encrypt_Alphabet_ShouldReturnEncryptedAlphabet()
        {
            var cipher = new AffineCalculationCipher(Alphabets.ALPHABET, 5, 10);

            var encrypted = cipher.Encrypt(Alphabets.ALPHABET);

            Assert.That(encrypted, Is.EqualTo("KPUZEJOTYDINSXCHMRWBGLQVAF"));
        }

        [Test]
        public void Decrypt_EncryptedAlphabet_ShouldReturnOriginalAlphabet()
        {
            var cipher = new AffineCalculationCipher(Alphabets.ALPHABET, 5, 10);

            var decrypted = cipher.Decrypt("KPUZEJOTYDINSXCHMRWBGLQVAF");

            Assert.That(decrypted, Is.EqualTo(Alphabets.ALPHABET));
        }
    }
}