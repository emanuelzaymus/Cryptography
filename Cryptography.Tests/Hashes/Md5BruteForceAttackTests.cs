using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using Cryptography.Hashes;
using NUnit.Framework;

namespace Cryptography.Tests.Hashes
{
    [TestFixture]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class Md5BruteForceAttackTests
    {
        [Test]
        public void TryCrackPassword_AlphabetLength6PasswordLength4_ShouldCrackCorrectly()
        {
            TryCrackPassword("abcdef", 2, 5, "z5am5fuZOz4k9DAg7cVU4g==", "L5O9wplS",
                true, "effd");
        }

        [Test]
        public void TryCrackPassword_AlphabetLength12PasswordLength6_ShouldCrackCorrectly()
        {
            TryCrackPassword("abcdABCD1234", 3, 6, "uFBF3bQXGx+GGhRcuBo2uw==", "L5O9wplS",
                true, "2Dad34");
        }

        [Test]
        public void TryCrackPassword_InvalidHashedPassword_ShouldNotCrack()
        {
            TryCrackPassword("abcdABCD1234", 4, 5, "uAbCdEFXGx+GGhRcuBo2uw==", "L5O9wplS",
                false, null);
        }

        private void TryCrackPassword(string alphabet, int minLength, int maxLength, string passwordHash, string salt,
            bool result, string expectedPassword)
        {
            Md5BruteForceAttack attack = new(alphabet, minLength, maxLength);

            var success = attack.TryCrackPassword(passwordHash, salt, out var crackedPassword);

            Assert.That(success, Is.EqualTo(result));
            Assert.That(crackedPassword, Is.EqualTo(expectedPassword));
        }

        [Test]
        public void Test()
        {
            var passSalt = Encoding.UTF8.GetBytes("2Dad34" + "L5O9wplS");

            Console.WriteLine(Convert.ToBase64String(MD5.HashData(passSalt)));
        }
    }
}