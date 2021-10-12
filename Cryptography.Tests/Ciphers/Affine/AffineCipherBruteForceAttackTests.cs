using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Ciphers.Affine;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.Affine
{
    [TestFixture]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class AffineCipherBruteForceAttackTests
    {
        [Test]
        public void Attack_Alphabet_ShouldReturnEncryptedAlphabet()
        {
            const string originalText = Alphabets.ALPHABET;
            const string encryptedText = "KPUZEJOTYDINSXCHMRWBGLQVAF";
            AttackChecker attackChecker = new(originalText);

            var attack = new AffineCipherBruteForceAttack(Alphabets.ALPHABET);
            var success = attack.Attack(encryptedText, attackChecker, out string decryptedText,
                out int? decryptKey1, out int? decryptKey2);

            Assert.That(success, Is.True);
            Assert.That(decryptedText, Is.EqualTo(originalText));
            Assert.That(decryptKey1, Is.EqualTo(21));
            Assert.That(decryptKey2, Is.EqualTo(24));
        }

        [Test]
        public void Attack_EncryptedAlphabet_ShouldReturnOriginalAlphabet()
        {
            const string originalText = "KPUZEJOTYDINSXCHMRWBGLQVAF";
            const string encryptedText = Alphabets.ALPHABET;
            AttackChecker attackChecker = new(originalText);

            var attack = new AffineCipherBruteForceAttack(Alphabets.ALPHABET);
            var success = attack.Attack(encryptedText, attackChecker, out string decryptedText,
                out int? decryptKey1, out int? decryptKey2);

            Assert.That(success, Is.True);
            Assert.That(decryptedText, Is.EqualTo(originalText));
            Assert.That(decryptKey1, Is.EqualTo(5));
            Assert.That(decryptKey2, Is.EqualTo(10));
        }

        [Test]
        public void Attack_EncryptedText_ShouldReturnOriginalText()
        {
            const string originalText = "VYRIESIL SOM LAHKU ULOHU";
            const string encryptedText = "LIYGTOGDPOAUPDFQNVPVDAQV";
            AttackChecker attackChecker = new(originalText);

            var attack = new AffineCipherBruteForceAttack(Alphabets.ALPHABET_);
            var success = attack.Attack(encryptedText, attackChecker, out string decryptedText,
                out int? decryptKey1, out int? decryptKey2);

            Assert.That(success, Is.True);
            Assert.That(decryptedText, Is.EqualTo(originalText));
            Assert.That(decryptKey1, Is.EqualTo(8));
            Assert.That(decryptKey2, Is.EqualTo(14));
        }

        [Test]
        public void Attack_SecreteMessage_ShouldReturnPlainText()
        {
            const string originalText = "TOTO JE TAJNA SPRAVA";
            const string encryptedText = "CECEKGIKCPG PKYJTPMP";
            AttackChecker attackChecker = new(originalText);

            var attack = new AffineCipherBruteForceAttack(Alphabets.ALPHABET_);
            var success = attack.Attack(encryptedText, attackChecker, out string decryptedText,
                out int? decryptKey1, out int? decryptKey2);

            Assert.That(success, Is.True);
            Assert.That(decryptedText, Is.EqualTo(originalText));
            Assert.That(decryptKey1, Is.EqualTo(11));
            Assert.That(decryptKey2, Is.EqualTo(24));
        }
    }
}