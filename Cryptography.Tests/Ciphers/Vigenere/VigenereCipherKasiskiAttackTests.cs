using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
            VigenereCipherKasiskiAttack attack = new(Alphabets.ALPHABET, ProbabilitiesOfLetters.SlovakLanguage);
            const string original =
                "UROBTEFREKVENCNUANALYZUANGLICKEHOASLOVENSKEHOJAZYKAURCTEPRAVDEPODOBNOSTIVYSKYTOVJEDNOTLIVYCHZNAKOVREFERENCNEHOTEXTUVROZNYCHKODOVYCHABECEDACHTELEGRAFNATELEGRAFNASMEDZEROUASCIILATINURCTEPRAVDEPODOBNOSTIDVOJICAJTROJICZNAKOVNAZAKLADEMERANINAVRHNITENAHODNYGENERATORSLOVDANEHOJAZYKA";

            var encryptedText = Texts.GetText1(new TextNormalizer(onlyValidCharacters: Alphabets.ALPHABET));
            var checker = new AttackChecker(original);
            bool success = attack.Attack(encryptedText, checker, out string decryptedText, out string password, 2);

            Assert.True(success);
            Assert.That(decryptedText, Is.EqualTo(original));
            Assert.That(password, Is.EqualTo("AHOJ"));
        }

        // TODO: different place
        [Test]
        public void CreateAllIndicesCombinations_PasswordLength3AndCombinationCount2_ShouldReturnCorrectly()
        {
            var list = VigenereCipherKasiskiAttack.CreateAllIndicesCombinations(3, 2);

            List<List<int>> expected = new()
            {
                new() {0, 0, 0},
                new() {1, 0, 0},
                new() {0, 1, 0},
                new() {1, 1, 0},
                new() {0, 0, 1},
                new() {1, 0, 1},
                new() {0, 1, 1},
                new() {1, 1, 1},
            };

            Assert.That(list, Is.EquivalentTo(expected));
        }

        [Test]
        public void CreateAllIndicesCombinations_PasswordLength4AndCombinationCount3_ShouldReturnCorrectly()
        {
            var list = VigenereCipherKasiskiAttack.CreateAllIndicesCombinations(4, 3);

            List<List<int>> expected = new()
            {
                new() {0, 0, 0, 0},
                new() {1, 0, 0, 0},
                new() {2, 0, 0, 0},

                new() {0, 1, 0, 0},
                new() {1, 1, 0, 0},
                new() {2, 1, 0, 0},

                new() {0, 2, 0, 0},
                new() {1, 2, 0, 0},
                new() {2, 2, 0, 0},


                new() {0, 0, 1, 0},
                new() {1, 0, 1, 0},
                new() {2, 0, 1, 0},

                new() {0, 1, 1, 0},
                new() {1, 1, 1, 0},
                new() {2, 1, 1, 0},

                new() {0, 2, 1, 0},
                new() {1, 2, 1, 0},
                new() {2, 2, 1, 0},

                new() {0, 0, 2, 0},
                new() {1, 0, 2, 0},
                new() {2, 0, 2, 0},

                new() {0, 1, 2, 0},
                new() {1, 1, 2, 0},
                new() {2, 1, 2, 0},

                new() {0, 2, 2, 0},
                new() {1, 2, 2, 0},
                new() {2, 2, 2, 0},


                new() {0, 0, 0, 1},
            };

            Assert.That(list.Take(28), Is.EquivalentTo(expected));

            Assert.That(list.Count(), Is.EqualTo(Math.Pow(3, 4)));
        }
    }
}