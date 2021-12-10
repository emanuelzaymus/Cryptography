using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Cryptography.Hashes;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Hashes
{
    [TestFixture]
    public class Md5DictionaryAttackTests
    {
        [Test]
        public void TryCrackPassword_CreatedWord_ShouldCrack()
        {
            // ReSharper disable once StringLiteralTypo
            var namesWithDiminutives = new List<string> {"anton", "moricko", "adam"};
            Md5DictionaryAttack attack = new(namesWithDiminutives);

            var success = attack.TryCrackPassword("oFr/pd10EvGP65k+xJvKMg==", "02SK64Xv", out var crackedPassword);

            Assert.That(success, Is.True);
            Assert.That(crackedPassword, Is.EqualTo("moricKo"));
        }

        [Test]
        public void TryCrackPassword_NotPresentWordInDictionary_ShouldNotCrack()
        {
            // ReSharper disable once StringLiteralTypo
            var namesWithDiminutives = new List<string> {"anton", "moricko", "adam"};
            Md5DictionaryAttack attack = new(namesWithDiminutives);

            var success = attack.TryCrackPassword("uinx90+IW3lJi8Rfob7nRg==", "02SK64Xv", out var crackedPassword);

            Assert.That(success, Is.False);
            Assert.That(crackedPassword, Is.Null);
        }

        [Test]
        public void TryCrackPassword_SlovakNamesPlainWord_ShouldCrack()
        {
            Md5DictionaryAttack attack = new(SlovakFirstNames.GetNamesWithDiminutives());

            var success = attack.TryCrackPassword("uinx90+IW3lJi8Rfob7nRg==", "02SK64Xv", out var crackedPassword);

            Assert.That(success, Is.True);
            Assert.That(crackedPassword, Is.EqualTo("peter"));
        }

        [Test]
        public void TryCrackPassword_SlovakNamesAndNotPresentWord_ShouldNotCrack()
        {
            Md5DictionaryAttack attack = new(SlovakFirstNames.GetNamesWithDiminutives());

            var success = attack.TryCrackPassword("lC7ZSWuNIzZfkoeJ22zK0Q==", "6589SoDE", out var crackedPassword);

            Assert.That(success, Is.False);
            Assert.That(crackedPassword, Is.Null);
        }

        [Test]
        public void TryCrackPassword_SlovakNamesAndCreatedWord_ShouldCrack()
        {
            Md5DictionaryAttack attack = new(SlovakFirstNames.GetNamesWithDiminutives());

            var success = attack.TryCrackPassword("lC7YC8uNIzZfkoeJ22zK0Q==", "6589SoDE", out var crackedPassword);

            Assert.That(success, Is.True);
            // ReSharper disable once StringLiteralTypo
            Assert.That(crackedPassword, Is.EqualTo("Katarinka"));
        }

        [Test]
        public void CrackPasswords_Shadow1_ShouldCrack()
        {
            var userShadows = ShadowLoader.LoadUserShadows(Paths.In.Assignment5.Shadow1);

            Md5DictionaryAttack attack = new(SlovakFirstNames.GetNamesWithDiminutives());
            var crackedPasswords = attack.CrackPasswords(userShadows).ToList();

            // ReSharper disable once StringLiteralTypo
            Assert.That(crackedPasswords.Select(s => s.CrackedPassword), Is.EquivalentTo(new[] {"maTusko"}));
        }

        [Test]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void CrackPasswords_Shadow2_ShouldCrack()
        {
            var userShadows = ShadowLoader.LoadUserShadows(Paths.In.Assignment5.Shadow2);

            Md5DictionaryAttack attack = new(SlovakFirstNames.GetNamesWithDiminutives());
            var crackedPasswords = attack.CrackPasswords(userShadows).ToList();

            var passwords = new[] {"maTusko", "kAtarina", "maTusko"};
            Assert.That(crackedPasswords.Select(s => s.CrackedPassword), Is.EquivalentTo(passwords));
        }

        [Test]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void CrackPasswords_Shadow3_ShouldCrack()
        {
            var userShadows = ShadowLoader.LoadUserShadows(Paths.In.Assignment5.Shadow3);

            Md5DictionaryAttack attack = new(SlovakFirstNames.GetNamesWithDiminutives());
            var crackedPasswords = attack.CrackPasswords(userShadows).ToList();

            var passwords = new[] {"milaDa", "maTusko", "adamKo"};
            Assert.That(crackedPasswords.Select(s => s.CrackedPassword), Is.EquivalentTo(passwords));
        }

        [Test]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void CrackPasswords_Shadow4_ShouldCrack()
        {
            var userShadows = ShadowLoader.LoadUserShadows(Paths.In.Assignment5.Shadow4);

            Md5DictionaryAttack attack = new(SlovakFirstNames.GetNamesWithDiminutives());
            var crackedPasswords = attack.CrackPasswords(userShadows).ToList();

            var passwords = new[] {"adamKo"};
            Assert.That(crackedPasswords.Select(s => s.CrackedPassword), Is.EquivalentTo(passwords));
        }

        // TODO: remove
        [Test]
        public void TestMd5()
        {
            const string str = "45" + "2";

            var bytes = Encoding.UTF8.GetBytes(str);

            var hash = MD5.HashData(bytes);

            Console.WriteLine(Convert.ToBase64String(hash));
        }

        // TODO: remove
        [Test]
        public void Test2()
        {
            const string pass = "moricKo";
            const string salt = "02SK64Xv";

            // var bytes = Encoding.UTF8.GetBytes(str);

            var passBytes = Encoding.UTF8.GetBytes(pass);

            var saltBytes = Encoding.UTF8.GetBytes(salt);

            var bytes = passBytes.Concat(saltBytes).ToArray();

            var hash = MD5.HashData(bytes);

            Console.WriteLine(Convert.ToBase64String(hash)); // eHhZ5lo0B4+sLETYGnFW2g==
            // eHhZ5lo0B4+sLETYGnFW2g==

            var str = "moricKo" + "02SK64Xv";

            var bytes1 = Encoding.UTF8.GetBytes(str);

            var hashData = MD5.HashData(bytes1);

            Assert.True(hash.SequenceEqual(hashData));
        }
    }
}