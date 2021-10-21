using System.Collections.Generic;
using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Analysis.TextNormalization;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Analysis
{
    [TestFixture]
    public class KasiskiExaminationTests
    {
        [Test]
        public void GetTrinityDistances_Text1_ShouldReturnCorrectDistances()
        {
            var normalizer = new TextNormalizer(onlyValidCharacters: Alphabets.ALPHABET);
            var distances = KasiskiExamination.GetTrinityDistances(Texts.GetText1(normalizer));

            var expectedDistances = new List<(string, int)>
            {
                ("JNN", 13), ("NNJ", 92), ("NJB", 92), ("IJN", 190), ("RSQ", 12), ("SQO", 12), ("DRJ", 128),
                ("RJH", 128), ("JHN", 128), ("HNP", 128), ("NPY", 128), ("PYO", 128), ("YOE", 128), ("OED", 128),
                ("EDL", 128), ("DLD", 128), ("LDX", 128), ("DXD", 128), ("XDV", 128), ("DVP", 128), ("VPW", 128),
                ("PWO", 128), ("WOZ", 128), ("OZH", 128), ("ZHR", 128), ("RVF", 16), ("BNH", 160), ("NHV", 137),
                ("NHV", 160), ("YJV", 8), ("CSI", 8), ("SIJ", 8), ("SAA", 24), ("NHV", 23), ("HVX", 23)
            };

            Assert.That(distances, Is.EquivalentTo(expectedDistances));
        }

        [Test]
        public void GetPasswordLengthEstimations_Text1_ShouldReturnCorrectEstimations()
        {
            var normalizer = new TextNormalizer(Casing.UpperCase, Alphabets.ALPHABET);
            var estimations = KasiskiExamination.GetPasswordLengthEstimations(Texts.GetText1(normalizer), 3, 12);

            var expectedEstimations = new List<(int, int)>
            {
                (4, 30), (8, 26), (3, 3), (5, 3), (6, 3), (10, 3), (12, 3), (7, 0), (9, 0), (11, 0)
            };

            Assert.That(estimations, Is.EquivalentTo(expectedEstimations));
        }
    }
}