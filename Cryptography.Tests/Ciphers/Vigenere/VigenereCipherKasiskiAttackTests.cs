using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Analysis.TextNormalization;
using Cryptography.Ciphers.Vigenere;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.Vigenere
{
    [TestFixture]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class VigenereCipherKasiskiAttackTests
    {
        [Test]
        public void Attack_LabsEncryptedText_DecryptedTextWithPassword()
        {
            VigenereCipherKasiskiAttack attack =
                new(Alphabets.ALPHABET, ProbabilitiesOfLetters.SkTelegraphWithoutSpace);
            const string original =
                "UROBTEFREKVENCNUANALYZUANGLICKEHOASLOVENSKEHOJAZYKAURCTEPRAVDEPODOBNOSTIVYSKYTOVJEDNOTLIVYCHZNAKOVREFERENCNEHOTEXTUVROZNYCHKODOVYCHABECEDACHTELEGRAFNATELEGRAFNASMEDZEROUASCIILATINURCTEPRAVDEPODOBNOSTIDVOJICAJTROJICZNAKOVNAZAKLADEMERANINAVRHNITENAHODNYGENERATORSLOVDANEHOJAZYKA";

            var encryptedText = Texts.GetText1(new TextNormalizer(onlyValidCharacters: Alphabets.ALPHABET));
            var checker = new AttackChecker(original);
            bool success = attack.Attack(encryptedText, checker, out string decryptedText, out string password,
                3, 8, 2);

            Assert.True(success);
            Assert.That(decryptedText, Is.EqualTo(original));
            Assert.That(password, Is.EqualTo("AHOJ"));
        }
    }
}