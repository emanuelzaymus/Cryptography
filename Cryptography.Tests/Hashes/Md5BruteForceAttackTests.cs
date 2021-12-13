using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        public void Test() // TODO: remove
        {
            var word = "7AB6sdZQWQWQWQ".ToCharArray();

            // var charArray = new char[10];
            // Array.Copy(word, 0, charArray, 7, 7);

            var passSalt = Encoding.UTF8.GetBytes(word.Concat("tVnMsoD5XZXZZXZX".ToCharArray()).ToArray());

            Console.WriteLine(word.Length);

            Console.WriteLine(passSalt.Length);
            var hashData = MD5.HashData(passSalt);
            Console.WriteLine(hashData.Length);
            Console.WriteLine(Convert.ToBase64String(hashData));
        }

        [Test]
        public void T2()
        {
            var fromBase64String = Convert.FromBase64String("uAbCdEFXGx+GGhRcuBo2uw==");

            Console.WriteLine(fromBase64String.Length);
        }
    }
}