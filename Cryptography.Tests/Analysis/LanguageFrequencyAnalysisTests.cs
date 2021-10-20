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
        private const string EnTelegraph = "Resources/TextsToAnalyse/en_teleg.txt";
        private const string SkTelegraph = "Resources/TextsToAnalyse/sk_teleg.txt";

        [Test]
        public void GetLettersProbabilities_EnglishTelegraphFile_ShouldReturnCorrectValues()
        {
            LanguageFrequencyAnalysis analysis = new(EnTelegraph, Alphabets.ALPHABET_,
                new TextNormalizer(Casing.UpperCase));

            var actual = analysis.GetLettersProbabilities();

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
            LanguageFrequencyAnalysis analysis = new(SkTelegraph, Alphabets.ALPHABET_,
                new SlovakTextNormalizer(Casing.UpperCase));

            var actual = analysis.GetLettersProbabilities();

            var expected = LettersProbabilities.SlovakLanguage;

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            foreach (var key in expected.Keys)
            {
                var actualValue = actual[key];
                var expectedValue = expected[key];

                Assert.That(Math.Abs(actualValue - expectedValue), Is.LessThan(0.0001));
            }
        }
    }
}