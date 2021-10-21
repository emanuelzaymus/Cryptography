using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Analysis;
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
            VigenereCipherKasiskiAttack attack = new(Alphabets.ALPHABET, ProbabilitiesOfLetters.SlovakLanguage);
            const string original =
                "UROBTEFREKVENCNUANALYZUANGLICKEHOASLOVENSKEHOJAZYKAURCTEPRAVDEPODOBNOSTIVYSKYTOVJEDNOTLIVYCHZNAKOVREFERENCNEHOTEXTUVROZNYCHKODOVYCHABECEDACHTELEGRAFNATELEGRAFNASMEDZEROUASCIILATINURCTEPRAVDEPODOBNOSTIDVOJICAJTROJICZNAKOVNAZAKLADEMERANINAVRHNITENAHODNYGENERATORSLOVDANEHOJAZYKA";

            var checker = new AttackChecker(original);
            bool success = attack.Attack(EncryptedTexts.VigenereEncryptedText, checker, out string decryptedText,
                out string password, 5);

            Assert.True(success);
            Assert.That(decryptedText, Is.EqualTo(original));
            Assert.That(password, Is.EqualTo("AHOJ"));
        }
    }
}