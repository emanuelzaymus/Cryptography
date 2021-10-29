using System;
using System.Linq;
using Cryptography.RandomNumberGenerators;
using NUnit.Framework;

namespace Cryptography.Tests.RandomNumberGenerators
{
    [TestFixture]
    public class LinearCongruentialRngTests
    {
        [Test]
        public void Sample_CorrectParameters_ShouldReturnExpectedSamples()
        {
            var rng = new LinearCongruentialRng(8121, 28411, 134456, 10000);

            var expectedSamples = new[]
            {
                0.200712501, 0.197521866, 0.286376212, 0.872523353, 0.973456, 0.647483192, 0.42230172, 0.723567561,
                0.303467305, 0.669289582, 0.51199649, 0.134795026, 0.881708514, 0.566148034, 0.899483846, 0.919616826,
                0.41954989, 0.375959422, 0.377766704, 0.054709347, 0.505912715, 0.728461355, 0.045970429, 0.537157137,
                0.46441215, 0.702371036, 0.16648569, 0.241595764, 0.210500089, 0.682528113, 0.022111323, 0.777354674
            };

            var actualSamples = Enumerable.Range(0, expectedSamples.Length).Select(_ => rng.Sample()).ToList();

            Assert.That(actualSamples.Count, Is.EqualTo(expectedSamples.Length));

            for (int i = 0; i < expectedSamples.Length; i++)
            {
                Assert.That(expectedSamples[i] - actualSamples[i], Is.LessThan(0.000_000_001));
            }
        }
    }
}