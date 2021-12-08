using System.Numerics;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Utilities
{
    [TestFixture]
    public class BigIntegerExtensionsTests
    {
        [Test]
        public void Sqrt_NumberFromTask1_ShouldReturnCorrectResult()
        {
            var sqrt = new BigInteger(13169004533).Sqrt();

            Assert.That(sqrt, Is.EqualTo(new BigInteger(114756)));
        }

        [Test]
        public void Sqrt_NumberFromTask2_ShouldReturnCorrectResult()
        {
            var sqrt = new BigInteger(1690428486610429).Sqrt();

            Assert.That(sqrt, Is.EqualTo(new BigInteger(41114820)));
        }

        [Test]
        public void Sqrt_NumberFromTask3_ShouldReturnCorrectResult()
        {
            var sqrt = BigInteger.Parse("56341958081545199783").Sqrt();

            Assert.That(sqrt, Is.EqualTo(new BigInteger(7506128035)));
        }
    }
}