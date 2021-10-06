using Cryptography.Alphabet;
using Cryptography.Caesar;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Caesar
{
    [TestFixture]
    public class CaesarCipherBruteForceAttackTests
    {
        [Test]
        public void Attack_PositiveShift_DecryptedTextCorrectly()
        {
            // ReSharper disable once StringLiteralTypo
            const string encryptedText = "DEFGHIJKLMNOPQRSTUVWXYZABC";
            const string originalText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            AttackChecker checker = new(originalText);

            CaesarCipherBruteForceAttack attack = new(Alphabets.ALPHABET);
            var success = attack.Attack(encryptedText, checker, out string decryptedText, out int? shift);

            Assert.IsTrue(success);
            Assert.AreEqual(originalText, decryptedText);
            Assert.AreEqual(3, shift);
        }

        [Test]
        public void Attack_NegativeShift_DecryptedTextCorrectly()
        {
            // ReSharper disable once StringLiteralTypo
            const string encryptedText = "TUVWXYZABCDEFGHIJKLMNOPQRS";
            const string originalText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            AttackChecker checker = new(originalText);

            CaesarCipherBruteForceAttack attack = new(Alphabets.ALPHABET);
            var success = attack.Attack(encryptedText, checker, out string decryptedText, out int? shift);

            Assert.IsTrue(success);
            Assert.AreEqual(originalText, decryptedText);
            Assert.AreEqual(19, shift);
        }
    }
}