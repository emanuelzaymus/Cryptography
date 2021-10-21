using System;
using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Analysis.TextNormalization;
using NUnit.Framework;

namespace Cryptography.Tests.Analysis
{
    [TestFixture]
    public class LanguageFrequencyAnalysisTests
    {
        [Test]
        public void GetLettersProbabilities_EnglishTelegraphFile_ShouldReturnCorrectValues()
        {
            var actual = LanguageFrequencyAnalysis
                .GetLettersProbabilities(Files.EnTelegraph, Alphabets.ALPHABET_, new TextNormalizer(Casing.UpperCase));

            var expected = LettersProbabilities.EnglishLanguage;

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            foreach (var key in expected.Keys)
            {
                var actualValue = actual[key];
                var expectedValue = expected[key];

                Assert.That(Math.Abs(actualValue - expectedValue), Is.LessThan(0.0001));
            }
        }

        [Test]
        public void GetLettersProbabilities_SlovakTelegraphFile_ShouldReturnCorrectValues()
        {
            var actual = LanguageFrequencyAnalysis.GetLettersProbabilities(Files.SkTelegraph, Alphabets.ALPHABET_,
                new SlovakTextNormalizer(Casing.UpperCase));

            var expected = LettersProbabilities.SlovakLanguage;

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            foreach (var key in expected.Keys)
            {
                var actualValue = actual[key];
                var expectedValue = expected[key];

                Assert.That(Math.Abs(actualValue - expectedValue), Is.LessThan(0.0001));
            }
        }

        [Test]
        public void GetIndexOfCoincidence_Text1_ShouldReturn0Point0422()
        {
            var indexOfCoincidence = LanguageFrequencyAnalysis.GetIndexOfCoincidence(Files.Text1, Alphabets.ALPHABET,
                new TextNormalizer(Casing.UpperCase));

            Assert.That(Math.Abs(indexOfCoincidence - 0.0422), Is.LessThan(0.00009));
        }

        [Test]
        public void GetIndexOfCoincidence_Text2_ShouldReturn0Point0589()
        {
            var indexOfCoincidence = LanguageFrequencyAnalysis.GetIndexOfCoincidence(Files.Text2, Alphabets.ALPHABET,
                new TextNormalizer(Casing.UpperCase));

            Assert.That(Math.Abs(indexOfCoincidence - 0.0589), Is.LessThan(0.00009));
        }
    }
}