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
        public void GetProbabilitiesOfLetters_EnglishTelegraphFile_ShouldReturnCorrectValues()
        {
            var enTelegraph = Texts.GetEnTelegraph(Casing.UpperCase);
            var actual = LanguageFrequencyAnalysis.GetProbabilitiesOfLetters(enTelegraph, Alphabets.ALPHABET_);

            var expected = ProbabilitiesOfLetters.EnTelegraphWithSpace;

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (int i = 0; i < expected.Count; i++)
            {
                var actualProbability = actual[i].Probability;
                var expectedProbability = expected[i].Probability;

                Assert.That(Math.Abs(actualProbability - expectedProbability), Is.LessThan(0.0001));
            }
        }

        [Test]
        public void GetProbabilitiesOfLetters_SlovakTelegraphFile_ShouldReturnCorrectValues()
        {
            var skTelegraph = Texts.GetSkTelegraph(new SlovakTextNormalizer(Casing.UpperCase));
            var actual = LanguageFrequencyAnalysis.GetProbabilitiesOfLetters(skTelegraph, Alphabets.ALPHABET_);

            var expected = ProbabilitiesOfLetters.SkTelegraphWithSpace;

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (int i = 0; i < expected.Count; i++)
            {
                var actualProbability = actual[i].Probability;
                var expectedProbability = expected[i].Probability;

                Assert.That(Math.Abs(actualProbability - expectedProbability), Is.LessThan(0.0001));
            }
        }
    }
}