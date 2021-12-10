using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Cryptography.Hashes;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Hashes
{
    [TestFixture]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class Md5DictionaryAttackTests
    {
        [Test]
        public void TryCrackPassword_CreatedWord_ShouldCrack()
        {
            TryCrackPassword(new List<string> {"anton", "moricko", "adam"}, "oFr/pd10EvGP65k+xJvKMg==", "02SK64Xv",
                true, "moricKo");
        }

        [Test]
        public void TryCrackPassword_NotPresentWordInDictionary_ShouldNotCrack()
        {
            TryCrackPassword(new List<string> {"anton", "moricko", "adam"}, "uinx90+IW3lJi8Rfob7nRg==", "02SK64Xv",
                false, null);
        }

        [Test]
        public void TryCrackPassword_SlovakNamesPlainWord_ShouldCrack()
        {
            TryCrackPassword(SlovakFirstNames.GetNamesWithDiminutives(), "uinx90+IW3lJi8Rfob7nRg==", "02SK64Xv",
                true, "peter");
        }

        [Test]
        public void TryCrackPassword_SlovakNamesAndNotPresentWord_ShouldNotCrack()
        {
            TryCrackPassword(SlovakFirstNames.GetNamesWithDiminutives(), "lC7ZSWuNIzZfkoeJ22zK0Q==", "6589SoDE",
                false, null);
        }

        [Test]
        public void TryCrackPassword_SlovakNamesAndCreatedWord_ShouldCrack()
        {
            TryCrackPassword(SlovakFirstNames.GetNamesWithDiminutives(), "lC7YC8uNIzZfkoeJ22zK0Q==", "6589SoDE",
                true, "Katarinka");
        }

        private void TryCrackPassword(IReadOnlyList<string> dictionary, string passwordHash, string salt, bool result,
            string expectedPassword)
        {
            Md5DictionaryAttack attack = new(dictionary);

            var success = attack.TryCrackPassword(passwordHash, salt, out var crackedPassword);

            Assert.That(success, Is.EqualTo(result));
            Assert.That(crackedPassword, Is.EqualTo(expectedPassword));
        }

        [Test]
        public void CrackPasswords_Shadow1_ShouldCrack()
        {
            CrackPasswords_Shadow_ShouldCrack(Paths.In.Assignment5.Shadow1, "maTusko");
        }

        [Test]
        public void CrackPasswords_Shadow2_ShouldCrack()
        {
            CrackPasswords_Shadow_ShouldCrack(Paths.In.Assignment5.Shadow2, "maTusko", "kAtarina", "maTusko");
        }

        [Test]
        public void CrackPasswords_Shadow3_ShouldCrack()
        {
            CrackPasswords_Shadow_ShouldCrack(Paths.In.Assignment5.Shadow3, "milaDa", "maTusko", "adamKo");
        }

        [Test]
        public void CrackPasswords_Shadow4_ShouldCrack()
        {
            CrackPasswords_Shadow_ShouldCrack(Paths.In.Assignment5.Shadow4, "adamKo");
        }

        private void CrackPasswords_Shadow_ShouldCrack(string shadowFilePath, params string[] passwords)
        {
            var userShadows = ShadowLoader.LoadUserShadows(shadowFilePath);

            Md5DictionaryAttack attack = new(SlovakFirstNames.GetNamesWithDiminutives());
            var crackedPasswords = attack.CrackPasswords(userShadows).ToList();

            Assert.That(crackedPasswords.Select(s => s.CrackedPassword), Is.EquivalentTo(passwords));
        }
    }
}