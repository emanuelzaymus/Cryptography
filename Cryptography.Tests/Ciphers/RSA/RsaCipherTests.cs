using System.Numerics;
using Cryptography.Ciphers.RSA;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.RSA
{
    [TestFixture]
    public class RsaCipherTests
    {
        [Test]
        public void Encrypt_12345_ShouldReturn3722383()
        {
            Encrypt_EncryptsPlainMessage_ReturnsExpectedMessage(8377, 9043, 65537, 31734449,
                12345, 3722383);
        }

        [Test]
        public void Decrypt_3722383_ShouldReturn12345()
        {
            Decrypt_DecryptsEncryptedMessage_ReturnsPlainMessage(8377, 9043, 65537, 31734449,
                12345, 3722383);
        }

        // Task 1 -------------------------------------------------------------------------------

        [Test]
        public void Encrypt_1234567890_ShouldReturn6029832903()
        {
            Encrypt_EncryptsPlainMessage_ReturnsExpectedMessage(101279, 130027, 65537, 72739001,
                1234567890, 6029832903);
        }

        [Test]
        public void Decrypt_6029832903_ShouldReturn1234567890()
        {
            Decrypt_DecryptsEncryptedMessage_ReturnsPlainMessage(101279, 130027, 65537, 72739001,
                1234567890, 6029832903);
        }

        // Task 2 -------------------------------------------------------------------------------

        [Test]
        public void Encrypt_1234567890_ShouldReturn22496913456008()
        {
            Encrypt_EncryptsPlainMessage_ReturnsExpectedMessage(35352181, 47816809, 65537, 1308297747522113,
                1234567890, 22496913456008);
        }

        [Test]
        public void Decrypt_22496913456008_ShouldReturn1234567890()
        {
            Decrypt_DecryptsEncryptedMessage_ReturnsPlainMessage(35352181, 47816809, 65537, 1308297747522113,
                1234567890, 22496913456008);
        }

        // Task 3 -------------------------------------------------------------------------------

        [Test]
        public void Encrypt_1234567890_ShouldReturn17014716723435111315()
        {
            Encrypt_EncryptsPlainMessage_ReturnsExpectedMessage(6940440583, 8117922401, 65537, 10931906232715055873,
                1234567890, 17014716723435111315);
        }

        [Test]
        public void Decrypt_17014716723435111315_ShouldReturn1234567890()
        {
            Decrypt_DecryptsEncryptedMessage_ReturnsPlainMessage(6940440583, 8117922401, 65537, 10931906232715055873,
                1234567890, 17014716723435111315);
        }

        private static void Encrypt_EncryptsPlainMessage_ReturnsExpectedMessage(BigInteger primeP, BigInteger primeQ,
            BigInteger publicKey, BigInteger privateKey, BigInteger plainMessage, BigInteger encryptedMessage)
        {
            RsaCipher rsa = new(primeP, primeQ, publicKey, privateKey);

            var encrypted = rsa.Encrypt(plainMessage);

            Assert.That(encrypted, Is.EqualTo(encryptedMessage));
        }

        private static void Decrypt_DecryptsEncryptedMessage_ReturnsPlainMessage(BigInteger primeP, BigInteger primeQ,
            BigInteger publicKey, BigInteger privateKey, BigInteger plainMessage, BigInteger encryptedMessage)
        {
            RsaCipher rsa = new(primeP, primeQ, publicKey, privateKey);

            var decrypted = rsa.Decrypt(encryptedMessage);

            Assert.That(decrypted, Is.EqualTo(plainMessage));
        }
    }
}