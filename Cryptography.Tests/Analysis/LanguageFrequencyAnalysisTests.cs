using System;
using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Analysis.TextNormalization;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Analysis
{
    [TestFixture]
    public class LanguageFrequencyAnalysisTests
    {
        [Test]
        public void GetLettersProbabilities_EnglishTelegraphFile_ShouldReturnCorrectValues()
        {
            var enTelegraph = Texts.GetEnTelegraph(Casing.UpperCase);
            var actual = LanguageFrequencyAnalysis.GetLettersProbabilities(enTelegraph, Alphabets.ALPHABET_);

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
            var skTelegraph = Texts.GetSkTelegraph(new SlovakTextNormalizer(Casing.UpperCase));
            var actual = LanguageFrequencyAnalysis.GetLettersProbabilities(skTelegraph, Alphabets.ALPHABET_);

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
            var text1 = Texts.GetText1(Casing.UpperCase);
            var indexOfCoincidence = LanguageFrequencyAnalysis.GetIndexOfCoincidence(text1, Alphabets.ALPHABET);

            Assert.That(Math.Abs(indexOfCoincidence - 0.0422), Is.LessThan(0.00009));
        }

        [Test]
        public void GetIndexOfCoincidence_Text2_ShouldReturn0Point0589()
        {
            var text2 = Texts.GetText2(Casing.UpperCase);
            var indexOfCoincidence = LanguageFrequencyAnalysis.GetIndexOfCoincidence(text2, Alphabets.ALPHABET);

            Assert.That(Math.Abs(indexOfCoincidence - 0.0589), Is.LessThan(0.00009));
        }
    }
}