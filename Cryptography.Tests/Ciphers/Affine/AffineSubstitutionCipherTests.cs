using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Ciphers.Affine;
using Cryptography.Ciphers.Common;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.Affine
{
    [TestFixture]
    [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class AffineSubstitutionCipherTests
    {
        [Test]
        public void AffineSubstitutionCipher_ValidKeys_ShouldCreateCorrectly()
        {
            const string alphabet = Alphabets.ALPHABET_;
            // alphabet.Length == 27
            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 1, 8));
            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 2, 8));

            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 4, 8));
            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 5, 8));

            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 7, 8));
            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 8, 8));

            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 10, 8));
            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 11, 8));

            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 13, 8));
            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 14, 8));

            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 16, 8));
            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 17, 8));

            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 19, 8));
            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 20, 8));

            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 22, 8));
            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 23, 8));

            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 25, 8));
            Assert.DoesNotThrow(() => new AffineSubstitutionCipher(alphabet, 26, 8));
        }

        [Test]
        public void AffineSubstitutionCipher_InvalidKeys_ShouldThrowException()
        {
            const string alphabet = Alphabets.ALPHABET_;
            // alphabet.Length == 27
            Assert.Throws<InvalidKeyException>(() => new AffineSubstitutionCipher(alphabet, 0, 8));
            Assert.Throws<InvalidKeyException>(() => new AffineSubstitutionCipher(alphabet, 3, 8));
            Assert.Throws<InvalidKeyException>(() => new AffineSubstitutionCipher(alphabet, 6, 8));
            Assert.Throws<InvalidKeyException>(() => new AffineSubstitutionCipher(alphabet, 9, 8));
            Assert.Throws<InvalidKeyException>(() => new AffineSubstitutionCipher(alphabet, 12, 8));
            Assert.Throws<InvalidKeyException>(() => new AffineSubstitutionCipher(alphabet, 15, 8));
            Assert.Throws<InvalidKeyException>(() => new AffineSubstitutionCipher(alphabet, 18, 8));
            Assert.Throws<InvalidKeyException>(() => new AffineSubstitutionCipher(alphabet, 21, 8));
            Assert.Throws<InvalidKeyException>(() => new AffineSubstitutionCipher(alphabet, 24, 8));
            Assert.Throws<InvalidKeyException>(() => new AffineSubstitutionCipher(alphabet, 27, 8));
        }

        [Test]
        public void Encrypt_Alphabet_ShouldReturnEncryptedAlphabet()
        {
            var cipher = new AffineSubstitutionCipher(Alphabets.ALPHABET, 5, 10);

            var encrypted = cipher.Encrypt(Alphabets.ALPHABET);

            Assert.That(encrypted, Is.EqualTo("KPUZEJOTYDINSXCHMRWBGLQVAF"));
        }

        [Test]
        public void Decrypt_EncryptedAlphabet_ShouldReturnOriginalAlphabet()
        {
            var cipher = new AffineSubstitutionCipher(Alphabets.ALPHABET, 5, 10);

            var decrypted = cipher.Decrypt("KPUZEJOTYDINSXCHMRWBGLQVAF");

            Assert.That(decrypted, Is.EqualTo(Alphabets.ALPHABET));
        }
    }
}