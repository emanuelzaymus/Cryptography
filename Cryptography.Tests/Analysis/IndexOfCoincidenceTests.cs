using System;
using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Analysis.TextNormalization;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Analysis
{
    [TestFixture]
    public class IndexOfCoincidenceTests
    {
        [Test]
        public void GetIndexOfCoincidence_Text1_ShouldReturn0Point0422()
        {
            var text1 = Texts.GetText1(Casing.UpperCase);
            var indexOfCoincidence = IndexOfCoincidence.GetIndexOfCoincidence(text1, Alphabets.ALPHABET);

            Assert.That(Math.Abs(indexOfCoincidence - 0.0422), Is.LessThan(0.00009));
        }

        [Test]
        public void GetIndexOfCoincidence_Text2_ShouldReturn0Point0589()
        {
            var text2 = Texts.GetText2(Casing.UpperCase);
            var indexOfCoincidence = IndexOfCoincidence.GetIndexOfCoincidence(text2, Alphabets.ALPHABET);

            Assert.That(Math.Abs(indexOfCoincidence - 0.0589), Is.LessThan(0.00009));
        }
    }
}