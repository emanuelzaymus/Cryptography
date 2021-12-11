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
            bool result,
            string expectedPassword)
        {
            Md5BruteForceAttack attack = new(alphabet, minLength, maxLength);

            var success = attack.TryCrackPassword(passwordHash, salt, out var crackedPassword);

            Assert.That(success, Is.EqualTo(result));
            Assert.That(crackedPassword, Is.EqualTo(expectedPassword));
        }

        [Test]
        public void AlphabetPermutations_WordLength3AlphabetLength4_ShouldReturn64Elements()
        {
            // ReSharper disable once StringLiteralTypo
            Md5BruteForceAttack attack = new("abcd", default, default);
            var enumerable = attack.AlphabetPermutations(3, 0, 4);

            char[][] expected =
            {
                new[] {'a', 'a', 'a'},
                new[] {'a', 'a', 'b'},
                new[] {'a', 'a', 'c'},
                new[] {'a', 'a', 'd'},

                new[] {'a', 'b', 'a'},
                new[] {'a', 'b', 'b'},
                new[] {'a', 'b', 'c'},
                new[] {'a', 'b', 'd'},

                new[] {'a', 'c', 'a'},
                new[] {'a', 'c', 'b'},
                new[] {'a', 'c', 'c'},
                new[] {'a', 'c', 'd'},

                new[] {'a', 'd', 'a'},
                new[] {'a', 'd', 'b'},
                new[] {'a', 'd', 'c'},
                new[] {'a', 'd', 'd'},

                new[] {'b', 'a', 'a'},
                new[] {'b', 'a', 'b'},
                new[] {'b', 'a', 'c'},
                new[] {'b', 'a', 'd'},

                new[] {'b', 'b', 'a'},
                new[] {'b', 'b', 'b'},
                new[] {'b', 'b', 'c'},
                new[] {'b', 'b', 'd'},

                new[] {'b', 'c', 'a'},
                new[] {'b', 'c', 'b'},
                new[] {'b', 'c', 'c'},
                new[] {'b', 'c', 'd'},

                new[] {'b', 'd', 'a'},
                new[] {'b', 'd', 'b'},
                new[] {'b', 'd', 'c'},
                new[] {'b', 'd', 'd'},

                new[] {'c', 'a', 'a'},
                new[] {'c', 'a', 'b'},
                new[] {'c', 'a', 'c'},
                new[] {'c', 'a', 'd'},

                new[] {'c', 'b', 'a'},
            };

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable.Take(37), Is.EquivalentTo(expected));

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable.Count(), Is.EqualTo((int) Math.Pow(4, 3))); // 64
        }

        [Test]
        public void AlphabetPermutations_FromInclusive2RangeSize2_ShouldReturn32Elements()
        {
            // ReSharper disable once StringLiteralTypo
            Md5BruteForceAttack attack = new("abcd", default, default);
            var enumerable = attack.AlphabetPermutations(3, 2, 2);

            char[][] expected =
            {
                new[] {'c', 'a', 'a'},
                new[] {'c', 'a', 'b'},
                new[] {'c', 'a', 'c'},
                new[] {'c', 'a', 'd'},

                new[] {'c', 'b', 'a'},
                new[] {'c', 'b', 'b'},
                new[] {'c', 'b', 'c'},
                new[] {'c', 'b', 'd'},

                new[] {'c', 'c', 'a'},
                new[] {'c', 'c', 'b'},
                new[] {'c', 'c', 'c'},
                new[] {'c', 'c', 'd'},

                new[] {'c', 'd', 'a'},
                new[] {'c', 'd', 'b'},
                new[] {'c', 'd', 'c'},
                new[] {'c', 'd', 'd'},

                new[] {'d', 'a', 'a'},
                new[] {'d', 'a', 'b'},
                new[] {'d', 'a', 'c'},
                new[] {'d', 'a', 'd'},

                new[] {'d', 'b', 'a'},
                new[] {'d', 'b', 'b'},
                new[] {'d', 'b', 'c'},
                new[] {'d', 'b', 'd'},

                new[] {'d', 'c', 'a'},
                new[] {'d', 'c', 'b'},
                new[] {'d', 'c', 'c'},
                new[] {'d', 'c', 'd'},

                new[] {'d', 'd', 'a'},
                new[] {'d', 'd', 'b'},
                new[] {'d', 'd', 'c'},
                new[] {'d', 'd', 'd'},
            };

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable, Is.EquivalentTo(expected));

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable.Count(), Is.EqualTo(32));
        }

        [Test]
        public void AlphabetPermutations_FromInclusive2RangeSize1_ShouldReturn16Elements()
        {
            // ReSharper disable once StringLiteralTypo
            Md5BruteForceAttack attack = new("abcd", default, default);
            var enumerable = attack.AlphabetPermutations(3, 2, 1);

            char[][] expected =
            {
                new[] {'c', 'a', 'a'},
                new[] {'c', 'a', 'b'},
                new[] {'c', 'a', 'c'},
                new[] {'c', 'a', 'd'},

                new[] {'c', 'b', 'a'},
                new[] {'c', 'b', 'b'},
                new[] {'c', 'b', 'c'},
                new[] {'c', 'b', 'd'},

                new[] {'c', 'c', 'a'},
                new[] {'c', 'c', 'b'},
                new[] {'c', 'c', 'c'},
                new[] {'c', 'c', 'd'},

                new[] {'c', 'd', 'a'},
                new[] {'c', 'd', 'b'},
                new[] {'c', 'd', 'c'},
                new[] {'c', 'd', 'd'},
            };

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable, Is.EquivalentTo(expected));

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable.Count(), Is.EqualTo(16));
        }

        [Test]
        public void Test()
        {
            var passSalt = Encoding.UTF8.GetBytes("2Dad34" + "L5O9wplS");

            Console.WriteLine(Convert.ToBase64String(MD5.HashData(passSalt)));
        }
    }
}